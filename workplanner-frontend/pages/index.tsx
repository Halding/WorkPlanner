import type {NextPage} from 'next'
import Head from 'next/head'
import axios from "axios";
import TestAll from "../components/TestAll";
import {signIn} from "next-auth/react";
import {useEffect, useState} from "react";
import {Employee} from "../models/Employee";

const Home: NextPage = ({}) => {


    const [employee, setEmployee] = useState<Employee>();
    const [employeeNumber, setEmployeeNumber] = useState("");
    const [password, setPassword] = useState("");
    const [loggedIn, setLoggedIn] = useState<boolean>()
    const credentials = {employeeNumber, password};







    return (
        <div className="flex min-h-screen flex-col items-center justify-center py-2">
            <Head>
                <title>Workplanner 1.0</title>
                <link rel="icon" href="/favicon.ico"/>
            </Head>
            <button onClick={() => {
                signIn()
            }}> login
            </button>
            <TestAll></TestAll>

        </div>
    )
}

export default Home
