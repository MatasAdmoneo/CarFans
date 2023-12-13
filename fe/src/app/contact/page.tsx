import {
  Card,
  Input,
  Button,
  Typography,
} from "@/lib/materialTailwindExports";

const ContactPage = () => {
  return (
    <div className="bg-gray-800 text-white flex h-screen p-8 md:p-16">
      <main className="flex-1 py-12 text-center">
        <h1 className="text-7xl md:text-7xl font-bold mb-12">Contact Us</h1>
        <div className="max-w-2xl mx-auto text-xl md:text-2xl leading-relaxed">
          <p className="mb-12">
            We{'\''}re here to assist you! Feel free to reach out if you have any questions, suggestions, or just want to say hello.
          </p>
          <p className="mb-12">
            Connect with us through the following channels:
          </p>
          <ul className="list-disc list-inside mb-8">
            <li>Email: <a href="mailto:info@example.com" className="text-blue-500 hover:underline">CarFans@example.com</a></li>
            <li>
              Phone: <span className="text-blue-500">866666666</span>
              <br />
              <span className="block ml-2 text-gray-400">Or send us a message</span>
              <span className="inline-block ml-2 transform translate-y-0.5 text-3xl">&#10140;</span>
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
            className=" !border-t-blue-gray-200 focus:!border-t-gray-900"
            crossOrigin=""
          />
          <Input
            size="lg"
            placeholder="Your Email"
            className=" !border-t-blue-gray-200 focus:!border-t-gray-900"
            crossOrigin=""
          />
          <Input
            size="lg"
            placeholder="Subject"
            className=" !border-t-blue-gray-200 focus:!border-t-gray-900"
            crossOrigin=""
          />
          <Input
            size="lg"
            placeholder="Your Message"
            className=" !border-t-blue-gray-200 focus:!border-t-gray-900"
            crossOrigin=""
          />
          <Button
            type="button"
            color="blue"
            size="lg"
            className="rounded"
          >
            Send Message
          </Button>
        </form>
      </Card>
    </div>
  );
};

export default ContactPage;