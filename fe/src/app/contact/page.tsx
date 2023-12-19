"use client";
import React, { useState } from "react";
import { Card, Input, Button, Typography } from "@/lib/materialTailwindExports";
import toast from "react-hot-toast";
import Link from "next/link";

const ContactPage = () => {
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    subject: "",
    message: "",
  });

  const handleFormSubmit = () => {
    setFormData({
      name: "",
      email: "",
      subject: "",
      message: "",
    });

    toast.success("Message sent successfully!");
  };

  return (
    <div className="bg-gray-800 text-white flex h-screen p-8 md:p-16">
      <main className="flex-1 py-12 text-center md:text-left">
        <h1 className="text-5xl md:text-7xl font-bold text-center mb-8 md:mb-12">
          Contact Us
        </h1>
        <div className="max-w-2xl mx-auto text-lg md:text-xl leading-relaxed">
          <p className="mb-8">
            We{"'"}re here to assist you! Feel free to reach out if you have any
            questions, suggestions, or just want to say hello.
          </p>
          <p className="mb-8">
            Connect with us through the following channels:
          </p>
          <ul className="list-none mb-8 pl-0">
            <li className="mb-6 flex items-center">
              <strong className="mr-4">Facebook:</strong>
              <Link
                href="https://facebook.com/CarFans"
                className="text-blue-500 hover:underline"
              >
                https://facebook.com/CarFans
              </Link>
            </li>
            <li className="mb-6 flex items-center">
              <strong className="mr-4">Twitter:</strong>
              <Link
                href="https://twitter.com/CarFans"
                className="text-blue-500 hover:underline"
              >
                https://twitter.com/CarFans
              </Link>
            </li>
            <li className="flex items-center">
              <strong className="mr-4">Phone:</strong>
              <span className="text-blue-500">866666666</span>
            </li>
            <li className="flex items-center ">
              <br />
              <h2 className="text-2xl md:text-5xl font-bold text-center mt-7">
                Or send us a message &#10140;
              </h2>
              <span className="ml-2 text-2xl block"></span>
            </li>
          </ul>
        </div>
      </main>

      <Card
        color="white"
        className="p-8 md:p-16 rounded-lg w-full md:w-1/2 lg:w-1/3 ml-10"
      >
        <Typography variant="h1" color="blue-gray" className="mb-8">
          Send us a Message
        </Typography>
        <form className="grid grid-cols-1 gap-8">
          <Input
            size="lg"
            color="blue-gray"
            label="Your name"
            name="name"
            crossOrigin=""
            value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
          />
          <Input
            size="lg"
            color="blue-gray"
            label="Your email"
            name="email"
            crossOrigin=""
            value={formData.email}
            onChange={(e) =>
              setFormData({ ...formData, email: e.target.value })
            }
          />
          <Input
            size="lg"
            color="blue-gray"
            label="Subject"
            name="subject"
            crossOrigin=""
            value={formData.subject}
            onChange={(e) =>
              setFormData({ ...formData, subject: e.target.value })
            }
          />
          <Input
            size="lg"
            color="blue-gray"
            label="Your Message"
            name="message"
            crossOrigin=""
            value={formData.message}
            onChange={(e) =>
              setFormData({ ...formData, message: e.target.value })
            }
          />
          <Button
            type="button"
            size="lg"
            className="rounded"
            variant="gradient"
            onClick={handleFormSubmit}
          >
            Send Message
          </Button>
        </form>
      </Card>
    </div>
  );
};

export default ContactPage;
