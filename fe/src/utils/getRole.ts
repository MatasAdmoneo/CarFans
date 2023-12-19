"use server";
import { getSession } from "@auth0/nextjs-auth0/edge";

export const getRole = async () => {
  const { user }: any = await getSession();
  return user;
};
