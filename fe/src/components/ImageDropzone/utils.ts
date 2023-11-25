export const readUploadedFile = async (acceptedFiles: File[]) => {
  const promises = acceptedFiles.reduce((acc: Promise<string>[], file: File) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
  
    const readerResult = new Promise<string>((resolve) => {
      reader.onloadend = () => {
        const readerResult = reader.result as string;
        resolve(readerResult);
      };
    });
    return [...acc, readerResult]
  }, []);
  return await Promise.all(promises);
};