import { BASE_API_URL, USER_JOBS_ROUTE } from "@/utils/urls";
import { getAccessToken } from "@auth0/nextjs-auth0";
import { Breadcrumbs } from "@/lib/materialTailwindExports";
import Link from "next/link";
import MyOffersList from "./components/MyOffersList";

const getAdvertJobs = async (advertId: string) => {
  const { accessToken } = await getAccessToken();
  if (!accessToken) {
    throw new Error(`Requires authorization`);
  }
  const res = await fetch(
    `${BASE_API_URL}${USER_JOBS_ROUTE}/${advertId}`,
    {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    }
  );
  if (!res.ok) {
    return null;
  }
  return res.json();
};

export default async function AdvertJobs({ params: { advertId } }: { params: { advertId: string }}) {
  const advertsJobs = await getAdvertJobs(advertId);

  return (
    <div className="max-w-3xl mx-auto my-5 py-10 px-5">
      <Breadcrumbs>
        <Link href="/home" className="opacity-60">
          <span>Home</span>
        </Link>
        <Link href="/list" className="opacity-60">
          <span>My Adverts</span>
        </Link>
        <Link href="#">
          <span>Offers</span>
        </Link>
      </Breadcrumbs>
      <MyOffersList jobs={advertsJobs} />
    </div>
  )
}