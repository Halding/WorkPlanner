import type {NextPage} from 'next'
import Head from 'next/head'
import axios from "axios";
import TestAll from "../components/TestAll";
import {signIn} from "next-auth/react";
import React, {useEffect, useState} from "react";
import {Employee} from "../models/Employee";
import {useRouter} from "next/router";
import Login from "../components/Login";
import Layout from "../components/Layout";


const Home: NextPage = () => {


    return (
       <div>
               <Login></Login>
       </div>
    )
}

export default Home
