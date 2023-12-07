import PdfDropzone from "@/components/PdfDropzone/PdfDropzone";

const PdfForm = () => {
  return (
    <main className="text-center">
      <h2 className="text-3xl font-bold py-10 px-4">
        Upload your legal documents below
      </h2>
      <p className="text-gray-700">
        Only files in &ldquo;.pdf&ldquo; format are accepted*
      </p>
      <PdfDropzone />
    </main>
  );
};

export default PdfForm;
