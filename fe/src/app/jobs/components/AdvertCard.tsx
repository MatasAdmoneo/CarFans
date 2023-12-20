import Link from "next/link";
import {
  Card,
  CardBody,
  CardHeader,
  Chip,
  Typography,
} from "@/lib/materialTailwindExports";
import { AdvertType } from "@/types/AdvertType";
import Timer from "@/components/Timer/Timer";
import ChipWithTooltip from "@/components/ChipWithTooltip/ChipWithTooltip";
import { ChipTooltipText } from "@/utils/constants";

const AdvertCard = ({ advert }: { advert: AdvertType }) => {
  return (
    <Link href={`/jobs/${advert.id}`}>
      <Card color="transparent" className="px-5 mt-5 flex justify-center">
        <CardHeader
          color="transparent"
          floated={false}
          shadow={false}
          className="mx-0 flex items-center gap-4 pt-0 pb-4"
        >
          <div className="flex w-full flex-col gap-3">
            <div className="flex items-center justify-between">
              <Typography variant="h5" color="blue-gray">
                {advert.title}
              </Typography>
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
            <div className="flex gap-5">
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
          </div>
        </CardHeader>
        <CardBody className="mb-6 p-0">
          <label>Description:</label>
          <Typography>
            {advert.description.substring(0, 200)}
            {advert.description.length > 200 && "..."}
          </Typography>
        </CardBody>
      </Card>
    </Link>
  );
};

export default AdvertCard;
