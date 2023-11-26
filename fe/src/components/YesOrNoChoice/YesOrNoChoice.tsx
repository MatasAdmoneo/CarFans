import { Radio } from "@/lib/materialTailwindExports";

type YesOrNoFormType = {
  question: string;
  name: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>, value: boolean) => void;
};

const YesOrNoChoice = ({ question, name, onChange }: YesOrNoFormType) =>
<>
  <h3 className="text-lg leading-6 font-medium text-gray-900">
    {question}
    <span style={{ color: 'red' }}>*</span>
  </h3>
  <div className="flex gap-10">
    <div className="flex gap-2">
      <Radio onChange={(e) => onChange(e, true)} color="blue-gray" name={name} crossOrigin="" label="Yes" />
    </div>
    <div className="flex gap-2">
      <Radio onChange={(e) => onChange(e, false)} color="blue-gray" name={name} crossOrigin="" label="No" />
    </div>
  </div>
</>

export default YesOrNoChoice;