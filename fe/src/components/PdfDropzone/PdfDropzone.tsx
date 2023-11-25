"use client";

import { Button } from "@/lib/materialTailwindExports";
import { getToken } from "@/utils/getToken";
import { BASE_API_URL, SERVICE_DOCUMENT_UPLOAD_ROUTE } from "@/utils/urls";
import { type } from "os";
import { FormEvent, useState } from "react";
import { useDropzone, FileWithPath } from "react-dropzone";

function PdfDropzone() {
  const maxFileSize = 10485760;
  const [uploading, setUploading] = useState(false);
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
    if (acceptedFiles.length === 0) return;
    setUploading(true);
    await uploadPdfToApi(acceptedFiles[0]);
    setUploading(false);
    console.log("uploaded");
  };

  const uploadPdfToApi = async (file: File) => {
    const accessToken = await getToken();
    console.log("Token: ", accessToken);

    const reader = new FileReader();

    reader.onload = async () => {
      const base64Content = reader.result?.toString().split(",")[1]; // Extract base64 content from data URL
      if (base64Content) {
        try {
          const res = await fetch(
            `${BASE_API_URL}${SERVICE_DOCUMENT_UPLOAD_ROUTE}`,
            {
              headers: {
                Authorization: `Bearer ${accessToken}`,
                "Content-Type": "application/json",
              },
              method: "POST",
              body: JSON.stringify(base64Content), // Send base64 content in the request body
            }
          );

          if (!res.ok) {
            const json = await res.json();
            throw new Error(
              json.message || res.statusText || "Unknown error occurred."
            );
          }

          console.log("File uploaded successfully");
        } catch (error) {
          console.error("Failed to upload file", error);
        }
      }
    };

    reader.onerror = () => {
      console.error("Error reading file");
    };

    reader.readAsDataURL(file);
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
        Submit
      </Button>
    </section>
  );
}

export default PdfDropzone;
