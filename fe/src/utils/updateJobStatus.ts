"use server"

import { getAccessToken } from "@auth0/nextjs-auth0";
import { BASE_API_URL, SERVICE_JOBS_ROUTE } from "./urls";
import { JobStatus, JobStatusAsString } from "./constants";

export const updateJobStatus = async (id: string, status?: string) => {
  const { accessToken } = await getAccessToken();
  await fetch(`${BASE_API_URL}${SERVICE_JOBS_ROUTE}/${id}`, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${accessToken}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      status:
        status != undefined
          ? status === JobStatusAsString.ACCEPTED
            ? JobStatus.InProgress
            : JobStatus.Done
          : JobStatus.Cancelled,
    }),
  })
}