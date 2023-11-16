import React from "react";
import { getSession, getAccessToken } from "@auth0/nextjs-auth0";
import { BASE_API_URL, SERVICE_ADVERTRS_ROUTE } from "@/utils/urls";
import { removeLettersAfterNewline } from "@/utils/removeLettersAfterNewLine";

const getServiceJobsData = async () => {
  const { accessToken } = await getAccessToken();
  if (!accessToken) {
    throw new Error(`Requires authorization`);
  }
  // Fix 500 error, accessToken is available and obtained
  const res = await fetch(
    `${process.env.API_SERVER_URL}${SERVICE_ADVERTRS_ROUTE}`,
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
  return JSON.stringify(res.json());
};

export default async function DashboardPage() {
  const { user }: any = await getSession();
  const serviceJobs = await getServiceJobsData();
  console.log(serviceJobs);
  return (
    <>
      <div>Hello {user.name}</div>
      <div>Dashboard page</div>
    </>
  );
}
