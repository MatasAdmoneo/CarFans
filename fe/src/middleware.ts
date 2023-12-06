import { withMiddlewareAuthRequired } from "@auth0/nextjs-auth0/edge";
import { NextRequest, NextResponse } from "next/server";
import { getRole } from "./utils/getRole";
export default withMiddlewareAuthRequired(async function middleware(req) {
  const user = await getRole();

  const roles = user["https://CarFans.com/roles"];
  // service only routes
  if (req.nextUrl.pathname.startsWith("/service")) {
    if (roles.length === 0 || !roles.includes("Service")) {
      return NextResponse.redirect(req.nextUrl.origin);
    }
  }

  // client only routes
  if (req.nextUrl.pathname.startsWith("/advert")) {
    if (roles.length === 0 || !roles.includes("User")) {
      return NextResponse.redirect(req.nextUrl.origin);
    }
  }
  return NextResponse.next();
});

export const config = {
  matcher: ["/dashboard", "/service/:path*"],
};
