import { Link, Skeleton,Switch } from "@nextui-org/react";
import { useState, useEffect } from "react";
import {Divider} from "@nextui-org/react";
import SensorItem, { ISensor } from "./SensorItem";

export interface WebSocketMessage<T> {
  Type: string; // Type of the message ("SensorData" or "Light")
  Data: T;      // Payload of the message
}
 function HomePage(){
   const [sensorData,setSensorData] = useState<ISensor|null>(null);
   const [isSelected, setIsSelected] = useState(false);
   const [lightStatus, setLightStatus] = useState("off");
   const [socket, setSocket] = useState<WebSocket | null>(null);
  //  const [sensorData, setSensorData] = useState({ temperature: null, humidity: null });
  const [connectionStatus, setConnectionStatus] = useState('Connecting...');

  const sendMessage = (socket: WebSocket, messageType: string, payload: any) => {   
    const message = {
        Type: messageType, // e.g., "sensorData", "command", etc.
        Data: payload      // The actual data to send
    };
    
    console.log(JSON.stringify(message))
    socket.send(JSON.stringify(message)); // Serialize and send the message
  };
  useEffect(() => {
    // Create a WebSocket connection to the server
    const socket = new WebSocket('ws://192.168.0.222:5222/api/ws');

    // Handle WebSocket events

    // On open, change connection status
    socket.onopen = () => {
      setConnectionStatus('Connected');
      console.log('WebSocket connection established');
    };

    // Handle incoming messages
    socket.onmessage = (event) => {
      try {
        const message : WebSocketMessage<any> = JSON.parse(event.data);
        console.log(message)
        if (message.Type === 'SensorData') {
          const rawData = message.Data
          const data: ISensor = {
            id: rawData.Id ?? null,                   // Map "Id" to "id", default to null if not present
            temperature: String(rawData.Temperature), // Convert Temperature to string if needed
            humidity: String(rawData.Humidity),       // Convert Humidity to string if needed
          }  
          // console.log(data)
          setSensorData(data);
        } else if (message.Type === 'Light') {
          const status = message.Data === "Light_on" ? true : false
          setIsSelected(status)
        }
      } catch (error) {
        console.error('Error parsing WebSocket data:', error);
      }
    };

    // Handle errors
    socket.onerror = (error) => {
      console.error('WebSocket error:', error);
      setConnectionStatus('Error in connection');
    };

    // Handle connection closure
    socket.onclose = () => {
      setConnectionStatus('Connection closed');
      console.log('WebSocket connection closed');
    };

    setSocket(socket);

    // Cleanup the WebSocket connection on component unmount
    return () => {
      socket.close();
      console.log('WebSocket connection closed from client side');
    };
  }, []); // Empty dependency array to run once on mount
  const toggleLight = () => {
    if (socket && socket.readyState  === WebSocket.OPEN) {
      const newStatus = lightStatus === "on" ? "off" : "on";
      console.log(newStatus)
      //send message
      sendMessage(socket,"Light",`Light_${newStatus}`)
      setIsSelected(newStatus  === "on")
      setLightStatus(newStatus);
    } else {
      console.log("WebSocket is not connected");
    }
  };
  return (
    <div>
      <h1>Sensor Data</h1>
      <p>Status: {connectionStatus}</p>

      { sensorData? (
          <div className="max-w-screen-lg m-auto flex flex-col gap-2">
              <SensorItem data={sensorData}/>
          </div>
      ) : (
        <p>Waiting for sensor data...</p>
      )}
      <Switch 
          size="lg" 
          color="warning" 
          isSelected={isSelected} 
          onValueChange={toggleLight}>Light</Switch>
    </div>
  );

   // return(
   //    <>
   //       {/* <div className="max-w-screen-xl mt-5 mb-10 m-auto">{homeData?.banners && <BannerSlide data={homeData?.banners} />}</div> */}
   //       <div className="mt-5 "> {/* <ListTag /> */}
   //          <div className="w-full" >
   //             <div className="mt-5 justify-between w-full items-center  ">
   //                <div className="items-center gap-4">
   //                   <h5 className="font-extrabold text-2xl">Hello</h5>
   //                </div>
   //             </div>
   //             <h5 className="text-white/2  mb-5">380.506+ người khác đã học</h5>
   //             <Divider className="my-1"/>
   //             <div className="max-w-screen-lg m-auto flex flex-col gap-2">
   //                {sensorData?.map((c,index) => (
   //                   <SensorItem key= {index} data={c}/>
   //                ))}
   //             </div>
            
   //             </div>
   //          </div>
   //    </>
   // );
 }
 export default HomePage;