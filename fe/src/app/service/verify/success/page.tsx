import { getSession } from "@auth0/nextjs-auth0";
//TODO: disable this page access if service is accepted or didn't submit documents
async function PdfUploadSuccesspage() {
  const { user }: any = await getSession();
  return (
    <main className="text-center">
      <h2 className="text-3xl font-bold py-10 px-4">
        Documents successfully uploaded ✅
      </h2>
      <p className="text-gray-900">
        Your application will be reviewed soon. Verdict will be sent via email{" "}
        <span className="text-blue-600">{user.email}</span>
      </p>
    </main>
  );
}

export default PdfUploadSuccesspage;
