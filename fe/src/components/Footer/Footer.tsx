import React from "react";
import Link from "next/link";

function Footer() {
  return (
    <footer className="py-6 bg-gray-900 text-white flex-col text-center">
      <Link href="/privacypolicy" className="hover:underline">
        Privacy Policy
      </Link>
    
    <p>&copy; 2023 CarFans</p>
  </footer>
  );
}

export default Footer;
