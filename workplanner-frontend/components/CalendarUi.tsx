import React, {useEffect, useState} from 'react';
import {Calendar, dateFnsLocalizer} from "react-big-calendar";
import "react-big-calendar/lib/css/react-big-calendar.css"
import axios from "axios";
import {startOfWeek, format, parse, getDay} from "date-fns";
import Layout from "./Layout";
import {Employee} from "../models/Employee";
import {getCookie} from "./Login";


export const getStaticProps = async () => {
    let jwt = getCookie("OurJwt")
    const res = await fetch(`http://localhost:5293/api/employee/user`, {
        headers: {
            Authorization: "Bearer " + jwt
        }
    });
    const data = await res.json();
    console.log(data)

    return {
        props: {user: data}
    }
}

const locales = {
    "da": require("date-fns/locale/da")
}

const localize = dateFnsLocalizer({
    format,
    parse,
    startOfWeek: () => {
        return startOfWeek(new Date(), { weekStartsOn: 1 });
    },
    getDay,
    locales
})

const events = [
    {
        title: "Big Meeting",
        start: new Date(2022, 10, 7, 11),
        end: new Date(2022, 10, 7,13),
    },
    {
        title: "Vacation",
        start: new Date(2022, 10, 7,8),
        end: new Date(2022, 10, 7,16),
    },
    {
        title: "Conference",
        start: new Date(2022, 10, 7,10,15),
        end: new Date(2022, 10, 7,14),
    },
]





function CalendarUi() {

    console.log("yo")
    console.log()

    const [newEvent, setNewEvent] = useState({title: "", start: "", end: ""})
    const [allEvents, setAllEvents] = useState(events)
    const [employee, setEmployee] = useState<Employee>()




    const getUser = async () => {

        let jwt = getCookie("OurJwt")
        const {data: userGotById} = await axios.get(`http://localhost:5293/api/employee/user`, {
            headers: {
                Authorization: "Bearer " + jwt
            }
        }).
        then((res) => res.data);
        setEmployee(userGotById)

        console.log("her")
        console.log("")
        console.log("her")
    };

    useEffect( () => {
        getUser()
    }, []);



    // function handleAddEvent() {
    //     setAllEvents([...allEvents, newEvent])
    // }

    return (
        <div>
            <Calendar localizer={localize} events={allEvents}
                      startAccessor="start" endAccessor="end" style={{height: 500, margin: "50px"}}>

            </Calendar>
        </div>
    );
}

export default CalendarUi;