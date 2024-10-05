import {
  Breadcrumbs,
  Typography,
} from "@/lib/materialTailwindExports";
import { BASE_API_URL, SERVICE_JOBS_ROUTE } from "@/utils/urls";
import Link from "next/link";
import JobStatuses from "@/components/JobStatuses/JobStatuses";
import { getAccessToken } from "@auth0/nextjs-auth0";

const getAcceptedJobs = async () => {
  const { accessToken } = await getAccessToken();
  const res = await fetch(`${BASE_API_URL}${SERVICE_JOBS_ROUTE}`, {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
  return res.json();
};

const JobStatusesPage = async () => {
  const jobStatuses = await getAcceptedJobs();

  return (
    <div className="max-w-3xl mx-auto my-5 py-10 px-5">
      <Breadcrumbs>
        <Link href="/home" className="opacity-60">
          <span>Home</span>
        </Link>
        <Link href="#">
          <span>Statuses</span>
        </Link>
      </Breadcrumbs>
      <Typography variant="h3" className="pb-4 mt-4">
        Active jobs statuses
      </Typography>
      <JobStatuses jobStatuses={jobStatuses} />
    </div>
  );
};

export default JobStatusesPage;
