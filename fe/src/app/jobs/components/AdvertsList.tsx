"use client"
import AdvertCard from "./AdvertCard"
import { Button, Checkbox, IconButton, Menu, MenuHandler, MenuItem, MenuList, Select, Typography, Option } from "@/lib/materialTailwindExports"
import { useMemo, useState } from "react"
import { AdvertType } from "@/types/AdvertType"
import { FaFilter, FaSortAmountDownAlt, FaSortAmountUp } from "react-icons/fa"

enum sortDirectionValue {
  ASC,
  DESC
}

const AdvertsList = ({ adverts }: { adverts: AdvertType[] }) => {
  const [sortBy, setSortBy] = useState<string>("endDate");
  const [sortDirection, setSortDirection] = useState<sortDirectionValue>(sortDirectionValue.DESC);
  const [filterByProblemTypes, setFilterByProblemTypes] = useState<Array<string>>([]);
  const CarProblemTypeLabels = ["Engine", "Chassis", "Body", "Tyres", "Maintenance", "Other", "Unknown"];

  const advertsList = useMemo(() => {
    var list: AdvertType[] = adverts;
    if (filterByProblemTypes.length > 0) {
      list = adverts.filter(x => filterByProblemTypes.includes(x.problemType));
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
  }, [adverts, filterByProblemTypes, sortBy, sortDirection]);

  const handleSortDirectionChange = () => {
    setSortDirection(state => state ? sortDirectionValue.ASC : sortDirectionValue.DESC);
  }

  const handleFilterByProblemTypes = (name: string) => {
    if (filterByProblemTypes.some(x => x === name))
      setFilterByProblemTypes(array => array.filter(x => x !== name));
    else
      setFilterByProblemTypes(array => [...array, name]);
  } 

  return (
    <>
      <div className="pb-5 flex flex-column justify-between">
        <Typography variant="h2">Adverts</Typography>
        <div className="flex flex-column justify-end gap-5">
        <Menu dismiss={{ itemPress: false }}>
          <MenuHandler>
            <Button variant="outlined" className="flex flex-column gap-2 items-center h-min">Filter<FaFilter /></Button>
          </MenuHandler>
          <MenuList className="shadow-black shadow-sm">
            {CarProblemTypeLabels.map((name, index) => (
              <MenuItem key={index} className="p-0">
                <label
                  htmlFor={name}
                  className="flex cursor-pointer items-center gap-2 p-2"
                >
                  <Checkbox
                    crossOrigin=""
                    ripple={false}
                    id={name}
                    containerProps={{ className: "p-0" }}
                    className="hover:before:content-none"
                    checked={filterByProblemTypes.includes(name)}
                    name={name}
                    onChange={(e) => handleFilterByProblemTypes(e.target.name)}
                  />
                  {name}
                </label>
              </MenuItem>
            ))}
          </MenuList>
        </Menu>
        <div className="flex flex-column gap-1">
          <Select value={sortBy} onChange={(value) => value && setSortBy(value)} label="Sort by" menuProps={{ className: "shadow-black shadow-sm" }}>
            <Option value="endDate">Advert end time</Option>
            <Option value="brand">Brand</Option>
            <Option value="manufactureYear">Manufacture year</Option>
            <Option value="model">Model</Option>
            <Option value="title">Title</Option>
          </Select>
          <IconButton className="text-xl" variant="outlined" onClick={handleSortDirectionChange}>
            {sortDirection ? <FaSortAmountDownAlt /> : <FaSortAmountUp />}
          </IconButton>
        </div>
      </div>
      </div>
      <div className="flex flex-col gap-8">
        {advertsList.map(advert => (
          <AdvertCard key={advert.id} advert={advert} />
        ))}
      </div>
    </>
  )
}

export default AdvertsList