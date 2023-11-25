import { getAccessToken } from "@auth0/nextjs-auth0/edge";
import { withMiddlewareAuthRequired } from "@auth0/nextjs-auth0/edge";
import type { NextRequest } from "next/server";
import { NextResponse } from "next/server";

export default withMiddlewareAuthRequired(async function middleware(
  request: NextRequest
) {
  const { accessToken } = await getAccessToken();
  console.log(accessToken)
  if (accessToken) {
    const requestHeaders = new Headers(request.headers);

    // console.log("HEADERSSSSSSSSSSSSS: ", request.headers);
    // requestHeaders.set("Authorization", `Bearer ${accessToken}`);
    // console.log(requestHeaders);
    const response = NextResponse.next({
      request: {
        headers: requestHeaders,
      },
    });
    return response;
  }
  return;
});

export const config = {
  matcher: ["/dashboard", "/advert"],
};
