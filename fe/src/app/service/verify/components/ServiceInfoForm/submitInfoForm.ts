"use server";

import { type ServiceInfoForm } from "@/types/ServiceInfoForm";
import { BASE_API_URL, SERVICE_ADDITIONAL_INFO_ROUTE } from "@/utils/urls";
import { getAccessToken } from "@auth0/nextjs-auth0";

export const submitInfoForm = async (isForwardButtonDisabled: boolean, data: ServiceInfoForm) => {
  try {
    const { accessToken } = await getAccessToken();
    const response = await fetch(
      `${BASE_API_URL}${SERVICE_ADDITIONAL_INFO_ROUTE}`,
      {
        method: `${isForwardButtonDisabled ? "POST" : "PATCH"}`,
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${accessToken}`,
        },
        body: JSON.stringify(data),
      }
    );

    if (!response.ok) {
      const json = await response.json();
      throw new Error(
        json.message || response.statusText || "Unknown error occurred."
      );
    }
  } catch (error) {
    throw new Error("Failed to submit form.");
  }
};