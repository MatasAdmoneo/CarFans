import {
  Dialog,
  DialogHeader,
  DialogBody,
  DialogFooter,
  Button,
  Input,
} from "@/lib/materialTailwindExports";
import { ServiceInfoForm, WorkingDay } from "@/types/ServiceInfoForm";
import { Dispatch, SetStateAction } from "react";

type ModalProps = {
  isOpen: boolean;
  handleSave: () => void;
  handleClose: () => void;
  workingDay: number | undefined;
  formData: ServiceInfoForm;
  setFormData: Dispatch<SetStateAction<ServiceInfoForm>>;
};

const Modal = ({
  isOpen,
  handleSave,
  handleClose,
  workingDay,
  formData,
  setFormData,
}: ModalProps) => {
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.currentTarget;
    setFormData((prevFormData) => ({
      ...prevFormData,
      weeklyWorkingHours: prevFormData.weeklyWorkingHours.map((day) =>
        day.dayOfWeek === workingDay ? { ...day, [name]: value } : day
      ),
    }));
  };
  return (
    <Dialog open={isOpen} size="xs" handler={handleClose}>
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
          value={
            formData.weeklyWorkingHours[workingDay!]
              ? formData.weeklyWorkingHours[workingDay!].startTime
              : ""
          }
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
          value={
            formData.weeklyWorkingHours[workingDay!]
              ? formData.weeklyWorkingHours[workingDay!].endTime
              : ""
          }
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
          value={
            formData.weeklyWorkingHours[workingDay!]
              ? formData.weeklyWorkingHours[workingDay!].lunchBreakStartTime
              : ""
          }
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
          value={
            formData.weeklyWorkingHours[workingDay!]
              ? formData.weeklyWorkingHours[workingDay!].lunchBreakEndTime
              : ""
          }
          onChange={handleChange}
        />
      </DialogBody>
      <DialogFooter>
        <Button
          variant="text"
          color="red"
          onClick={handleClose}
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
