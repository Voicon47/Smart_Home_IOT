import { Chip,Image } from "@nextui-org/react";
import { useState } from "react";

export type ISensor = {
    id?: number | string | null;
    temperature: number | string;
    humidity: number |string;
    
 };
type ItemProps = {
    data: ISensor
    // isClick:() => void
    
 };
 function SensorItem(props: ItemProps) {
    const [isLoading, setIsLoading] = useState(false);
    
    return (
        <div  className=" flex flex-row gap-x-4 p-2 rounded-2xl hover:bg-slate-400/15 hover:shadow-md border-4 border-sky-500">
            {/* <img className="size-28 p-4 flex-none rounded-lg bg-gray-50" src="https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80" alt=""/> */}
            <div className="min-w-0 flex-auto ">
                {/* <p className="text-xl font-semibold ">{props.data.id}</p>
                <p className="mt-1 truncate text-sm text-gray-500">{props.data.temperature}</p> */}
                <p className="text-xl font-semibold ">Humidity + {props.data.humidity}</p>
                <p className="mt-1 truncate text-sm text-gray-500">Temperature + {props.data.temperature}</p>
            </div>
        </div>
    );
 }
 
 export default SensorItem;
 
 