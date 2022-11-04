import React, {useEffect, useState} from 'react';
import {Employee} from "../models/Employee";
import {useRouter} from "next/router";
import Head from "next/head";
import axios from "axios";
import {NextRequest} from "next/server";

function Login() {
    const [employee, setEmployee] = useState<Employee>();
    const [employeeNumber, setEmployeeNumber] = useState("");
    const [password, setPassword] = useState("");

    const router = useRouter();

    function getCookie(name: string) {
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


    const handleSubmit = async (e: any) => {
        e.preventDefault()

        const credentials = {employeeNumber, password};
        console.log(credentials)

        const login = await axios.post("http://localhost:5293/api/Auth/login", credentials, {withCredentials: true});


        console.log(login)
        let jwt = getCookie("OurJwt")
        console.log("test");
        console.log(jwt);
        console.log("test");


        const {data: employeeGotByEmployeeNumber} = await axios.get(`http://localhost:5293/api/employee/employeeNumber/${employeeNumber}`, {
            headers: {
                Authorization: "Bearer " + jwt
            }
        });
        console.log(employeeGotByEmployeeNumber);
        setEmployee(employeeGotByEmployeeNumber);

        console.log(login.status)
        console.log(login);

        if (login.status === 200) {


            await router.push("http://localhost:3000/dashboard/test")
        }

    };

    const handleGetUser = async () => {
        const user = await axios.get("/api/user");

        console.log(user);
    };

    const handleLogOut = async () => {
        const user = await axios.get("/api/auth/logout");

        console.log(user);
    };


    return (
        <div className="flex min-h-screen flex-col items-center justify-center py-2">
            <Head>
                <title>Workplanner 1.0</title>
                <link rel="icon" href="/favicon.ico"/>
            </Head>

            <div className="m-3">

                <form onSubmit={handleSubmit}>
                    <label htmlFor="username"> EmployeeNumber </label>
                    <input className="inpField"
                           placeholder="EmployeeNumber"
                           type="text"
                           name="username"
                           id="username"
                           onChange={(e) => setEmployeeNumber(e.target.value)}
                    />

                    <label htmlFor="password"> Password </label>
                    <input
                        className="inpField"
                        type="password"
                        name="password"
                        placeholder="Password"
                        id="password"
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <div className="my-2">
                        <button className="btnStd" onClick={handleSubmit}> Log in</button>

                        <button className="btnStd" onClick={() => handleLogOut()}> Logout</button>

                        <button className="btnStd" onClick={() => handleGetUser()}> User</button>
                    </div>

                </form>
            </div>


        </div>
    )
}

export default Login;