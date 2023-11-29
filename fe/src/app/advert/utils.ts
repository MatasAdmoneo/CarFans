import { object, string, boolean, array, date, number } from 'yup';

export const SIMPLE_FORM_TYPE = "simple";
export const QUESTIONS_FORM_TYPE = "questions";

export const simpleFormSchema = object({
  title: string().required(),
  photos: array().of(string()),
  description: string().required(),
  endDate: date().min(new Date(), 'Date must be in the future').required(),
  model: string().required(),
  brand: string().required(),
  manufactureYear: number().required(),
  problemType: string().required()
});

export const simpleFormDefaultValues = {
  title: "",
  photos: [],
  description: "",
  endDate: null,
  model: "",
  brand: "",
  manufactureYear: 0,
  problemType: ""
};

export const questionsFormSchema = object({
  title: string().required(),
  isSoundBad: boolean().required(),
  isScentBad: boolean().required(),
  isPanelInvalid: boolean().required(),
  photos: array().of(string()),
  isLeakedLiquids: boolean().required(),
  isUnstableCar: boolean().required(),
  description: string(),
  endDate: date().min(new Date(), 'Date must be in the future').required(),
  model: string().required(),
  brand: string().required(),
  manufactureYear: number().required(),
  problemType: string().required()
});

export const questionsFormDefaultValues = {
  title: "",
  isSoundBad: undefined,
  isScentBad: undefined,
  isPanelInvalid: undefined,
  photos: [],
  isLeakedLiquids: undefined,
  isUnstableCar: undefined,
  description: "",
  endDate: null,
  model: "",
  brand: "",
  manufactureYear: 0,
  problemType: ""
};