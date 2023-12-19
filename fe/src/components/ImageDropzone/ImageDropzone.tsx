"use client";

import { useEffect } from "react";
import toast from "react-hot-toast";
import { useDropzone, FileWithPath } from "react-dropzone";
import { readUploadedFile } from "./utils";

type ImageDropzoneType = {
  onUpload: (data: string[]) => void;
  uploadedPhotos: string[];
};

function ImageDropzone({ onUpload, uploadedPhotos }: ImageDropzoneType) {
  const maxFileSize = 10485760;
  const {
    acceptedFiles,
    getRootProps,
    getInputProps,
    isDragActive,
    isDragReject,
    fileRejections,
  } = useDropzone({
    accept: {
      "image/png": [".png"],
      "image/jpeg": [".jpg", ".jpeg"]
    },
    maxFiles: 5,
    maxSize: maxFileSize,
    onDrop: handleOnDrop
  });

  async function handleOnDrop(acceptedFiles: File[]) {
    const readerResult = await readUploadedFile(acceptedFiles);
    onUpload(readerResult);
  }
  
  const acceptedFileItems = !fileRejections.length && acceptedFiles.map((file: FileWithPath) => (
    <li key={file.path}>
      {file.path} - {file.size} bytes
    </li>
  ));

  useEffect(() => {
    if (fileRejections.length > 5) {
      toast.error("Too many uploaded images. You can upload up to 5 images.")
    } else if (fileRejections.length > 0) {
      toast.error("Wrong image types were uploaded.")
    }
  }, [fileRejections])

  return (
    <section className="">
      <div
        className="bg-gray-200 h-[15vh] flex rounded-md justify-center items-center mx-auto border-gray-300 border-dashed border-4 hover:cursor-pointer mb-5"
        {...getRootProps()}
      >
        <input {...getInputProps()} />
        {isDragActive ? (
          <p>Release left mouse button</p>
        ) : (
          <p>Drag and drop images here or click to browse for upload</p>
        )}
      </div>
      <div>
        {isDragReject && (
          <p className="text-red-500">
            File type not accepted. Please upload png, jpg, jpeg only files.
          </p>
        )}
      </div>
      <aside>
        {acceptedFiles.length > 0 && uploadedPhotos.length > 0 && (
          <>
            <h3 className="text-xl">Uploaded Files</h3>
            <ul>{acceptedFileItems}</ul>
          </>
        )}
      </aside>
    </section>
  );
}

export default ImageDropzone;