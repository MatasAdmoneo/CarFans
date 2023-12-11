"use client";

import { ServiceInfoForm, WorkingDay } from "@/types/ServiceInfoForm";
import { Checkbox, Typography } from "@/lib/materialTailwindExports";
import { ChangeEvent, Dispatch, SetStateAction, useState } from "react";
import Modal from "./Modal";

const DAYS = [
  {
    label: "S",
    value: 0,
  },
  {
    label: "M",
    value: 1,
  },
  {
    label: "T",
    value: 2,
  },
  {
    label: "W",
    value: 3,
  },
  {
    label: "T",
    value: 4,
  },
  {
    label: "F",
    value: 5,
  },
  {
    label: "S",
    value: 6,
  },
];

type DayPickerProps = {
  formData: ServiceInfoForm;
  setFormData: Dispatch<SetStateAction<ServiceInfoForm>>;
};

const DayPicker = ({ formData, setFormData }: DayPickerProps) => {
  const [selectedDay, setSelectedDay] = useState<number>();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const handleModalChange = () => setIsModalOpen((prev) => !prev);
  const handleModalClose = () => {
    setIsModalOpen(false);
    setFormData((prevFormData) => ({
      ...prevFormData,
      weeklyWorkingHours: prevFormData.weeklyWorkingHours.filter(
        (day) => day.dayOfWeek !== selectedDay
      ),
    }));
  };
  const handleCheckboxChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { checked, value } = e.target;
    setSelectedDay(Number(value));

    if (checked) {
      const newWorkingHour = {
        dayOfWeek: Number(value),
        startTime: "",
        endTime: "",
        lunchBreakStartTime: "",
        lunchBreakEndTime: "",
      };

      setFormData((prevFormData) => ({
        ...prevFormData,
        weeklyWorkingHours: [
          ...prevFormData.weeklyWorkingHours,
          newWorkingHour,
        ],
      }));
      setIsModalOpen(true);
    } else {
      // Remove the object for the unchecked day from the state
      setFormData((prevFormData) => ({
        ...prevFormData,
        weeklyWorkingHours: prevFormData.weeklyWorkingHours.filter(
          (day) => day.dayOfWeek !== Number(value)
        ),
      }));
    }
  };
  console.log(formData);
  return (
    <div className="border-blue-gray-200 border-[1px] rounded-md p-1 pb-2">
      <Typography className="text-blue-gray-500 text-[0.875rem] ml-2 mt-1">
        Working days<span className="text-red-500"> *</span>
      </Typography>
      <div className="flex justify-center">
        {DAYS.map((day) => (
          <div key={day.value} className="w-fit">
            <Checkbox
              onChange={(e) => handleCheckboxChange(e)}
              ripple={false}
              className="h-8 w-8 rounded-full border-gray-900/20 bg-gray-900/10 transition-all hover:scale-105 hover:before:opacity-0"
              crossOrigin=""
              value={day.value}
              checked={formData.weeklyWorkingHours.some(
                (d) => d.dayOfWeek === day.value
              )}
            />
            <span className="block text-center">{day.label}</span>
          </div>
        ))}
        <Modal
          isOpen={isModalOpen}
          handleSave={handleModalChange}
          handleClose={handleModalClose}
          workingDay={selectedDay}
          formData={formData}
          setFormData={setFormData}
        />
      </div>
    </div>
  );
};

export default DayPicker;
