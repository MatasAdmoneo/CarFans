"use client";

import { Button } from "@/lib/materialTailwindExports";
import { FormEvent, useState } from "react";
import { useDropzone, FileWithPath } from "react-dropzone";
import { uploadPdfToApi } from "./uploadPdfToApi";
import toast from "react-hot-toast/headless";
import { useRouter } from "next/navigation";

// TODO: not allow this page access if submission status is pending
function PdfDropzone() {
  const maxFileSize = 10485760;
  const [uploading, setUploading] = useState(false);
  const [successUpload, setSuccessUpload] = useState(false);

  const {
    acceptedFiles,
    getRootProps,
    getInputProps,
    isDragActive,
    isDragReject,
    fileRejections,
  } = useDropzone({
    accept: {
      "text/pdf": [".pdf"],
    },
    maxFiles: 1,
    maxSize: maxFileSize,
  });
  const router = useRouter();

  const acceptedFileItems = acceptedFiles.map((file: FileWithPath) => (
    <li key={file.path}>
      {file.path} - {file.size} bytes
    </li>
  ));

  const fileRejectionItems = fileRejections.map(({ file, errors }) =>
    errors.map((e) => (
      <p className="text-red-400" key={e.code}>
        {e.message}
      </p>
    ))
  );

  const handlePdfUpload = async (e: FormEvent) => {
    e.preventDefault();
    if (acceptedFiles.length === 0) {
      toast.error("Please select the file to upload.");
      return;
    }
    setUploading(true);
    await uploadPdfToApi(acceptedFiles[0], setSuccessUpload);
    setUploading(false);
    router.push(`verify/success`);
  };

  return (
    <section className="">
      <div
        className="bg-gray-200 w-3/4 h-[15vh] flex rounded-md justify-center items-center mx-auto border-gray-300 border-dashed border-4 hover:cursor-pointer mb-5"
        {...getRootProps()}
      >
        <input {...getInputProps()} />
        {isDragActive ? (
          <p>Release left mouse button</p>
        ) : (
          <p>Drag and drop file here or click to browse</p>
        )}
      </div>
      <div>
        {isDragReject && (
          <p className="text-red-500">
            File type not accepted. Please upload .pdf file.
          </p>
        )}
        {fileRejectionItems}
      </div>
      <aside>
        {acceptedFiles.length > 0 && (
          <h3 className="text-xl">Uploaded Files</h3>
        )}
        <ul>{acceptedFileItems}</ul>
      </aside>
      <Button
        type="submit"
        color="blue"
        className="mt-5"
        disabled={fileRejections.length > 0 || acceptedFiles.length == 0}
        onClick={(e) => handlePdfUpload(e)}
      >
        {uploading ? "Submitting" : "Submit"}
      </Button>
    </section>
  );
}

export default PdfDropzone;
