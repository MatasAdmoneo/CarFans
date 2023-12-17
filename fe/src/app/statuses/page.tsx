"use client"
import { useState, useEffect } from "react";
import { Accordion, AccordionBody, AccordionHeader, Button, Chip, Typography } from "@/lib/materialTailwindExports"
import { ServiceJobType } from "@/types/ServiceJobType";
import { BASE_API_URL, SERVICE_JOBS_ROUTE } from "@/utils/urls";
import toast from "react-hot-toast";
import { SOMETHING_WENT_WRONG_MESSAGE } from "@/utils/genericMessages";
import { getToken } from "@/utils/getToken";
import { JobStatus } from "@/utils/constants";

const ACCEPTED = "Accepted";
const IN_PROGRESS = "InProgress";
const DONE = "Done";
const CANCELLED = "Cancelled";

function Icon({ jobId, openJobId }: { jobId: string; openJobId: string }) {
  return (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      strokeWidth={2}
      stroke="currentColor"
      className={`${jobId === openJobId ? "rotate-180" : ""} h-5 w-5 transition-transform`}
    >
      <path strokeLinecap="round" strokeLinejoin="round" d="M19.5 8.25l-7.5 7.5-7.5-7.5" />
    </svg>
  );
}

const JobStatusesPage = () => {
  const [jobs, setJobs] = useState<Array<ServiceJobType>>([]);
  const [openJobId, setOpenJobId] = useState("");

  const handleOpen = (value: string) => setOpenJobId(value === openJobId ? "" : value);

  useEffect(() => {
    getAcceptedJobs();
  }, []);

  const getAcceptedJobs = async () => {
    const token = await getToken();
    const res = await fetch(
      `${BASE_API_URL}${SERVICE_JOBS_ROUTE}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    if (!res.ok) {
      toast.error(SOMETHING_WENT_WRONG_MESSAGE);
    }
    setJobs(await res.json());
  };

  const handleChipColor = (status: string) => {
    switch(status) {
      case IN_PROGRESS:
        return "amber";
      case DONE:
        return "green";
      case CANCELLED:
        return "red";
      default:
        return "gray";
    }
  }

  const handleJobStatusUpdate = async (id: string, status?: string) => {
    const token = await getToken();
    await fetch(
      `${BASE_API_URL}${SERVICE_JOBS_ROUTE}/${id}`,
      {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ status: 
          status != undefined
            ? status === ACCEPTED ? JobStatus.InProgress : JobStatus.Done
            : JobStatus.Cancelled})
      }
    )
      .then(() => {
        setJobs((prev) => prev.map(job => job.id === id ? { ...job, status: status ?? CANCELLED } : job));
        toast.success(status != undefined ? "Job status updated!" : "Job progress cancelled!");
      })
      .catch(() => toast.error(SOMETHING_WENT_WRONG_MESSAGE));
  }

  useEffect(() => {
    console.log(jobs);
  }, [jobs])

  return (
    <div className="max-w-3xl mx-auto my-5 py-10 px-5">
      <Typography variant="h3" className="pb-4">Job statuses:</Typography>
      {jobs.map(job => (
        <Accordion key={job.id} className="mb-2 rounded-lg border border-blue-gray-100 px-4" open={openJobId === job.id} icon={<Icon jobId={job.id} openJobId={openJobId} />}>
          <AccordionHeader onClick={() => handleOpen(job.id)}>
            <div className="w-full flex justify-between">
              <Typography>{job.title}</Typography>
              <Chip
                variant="ghost"
                color={handleChipColor(job.status)}
                size="sm"
                value={job.status}
                icon={
                  <span className={`mx-auto mt-1 block h-2 w-2 rounded-full bg-${handleChipColor(job.status)}-900 content-['']`} />
                }
              />
            </div>
          </AccordionHeader>
          <AccordionBody>
            <Typography><span className="font-bold">Brand:</span> {job.brand}</Typography>
            <Typography><span className="font-bold">Model:</span> {job.model}</Typography>
            <Typography><span className="font-bold">Manufacture year:</span> {job.manufactureYear}</Typography>
            <Typography><span className="font-bold">Problem:</span> {job.problemType}</Typography>
            <Typography><span className="font-bold">Price:</span> {job.price}</Typography>
            <div className="w-full flex justify-end my-2 gap-3">
              {job.status === IN_PROGRESS && (
                <Button variant="outlined" onClick={() => handleJobStatusUpdate(job.id)}>Cancel a job</Button>
              )}
              {job.status !== DONE && job.status !== CANCELLED && (
                <Button variant="gradient" onClick={() => handleJobStatusUpdate(job.id, job.status)}>{job.status === ACCEPTED ? "Start" : "Finish"} a job</Button>
              )}
            </div>
          </AccordionBody>
        </Accordion>
      ))}
    </div>
  )
}

export default JobStatusesPage;