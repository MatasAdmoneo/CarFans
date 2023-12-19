import { object, string, array, number } from "yup";

const phoneNumberRegExp = /^\+370[0-9]{8}$/;

export const serviceInfoFormSchema = object({
  serviceName: string().required("Service name is required field"),
  address: string().required("Address is required field"),
  city: string().required("City is required field"),
  weeklyWorkingHours: array()
    .of(
      object().shape({
        dayOfWeek: number().min(0).max(6).required(),
        startTime: string().required("Opening hours were not selected"),
        endTime: string().required("Closing hours were not selected"),
        lunchBreakStartTime: string().required(
          "Lunch start hours were not selected"
        ),
        lunchBreakEndTime: string().required(
          "Lunch end hours were not selected"
        ),
      })
    )
    .min(1)
    .required("Working days field must have at least 1 day checked"),
  contactPhone: string().matches(
    phoneNumberRegExp,
    "Phone number is not valid"
  ),
  description: string(),
});

export const serviceWorkingDayFormSchema = object({
  dayOfWeek: number().min(0).max(6).required(),
  startTime: string().required("Opening hours were not selected"),
  endTime: string().required("Closing hours were not selected"),
  lunchBreakStartTime: string().required("Lunch start hours were not selected"),
  lunchBreakEndTime: string().required("Lunch end hours were not selected"),
});

export const emptyInfoFormValues = {
  serviceName: "",
  address: "",
  city: "",
  weeklyWorkingHours: [],
  contactPhone: "+370",
  description: "",
};
