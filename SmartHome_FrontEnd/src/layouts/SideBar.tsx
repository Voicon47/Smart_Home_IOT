import logo from '../../assets/react.svg';
import { GiOpenBook } from "react-icons/gi";
import { MdSpaceDashboard } from "react-icons/md";
import { useLocation } from "react-router-dom";
import { path } from "../routes/Path";
import { BsPersonFill } from "react-icons/bs";
import { Image, Accordion, AccordionItem, Chip } from '@nextui-org/react';
import { useRouter } from '../hooks/use-router';

function Sidebar() {
    const itemClasses = {
        base: "py-0 w-full",
        title: "font-normal text-medium",
        trigger: "px-3 py-0 mx-auto mt-2 data-[hover=true]:bg-primary/80 rounded-lg h-12 w-11/12 ",
        indicator: "text-medium",
        content: "text-small px-0 py-0",
      };
    const { pathname } = useLocation();
    const router = useRouter();
    const navs = [
        {
            name: 'Dashboard',
            icon: <MdSpaceDashboard className="text-xl" />,
            key: 1,
            path: path.ADMIN.DASHBOARD,
        },
        {
            name: 'Courses',
            icon: <GiOpenBook className="text-xl" />,
            key: 2,
            path: path.ADMIN.COURSE,
            subNav: [
                {
                    name: 'Quản lý',
                    path: path.ADMIN.COURSE,
                },
                {
                    name: 'Thêm',
                    path: path.ADMIN.ADD_COURSE,
                },
                {
                    name: 'Danh mục',
                    path: path.ADMIN.MANAGER_COURSE_CATEGORY,
                },
            ],
        },
        // {
        //     name: 'Posts',
        //     icon: <BiBookContent className="text-xl" />,
        //     key: 3,
        //     path: path.ADMIN.POST,
        //     subNav: [
        //         {
        //             name: 'Quản lý',
        //             path: path.ADMIN.POST,
        //         },
        //         {
        //             name: 'Thêm bài viết',
        //             path: path.ADMIN.ADD_POST,
        //         },
        //     ],
        // },
        {
            name: 'Users',
            icon: <BsPersonFill className="text-xl" />,
            key: 4,
            path: path.ADMIN.USER,
        },
    ];
    return (
        <>
            <div className="flex justify-center items-center w-full border-b-[1px] border-solid border-second pb-5 mb-5">
                {/* <Image className="scale-[200%] mt-5" isBlurred width={50} src={logo} alt="Course Edut" /> */}
            </div>
            <Accordion 
                showDivider={false}
                className="m-0 p-0 "
                itemClasses={itemClasses}
                // variant="shadow"
                >
                {navs.map((nav) => (
                    <AccordionItem
                        textValue={'nav'}
                        key={nav.key}
                        hideIndicator={!nav?.subNav}
                        // className={`px-3 hover:bg-[#AE57EA] rounded-lg h-14 `}
                        aria-label="Connected devices"
                        startContent={
                            // <Chip
                            //     color='primary'
                            //     variant={nav.path === pathname ? 'bordered' : 'light'}
                            //     className={` rounded-lg  min-w-[8rem] `}
                            // >
                                <div
                                    onClick={() => router.push(nav.path)}
                                    className={`hover:text-white ${
                                        pathname.includes(nav.path) ? 'text-primary' : 'text-black dark:text-white'
                                    } px-3 flex justify-start items-center gap-4 `}
                                >
                                    {nav.icon}
                                    <h2>{nav.name}</h2>
                                </div>
                            // </Chip>
                            // <div>{nav.icon}</div>
                        }
                        // title = {nav.name}
                        

                    >
                        {nav.subNav &&
                            nav.subNav.map((subNav, index) => (
                                <div
                                    onClick={() => router.push(subNav.path)}
                                    className={`${
                                        pathname.substring(1) === subNav.path
                                            ? 'text-primary '
                                            : ' text-black  dark:text-white '
                                    } pl-10 p-2 cursor-pointer hover:text-primary hover:dark:text-primary`}
                                    key={index}
                                >
                                    {subNav.name}
                                </div>
                            ))}
                    </AccordionItem>
                ))}
            </Accordion>
        </>
    );
}

export default Sidebar;