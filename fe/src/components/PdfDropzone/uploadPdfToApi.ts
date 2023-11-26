"use client";

import { getToken } from "@/utils/getToken";
import { BASE_API_URL, SERVICE_DOCUMENT_UPLOAD_ROUTE } from "@/utils/urls";
import { Dispatch, SetStateAction } from "react";
import { toast } from "@/lib/reactHotToastExports";

export const uploadPdfToApi = async (
  file: File,
  setSuccessUpload: Dispatch<SetStateAction<boolean>>
) => {
  const accessToken = await getToken();
  const reader = new FileReader();

  reader.onload = async () => {
    // Extract base64 content from data URL
    const base64Content = reader.result?.toString().split(",")[1];
    if (base64Content) {
      try {
        const res = await fetch(
          `${BASE_API_URL}${SERVICE_DOCUMENT_UPLOAD_ROUTE}`,
          {
            headers: {
              Authorization: `Bearer ${accessToken}`,
              "Content-Type": "application/json",
            },
            method: "POST",
            body: JSON.stringify(base64Content), // Send base64 content in the request body
          }
        );

        if (!res.ok) {
          const json = await res.json();
          throw new Error(
            json.message || res.statusText || "Unknown error occurred."
          );
        }

        toast.success("Documents uploaded successfully.");
        setSuccessUpload(true);
      } catch (error) {
        toast.error("Failed to upload file.");
      }
    }
  };

  reader.onerror = () => {
    toast.error("Error reading file. Try uploading again");
  };

  reader.readAsDataURL(file);
};
