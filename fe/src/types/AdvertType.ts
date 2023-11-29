import { DateType } from "react-tailwindcss-datepicker";

export type AdvertType = {
  id: string;
  title: string;
  endDate: DateType | null;
  isSoundBad?: boolean;
  isScentBad?: boolean;
  isPanelInvalid?: boolean;
  photos: Array<string>;
  isLeakedLiquids?: boolean;
  isUnstableCar?: boolean;
  description: string;
  brand: string;
  model: string;
  manufactureYear: number;
  problemType: string;
  createdDate: Date;
};