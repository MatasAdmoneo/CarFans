import { object, string, array, number } from "yup";

export const serviceInfoFormSchema = object({
  serviceName: string().required(),
  adress: string().required(),
  city: string().required(),
  weeklyWorkingHours: array()
    .of(
      object().shape({
        dayOfWeek: number().min(0).max(6).required(),
        startTime: string().required(),
        endTime: string().required(),
        lunchBreakStartTime: string().required(),
        lunchBreakEndTime: string().required(),
      })
    )
    .min(1)
    .required(),
  contactPhone: string().required(),
  description: string(),
});

export const emptyInfoFormValues = {
  serviceName: "",
  adress: "",
  city: "",
  weeklyWorkingHours: [],
  contactPhone: "+370",
  description: "",
};
