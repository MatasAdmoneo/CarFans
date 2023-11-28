"use server";
import { getAccessToken } from "@auth0/nextjs-auth0";

export const getToken = async () => {
  const { accessToken } = await getAccessToken();
  if (accessToken) return accessToken;
  return null;
};
