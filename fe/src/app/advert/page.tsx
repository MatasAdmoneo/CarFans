"use client"
import { useState } from "react";
import Datepicker, { DateValueType } from "react-tailwindcss-datepicker"; 
import YesOrNoChoice from "@/components/YesOrNoChoice/YesOrNoChoice";
import { Button, Input, Tab, TabPanel, Tabs, TabsBody, TabsHeader, Textarea } from "@/lib/materialTailwindExports";
import { QuestionsFormType, SimpleFormType } from "@/types/CreateAdvertForm";
import { FORM_BOX_SHADOW } from "@/utils/constants";
import { QUESTIONS_FORM_TYPE, SIMPLE_FORM_TYPE, questionsFormDefaultValues, questionsFormSchema, simpleFormDefaultValues, simpleFormSchema } from "./utils";
import { BASE_API_URL, USER_ADVERTS_ROUTE } from "@/utils/urls";
import ImageDropzone from "@/components/ImageDropzone/ImageDropzone";
import { toast } from "@/lib/reactHotToastExports";
import { getToken } from "@/utils/getToken";

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
    console.log("ASDF ->", simpleFormData)
    const isValid = await validateData();

    if (isValid) {
      const data = formType === SIMPLE_FORM_TYPE ? simpleFormData : questionsFormData;
      const body = {
        ...data,
        endDate: new Date(data.endDate!),
        isQuestionsFormType: formType === QUESTIONS_FORM_TYPE,
      }
      const token = await getToken();
      const response = await fetch(`${BASE_API_URL}${USER_ADVERTS_ROUTE}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(body),
      });
      setQuestionsFormData(questionsFormDefaultValues);
      setSimpleFormData(simpleFormDefaultValues);
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
    <div style={{ boxShadow: FORM_BOX_SHADOW }} className="max-w-3xl mx-auto my-5 py-10 px-5 shadow-md hover:shadow-lg transition duration-300">
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
              <Input crossOrigin="" value={simpleFormData.title} name="title" onChange={handleChange} color="blue-gray" label="Problem title" />
              <Textarea value={simpleFormData.description} name="description" onChange={handleChange} resize rows={4} color="blue-gray" label="Description of the problem" />
              <ImageDropzone uploadedPhotos={simpleFormData.photos} onUpload={handleImagesUpload} />
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
              <Input value={questionsFormData.title} crossOrigin="" name="title" onChange={handleChange} color="blue-gray" label="Problem title" />
              <YesOrNoChoice name="isSoundBad" value={questionsFormData.isSoundBad} onChange={handleYesOrNoValue} question="Have you noticed strange sounds coming from your car?" />
              <YesOrNoChoice name="isScentBad" value={questionsFormData.isScentBad} onChange={handleYesOrNoValue} question="Have you noticed a strange scent coming from your car?" />
              <YesOrNoChoice name="isPanelInvalid" value={questionsFormData.isPanelInvalid} onChange={handleYesOrNoValue} question="Does your car shows warning or error marks in a panel?" />
              <YesOrNoChoice name="isLeakedLiquids" value={questionsFormData.isLeakedLiquids} onChange={handleYesOrNoValue} question="Have you noticed leaked liquids under a car?" />
              <YesOrNoChoice name="isUnstableCar" value={questionsFormData.isUnstableCar} onChange={handleYesOrNoValue} question="Have you noticed that your car is unstable when driving?" />
              <ImageDropzone uploadedPhotos={questionsFormData.photos} onUpload={handleImagesUpload} />
              <Textarea value={questionsFormData.description} name="description" onChange={handleChange} resize rows={4} color="blue-gray" label="Additional information" />
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