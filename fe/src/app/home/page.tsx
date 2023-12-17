import Link from "next/link";

function HomePage() {
  return (
    <main>
      <section className="py-20 text-center bg-gray-700 text-white">
        <h1 className="text-5xl font-bold">Welcome to CarFans!</h1>
        <p className="mt-4 text-xl">
          Register your vehicle&apos;s problem and find a solution in no time
        </p>

        <div className="flex justify-center mt-8">
          <Link
            href="/advert"
            className="bg-red-500 text-white px-8 py-4 rounded-full text-2xl font-bold"
          >
            Register Problem
          </Link>
        </div>
      </section>

      <section className="py-6 text-center bg-gray-800 text-white">
        <h2 className="text-3xl font-bold">How it works?</h2>
        <p className="mt-4 text-xl">
          I. Register your vehicle&apos;s problem with 3 easy steps
        </p>
        <p className="mt-4 text-xl">
          II. Get a list of offers from certified car services
        </p>
        <p className="mt-4 text-xl">
          III. Choose the most appealing proposal to you
        </p>
      </section>

      <section className="py-20 text-center bg-gray-700 text-white">
        <h2 className="text-3xl font-bold">What are the benefits?</h2>
        <p className="mt-4 text-xl">
          I. Choose highly rated service or the one with the most appealing
          price for your problem
        </p>
        <p className="mt-4 text-xl">II. Track your repair status</p>
        <p className="mt-4 text-xl">
          III. Read reviews from other drivers about each service
        </p>
      </section>
    </main>
  );
}

export default HomePage;
