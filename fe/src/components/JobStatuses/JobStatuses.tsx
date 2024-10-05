"use client";

import { ServiceJobType } from "@/types/ServiceJobType";
import { useState } from "react";
import JobAccordion from "../JobAccordion/JobAccordion";
import { Card } from "@material-tailwind/react";
import { updateJobStatus } from "@/utils/updateJobStatus";
import { JobStatusAsString } from "@/utils/constants";
import toast from "react-hot-toast";
import { SOMETHING_WENT_WRONG_MESSAGE } from "@/utils/genericMessages";

type JobStatusesType = {
  jobStatuses: Array<ServiceJobType>;
}

const JobStatuses = ({ jobStatuses }: JobStatusesType) => {
  const [jobs, setJobs] = useState<Array<ServiceJobType>>(jobStatuses || []);
  const [openJobId, setOpenJobId] = useState("");

  const handleOpen = (value: string) =>
    setOpenJobId(value === openJobId ? "" : value);

  const handleJobStatusUpdate = async (id: string, status?: string) => {
    await updateJobStatus(id, status)
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
      .catch((error) => {toast.error(SOMETHING_WENT_WRONG_MESSAGE); console.log(error)});
  };
  
  return (
    jobs.length !== 0 ? (
      jobs.map((job) => (
        <JobAccordion key={job.id} job={job} handleUpdate={handleJobStatusUpdate} handleOpen={handleOpen} openJobId={openJobId} />
      ))
    ) : (
      <Card className="h-40 mt-5 flex justify-center">
        <h2 className="text-3xl font-bold text-center">
          You have no active jobs
        </h2>
      </Card>
    )
  )
}

export default JobStatuses;