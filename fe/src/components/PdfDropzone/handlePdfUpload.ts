import { SERVICE_DOCUMENT_UPLOAD_ROUTE } from "@/utils/urls";

export const uploadPdfToApi = async (acceptedFiles: File[]) => {
  const formData = new FormData();
  formData.append("file", acceptedFiles[0]);
  const res = await fetch(
    `${process.env.API_SERVER_URL}${SERVICE_DOCUMENT_UPLOAD_ROUTE}`,
    {
      method: "POST",
      body: formData,
    }
  );

  if (!res.ok) {
    const json = await res.json();

    return {
      text: json.message || res.statusText || "Unknown error occoured.",
    };
  }
  return res.json();
};
