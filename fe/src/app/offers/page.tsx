import AdvertCard from "@/components/AdvertCard/AdvertCard";
import { Breadcrumbs, Card, Typography } from "@/lib/materialTailwindExports";
import { UserAdvertType } from "@/types/AdvertType";
import { BASE_API_URL, USER_ADVERTS_ROUTE } from "@/utils/urls";
import { getAccessToken } from "@auth0/nextjs-auth0";
import Link from "next/link";

const getUserAdvertsData = async () => {
  const { accessToken } = await getAccessToken();
  if (!accessToken) {
    throw new Error(`Requires authorization`);
  }
  const res = await fetch(`${BASE_API_URL}${USER_ADVERTS_ROUTE}`, {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
  if (!res.ok) {
    return null;
  }
  return res.json();
};

export default async function MyOffersPage() {
  const userAdverts = await getUserAdvertsData();

  if (!userAdverts) {
    return "Something went wrong";
  }

  return (
    <div className="max-w-3xl mx-auto my-5 py-10 px-5">
      <Breadcrumbs>
        <Link href="/home" className="opacity-60">
          <span>Home</span>
        </Link>
        <Link href="#">
          <span>My Adverts</span>
        </Link>
      </Breadcrumbs>
      <Typography variant="h3">My Adverts</Typography>
      {userAdverts.length !== 0 ? (
        userAdverts.map((advert: UserAdvertType) => (
          <AdvertCard advert={advert} key={advert.id} />
        ))
      ) : (
        <Card className="h-40 mt-5 flex justify-center">
          <h2 className="text-3xl font-bold text-center">
            You have no adverts created
          </h2>
        </Card>
      )}
    </div>
  );
}
