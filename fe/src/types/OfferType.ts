import { DateType } from "react-tailwindcss-datepicker";

export type OfferType = {
  price: number;
  startDate: DateType | null;
  description: string;
};