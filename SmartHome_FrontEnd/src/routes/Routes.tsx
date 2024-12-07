import {ReactNode} from "react";
import { path } from "./Path";
import HomePage from "../pages/HomePage";
import Phat from "../pages/Phat";


export type routeProps = {
    path: string;
    component: ReactNode;
    type: typeRoute;
 };
 
 export enum typeRoute {
    PRIVATE_ROUTE = 'private_route',
    PROTECTED_ROUTE = 'protected_route',
    PUBLIC_ROUTE = 'public_route',
 }

export const publicRoutes: routeProps[] = [
   {
      path: path.HOME,
      component: <HomePage />,
      type: typeRoute.PUBLIC_ROUTE,
   },
   {
      path: path.PHAT,
      component:<Phat/>,
      type: typeRoute.PUBLIC_ROUTE,
   }
   // {
   //    path: '/test',
   //    component: <TestPage />,
   //    type: typeRoute.PUBLIC_ROUTE,
   // },
   // {
   //    path: path.AUTH.VERIFY_ACCOUNT,
   //    component: <VerifyAccount />,
   //    type: typeRoute.PUBLIC_ROUTE,
   // },
   // {
   //    path: path.AUTH.RESET_PASSWORD,
   //    component: <RecoveryPassword />,
   //    type: typeRoute.PUBLIC_ROUTE,
   // },
   // {
   //    path: path.POST.DETAIL + '/:postId',
   //    component: <DetailPost />,
   //    type: typeRoute.PUBLIC_ROUTE,
   // },
];
export const protectedRoutes: routeProps[] = [
   
   // {
   //    path: path.ADMIN.POST,
   //    component: <PostManagement />,
   //    type: typeRoute.PROTECTED_ROUTE,
   // },
   // {
   //    path: path.ADMIN.POST + '/add',
   //    component: <AddPost />,
   //    type: typeRoute.PROTECTED_ROUTE,
   // },
   
];
export const privateRoutes: routeProps[] = [
   
   // {
   //    path: path.POST.VIEW,
   //    component: <ViewPost />,
   //    type: typeRoute.PRIVATE_ROUTE,
   // },
];

export const learningRoutes: routeProps[] = [
   
];
