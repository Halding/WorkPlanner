import React, {useState} from 'react';
import {Employee} from "../models/Employee";
import {useRouter} from "next/router";
import axios from "axios";


export function getCookie(name: string) {
    // Split cookie string and get all individual name=value pairs in an array
    var cookieArr = document.cookie.split(";");

    // Loop through the array elements
    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");

        /* Removing whitespace at the beginning of the cookie name
        and compare it with the given string */
        if (name == cookiePair[0].trim()) {
            // Decode the cookie value and return
            return decodeURIComponent(cookiePair[1]);
        }
    }

    return null;
}



function Login() {

    const [employee, setEmployee] = useState<Employee>();
    const [employeeNumber, setEmployeeNumber] = useState("");
    const [password, setPassword] = useState("");

    const router = useRouter();



    const handleSubmit = async (e: any) => {
        e.preventDefault()

        const credentials = {employeeNumber, password};
        console.log(credentials)

        const login = await axios.post(`${process.env.NEXT_PUBLIC_BASEURL}employee/employeeNumber/${employeeNumber}`, credentials, {withCredentials: true});


        console.log(login)
        let jwt = getCookie("OurJwt")
        console.log("test");
        console.log(jwt);
        console.log("test");


        const {data: employeeGotByEmployeeNumber} = await axios.get(`${process.env.NEXT_PUBLIC_BASEURL}employee/employeeNumber/${employeeNumber}`, {
            headers: {
                Authorization: "Bearer " + jwt
            }
        });
        console.log(employeeGotByEmployeeNumber);
        setEmployee(employeeGotByEmployeeNumber);

        if (login.status === 200) {


            await router.push(`${process.env.NEXT_PUBLIC_CLEAN_URL}dashboard/overview`)
        }

    };



    return (

        <div className="flex min-h-full items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
            <div className="w-full max-w-md space-y-8">
                <div>
                    <img className="mx-auto h-12 w-auto"
                         src="https://tailwindui.com/img/logos/mark.svg?color=blue&shade=600"
                         alt="Your Company"></img>
                    <h2 className="mt-6 text-center text-3xl font-bold tracking-tight text-gray-900">Sign in to your
                        account</h2>
                </div>
                <form className="mt-8 space-y-6" action="#" method="POST">
                    <input type="hidden" name="remember" value="true"></input>
                    <div className="-space-y-px rounded-md shadow-sm">
                        <div>
                            <label htmlFor="username" className="sr-only"> EmployeeNumber </label>
                            <input
                                className="relative block w-full appearance-none rounded-none rounded-b-md border border-gray-300 px-3 py-2 text-gray-900 placeholder-gray-500 focus:z-10 focus:border-blue-500 focus:outline-none focus:ring-blue-500 sm:text-sm"
                                placeholder="EmployeeNumber"
                                type="text"
                                name="username"
                                id="username" onChange={(e) => setEmployeeNumber(e.target.value)}
                            />
                        </div>
                        <div>
                            <label htmlFor="password" className="sr-only"> Password </label>
                            <input
                                className="relative block w-full appearance-none rounded-none rounded-b-md border border-gray-300 px-3 py-2 text-gray-900 placeholder-gray-500 focus:z-10 focus:border-blue-500 focus:outline-none focus:ring-blue-500 sm:text-sm"
                                type="password"
                                name="password"
                                placeholder="Password"
                                id="password"
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </div>
                    </div>

                    <div>
                        <button type="submit"
                                className="group relative flex w-full justify-center rounded-md border border-transparent bg-blue-600 py-2 px-4 text-sm font-medium text-white hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                                onClick={handleSubmit}>
          <span className="absolute inset-y-0 left-0 flex items-center pl-3">
              <svg className="h-5 w-5 text-blue-500 group-hover:text-blue-400" xmlns="http://www.w3.org/2000/svg"
                   viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
              <path fillRule="evenodd"
                    d="M10 1a4.5 4.5 0 00-4.5 4.5V9H5a2 2 0 00-2 2v6a2 2 0 002 2h10a2 2 0 002-2v-6a2 2 0 00-2-2h-.5V5.5A4.5 4.5 0 0010 1zm3 8V5.5a3 3 0 10-6 0V9h6z"
                    clipRule="evenodd"/>
            </svg>
          </span>
                            Sign in
                        </button>
                    </div>
                </form>
            </div>
        </div>

    )
}



export default Login;