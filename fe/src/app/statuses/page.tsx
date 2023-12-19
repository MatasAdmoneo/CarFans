"use client";
import { useState, useEffect } from "react";
import {
  Accordion,
  AccordionBody,
  AccordionHeader,
  Button,
  Card,
  Chip,
  Typography,
} from "@/lib/materialTailwindExports";
import { ServiceJobType } from "@/types/ServiceJobType";
import { BASE_API_URL, SERVICE_JOBS_ROUTE } from "@/utils/urls";
import toast from "react-hot-toast";
import { SOMETHING_WENT_WRONG_MESSAGE } from "@/utils/genericMessages";
import { getToken } from "@/utils/getToken";
import { JobStatus, JobStatusAsString } from "@/utils/constants";
import { handleChipColor, handleChipIconColor } from "@/utils/handleChipStyles";

function Icon({ jobId, openJobId }: { jobId: string; openJobId: string }) {
  return (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      strokeWidth={2}
      stroke="currentColor"
      className={`${
        jobId === openJobId ? "rotate-180" : ""
      } h-5 w-5 transition-transform`}
    >
      <path
        strokeLinecap="round"
        strokeLinejoin="round"
        d="M19.5 8.25l-7.5 7.5-7.5-7.5"
      />
    </svg>
  );
}

function ChipDot({ status }: { status: string }) {
  const color = handleChipIconColor(status);
  console.log(color);
  return (
    <svg
      className="w-full h-2 mt-1"
      version="1.1"
      id="Capa_1"
      xmlns="http://www.w3.org/2000/svg"
      viewBox="0 0 31.955 31.955"
    >
      <g>
        <path
          fill={color}
          d="M27.25,4.655C20.996-1.571,10.88-1.546,4.656,4.706C-1.571,10.96-1.548,21.076,4.705,27.3
		c6.256,6.226,16.374,6.203,22.597-0.051C33.526,20.995,33.505,10.878,27.25,4.655z"
        />
        <path
          fill={color}
          d="M13.288,23.896l-1.768,5.207c2.567,0.829,5.331,0.886,7.926,0.17l-0.665-5.416
		C17.01,24.487,15.067,24.5,13.288,23.896z M8.12,13.122l-5.645-0.859c-0.741,2.666-0.666,5.514,0.225,8.143l5.491-1.375
		C7.452,17.138,7.426,15.029,8.12,13.122z M28.763,11.333l-4.965,1.675c0.798,2.106,0.716,4.468-0.247,6.522l5.351,0.672
		C29.827,17.319,29.78,14.193,28.763,11.333z M11.394,2.883l1.018,5.528c2.027-0.954,4.356-1.05,6.442-0.288l1.583-5.137
		C17.523,1.94,14.328,1.906,11.394,2.883z"
        />
        <circle fill={color} cx="15.979" cy="15.977" r="6.117" />
      </g>
    </svg>
  );
}

const JobStatusesPage = () => {
  const [jobs, setJobs] = useState<Array<ServiceJobType>>([]);
  const [openJobId, setOpenJobId] = useState("");

  const handleOpen = (value: string) =>
    setOpenJobId(value === openJobId ? "" : value);

  useEffect(() => {
    getAcceptedJobs();
  }, []);

  const getAcceptedJobs = async () => {
    const token = await getToken();
    const res = await fetch(`${BASE_API_URL}${SERVICE_JOBS_ROUTE}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    if (!res.ok) {
      toast.error(SOMETHING_WENT_WRONG_MESSAGE);
    }
    setJobs(await res.json());
  };

  const handleJobStatusUpdate = async (id: string, status?: string) => {
    const token = await getToken();
    await fetch(`${BASE_API_URL}${SERVICE_JOBS_ROUTE}/${id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
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
      .then(() => {
        setJobs((prev) =>
          prev.map((job) =>
            job.id === id
              ? {
                  ...job,
                  status:
                    status != undefined
                      ? status === JobStatusAsString.ACCEPTED
                        ? JobStatusAsString.IN_PROGRESS
                        : JobStatusAsString.DONE
                      : JobStatusAsString.CANCELLED,
                }
              : job
          )
        );
        toast.success(
          status != undefined
            ? "Job status updated!"
            : "Job progress cancelled!"
        );
      })
      .catch(() => toast.error(SOMETHING_WENT_WRONG_MESSAGE));
  };

  return (
    <div className="max-w-3xl mx-auto my-5 py-10 px-5">
      <Typography variant="h3" className="pb-4">
        Active job statuses:
      </Typography>
      {jobs.length !== 0 ? (
        jobs.map((job) => (
          <Accordion
            key={job.id}
            className="mb-2 rounded-lg border border-blue-gray-100 px-4"
            open={openJobId === job.id}
            icon={<Icon jobId={job.id} openJobId={openJobId} />}
          >
            <AccordionHeader onClick={() => handleOpen(job.id)}>
              <div className="w-full flex justify-between">
                <Typography>{job.title}</Typography>
                <Chip
                  variant="ghost"
                  color={handleChipColor(job.status)}
                  size="sm"
                  value={job.status}
                  icon={<ChipDot status={job.status} />}
                />
              </div>
            </AccordionHeader>
            <AccordionBody>
              <Typography>
                <span className="font-bold">Brand:</span> {job.brand}
              </Typography>
              <Typography>
                <span className="font-bold">Model:</span> {job.model}
              </Typography>
              <Typography>
                <span className="font-bold">Manufacture year:</span>{" "}
                {job.manufactureYear}
              </Typography>
              <Typography>
                <span className="font-bold">Problem:</span> {job.problemType}
              </Typography>
              <Typography>
                <span className="font-bold">Price:</span> {job.price}
              </Typography>
              <div className="w-full flex justify-end my-2 gap-3">
                {job.status === JobStatusAsString.IN_PROGRESS && (
                  <Button
                    variant="outlined"
                    onClick={() => handleJobStatusUpdate(job.id)}
                  >
                    Cancel a job
                  </Button>
                )}
                {job.status !== JobStatusAsString.DONE &&
                  job.status !== JobStatusAsString.CANCELLED && (
                    <Button
                      variant="gradient"
                      onClick={() => handleJobStatusUpdate(job.id, job.status)}
                    >
                      {job.status === JobStatusAsString.ACCEPTED
                        ? "Start"
                        : "Finish"}{" "}
                      a job
                    </Button>
                  )}
              </div>
            </AccordionBody>
          </Accordion>
        ))
      ) : (
        <Card className="h-40 mt-5 flex justify-center">
          <h2 className="text-3xl font-bold text-center">
            You have no active jobs
          </h2>
        </Card>
      )}
    </div>
  );
};

export default JobStatusesPage;
