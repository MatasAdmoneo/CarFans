"use client";

import { ServiceInfoForm } from "@/types/ServiceInfoForm";
import { Checkbox, Typography } from "@/lib/materialTailwindExports";
import { ChangeEvent, Dispatch, SetStateAction, useState } from "react";
import { MdEdit } from "@/lib/reactIconsExports";
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

  const handleModalOpen = (value: number) => {
    setSelectedDay(value);
    setIsModalOpen(true);
  };

  const handleCheckboxChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { checked, value } = e.target;
    if (checked) {
      handleModalOpen(Number(value));
    } else {
      // Remove the working day if unchecked
      setFormData((prevFormData) => ({
        ...prevFormData,
        weeklyWorkingHours: prevFormData.weeklyWorkingHours.filter(
          (day) => day.dayOfWeek !== Number(value)
        ),
      }));
    }
  };

  return (
    <div className="border-blue-gray-200 border-[1px] rounded-md p-1 pb-2">
      <Typography className="text-blue-gray-500 text-[0.875rem] ml-2 mt-1">
        Working days<span className="text-red-500"> *</span>
      </Typography>
      <div className="flex justify-center items-baseline ">
        {DAYS.map((day) => {
          const isChecked = formData.weeklyWorkingHours.some(
            (d) => d.dayOfWeek === day.value
          );
          return (
            <div key={day.value} className="w-fit mt-4">
              {isChecked && (
                <div
                  className="rounded-full bg-blue-gray-50 w-fit p-1"
                  onClick={() => handleModalOpen(day.value)}
                >
                  <MdEdit />
                </div>
              )}
              <Checkbox
                onChange={(e) => handleCheckboxChange(e)}
                ripple={false}
                className="h-8 w-8 rounded-full border-gray-900/20 bg-gray-900/10 transition-all hover:scale-105 hover:before:opacity-0"
                crossOrigin=""
                value={day.value}
                checked={isChecked}
              />
              <span className="block text-center">{day.label}</span>
            </div>
          );
        })}
        <Modal
          isOpen={isModalOpen}
          selectedDay={selectedDay}
          formData={formData}
          setFormData={setFormData}
          setIsModalOpen={setIsModalOpen}
        />
      </div>
    </div>
  );
};

export default DayPicker;
