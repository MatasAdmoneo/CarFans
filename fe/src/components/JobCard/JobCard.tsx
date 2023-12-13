"use client"
import { Button, Card, CardBody, CardHeader, Typography } from "@/lib/materialTailwindExports"
import { toast } from "@/lib/reactHotToastExports";
import { JobType } from "@/types/OfferType"
import { JobStatus } from "@/utils/constants";
import { SOMETHING_WENT_WRONG_MESSAGE } from "@/utils/genericMessages";
import { getToken } from "@/utils/getToken";
import { BASE_API_URL, USER_JOBS_ROUTE } from "@/utils/urls";
import { useRouter } from 'next/navigation'

const JobCard = ({ job }: { job: JobType }) => {
  const router = useRouter();
  
  const handleOfferAccept = async () => {
    const token = await getToken();
    await fetch(
      `${BASE_API_URL}${USER_JOBS_ROUTE}/${job.id}`,
      {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ status: JobStatus.Accepted })
      }
    )
      .then(() => {
        toast.success("Offer succesfully accepted!");
        router.push("/list");
      })
      .catch(() => toast.error(SOMETHING_WENT_WRONG_MESSAGE));
  }

  return (
    <Card color="transparent" className="w-full border-black border-2 px-5">
      <CardHeader
        color="transparent"
        floated={false}
        shadow={false}
        className="mx-0 flex items-center gap-4 pt-0 pb-4"
      >
        <div className="flex w-full flex-col">
          <div className="flex items-center justify-between">
            <Typography variant="h6" color="blue-gray">
              Start date: {new Date(job.startDate).toISOString().split('T')[0]}
            </Typography>
            <Button variant="gradient" onClick={handleOfferAccept}>Accept</Button>
          </div>
          <Typography variant="h6" color="blue-gray">
            Price: {job.price}€
          </Typography>
        </div>
      </CardHeader>
      <CardBody className="mb-6 p-0">
        <label>Description:</label>
        <Typography>{job.description}</Typography>
        <Typography variant="h3">NEED TO SHOW SERVICE INFO</Typography>
      </CardBody>
    </Card>
  )
}

export default JobCard;