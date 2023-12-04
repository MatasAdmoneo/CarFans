import Carousel from "@/components/Carousel/Carousel";
import Timer from "@/components/Timer/Timer";
import { Button, Chip, Typography } from "@/lib/materialTailwindExports";
import { AdvertType } from "@/types/AdvertType";
import { getToken } from "@/utils/getToken";
import { BASE_API_URL, SERVICE_ADVERTRS_ROUTE } from "@/utils/urls";
import { FaCheckCircle } from "react-icons/fa";
import { FaCircleXmark } from "react-icons/fa6";

const AnswerIcon = (isPositive: boolean) => isPositive ? <FaCheckCircle className="text-green-500 text-2xl" /> : <FaCircleXmark className="text-red-500 text-2xl" />;

async function getAdvert(jobId: string) {
  const token = await getToken();
  const response = await fetch(`${BASE_API_URL}${SERVICE_ADVERTRS_ROUTE}/${jobId}`, {
    headers: { Authorization: `Bearer ${token}` }
  });
  if (!response.ok) {
    return;
  }

  return response.json();
}

export default async function AdvertInfoPage({ params: { jobId } }: { params: { jobId: string }}) {
  const advert: AdvertType = await getAdvert(jobId);

  if (!advert) {
    return "Not found";
  }

  return (
    <div className="flex flex-col gap-3 max-w-3xl mx-auto my-5 py-10 px-5">
      <Typography variant="h3">
        {advert.title}
      </Typography>
      <div className="flex justify-between">
        <div className="flex gap-5">
          <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={advert.brand} />
          <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={advert.model} />
          <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={advert.manufactureYear} />
          <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={advert.problemType} />
        </div>
        <Chip
          variant="ghost"
          color="green"
          className="chip-opacity"
          value={<Timer expiryTimestamp={advert.endDate} />}
          icon={
            <span className="mx-auto mt-1 block h-2 w-2 rounded-full bg-green-900 content-['']" />
          }
        />
      </div>
      {advert.photos.length > 0 && <Carousel photos={advert.photos} />}
      {advert.isQuestionsFormType && (
        <div className="flex flex-col gap-1">
          <label>Questions & answers:</label>
          <ol className="border-black border-2 rounded-lg px-2 py-1">
            <li className="flex justify-between">
              <Typography>
                Noticed strange sounds coming from the car
              </Typography>
              {AnswerIcon(advert.isSoundBad!)}
            </li>
            <li className="flex justify-between">
              <Typography>
                Noticed a strange scent coming from the car
              </Typography>
              {AnswerIcon(advert.isScentBad!)}
            </li>
            <li className="flex justify-between">
              <Typography>
                Car has warning or error marks in a panel
              </Typography>
              {AnswerIcon(advert.isPanelInvalid!)}
            </li>
            <li className="flex justify-between">
              <Typography>
                Noticed leaked liquids under a car
              </Typography>
              {AnswerIcon(advert.isLeakedLiquids!)}
            </li>
            <li className="flex justify-between">
              <Typography>
                Noticed that the car is unstable when driving
              </Typography>
              {AnswerIcon(advert.isUnstableCar!)}
            </li>
          </ol>
        </div>
      )}
      <div className="flex flex-col gap-1">
        <label>Description:</label>
        <div className="border-black border-2 rounded-lg px-2 py-1">
          <Typography>
            {advert.description}
          </Typography>
        </div>
      </div>
      <Button variant="gradient">Send an offer</Button>
    </div>
  )
}