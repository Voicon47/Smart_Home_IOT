import React, { useState } from "react";

const FlameSensor: React.FC = () => {
  const [isSensorOn, setIsSensorOn] = useState<boolean>(false);

  const toggleSensor = () => {
    setIsSensorOn(!isSensorOn);
  };

  return (
    <div
      className={`border rounded-xl p-4 flex flex-col justify-between items-center w-[180px] h-[130px] relative shadow-sm hover:shadow-md transition-all duration-300 ${
        isSensorOn ? "bg-[#458484]" : "bg-white"
      }`}
    >
      <div className="absolute top-2 left-2">
        <span
          className={`font-medium text-sm ${
            isSensorOn ? "text-white" : "text-black"
          }`}
        >
          {isSensorOn ? "ON" : "OFF"}
        </span>
      </div>
      <div className="absolute top-12 left-1 flex flex-col items-center">
        <div className="w-10 h-10 flex items-center justify-center mb-1">
          <span className="material-symbols-outlined text-[24px] text-gray-500">
            light_mode
          </span>
        </div>
        <h3 className="text-teal-500 font-semibold">Lights</h3>
      </div>
      <div className="absolute top-2 right-2">
        <label className="relative inline-block w-10 h-6">
          <input
            type="checkbox"
            className="sr-only"
            checked={isSensorOn}
            onChange={toggleSensor}
          />
          <span className="block w-full h-full bg-gray-300 rounded-full transition-all duration-300" />
          <span
            className={`absolute left-1 top-1 w-4 h-4 bg-white rounded-full shadow-sm transition-transform duration-300 ${
              isSensorOn && "transform translate-x-4"
            }`}
          />
        </label>
      </div>
    </div>
  );
};

export default FlameSensor;
