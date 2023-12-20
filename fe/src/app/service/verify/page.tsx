import Stepper from "./components/Stepper";
import Success from "./components/Sucess";

function ServiceVerifyPage() {
  return (
    <div className="flex justify-center">
      <div className=" md:w-3/4 py-6 px-8 ">
        <Stepper>
          <Success />
        </Stepper>
      </div>
    </div>
  );
}

export default ServiceVerifyPage;
