"use client";
import { useRouter } from "next/navigation";
import {
  Navbar as Nav,
  Typography,
  Button,
} from "@/lib/materialTailwindExports";
import Link from "next/link";
import { useUser } from "@auth0/nextjs-auth0/client";
import { useMemo } from "react";

const ROLES_SELECTOR = "https://CarFans.com/roles";
const SERIVCE = "Service";

const Navbar = () => {
  const { user, isLoading } = useUser();
  const router = useRouter();
  const userRoles = useMemo(() => user ? user[ROLES_SELECTOR] as string[] : [], [user]);

  const navigateToLogin = () => {
    router.push("/api/auth/login");
  };

  const navigateToLogout = () => {
    router.push("/api/auth/logout");
  };

  const navList = (
    <ul className="mt-2 mb-4 flex flex-col gap-2 lg:mb-0 lg:mt-0 lg:flex-row lg:items-center lg:gap-6">
      {user && (userRoles.includes(SERIVCE) ? (
        <>
          <Typography
            as="li"
            variant="small"
            color="blue-gray"
            className="p-1 font-normal"
          >
            <Link href="/jobs" className="flex items-center">
              Jobs
            </Link>
          </Typography>
          <Typography
            as="li"
            variant="small"
            color="blue-gray"
            className="p-1 font-normal"
          >
            <Link href="/statuses" className="flex items-center">
              Statuses
            </Link>
          </Typography>
        </>
      ) : (
        <>
          <Typography
            as="li"
            variant="small"
            color="blue-gray"
            className="p-1 font-normal"
          >
            <Link href="/advert" className="flex items-center">
              Create advert
            </Link>
          </Typography>
          <Typography
            as="li"
            variant="small"
            color="blue-gray"
            className="p-1 font-normal"
          >
            <Link href="/list" className="flex items-center">
              My Adverts
            </Link>
          </Typography>
        </>
      ))}
    </ul>
  );

  return (
    <Nav className="sticky top-0 z-10 h-max max-w-full rounded-none px-4 py-2 lg:px-8 lg:py-4">
      <div className="flex items-center justify-between text-blue-gray-900">
        <Typography className="mr-4 cursor-pointer py-1.5 font-medium">
          <Link href="/home">Car Fans</Link>
        </Typography>
        <div className="flex items-center gap-4">
          <div className="mr-4 hidden lg:block">{navList}</div>
          <div className="flex items-center gap-x-1">
            {!isLoading && (userRoles.length ? (
              <Button
                variant="gradient"
                size="sm"
                className="hidden lg:inline-block"
                onClick={() => navigateToLogout()}
              >
                Log out
              </Button>
            ) : (
              <>
                <Button
                  variant="text"
                  size="sm"
                  className="hidden lg:inline-block"
                  onClick={() => navigateToLogin()}
                >
                  Log In
                </Button>
                <Button
                  variant="text"
                  size="sm"
                  className="hidden lg:inline-block"
                  onClick={() => navigateToLogin()}
                >
                  Sign In
                </Button>
              </>
            ))}
          </div>
        </div>
      </div>
    </Nav>
  );
};

export default Navbar;