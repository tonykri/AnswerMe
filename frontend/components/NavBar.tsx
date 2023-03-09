'use client'
import { Navbar, Dropdown } from "flowbite-react"
import ButtonTheme from "./ButtonTheme"
import { useRouter } from "next/navigation";
import axios from "axios";
import { useEffect } from "react";


export default function NavBar() {
    const router = useRouter();

    useEffect(() => {
    if (!localStorage.getItem('token')) {
        router.push('/');
    } else {
        axios.get('https://localhost:8080/api/User/activeToken', {
            headers: {
              'Authorization': `Bearer ${localStorage.getItem('token')}` 
            }
          }).catch(() => {
            router.push('/');
        });
    }
}, [])

    function Logout(){
        localStorage.removeItem('token');
        localStorage.removeItem('firstName');
        localStorage.removeItem('lastName');
        localStorage.removeItem('email');
        localStorage.removeItem('birthDate');
        router.push('/');
    }
    return (
        <Navbar
            fluid={true}
            rounded={true}
            className='bg-slate-100'
        >
            <Navbar.Brand href="/home">
                <span className="self-center whitespace-nowrap text-xl font-semibold dark:text-white">
                    AnswerMe
                </span>
            </Navbar.Brand>
            <div className="flex md:order-2">
                <div className="collapse md:visible">
                    <Dropdown
                        label="Profile"
                        placement="bottom"
                        inline={true}
                    >
                        <Dropdown.Item>
                            View Profile
                        </Dropdown.Item>
                        <Dropdown.Item>
                            Settings
                        </Dropdown.Item>
                        <Dropdown.Item className="hover:bg-red-700 hover:text-white dark:hover:bg-red-700 dark:hover:text-white" onClick={Logout}>
                            Sign out
                        </Dropdown.Item>
                        <Dropdown.Item>
                            <div>
                                <ButtonTheme />
                            </div>
                        </Dropdown.Item>
                    </Dropdown>
                </div>

                <Navbar.Toggle />
            </div>
            <Navbar.Collapse>
                <Navbar.Link href="#">
                    <form className="flex items-center">
                        <div className="relative w-full">
                            <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                                <svg aria-hidden="true" className="w-5 h-5 text-gray-500 dark:text-gray-400" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z" clip-rule="evenodd"></path></svg>
                            </div>
                            <input type="text" id="simple-search" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full pl-10 p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Search User" required />
                        </div>
                        <button type="submit" className="p-2.5 ml-2 text-sm font-medium text-white bg-blue-700 rounded-lg border border-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                            <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path></svg>
                            <span className="sr-only">Search</span>
                        </button>
                    </form>
                </Navbar.Link>


                <Navbar.Link>
                    <div className="md:collapse justify-start flex">
                        <Dropdown
                            label="Profile"
                            placement="right"
                            inline={true}
                        >
                            <Dropdown.Item>
                                View Profile
                            </Dropdown.Item>
                            <Dropdown.Item>
                                Settings
                            </Dropdown.Item>
                            <Dropdown.Item className="hover:bg-red-700 hover:text-white dark:hover:bg-red-700 dark:hover:text-white">
                                Sign out
                            </Dropdown.Item>
                            <Dropdown.Item>
                                <div>
                                    <ButtonTheme />
                                </div>
                            </Dropdown.Item>
                        </Dropdown>
                    </div>
                </Navbar.Link>

            </Navbar.Collapse>
        </Navbar>
    )
}