import { getToken } from "@/utils/getToken"
import { BASE_API_URL, SERVICE_ADVERTRS_ROUTE } from "@/utils/urls"
import AdvertsList from "./components/AdvertsList"

async function getAdvertsList() {
  const token = await getToken();
  const response = await fetch(`${BASE_API_URL}${SERVICE_ADVERTRS_ROUTE}`, {
    headers: { Authorization: `Bearer ${token}` }
  });

  if (!response.ok) {
    return null;
  }

  return response.json();
}

export default async function Page() {
  const adverts = await getAdvertsList();

  if (!adverts) {
    return "Not found";
  }

  return (
    <div className="max-w-3xl mx-auto my-5 py-10 px-5">
      <AdvertsList adverts={adverts} />
    </div>
  )
}