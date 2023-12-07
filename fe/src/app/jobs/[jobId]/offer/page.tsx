"use client"
import { Breadcrumbs, Button, Input, Textarea, Typography } from "@/lib/materialTailwindExports"
import { OfferType } from "@/types/OfferType"
import { getToken } from "@/utils/getToken";
import { BASE_API_URL, SERVICE_JOBS_ROUTE } from "@/utils/urls";
import { useParams, useRouter } from 'next/navigation';
import { useState } from "react"
import toast from "react-hot-toast";
import Datepicker, { DateValueType } from "react-tailwindcss-datepicker";
import { date, number, object, string } from "yup"

const offerFormSchema = object({
  price: number().min(1).required(),
  startDate: date().min(new Date(), 'Date must be in the future').required(),
  description: string().max(1000).required(),
});

const defaultValues = {
  price: 0,
  description: "",
  startDate: null
};

const OfferForm = () => {
  const [offerData, setOfferData] = useState<OfferType>(defaultValues);
  const params = useParams();
  const router = useRouter();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setOfferData((formData) => ({
      ...formData,
      [name]: value,
    }));
  };

  const handleStartDateChange = (date: DateValueType) => {
    setOfferData((formData) => ({
      ...formData,
      startDate: date!.startDate,
    }));
  }

  const validateData = async (): Promise<boolean> => {
    try {
      await offerFormSchema.validate(offerData, { abortEarly: false });
      return true;
    } catch {
      return false;
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const isValid = await validateData();

    if (isValid) {
      const body = {
        ...offerData,
        startDate: new Date(offerData.startDate!)
      }
      const token = await getToken();
      const response = await fetch(`${BASE_API_URL}${SERVICE_JOBS_ROUTE}/${params.jobId}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(body),
      });
      if (!response.ok) {
        const json = await response.json();
        toast.error(json.message || response.statusText || "Unknown error occoured.");
        return;
      }
      setOfferData(defaultValues);
      router.push("/jobs");
      toast.success("Offer successfully sent");
    } else {
      toast.error("Please fulfill all form fields");
    }
  }

  return (
    <div className="flex flex-col gap-3 max-w-3xl mx-auto my-5 py-10 px-5">
      <Breadcrumbs>
        <a href="/home" className="opacity-60">
          <span>Home</span>
        </a>
        <a href="/jobs" className="opacity-60">
          <span>Jobs</span>
        </a>
        <a href={`/jobs/${params.jobId}`} className="opacity-60">
          <span>Advert</span>
        </a>
        <a href="#">
          <span>Offer</span>
        </a>
      </Breadcrumbs>
      <Typography variant="h3">Offer form</Typography>
      <form onSubmit={handleSubmit} className="flex flex-col gap-5">
        <Datepicker
          placeholder={"Select preliminari job start date"}
          minDate={new Date()}
          startFrom={new Date()}
          asSingle={true}
          value={{ startDate: offerData.startDate!, endDate: offerData.startDate! }}
          onChange={handleStartDateChange}
          containerClassName="date-color-override"
        />
        <Input crossOrigin="" type="number" value={offerData.price} name="price" onChange={handleChange} color="blue-gray" label="Price" />
        <Textarea value={offerData.description} name="description" onChange={handleChange} resize rows={4} color="blue-gray" label="Description" />
        <Button type="submit" variant="gradient">Submit</Button>
      </form>
    </div>
  )
}

export default OfferForm;