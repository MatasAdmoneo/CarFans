"use client";

import { ReactNode, useState } from "react";
import { Stepper as St, Step, Button } from "@/lib/materialTailwindExports";
import {
  FaCheck,
  HiOutlineDocumentText,
  IoDocumentAttachSharp,
} from "@/lib/reactIconsExports";
import InfoForm from "./InfoForm/InfoForm";
import PdfForm from "./PdfForm";

const Stepper = ({ children }: { children: ReactNode }) => {
  const [activeStep, setActiveStep] = useState(0);
  const [isLastStep, setIsLastStep] = useState(false);
  const [isFirstStep, setIsFirstStep] = useState(false);

  const handleNext = () => !isLastStep && setActiveStep((cur) => cur + 1);
  const handlePrev = () => !isFirstStep && setActiveStep((cur) => cur - 1);
  return (
    <>
      <St
        className="z-0"
        activeStep={activeStep}
        isLastStep={(value) => setIsLastStep(value)}
        isFirstStep={(value) => setIsFirstStep(value)}
      >
        <Step onClick={() => setActiveStep(0)}>
          <HiOutlineDocumentText />
        </Step>
        <Step onClick={() => setActiveStep(1)}>
          <IoDocumentAttachSharp />
        </Step>
        <Step onClick={() => setActiveStep(2)}>
          <FaCheck />
        </Step>
      </St>
      <div className="shadow-lg flex flex-col justify-center rounded-xl my-5 p-4 min-h-">
        {activeStep == 0 && <InfoForm />}
        {activeStep == 1 && <PdfForm />}
        {activeStep == 2 && children}
      </div>
      <div className="mt-8 flex justify-end gap-2">
        <Button onClick={handlePrev} disabled={isFirstStep}>
          Prev
        </Button>
        <Button onClick={handleNext} disabled={isLastStep}>
          Next
        </Button>
      </div>
    </>
  );
};

export default Stepper;
