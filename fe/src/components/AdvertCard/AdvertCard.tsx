import {
  Card,
  CardBody,
  CardFooter,
  Typography,
  Button,
  Chip,
} from "@/lib/materialTailwindExports";
import { UserAdvertType } from "@/types/AdvertType";
import {
  ChipTooltipText,
  FORM_BOX_SHADOW,
  JobStatusAsString,
} from "@/utils/constants";
import Link from "next/link";
import ChipWithTooltip from "../ChipWithTooltip/ChipWithTooltip";
import { getAccessToken } from "@auth0/nextjs-auth0";
import { BASE_API_URL, USER_JOBS_ROUTE } from "@/utils/urls";
import { JobType, JobTypeStatusAsString } from "@/types/OfferType";

const getAdvertJobs = async (advertId: string) => {
  const { accessToken } = await getAccessToken();
  const res = await fetch(`${BASE_API_URL}${USER_JOBS_ROUTE}/${advertId}`, {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
  if (!res.ok) {
    return null;
  }
  return res.json();
};

const AdvertCard = async ({ advert }: { advert: UserAdvertType }) => {
  const advertJobs = await getAdvertJobs(advert.id);

  const getJobId = (advertJobs: JobTypeStatusAsString[]) => {
    console.log(advertJobs);
    const job: JobTypeStatusAsString | undefined = advertJobs.find(
      (job: JobTypeStatusAsString) => job.status === "Done"
    );
    if (job) {
      return job.id;
    } else return "";
  };

  const jobId = getJobId(advertJobs);

  const getJobStatus = () => {
    const isJobStatusDone = advertJobs?.some(
      (job: JobTypeStatusAsString) => job.status === "Done"
    );
    const isJobStatusCanceled = advertJobs?.some(
      (job: JobTypeStatusAsString) => job.status === "Cancelled"
    );

    const isJobStatusInProgress = advertJobs?.some(
      (job: JobTypeStatusAsString) => job.status === "InProgress"
    );

    if (isJobStatusDone) {
      return "Done";
    } else if (isJobStatusCanceled) {
      return "Cancelled";
    } else if (isJobStatusInProgress) {
      return "InProgress";
    } else return;
  };

  return (
    <Card style={{ boxShadow: FORM_BOX_SHADOW }} className="mt-6">
      <CardBody>
        <div className="flex flex-column justify-between py-2">
          <Typography variant="h5" color="blue-gray">
            {advert.title}
          </Typography>
          <div className="flex gap-2">
            {advert.isOfferAccepted && (
              <Chip
                variant="ghost"
                size="lg"
                color="orange"
                value="Offer accepted"
              />
            )}
            {!getJobStatus() ? (
              <Chip
                variant="ghost"
                size="lg"
                color={new Date(advert.endDate) > new Date() ? "green" : "red"}
                value={
                  new Date(advert.endDate) > new Date() ? "Ongoing" : "Ended"
                }
              />
            ) : (
              <Chip
                variant="ghost"
                size="lg"
                color={
                  getJobStatus() === "Done"
                    ? "green"
                    : getJobStatus() === "Cancelled"
                    ? "red"
                    : "orange"
                }
                value={
                  getJobStatus() === "Done"
                    ? "Repairs Completed"
                    : getJobStatus() === "Cancelled"
                    ? "Repairs Cancelled"
                    : "Repairs In Progress"
                }
              />
            )}
          </div>
        </div>
        <div className="flex gap-5 py-2">
          <ChipWithTooltip
            value={advert.brand}
            tooltipText={ChipTooltipText.Brand}
          />
          <ChipWithTooltip
            value={advert.model}
            tooltipText={ChipTooltipText.Model}
          />
          <ChipWithTooltip
            value={advert.manufactureYear.toString()}
            tooltipText={ChipTooltipText.ManufactureYear}
          />
          <ChipWithTooltip
            value={advert.problemType}
            tooltipText={ChipTooltipText.ProblemType}
          />
        </div>
        <Typography>
          {advert.description.substring(0, 200)}
          {advert.description.length > 200 && "..."}
        </Typography>
      </CardBody>
      <CardFooter className="pt-0">
        <Button
          disabled={advert.isOfferAccepted}
          className="p-0"
          variant="gradient"
        >
          <Link
            className="w-full flex justify-center px-5 py-3"
            href={`/offers/${advert.id}`}
          >
            See offers
          </Link>
        </Button>
        <Button
          disabled={getJobStatus() !== "Done"}
          className="ml-2 p-0"
          variant="gradient"
        >
          <Link
            className="w-full flex justify-center px-5 py-3"
            href={`/review/${jobId}`}
          >
            Review
          </Link>
        </Button>
      </CardFooter>
    </Card>
  );
};

export default AdvertCard;
