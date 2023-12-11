"use client";

import { ReactNode, useEffect, useState } from "react";
import { Stepper as St, Step, Button } from "@/lib/materialTailwindExports";
import {
  FaCheck,
  HiOutlineDocumentText,
  IoDocumentAttachSharp,
} from "@/lib/reactIconsExports";
import InfoForm from "./InfoForm/InfoForm";
import PdfForm from "./PdfForm";
import { BASE_API_URL, SERVICE_STATUS_ROUTE } from "@/utils/urls";
import { getToken } from "@/utils/getToken";
import { ServiceStatus } from "@/utils/constants";

const Stepper = ({ children }: { children: ReactNode }) => {
  const [activeStep, setActiveStep] = useState(-1);
  const [isLastStep, setIsLastStep] = useState(false);
  const [isFirstStep, setIsFirstStep] = useState(false);
  const [isForwardButtonDisabled, setIsForwardButtonDisabled] = useState(false);

  const handleNext = () => !isLastStep && setActiveStep((cur) => cur + 1);
  const handlePrev = () => !isFirstStep && setActiveStep((cur) => cur - 1);

  useEffect(() => {
    getServiceStatus();
  }, []);

  const getServiceStatus = async () => {
    const token = await getToken();
    const response = await fetch(`${BASE_API_URL}${SERVICE_STATUS_ROUTE}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });
    const serviceStatusObject = await response.json();
    const serviceStatus = serviceStatusObject.status;
    if (
      serviceStatus === ServiceStatus[ServiceStatus.Exists] ||
      serviceStatus === ServiceStatus[ServiceStatus.Denied]
    ) {
      setIsForwardButtonDisabled(true);
      setActiveStep(0);
    } else if (
      serviceStatus === ServiceStatus[ServiceStatus.CreatedInDataBase]
    ) {
      setActiveStep(1);
    } else if (serviceStatus === ServiceStatus[ServiceStatus.Pending]) {
      setActiveStep(2);
    }
  };
  return (
    <>
      <St
        className="z-0"
        activeStep={activeStep}
        isLastStep={(value) => setIsLastStep(value)}
        isFirstStep={(value) => setIsFirstStep(value)}
      >
        <Step>
          <HiOutlineDocumentText />
        </Step>
        <Step>
          <IoDocumentAttachSharp />
        </Step>
        <Step>
          <FaCheck />
        </Step>
      </St>
      <div className="shadow-lg flex flex-col justify-center rounded-xl my-5 p-4 min-h-">
        {activeStep == -1 && (
          <h2 className="text-3xl font-bold py-10 px-4 text-center">
            Loading...
          </h2>
        )}
        {activeStep == 0 && (
          <InfoForm
            isForwardButtonDisabled={isForwardButtonDisabled}
            setIsForwardButtonDisabled={setIsForwardButtonDisabled}
          />
        )}
        {activeStep == 1 && (
          <PdfForm setIsForwardButtonDisabled={setIsForwardButtonDisabled} />
        )}
        {activeStep == 2 && children}
      </div>
      {activeStep !== 2 && activeStep !== -1 && (
        <div className="mt-8 flex justify-end gap-2">
          <Button onClick={handlePrev} disabled={isFirstStep}>
            Prev
          </Button>
          <Button
            onClick={handleNext}
            disabled={isLastStep || isForwardButtonDisabled}
          >
            Next
          </Button>
        </div>
      )}
    </>
  );
};

export default Stepper;
