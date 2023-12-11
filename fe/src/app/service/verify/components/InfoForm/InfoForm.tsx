"use client";

import { ServiceInfoForm } from "@/types/ServiceInfoForm";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { emptyInfoFormValues, serviceInfoFormSchema } from "./utils";
import { getToken } from "@/utils/getToken";
import { BASE_API_URL, SERVICE_ADDITIONAL_INFO_ROUTE } from "@/utils/urls";
import toast from "react-hot-toast";
import { Button, Input, Textarea } from "@/lib/materialTailwindExports";
import DayPicker from "./DayPicker";
import { ValidationError } from "yup";

type InfoFormProps = {
  isForwardButtonDisabled: boolean;
  setIsForwardButtonDisabled: Dispatch<SetStateAction<boolean>>;
};

const InfoForm = ({
  isForwardButtonDisabled,
  setIsForwardButtonDisabled,
}: InfoFormProps) => {
  const [formData, setFormData] =
    useState<ServiceInfoForm>(emptyInfoFormValues);

  useEffect(() => {
    getServiceInfo();
    setIsForwardButtonDisabled(false);
  }, []);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;

    setFormData((formData) => ({
      ...formData,
      [name]: value,
    }));
  };

  const validateFormData = async (): Promise<boolean> => {
    try {
      await serviceInfoFormSchema.validate(formData, {
        abortEarly: false,
      });
      return true;
    } catch (error) {
      if (error instanceof ValidationError) {
        const validationErrors = error as ValidationError;
        validationErrors.errors.map((error) => toast.error(error));
      } else {
        return false;
      }
      return false;
    }
  };

  const getServiceInfo = async () => {
    const token = await getToken();
    const response = await fetch(
      `${BASE_API_URL}${SERVICE_ADDITIONAL_INFO_ROUTE}`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );
    const serviceInfo = await response.json();
    if (serviceInfo && Object.keys(serviceInfo).length !== 0) {
      setFormData(serviceInfo);
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const isValid = await validateFormData();

    if (isValid) {
      const data = formData;
      const body = {
        ...data,
      };
      const token = await getToken();
      const response = await fetch(
        `${BASE_API_URL}${SERVICE_ADDITIONAL_INFO_ROUTE}`,
        {
          method: `${isForwardButtonDisabled ? "POST" : "PATCH"}`,
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify(body),
        }
      );

      if (!response.ok) {
        const json = await response.json();
        toast.error(
          json.message || response.statusText || "Unknown error occoured."
        );
        return;
      }
      toast.success("Your info has been successfully submitted");
      setIsForwardButtonDisabled(false);
    } else {
      return;
    }
  };

  return (
    <div>
      <h2 className="text-3xl font-bold py-10 px-4 text-center">
        Please fill out the form
      </h2>
      <form onSubmit={handleSubmit} className="flex flex-col gap-5" noValidate>
        <Input
          crossOrigin=""
          required
          value={formData.serviceName}
          name="serviceName"
          onChange={handleChange}
          color="blue-gray"
          label="Service Name"
        />
        <Input
          crossOrigin=""
          required
          value={formData.address}
          name="address"
          onChange={handleChange}
          color="blue-gray"
          label="Address"
        />
        <Input
          crossOrigin=""
          required
          value={formData.city}
          name="city"
          onChange={handleChange}
          color="blue-gray"
          label="City"
        />
        <Input
          crossOrigin=""
          value={formData.contactPhone}
          name="contactPhone"
          onChange={handleChange}
          color="blue-gray"
          label="Contact Phone"
        />

        <DayPicker formData={formData} setFormData={setFormData} />

        <Textarea
          value={formData.description}
          name="description"
          onChange={handleChange}
          resize
          rows={4}
          color="blue-gray"
          label="Additional information"
        />
        <Button
          type="submit"
          color="blue"
          size="lg"
          className="w-fit self-center rounded-md"
        >
          Submit
        </Button>
      </form>
    </div>
  );
};

export default InfoForm;
