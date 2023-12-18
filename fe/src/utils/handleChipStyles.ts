import { JobStatusAsString } from "./constants";

export const handleChipColor = (status: string) => {
  switch (status) {
    case JobStatusAsString.IN_PROGRESS:
      return "amber";
    case JobStatusAsString.DONE:
      return "green";
    case JobStatusAsString.CANCELLED:
      return "red";
    default:
      return "gray";
  }
};

export const handleChipIconColor = (status: string) => {
  switch (status) {
    case JobStatusAsString.IN_PROGRESS:
      return "#FF6600";
    case JobStatusAsString.DONE:
      return "#1B5E20";
    case JobStatusAsString.CANCELLED:
      return "#B71C1C";
    default:
      return "#212121";
  }
};
