"use client";
import { Breadcrumbs, Button, Input, Textarea, Typography, Dialog, Rating } from "@/lib/materialTailwindExports";
import { getToken } from "@/utils/getToken";
import { BASE_API_URL, SERVICE_REVIEWS_ROUTE } from "@/utils/urls";
import { useParams, useRouter } from 'next/navigation';
import { useState } from "react";
import Link from "next/link";
import toast from "react-hot-toast";

const ReviewForm = () => {
  const [reviewData, setReviewData] = useState({
    fullName: "",
    serviceName: "",
    driverName: "",
    score: 0,
    description: "",
  });

  const params = useParams();
  const router = useRouter();
  const [showDialog, setShowDialog] = useState(false);

  // Handle input change
    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
  
    setReviewData((formData) => ({
      ...formData,
      [name]: value,
    }));
  };
  

  // Validate form data
  const validateData = async (): Promise<boolean> => {
    try {
      for (const key in reviewData) {
        if (!reviewData[key]) {
          toast.error(`Please provide a value for ${key}`);
          return false;
        }
      }

      if (!reviewData.score || reviewData.score === 0) {
        toast.error("Please provide a star rating");
        return false;
      }

      return true;
    } catch {
      return false;
    }
  };

  // Handle form submission
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const isValid = await validateData();

    if (isValid) {
      setShowDialog(true);
    } else {
      toast.error("Please fulfill all form fields");
    }
  };

  // Handle confirmation after dialog is shown
  const handleConfirm = async () => {
    const token = await getToken();
    const body = {
      ...reviewData,
      // Additional fields to include in the request body
    };

    const response = await fetch(`${BASE_API_URL}${SERVICE_REVIEWS_ROUTE}/${params?.serviceId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(body),
    });

    if (!response.ok) {
      const json = await response.json();
      toast.error(json.message || response.statusText || "Unknown error occurred.");
      return;
    }

    setReviewData({
      fullName: "",
      serviceName: "",
      driverName: "",
      score: 0,
      description: "",
    });

    router.push(`/services/${params?.serviceId}`);
    toast.success("Review successfully submitted");

    setShowDialog(false);
  };

  return (
    <div className="flex flex-col gap-3 max-w-3xl mx-auto my-5 py-10 px-5">
      <Breadcrumbs>
        <Link href="/home" className="opacity-60">
          <span>Home</span>
        </Link>
        <Link href="/services" className="opacity-60">
          <span>Services</span>
        </Link>
        <Link href={`/services/${params?.serviceId}`} className="opacity-60">
          <span>Service Details</span>
        </Link>
        <Link href="#">
          <span>Leave a Review</span>
        </Link>
      </Breadcrumbs>

      <Typography variant="h3">Leave a review</Typography>

      <form onSubmit={handleSubmit} className="flex flex-col gap-5">
        <Input crossOrigin="" type="text" value={reviewData.fullName} name="fullName" onChange={handleChange} color="blue-gray" label="Full Name" />
        <Input crossOrigin="" type="text" value={reviewData.serviceName} name="serviceName" onChange={handleChange} color="blue-gray" label="Service Name" />
        <Input crossOrigin="" type="text" value={reviewData.driverName} name="driverName" onChange={handleChange} color="blue-gray" label="Driver Name" />

        {/* Material Tailwind Rating component */}
        <Rating
          value={reviewData.score}
          onChange={(value) => setReviewData((data) => ({ ...data, score: value }))}
        />

        <Textarea value={reviewData.description} name="description" onChange={handleChange} resize rows={4} color="blue-gray" label="Review Description" />

        <Button type="submit" variant="gradient">Submit Review</Button>

        {/* Styled Dialog for confirmation */}
        <Dialog open={showDialog} onClose={() => setShowDialog(false)}>
          <div className="text-left p-6 bg-white rounded-lg shadow-lg">
            <div className="text-lg font-medium mb-4">Leave a Review</div>
            <div className="text-sm text-gray-500 mb-6">
              Are you sure you want to submit this review?
            </div>
            <div className="flex justify-end">
              <Button onClick={() => setShowDialog(false)} className="mr-2">
                Cancel
              </Button>
              <Button onClick={handleConfirm} variant="gradient">
                Confirm
              </Button>
            </div>
          </div>
        </Dialog>
      </form>
    </div>
  );
};


export default ReviewForm;
