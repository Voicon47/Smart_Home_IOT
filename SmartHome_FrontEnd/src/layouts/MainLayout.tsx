import { Outlet } from "react-router-dom";
import Header from "./Header";
import { Roles } from "../App";
import Sidebar from "./SideBar";
type MainLayoutProps = {
    children: React.ReactNode;
    // isAuthenticated: boolean;
    // roles?: Roles[];
 };
 
 function MainLayout(props: MainLayoutProps) {
    return (
        <div className="overflow-hidden min-w-full min-h-screen select-none">
            <Header/>
            <div className="flex  mt-20 gap-2 pb-32">
                <div className="flex-none select-none min-w-[15rem] shadow-2xl fixed z-auto dark:bg-dark-sidebar  bg-white  flex justify-start  max-h-screen rounded-xl left-4 bottom-4 top-20 items-start flex-col">
                    <Sidebar/>
                </div>
                <div className="ml-[17rem] w-screen p-4">{props.children}</div>
            </div>
            {/* <Footer/> */}
        </div>
    );
};

export default MainLayout;