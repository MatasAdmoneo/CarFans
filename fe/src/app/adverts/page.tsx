import AdvertCard from "@/app/adverts/components/AdvertCard"
import { Typography } from "@/lib/materialTailwindExports"
import { toast } from "@/lib/reactHotToastExports"
import { getToken } from "@/utils/getToken"
import { BASE_API_URL, SERVICE_ADVERTRS_ROUTE } from "@/utils/urls"
import AdvertsList from "./components/AdvertsList"

async function getAdvertsList() {
  const token = await getToken();
  const response = await fetch(`${BASE_API_URL}${SERVICE_ADVERTRS_ROUTE}`, {
    headers: { Authorization: `Bearer ${token}` }
  });

  if (!response.ok) {
    toast.error("Failed to fetch data");
    return;
  }

  return response.json();
}

async function AdvertsPage() {
  const adverts = await getAdvertsList();

  return (
    <div className="max-w-3xl mx-auto my-5 py-10 px-5">
      <div>
        <Typography variant="h2">Adverts</Typography>
      </div>
      <AdvertsList adverts={adverts} />
    </div>
  )
}

export default AdvertsPage;