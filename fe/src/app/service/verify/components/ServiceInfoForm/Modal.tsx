import {
  Dialog,
  DialogHeader,
  DialogBody,
  DialogFooter,
  Button,
  Input,
} from "@/lib/materialTailwindExports";
import { ServiceInfoForm } from "@/types/ServiceInfoForm";
import { Dispatch, SetStateAction, useEffect, useMemo, useState } from "react";
import { ValidationError } from "yup";
import { serviceWorkingDayFormSchema } from "./utils";

type ModalProps = {
  isOpen: boolean;
  selectedDay: number | undefined;
  formData: ServiceInfoForm;
  setFormData: Dispatch<SetStateAction<ServiceInfoForm>>;
  setIsModalOpen: Dispatch<SetStateAction<boolean>>;
};

const Modal = ({
  isOpen,
  selectedDay,
  formData,
  setFormData,
  setIsModalOpen,
}: ModalProps) => {
  // define selected working day object that will be used to set new working day form data
  const workingDay = useMemo(() => {
    const emptyWorkingDayFormValues = {
      dayOfWeek: selectedDay!,
      startTime: "",
      endTime: "",
      lunchBreakStartTime: "",
      lunchBreakEndTime: "",
    };

    return (
      formData.weeklyWorkingHours.find(
        (day) => day.dayOfWeek === selectedDay
      ) || emptyWorkingDayFormValues
    );
  }, [selectedDay, formData]);

  // updates workingDayForm state if selected day changes or modal is reopened
  useEffect(() => {
    setWorkingDayForm(workingDay);
  }, [workingDay, isOpen]);

  // separate form just for selected working day
  const [workingDayForm, setWorkingDayForm] = useState(workingDay);

  // show form errors directly in the modal because toast.error is blurred behind the modal
  const [workingDayFormErrors, setWorkingDayFormErrors] = useState<string[]>(
    []
  );
  const handleModalClose = () => {
    setIsModalOpen(false);
    setWorkingDayFormErrors([]);
  };
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.currentTarget;
    setWorkingDayForm((prevWorkingDay) => ({
      ...prevWorkingDay,
      [name]: value,
    }));
  };

  const handleSave = async () => {
    const isValid = await validateFormData();
    if (isValid) {
      setWorkingDayFormErrors([]);
      setFormData((prevFormData) => {
        const existingDayIndex = prevFormData.weeklyWorkingHours.findIndex(
          (day) => day.dayOfWeek === workingDayForm.dayOfWeek
        );
        if (existingDayIndex !== -1) {
          return {
            ...prevFormData,
            weeklyWorkingHours: prevFormData.weeklyWorkingHours.map((day) =>
              day.dayOfWeek === workingDayForm.dayOfWeek ? workingDayForm : day
            ),
          };
        } else {
          return {
            ...prevFormData,
            weeklyWorkingHours: [
              ...prevFormData.weeklyWorkingHours,
              workingDayForm,
            ],
          };
        }
      });
      setIsModalOpen(false);
    } else {
      return;
    }
  };

  const validateFormData = async (): Promise<boolean> => {
    try {
      await serviceWorkingDayFormSchema.validate(workingDayForm, {
        abortEarly: false,
      });
      return true;
    } catch (error) {
      if (error instanceof ValidationError) {
        const validationErrors = error as ValidationError;
        setWorkingDayFormErrors(validationErrors.errors);
      } else {
        return false;
      }
      return false;
    }
  };

  return (
    <Dialog open={isOpen} size="xs" handler={handleModalClose}>
      <DialogHeader>Select working hours</DialogHeader>
      <DialogBody className="[&>div]:my-5">
        <Input
          size="lg"
          required
          className=""
          crossOrigin=""
          type="time"
          label="Opening hours"
          name="startTime"
          value={workingDayForm.startTime}
          onChange={handleChange}
        />
        <Input
          size="lg"
          required
          className=""
          crossOrigin=""
          type="time"
          label="Closing hours"
          name="endTime"
          value={workingDayForm.endTime}
          onChange={handleChange}
        />
        <Input
          size="lg"
          required
          className=""
          crossOrigin=""
          type="time"
          label="Lunch start hours"
          name="lunchBreakStartTime"
          value={workingDayForm.lunchBreakStartTime}
          onChange={handleChange}
        />
        <Input
          size="lg"
          required
          className=""
          crossOrigin=""
          type="time"
          label="Lunch end hours"
          name="lunchBreakEndTime"
          value={workingDayForm.lunchBreakEndTime}
          onChange={handleChange}
        />
        {workingDayFormErrors.length > 0 &&
          workingDayFormErrors.map((error) => (
            <p key={error} className="text-sm text-red-500">
              {error}{" "}
            </p>
          ))}
      </DialogBody>
      <DialogFooter>
        <Button
          variant="text"
          color="red"
          onClick={() => handleModalClose()}
          className="mr-1"
        >
          <span>Cancel</span>
        </Button>
        <Button variant="gradient" onClick={handleSave}>
          <span>Confirm</span>
        </Button>
      </DialogFooter>
    </Dialog>
  );
};

export default Modal;
