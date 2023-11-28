import React from "react";
import { SERVICE_ADVERTRS_ROUTE } from "@/utils/urls";
import { getAccessToken, getSession } from "@auth0/nextjs-auth0";
import { UNKNOW_ERROR_MESSAGE } from "@/utils/genericMessages";

const getServiceAdvertsData = async () => {
  const { accessToken } = await getAccessToken();
  if (!accessToken) {
    throw new Error(`Requires authorization`);
  }
  const res = await fetch(
    `${process.env.API_SERVER_URL}${SERVICE_ADVERTRS_ROUTE}`,
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
      text: json.message || res.statusText || UNKNOW_ERROR_MESSAGE,
    };
  }
  return res.json();
};

export default async function DashboardPage() {
  const { user }: any = await getSession();
  const serviceAdverts = await getServiceAdvertsData();

  return (
    <>
      <div>Hello {user.name}</div>
      <div>Dashboard page</div>
    </>
  );
}
