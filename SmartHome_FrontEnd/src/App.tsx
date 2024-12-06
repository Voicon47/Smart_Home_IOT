import { useState } from 'react'
import './App.css'
import { Toaster } from 'react-hot-toast';
import { createBrowserRouter, Navigate, Outlet, Route, RouteObject, RouterProvider, Routes,useLocation  } from 'react-router-dom';
import { publicRoutes } from './routes/Routes';
import { useTheme } from './context/themeContext';
import MainLayout from './layouts/MainLayout';

export enum Roles {
  ADMIN,
  STUDENT,
}

function App() {
//   const [count, setCount] = useState(0)
  const { theme } = useTheme();
  const role = Roles.ADMIN
  return (
    <>
       <Toaster
          position="top-center"
          containerStyle={{
             zIndex: 1000000000000,
          }}
          toastOptions={{
             style: {
                background: theme ? 'rgba(0,0,0,0.5)' : 'rgba(0,0,0,0.5)',
                color: theme ? 'white' : 'black',
                backdropFilter: 'blur(20rem)',
                userSelect: 'none',
             },
          }}
       />
       <Routes>
          {/* Public Routes */}
          {publicRoutes.map((route, index) => (
             <Route
                key={index}
                path={route.path}
                element={<MainLayout >{route.component}</MainLayout>}
             />
          ))}
       </Routes>
    </>
 );
}

export default App
