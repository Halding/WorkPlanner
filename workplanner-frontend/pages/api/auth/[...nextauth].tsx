import NextAuth, {NextAuthOptions} from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";

import axios from "axios";
import {useState} from "react";
import {Employee} from "../../../models/Employee";


// const [employee, setEmployee] = useState<Employee>();




const authOptions: NextAuthOptions = {
    session:{
        strategy: `jwt`
    },
    providers:[
        CredentialsProvider({
            type: "credentials",
            credentials: {
                employeeNumber: {label: "Employee Number", type:"EmployeeNumber", placeholder: "1234"},
                password: {label: "Password", type: "password"}
            },
            async authorize(credentials, req) {
                const {employeeNumber, password} = credentials as {
                    employeeNumber: string;
                    password: string;
                };

                const login = await axios.post("https://localhost:7293/api/Auth/login", credentials);
                const {data : employeeGotByEmployeeNumber} = await axios.get(`https://localhost:7293/api/employee/employeeNumber/${employeeNumber}`);
                console.log(employeeGotByEmployeeNumber);
                // setEmployee(employeeGotByEmployeeNumber);

                if (login.status !== 200) {
                    return null;
                }

                return {id: "1234", name: "john", employeeNumber: "test@gmail.com"}
            },

        }),
    ],
    // pages: {
    //     signIn: "/",
    // }
}


export default NextAuth(authOptions)