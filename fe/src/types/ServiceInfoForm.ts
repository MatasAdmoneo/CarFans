export type ServiceInfoForm = {
  serviceName: string;
  address: string;
  city: string;
  weeklyWorkingHours: {
    dayOfWeek: number;
    startTime: string;
    endTime: string;
    lunchBreakStartTime: string;
    lunchBreakEndTime: string;
  }[];
  contactPhone: string;
  description: string;
};

export type WorkingDay = {
  dayOfWeek: number;
  startTime: string;
  endTime: string;
  lunchBreakStartTime: string;
  lunchBreakEndTime: string;
};
