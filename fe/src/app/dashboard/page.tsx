import React from "react";
import { getSession, getAccessToken } from "@auth0/nextjs-auth0";
import { BASE_API_URL, SERVICE_ADVERTRS_ROUTE } from "@/utils/urls";

const getServiceAdvertsData = async () => {
  const { accessToken } = await getAccessToken();
  if (!accessToken) {
    throw new Error(`Requires authorization`);
  }
  // Fix 500 error, accessToken is available and obtained
  const res = await fetch(
    `${BASE_API_URL}${SERVICE_ADVERTRS_ROUTE}`,
    {
      method: "GET",
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    }
  );
  // console.log(res);
  if (!res.ok) {
    const json = await res.json();

    return {
      text: json.message || res.statusText || "Unable to fetch message.",
    };
  }
  return res.json();
};

export default async function DashboardPage() {
  const { user }: any = await getSession();
  const serviceAdverts = await getServiceAdvertsData();
  console.log(serviceAdverts);
  return (
    <>
      <div>Hello {user.name}</div>
      <div>Dashboard page</div>
    </>
  );
}
