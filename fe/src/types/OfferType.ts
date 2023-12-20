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
  status: number;
  serviceName: string;
  address: string;
  city: string;
  phone: string;
  serviceDescription: string;
};

export type JobTypeStatusAsString = {
  id: number;
  price: number;
  startDate: Date;
  description: string;
  status: string;
  serviceName: string;
  address: string;
  city: string;
  phone: string;
  serviceDescription: string;
};
