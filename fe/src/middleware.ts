import { withMiddlewareAuthRequired } from "@auth0/nextjs-auth0/edge";
import { NextResponse } from "next/server";
import { getRole } from "./utils/getRole";
import { getServiceStatus } from "./utils/getServiceStatus";
import { ServiceStatus } from "./utils/constants";

export default withMiddlewareAuthRequired(async function middleware(req) {
  const user = await getRole();
  const roles = user["https://CarFans.com/roles"];

  const serviceRoutes = ["/service", "/jobs", "/dashboard"];
  const driverRoutes = ["/advert"];

  if (serviceRoutes.some((path) => req.nextUrl.pathname.startsWith(path))) {
    if (roles.length === 0 || !roles.includes("Service")) {
      return NextResponse.redirect(req.nextUrl.origin);
    }
    const serviceStatus = await getServiceStatus();
    if (req.nextUrl.pathname === "/service/verify") {
      if (serviceStatus === ServiceStatus[ServiceStatus.Accepted]) {
        return NextResponse.redirect("https://localhost:3000/jobs");
      }
    }
    if (
      serviceStatus !== ServiceStatus[ServiceStatus.Accepted] &&
      req.nextUrl.pathname !== "/service/verify"
    ) {
      return NextResponse.redirect("https://localhost:3000/service/verify");
    }
  }

  // client only routes
  if (driverRoutes.some((path) => req.nextUrl.pathname.startsWith(path))) {
    if (roles.length === 0 || !roles.includes("User")) {
      return NextResponse.redirect("https://localhost:3000/jobs");
    }
  }
  return NextResponse.next();
});

export const config = {
  matcher: ["/dashboard", "/service/:path*", "/jobs/:path*", "/advert", "/offers/:path*"],
};
