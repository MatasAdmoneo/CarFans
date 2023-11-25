"use client"

import { useState } from "react";
import toast from "react-hot-toast";
import Datepicker, { DateValueType } from "react-tailwindcss-datepicker"; 
import YesOrNoChoice from "@/components/YesOrNoChoice/YesOrNoChoice";
import { Button, Input, Tab, TabPanel, Tabs, TabsBody, TabsHeader, Textarea } from "@/lib/materialTailwindExports";
import { QuestionsFormType, SimpleFormType } from "@/types/CreateAdvertForm";
import { FORM_BOX_SHADOW } from "@/utils/constants";
import { QUESTIONS_FORM_TYPE, SIMPLE_FORM_TYPE, questionsFormDefaultValues, questionsFormSchema, simpleFormDefaultValues, simpleFormSchema } from "./utils";
import { USER_ADVERTS_ROUTE } from "@/utils/urls";
import ImageDropzone from "@/components/ImageDropzone/ImageDropzone";

const AdvertPage = () => {
  const [formType, setFormType] = useState<string>(SIMPLE_FORM_TYPE);
  const [questionsFormData, setQuestionsFormData] = useState<QuestionsFormType>(questionsFormDefaultValues);
  const [simpleFormData, setSimpleFormData] = useState<SimpleFormType>(simpleFormDefaultValues);

  const handleEndDateChange = (date: DateValueType) => {
    if (formType === SIMPLE_FORM_TYPE && date) {
      setSimpleFormData((formData) => ({
        ...formData,
        endDate: date.endDate,
      }));
    } else if (date) {
      setQuestionsFormData((formData) => ({
        ...formData,
        endDate: date.endDate,
      }));
    }
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    if (formType === SIMPLE_FORM_TYPE) {
      setSimpleFormData((formData) => ({
        ...formData,
        [name]: value,
      }));
    } else {
      setQuestionsFormData((formData) => ({
        ...formData,
        [name]: value,
      }));
    }
  };

  const handleYesOrNoValue = (e: React.ChangeEvent<HTMLInputElement>, value: boolean) => {
    const { name } = e.target;
    setQuestionsFormData((formData) => ({
      ...formData,
      [name]: value,
    }));
  }

  const handleImagesUpload = (imagesBase64Data: string[]) => {
    if (formType === SIMPLE_FORM_TYPE) {
      setSimpleFormData((formData) => ({
        ...formData,
        photos: imagesBase64Data
      }));
    } else {
      setQuestionsFormData((formData) => ({
        ...formData,
        photos: imagesBase64Data
      }));
    }
  }

  const validateData = async (): Promise<boolean> => {
    try {
      if (formType === SIMPLE_FORM_TYPE) await simpleFormSchema.validate(simpleFormData, { abortEarly: false });
      else await questionsFormSchema.validate(questionsFormData, { abortEarly: false });
      return true;
    } catch {
      return false;
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const isValid = await validateData();

    if (isValid) {
      const data = formType === SIMPLE_FORM_TYPE ? simpleFormData : questionsFormData;
      const body = {
        ...data,
        isQuestionsFormType: formType,
      }
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_SERVER_URL}${USER_ADVERTS_ROUTE}`, {
        method: "POST",
        mode: "no-cors",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImFKYk9MT1REODRPdHNfNlR4QVBINyJ9.eyJodHRwczovL0NhckZhbnMuY29tL3JvbGVzIjpbIlNlcnZpY2UiLCJVc2VyIl0sImh0dHBzOi8vQ2FyRmFucy5jb20vaWQiOiJhdXRoMHw2NTViYzk0ZTMxMTA4ZmQxYmExNTNiNTEiLCJpc3MiOiJodHRwczovL2Rldi0xNXFpazFiYWI4emt6c3BiLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw2NTViYzk0ZTMxMTA4ZmQxYmExNTNiNTEiLCJhdWQiOiJodHRwczovL0NhckZhbnMuY29tIiwiaWF0IjoxNzAwODUwNDI1LCJleHAiOjE3MDA4NTc2MjUsImF6cCI6Inl4b2FHNzQxUjM2Ujc2UHJ1a0tSaVlXb0p4cXJxY2UzIiwic2NvcGUiOiIiLCJwZXJtaXNzaW9ucyI6WyJmdWxsX2FjY2VzcyJdfQ.rdbYpTbt1MkgA1f5JiIWxydb-YeFGs7SZ7n5zH_EM6nG1Xf2jFl3Tmkc0L4Ts2bJfZsqho7XjGsC5y0UrNhVqZq9Jz8YlD7zHafAMtkqjvFfQbJe2TWKJPoHg4VeXVAfZB_m0JNiCP2Z7ZkBXtg2uw3vAOckUv0RT8ESCliTfjbjMZsme0hKaIZ633LsGQW86r0ACxlHS5_hvF5Af89dmtBqcbMeHu25PlCAzroWyegTrfYq1fJKGfWakhcv--7-xaJxxgf8S7nvjpihQVpX9X7vDRmDKJqvkN7NL61V1SX7uTMx5WBUGClwij-AOoGGuxzk2IQ5MgNOeQ1kcmRbPA`
        },
        body: JSON.stringify(body),
      });
      console.log(response);
      if (!response.ok) {
        const json = await response.json();
        toast.error(json.message || response.statusText || "Unknown error occoured.");
        return;
      }
      toast.success("Advert successfully created");
    } else {
      toast.error("Please fulfill all form fields");
    }
  }

  return (
    <div style={{ boxShadow: FORM_BOX_SHADOW }} className="max-w-2xl mx-auto my-5 py-10 px-5 shadow-md hover:shadow-lg transition duration-300">
      <h1 className="text-center text-2xl pb-5 leading-6 font-medium text-gray-900">Create an advert</h1>
      <Tabs value={formType}>
        <TabsHeader className="relative z-0">
          <Tab value="simple" onClick={() => setFormType(SIMPLE_FORM_TYPE)}>
            Simple form
          </Tab>
          <Tab value="questions" onClick={() => setFormType(QUESTIONS_FORM_TYPE)}>
            Questions form
          </Tab>
        </TabsHeader>
        <TabsBody
          className="!overflow-x-hidden"
          animate={{
            initial: {
              x: formType === QUESTIONS_FORM_TYPE ? 400 : -400,
            },
            mount: {
              x: 0,
            },
            unmount: {
              x: formType === QUESTIONS_FORM_TYPE ? 400 : -400,
            },
          }}
        >
          <TabPanel value="simple">
            <form onSubmit={handleSubmit} className="flex flex-col gap-5">
              <Datepicker
                placeholder={"Select advert's end date"}
                minDate={new Date()}
                startFrom={new Date()}
                asSingle={true}
                value={{ startDate: simpleFormData.endDate, endDate: simpleFormData.endDate }}
                onChange={handleEndDateChange} 
              />
              <Input crossOrigin="" name="title" onChange={handleChange} color="blue-gray" label="Problem title" />
              <Textarea name="description" onChange={handleChange} resize rows={4} color="blue-gray" label="Description of the problem" />
              <ImageDropzone onUpload={handleImagesUpload} />
              <Button type="submit" size="lg" className="rounded-md">
                Create
              </Button>
            </form>
          </TabPanel>
          <TabPanel value="questions">
            <form onSubmit={handleSubmit} className="flex flex-col gap-5">
              <Datepicker
                placeholder={"Select advert's end date"}
                minDate={new Date()}
                startFrom={new Date()}
                asSingle={true}
                value={{ startDate: questionsFormData.endDate, endDate: questionsFormData.endDate }}
                onChange={handleEndDateChange} 
              />
              <Input crossOrigin="" name="title" onChange={handleChange} color="blue-gray" label="Problem title" />
              <YesOrNoChoice name="isSoundBad" onChange={handleYesOrNoValue} question="Have you noticed strange sounds coming from your car?" />
              <YesOrNoChoice name="isScentBad" onChange={handleYesOrNoValue} question="Have you noticed a strange scent coming from your car?" />
              <YesOrNoChoice name="isPanelInvalid" onChange={handleYesOrNoValue} question="Does your car shows warning or error marks in a panel?" />
              <YesOrNoChoice name="isLeakedLiquids" onChange={handleYesOrNoValue} question="Have you noticed leaked liquids under a car?" />
              <YesOrNoChoice name="isUnstableCar" onChange={handleYesOrNoValue} question="Have you noticed that your car is unstable when driving?" />
              <ImageDropzone onUpload={handleImagesUpload} />
              <Textarea name="description" onChange={handleChange} resize rows={4} color="blue-gray" label="Additional information" />
              <Button type="submit" size="lg" className="rounded-md">
                Create
              </Button>
            </form>
          </TabPanel>
        </TabsBody>
      </Tabs>
    </div>
  )
}

export default AdvertPage;