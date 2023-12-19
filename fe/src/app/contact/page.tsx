"use client"
import React, { useState } from 'react';
import { Card, Input, Button, Typography } from "@/lib/materialTailwindExports";
import toast from "react-hot-toast";

const ContactPage = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    subject: '',
    message: '',
  });

  const handleFormSubmit = async () => {
    toast.success("Message sent successfully!");

    setTimeout(() => {
      setFormData({
        name: '',
        email: '',
        subject: '',
        message: '',
      });
    }, 2000);
  };

  return (
    <div className="bg-gray-800 text-white flex h-screen p-8 md:p-16">
      <main className="flex-1 py-12 text-center md:text-left">
        <h1 className="text-5xl md:text-7xl font-bold text-center mb-8 md:mb-12">Contact Us</h1>
        <div className="max-w-2xl mx-auto text-lg md:text-xl leading-relaxed">
          <p className="mb-8">
            We{'\''}re here to assist you! Feel free to reach out if you have any questions, suggestions, or just want to say hello.
          </p>
          <p className="mb-8">
            Connect with us through the following channels:
          </p>
          <ul className="list-none mb-8 pl-0">
            <li className="mb-6 flex items-center">
              <strong className="mr-4">Facebook:</strong>
              <a href="https://facebook.com/CarFans" className="text-blue-500 hover:underline">https://facebook.com/CarFans</a>
            </li>
            <li className="mb-6 flex items-center">
              <strong className="mr-4">Twitter:</strong>
              <a href="https://twitter.com/CarFans" className="text-blue-500 hover:underline">https://twitter.com/CarFans</a>
            </li>
            <li className="flex items-center">
              <strong className="mr-4">Phone:</strong>
              <span className="text-blue-500">866666666</span>
            </li>
            <li className="flex items-center ">
              <br />
              <span className="text-gray-500 mb-2">Or send us a message</span>
              <span className="ml-2 text-2xl block">&#10140;</span>
            </li>
          </ul>
        </div>
      </main>

      <Card color="white" className="p-8 md:p-16 rounded-lg w-full md:w-1/2 lg:w-1/3 ml-10">
        <Typography variant="h1" color="blue-gray" className="mb-8">Send us a Message</Typography>
        <form className="grid grid-cols-1 gap-8">
          <Input
            size="lg"
            placeholder="Your Name"
            className="!border-t-blue-gray-200 focus:!border-t-gray-900"
            crossOrigin=""
            value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
          />
          <Input
            size="lg"
            placeholder="Your Email"
            className="!border-t-blue-gray-200 focus:!border-t-gray-900"
            crossOrigin=""
            value={formData.email}
            onChange={(e) => setFormData({ ...formData, email: e.target.value })}
          />
          <Input
            size="lg"
            placeholder="Subject"
            className="!border-t-blue-gray-200 focus:!border-t-gray-900"
            crossOrigin=""
            value={formData.subject}
            onChange={(e) => setFormData({ ...formData, subject: e.target.value })}
          />
          <Input
            size="lg"
            placeholder="Your Message"
            className="!border-t-blue-gray-200 focus:!border-t-gray-900"
            crossOrigin=""
            value={formData.message}
            onChange={(e) => setFormData({ ...formData, message: e.target.value })}
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
