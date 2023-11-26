import React from "react";
import { getSession } from "@auth0/nextjs-auth0";
import { BASE_API_URL, SERVICE_ADVERTRS_ROUTE } from "@/utils/urls";
import { getAccessToken } from "@auth0/nextjs-auth0";

const getServiceAdvertsData = async () => {
  const { accessToken } = await getAccessToken();
  if (!accessToken) {
    throw new Error(`Requires authorization`);
  }
  const res = await fetch(
    `${BASE_API_URL}${SERVICE_ADVERTRS_ROUTE}`,
    {
      method: "GET",
      headers: {
        Authorization: `Bearer ${accessToken}`,
        "Access-Control-Allow-Origin": "*",
      },
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

export default async function DashboardPage() {
  const { user }: any = await getSession();
  const serviceAdverts = await getServiceAdvertsData();
  console.log(JSON.stringify(serviceAdverts));
  return (
    <>
      <div>Hello {user.name}</div>
      <div>Dashboard page</div>
    </>
  );
}
