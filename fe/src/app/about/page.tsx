import React from 'react';
import Image from 'next/image';

const AboutUsPage = () => {
  return (
    <div className="bg-gray-800 text-white">
      <main className="py-15 text-center">
        <div className="relative overflow-hidden w-full h-96 rounded-lg shadow-lg mb-8">
            <Image
              src="/public/images/about-us.jpg"
              alt="About Us Image"
              layout="fill"
              className="rounded-lg transition-transform transform hover:scale-105 duration-300"
              style={{ filter: 'brightness(80%)' }}
            />
          <div className="absolute top-0 left-0 w-full h-full bg-black opacity-40"></div>
          <h1 className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 text-5xl font-bold text-white z-10">
            About Us
          </h1>
        </div>
        <div className="max-w-3xl mx-auto text-xl leading-relaxed">
          <p className="mb-6">
            Currently   , in the car repair market, drivers waste time searching for suitable auto repair services, and communication between drivers and service centers can lead to misunderstandings and potential loss of customers.
          </p>
          <p className="mb-6">
            Our system simplifies the car registration process, providing drivers with easily understandable information about repair services. We supply service centers with the necessary information, enabling them to serve customers more quickly and efficiently. Our goal is to optimally align the needs of drivers and service centers, ensuring a smooth and effective collaboration.
          </p>
        </div>
      </main>
      <section className="py-8 text-center bg-gray-700 text-white">
        <h2 className="text-3xl font-bold">Contact Us</h2>
        <p className="mt-4 text-lg">
          Have questions or want to get in touch? Visit our{' '}
          <a href="/contact" className="text-blue-500 hover:underline">
            Contact Page
          </a>
          .
        </p>
      </section>
    </div>
  );
};

export default AboutUsPage;
