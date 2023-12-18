import {
  Card,
  CardBody,
  CardFooter,
  Typography,
  Button,
  Chip,
} from "@/lib/materialTailwindExports";
import { UserAdvertType } from "@/types/AdvertType";
import { ChipTooltipText, FORM_BOX_SHADOW } from "@/utils/constants";
import Link from "next/link";
import ChipWithTooltip from "../ChipWithTooltip/ChipWithTooltip";

const AdvertCard = ({ advert }: { advert: UserAdvertType }) => {
  return (
    <Card style={{ boxShadow: FORM_BOX_SHADOW }} className="mt-6">
      <CardBody>
        <div className="flex flex-column justify-between py-2">
          <Typography variant="h5" color="blue-gray">
            {advert.title}
          </Typography>
          <div className="flex gap-2">
            {advert.isOfferAccepted && <Chip variant="ghost" size="lg" color="orange" value="Offer accepted" />}
            <Chip variant="ghost" size="lg" color={new Date(advert.endDate) > new Date() ? "green" : "red"} value={new Date(advert.endDate) > new Date() ? "Ongoing" : "Ended"} />
          </div>
        </div>
        <div className="flex gap-5 py-2">
          <ChipWithTooltip value={advert.brand} tooltipText={ChipTooltipText.Brand} />
          <ChipWithTooltip value={advert.model} tooltipText={ChipTooltipText.Model} />
          <ChipWithTooltip value={advert.manufactureYear.toString()} tooltipText={ChipTooltipText.ManufactureYear} />
          <ChipWithTooltip value={advert.problemType} tooltipText={ChipTooltipText.ProblemType} />
        </div>
        <Typography>{advert.description.substring(0, 200)}{advert.description.length > 200 && "..."}</Typography>
      </CardBody>
      <CardFooter className="pt-0">
        <Button disabled={advert.isOfferAccepted} className="p-0" variant="gradient">
          <Link className="w-full flex justify-center px-5 py-3" href={`/offers/${advert.id}`}>See offers</Link>
        </Button>
      </CardFooter>
    </Card>
  )
}

export default AdvertCard;