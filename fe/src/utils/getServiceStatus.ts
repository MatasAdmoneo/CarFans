import { getAccessToken } from "@auth0/nextjs-auth0/edge";
import { SERVICE_STATUS_ROUTE } from "./urls";

enum ServiceStatus {
  Exists = 0,
  CreatedInDataBase = 1,
  Pending = 2,
  Accepted = 3,
  Denied = 4,
}

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
  const status = await res.json();
  if (Object.values(ServiceStatus).includes(status as ServiceStatus)) {
    return ServiceStatus[status as keyof typeof ServiceStatus];
  }
  return -1;
};
