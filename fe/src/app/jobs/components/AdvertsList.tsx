import { AdvertType } from "@/types/AdvertType"
import AdvertCard from "./AdvertCard"

const AdvertsList = ({ adverts }: { adverts: AdvertType[] }) => {
  return (
    <div className="flex flex-col gap-8">
      {adverts.map(advert => (
        <AdvertCard key={advert.id} advert={advert} />
      ))}
    </div>
  )
}

export default AdvertsList