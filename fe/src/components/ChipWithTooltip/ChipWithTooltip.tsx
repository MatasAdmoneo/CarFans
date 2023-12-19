"use client"
import { ChipTooltipText } from "@/utils/constants";
import { Tooltip, Chip } from "@material-tailwind/react";

type ChipWithTooltipType = {
  tooltipText: ChipTooltipText;
  value: string;
};

const ChipWithTooltip = ({ tooltipText, value }: ChipWithTooltipType) => {
  const getTooltipText = () => {
    switch (tooltipText) {
      case ChipTooltipText.Brand:
        return "Brand"
      case ChipTooltipText.Model:
        return "Model"
      case ChipTooltipText.ProblemType:
        return "Problem type"
      case ChipTooltipText.ManufactureYear:
        return "Manufacture year"
      default:
        return "Unknown"
    }
  }

  return (
    <Tooltip content={getTooltipText()}>
      <Chip size="lg" variant="gradient" className="rounded-full chip-opacity" value={value} />
    </Tooltip>
  )
}

export default ChipWithTooltip;