import React from "react";
import Link from "next/link";

function Footer() {
  return (
    <footer className="py-6 text-center bg-gray-900 text-white">
      <div className="flex justify-center">
        <Link href="/privacypolicy" className="flex items-center">
            Privacy Policy
        </Link>
      </div>
      <p> </p>
      <p>&copy; 2023 CarFans</p>
    </footer>
  );
}

export default Footer;
