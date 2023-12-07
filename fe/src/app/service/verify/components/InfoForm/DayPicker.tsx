"use client";

import { ServiceInfoForm, WorkingDay } from "@/types/ServiceInfoForm";
import { Checkbox, Typography } from "@/lib/materialTailwindExports";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import type { CheckboxProps } from "@material-tailwind/react";
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
  const [days, setDays] = useState(
    formData.weeklyWorkingHours.map((day) => day.dayOfWeek)
  );
  const [selectedDay, setSelectedDay] = useState<number>();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const handleModalOpen = () => setIsModalOpen((prev) => !prev);
  const handleCheckboxChange = (value: boolean, dayValue: number) => {
    setSelectedDay(dayValue);

    if (value) {
      const newWorkingHour = {
        dayOfWeek: dayValue,
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
          (day) => day.dayOfWeek !== dayValue
        ),
      }));
    }
  };

  return (
    <div className="border-blue-gray-200 border-[1px] rounded-md p-1 pb-2">
      <Typography className="text-blue-gray-500 text-[0.875rem] ml-2 mt-1">
        Working days<span className="text-red-500"> *</span>
      </Typography>
      <div className="flex justify-center">
        {DAYS.map((day, index) => (
          <div key={day.value} className="w-fit">
            <Checkbox
              onChange={(e) =>
                handleCheckboxChange(e.target.checked, day.value)
              }
              ripple={false}
              className="h-8 w-8 rounded-full border-gray-900/20 bg-gray-900/10 transition-all hover:scale-105 hover:before:opacity-0"
              crossOrigin=""
              value={day.value}
            />
            <span className="block text-center">{day.label}</span>
          </div>
        ))}
        <Modal
          isOpen={isModalOpen}
          handler={handleModalOpen}
          workingDay={selectedDay}
          formData={formData}
          setFormData={setFormData}
        />
      </div>
    </div>
  );
};

export default DayPicker;
