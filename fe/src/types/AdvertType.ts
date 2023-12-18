export type AdvertType = {
  id: string;
  title: string;
  endDate: Date;
  isQuestionsFormType: boolean;
  isSoundBad?: boolean;
  isScentBad?: boolean;
  isPanelInvalid?: boolean;
  photos: Array<string>;
  isLeakedLiquids?: boolean;
  isUnstableCar?: boolean;
  description: string;
  brand: string;
  model: string;
  manufactureYear: number;
  problemType: string;
  createdDate: Date;
};

export type UserAdvertType = {
  id: string;
  title: string;
  endDate: Date;
  description: string;
  brand: string;
  model: string;
  manufactureYear: number;
  problemType: string;
  isOfferAccepted: boolean;
};