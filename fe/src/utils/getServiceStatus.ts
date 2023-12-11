import { getAccessToken } from "@auth0/nextjs-auth0/edge";
import { SERVICE_STATUS_ROUTE } from "./urls";
import { ServiceStatus } from "./constants";

export const getServiceStatus = async () => {
  // Must use edge environment, instead of getToken.ts, since this function is called in middleware and middleware runs on edge
  const { accessToken } = await getAccessToken();
  const res = await fetch(
    `${process.env.API_SERVER_URL}${SERVICE_STATUS_ROUTE}`,
    {
      method: "GET",
      headers: {
        Authorization: `Bearer ${accessToken}`,
        "Access-Control-Allow-Origin": "*",
      },
    }
  );
  const response = await res.json();
  if (Object.values(ServiceStatus).includes(response.status as ServiceStatus)) {
    return response.status;
  }
  return -1;
};
