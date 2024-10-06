import { type ServiceInfoForm } from "@/types/ServiceInfoForm";
import { BASE_API_URL, SERVICE_ADDITIONAL_INFO_ROUTE } from "@/utils/urls";

export const submitInfoForm = async (isForwardButtonDisabled: boolean, data: ServiceInfoForm, token: string) => {
  try {
    const response = await fetch(
      `${BASE_API_URL}${SERVICE_ADDITIONAL_INFO_ROUTE}`,
      {
        method: `${isForwardButtonDisabled ? "PATCH" : "POST"}`,
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
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