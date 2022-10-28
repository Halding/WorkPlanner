import React, {useState} from 'react';
import axios from "axios";
import {useRouter} from 'next/router'
import {NextRouter} from "next/dist/client/router";
import {Employee} from "../models/Employee";
import {sign} from "crypto";


function Login() {

    const [employee, setEmployee] = useState<Employee>();
    const [employeeNumber, setEmployeeNumber] = useState("");
    const [password, setPassword] = useState("");

    const router = useRouter();




    const handleSubmit = async (e: any) => {
        e.preventDefault()


        const credentials = {employeeNumber, password};
        console.log(credentials)

        const login = await axios.post("http://localhost:5293/api/Auth/login", credentials, { withCredentials: true });




        const {data: employeeGotByEmployeeNumber} = await axios.get(`http://localhost:5293/api/employee/employeeNumber/${employeeNumber}`);
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
    );
}

export default Login;