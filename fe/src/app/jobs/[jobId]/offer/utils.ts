import { date, number, object, string } from "yup"

export const offerFormSchema = object({
  price: number().min(1).required(),
  startDate: date().min(new Date(), 'Date must be in the future').required(),
  description: string().max(1000).required(),
});

export const defaultValues = {
  price: 0,
  description: "",
  startDate: null
};