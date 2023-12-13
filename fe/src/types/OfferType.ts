import { DateType } from "react-tailwindcss-datepicker";

export type OfferType = {
  price: number;
  startDate: DateType | null;
  description: string;
};

export type JobType = {
  id: number;
  price: number;
  startDate: Date;
  description: string;
  serviceId: string;
  status: number;
};