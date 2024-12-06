import { Outlet } from "react-router-dom";
import Header from "./Header";
import { Roles } from "../App";
type MainLayoutProps = {
    children: React.ReactNode;
    // isAuthenticated: boolean;
    // roles?: Roles[];
 };
 
 function MainLayout(props: MainLayoutProps) {
    return (
        <div className="overflow-hidden w-screen min-h-screen select-none">
            <Header/>
            <div className="flex pt-20 gap-2 justify-start items-start  pb-32">
                <div className="w-screen  ">{props.children}</div>
            </div>
            {/* <Footer/> */}
        </div>
    );
};

export default MainLayout;