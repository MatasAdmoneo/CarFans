import { getAccessToken } from "@auth0/nextjs-auth0";
import Stepper from "./components/Stepper";
import Success from "./components/Sucess";
import { BASE_API_URL, SERVICE_ADDITIONAL_INFO_ROUTE, SERVICE_STATUS_ROUTE } from "@/utils/urls";

const getServiceInfo = async () => {
  const { accessToken } = await getAccessToken();
  const serviceInfo = await fetch(`${BASE_API_URL}${SERVICE_ADDITIONAL_INFO_ROUTE}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${accessToken}`,
    },
  }).then(res => res.json());
  
  if (serviceInfo && Object.keys(serviceInfo).length !== 0) {
    return serviceInfo;
  }
  return null;
};

const getServiceStatus = async () => {
  const { accessToken } = await getAccessToken();
  const response = await fetch(`${BASE_API_URL}${SERVICE_STATUS_ROUTE}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${accessToken}`,
    },
  });
  return response.json().then(data => data.status);
}

async function ServiceVerifyPage() {
  const { accessToken } = await getAccessToken();
  const service = await getServiceInfo();
  const status = await getServiceStatus();

  return (
    <div className="flex justify-center">
      <div className=" md:w-3/4 py-6 px-8 ">
        <Stepper service={service} status={status} token={accessToken ?? ''}>
          <Success />
        </Stepper>
      </div>
    </div>
  );
}

export default ServiceVerifyPage;
