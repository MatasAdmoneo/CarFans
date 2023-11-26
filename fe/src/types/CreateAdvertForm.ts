import { DateType } from "react-tailwindcss-datepicker";

export type QuestionsFormType = {
  title: string;
  endDate: DateType | null;
  isSoundBad?: boolean;
  isScentBad?: boolean;
  isPanelInvalid?: boolean;
  photos: Array<string>;
  isLeakedLiquids?: boolean;
  isUnstableCar?: boolean;
  description: string;
};

export type SimpleFormType = {
  title: string;
  endDate: DateType | null;
  photos: Array<string>;
  description: string;
};