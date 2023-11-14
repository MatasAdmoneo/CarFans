import React from "react";
import { getSession, getAccessToken } from "@auth0/nextjs-auth0";
import { BASE_API_URL, SERVICE_JOBS_ROUTE } from "@/utils/urls";

const getServiceJobsData = async () => {
  const { accessToken } = await getAccessToken();
  if (!accessToken) {
    throw new Error(`Requires authorization`);
  }
  // Fix 500 error, accessToken is available and obtained
  const res = await fetch(
    `${process.env.API_SERVER_URL}${SERVICE_JOBS_ROUTE}`,
    {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    }
  );
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
  //   const serviceJobs = await getServiceJobsData();
  //   console.log(serviceJobs);
  return (
    <>
      <div>Hello {user.name}</div>
      <div>Dashboard page</div>
    </>
  );
}
