import React from "react";

type RoomCardProps = {
  name: string;
  status: string;
  price: number;
  image?: string;
};

const RoomCard: React.FC<RoomCardProps> = ({ name, status, price, image }) => {
  return (
    <div className="flex flex-row rounded-[10px] border border-gray-300 p-4 w-[302px] h-[208px] bg-[#dff4f1] hover:bg-[#294646] hover:text-white hover:shadow-lg hover:shadow-black/20 hover:transition-all hover:duration-500">
      <img
        src={
          image ||
          "https://cf.bstatic.com/xdata/images/hotel/max1280x900/488066775.jpg?k=624c3f5d623f9b33fe6cb9b2782d96b41f625a2bc7d325ae1e8e4ad1149012e8&o=&hp=1"
        }
        alt="room-image"
        className="rounded-[10px] w-[129px] h-[176px] object-cover mr-4"
      />
      <div className="flex flex-col justify-center flex-1">
        <p className="text-sm font-medium hover:text-white">Room 11{name}</p>
        <p className="text-xs font-serif text-gray-600 mt-1 hover:text-white">Name: Nguyen Van A</p>
        <p className="text-xs font-serif text-gray-600 mt-1 hover:text-white">Status: {status}</p>
        <p className="text-lg font-bold mt-4 hover:text-white">${price}</p>
      </div>
    </div>
  );
};

export default RoomCard;
