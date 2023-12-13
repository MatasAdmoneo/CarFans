"use client"
import JobCard from "@/components/JobCard/JobCard";
import { JobType } from "@/types/OfferType";
import { Button, Select, Typography, Option, IconButton, Input, Popover, PopoverHandler, PopoverContent } from "@material-tailwind/react";
import { useMemo, useState } from "react";
import { FaFilter, FaSortAmountDownAlt, FaSortAmountUp } from "react-icons/fa";

enum sortDirectionValue {
  ASC,
  DESC
}

const MyOffersList = ({ jobs }: { jobs: JobType[] }) => {
  const [sortBy, setSortBy] = useState<string>("startDate");
  const [sortDirection, setSortDirection] = useState<sortDirectionValue>(sortDirectionValue.DESC);
  const [minPrice, setMinPrice] = useState<string>();
  const [maxPrice, setMaxPrice] = useState<string>();

  const jobsList = useMemo(() => {
    var list: JobType[] = jobs;
    if (minPrice?.length) {
      list = list.filter(x => x.price >= parseFloat(minPrice));
    }

    if (maxPrice?.length) {
      list = list.filter(x => x.price <= parseFloat(maxPrice));
    }

    return list.toSorted((a, b) => {
      const compareValueA = a[sortBy as keyof typeof a]!;
      const compareValueB = b[sortBy as keyof typeof b]!;

      if (compareValueA < compareValueB) {
        return sortDirection ? -1 : 1;
      } else {
        return sortDirection ? 1 : -1;
      }
    });
  }, [jobs, minPrice, maxPrice, sortBy, sortDirection]);

  const handleSortDirectionChange = () => {
    setSortDirection(state => state ? sortDirectionValue.ASC : sortDirectionValue.DESC);
  }
  
  return (
    <>
      <div className="flex justify-between">
        <Typography variant="h2">Offers</Typography>
        <div className="flex flex-column justify-end gap-5">
          <Popover placement="bottom">
            <PopoverHandler>
              <Button variant="outlined" className="flex flex-column gap-2 items-center h-min">Filter<FaFilter /></Button>
            </PopoverHandler>
            <PopoverContent className="shadow-black shadow-sm">
              <label>Price</label>
              <div className="flex gap-2 items-center">
                <Input onChange={(e) => setMinPrice(e.target.value)} type="number" value={minPrice} min={0} crossOrigin="" label="Min" />
                -
                <Input onChange={(e) => setMaxPrice(e.target.value)} type="number" value={maxPrice} min={0} crossOrigin="" label="Max" />
              </div>
            </PopoverContent>
          </Popover>
          <div className="flex flex-column gap-1">
            <Select value={sortBy} onChange={(value) => value && setSortBy(value)} label="Sort by" menuProps={{ className: "shadow-black shadow-sm" }}>
              <Option value="startDate">Work start date</Option>
              <Option value="price">Price</Option>
            </Select>
            <IconButton className="text-xl" variant="outlined" onClick={handleSortDirectionChange}>
              {sortDirection ? <FaSortAmountDownAlt /> : <FaSortAmountUp />}
            </IconButton>
          </div>
        </div>
      </div>
      <div className="flex flex-col gap-4 py-3">
        {jobsList?.length ? jobsList.map((job: JobType) => (
          <JobCard key={job.id} job={job} />
        )) : "No offers yet"}
      </div>
    </>
  )
}

export default MyOffersList;