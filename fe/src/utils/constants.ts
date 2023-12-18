export const FORM_BOX_SHADOW = `
  0px 0px 1.5px rgba(0, 0, 0, -0.031),
  0px 0px 3.9px rgba(0, 0, 0, -0.069),
  0px 0px 8px rgba(0, 0, 0, 0.014),
  0px 0px 16.4px rgba(0, 0, 0, 0.102),
  0px 0px 45px rgba(0, 0, 0, 0.17)
`;

export const CarProblemsCategories = [
  "Engine",
  "Chassis",
  "Body",
  "Tyres",
  "Maintenance",
  "Other",
  "Unknown",
];

export enum ChipTooltipText {
  Brand,
  Model,
  ManufactureYear,
  ProblemType,
}

export enum JobStatus {
  Pending,
  Accepted,
  InProgress,
  Done,
  Cancelled,
  Declined,
}
export enum ServiceStatus {
  Exists,
  CreatedInDataBase,
  Pending,
  Accepted,
  Denied,
}

export const DateSelectInputClassNames =
  "relative transition-all duration-300 py-2.5 pl-4 pr-14 w-full border border-blue-gray-200 rounded-lg tracking-wide font-light text-sm placeholder-blue-gray-800 focus:ring focus:border-blue-500 focus:ring-blue-500/20";
