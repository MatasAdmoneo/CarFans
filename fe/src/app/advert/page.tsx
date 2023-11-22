
const page = () => {
  return (
    <div className="">
      <div className="flex flex-col gap-5 max-w-2xl m-auto py-10">
        <h3 className="text-lg leading-6 font-medium text-gray-900" id="modal-headline">
          Have you noticed strange sounds coming from your car?
        </h3>
        <div className="flex justify-evenly">
          <div className="flex gap-2">
            <input type="radio" name="sound" aria-label="Yes" value="Yes" />
            <label htmlFor="sound">Yes</label>
          </div>
          <div className="flex gap-2">
            <input type="radio" name="sound" value="No" />
            <label htmlFor="sound">No</label>
          </div>
        </div>
        <h3 className="text-lg leading-6 font-medium text-gray-900" id="modal-headline">
          Please leave additional comments below.
        </h3>
        <textarea id="message" rows={4} className="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Your message..."></textarea>
      </div>
    </div>
  )
}

export default page;