"use client"
import Link from 'next/link'
import { Card, CardBody, CardHeader, Chip, Typography } from "@/lib/materialTailwindExports"
import { AdvertType } from "@/types/AdvertType"
import Timer from '@/components/Timer/Timer';

const AdvertCard = ({ advert }: { advert: AdvertType }) => {
  return (
    <Link href={`/jobs/${advert.id}`}>
      <Card color="transparent" className="w-full border-black border-2 px-5">
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
              {advert.brand && <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={advert.brand} />}
              {advert.model && <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={advert.model} />}
              {advert.manufactureYear && <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={advert.manufactureYear} />}
              {advert.problemType && <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={advert.problemType} />}
            </div>
          </div>
        </CardHeader>
        <CardBody className="mb-6 p-0">
          <label>Description:</label>
          <Typography>
            {advert.description.substring(0, 200)}{advert.description.length > 200 && "..."}
          </Typography>
        </CardBody>
      </Card>
    </Link>
  )
}

export default AdvertCard;