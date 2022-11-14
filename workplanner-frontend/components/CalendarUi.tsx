import React, {useEffect, useState} from 'react';
import {Calendar, dateFnsLocalizer} from "react-big-calendar";
import "react-big-calendar/lib/css/react-big-calendar.css"
import axios from "axios";
import {startOfWeek, format, parse, getDay} from "date-fns";
import {Employee} from "../models/Employee";
import {getCookie} from "./Login";
import {Shift} from "../models/Shift";
import {CaEvent} from "../models/CaEvent";


const locales = {
    "da": require("date-fns/locale/da")
}

const localize = dateFnsLocalizer({
    format,
    parse,
    startOfWeek: () => {
        return startOfWeek(new Date(), {weekStartsOn: 1});
    },
    getDay,
    locales
})



function CalendarUi() {

    const [allEvents, setAllEvents] = useState<CaEvent[]>()
    const [employee, setEmployee] = useState<Employee>()
    const [allShifts, setAllShifts] = useState<Shift[]>()


    const handleClockIn = async () => {


    }

    const getUserShifts = async () => {

        const jwt = getCookie("OurJwt")

        const {data: usersShifts} = await axios.get(`https://localhost:7293/api/shift/employeeId`, {
            headers: {
                Authorization: "Bearer " + jwt
            }
        })

        const {data: employeeFromToken} = await axios.get(`https://localhost:7293/api/employee/user`, {
            headers: {
                Authorization: "Bearer " + jwt
            }
        })



        console.log("yo")
        console.log(employeeFromToken)
        console.log("yo")

        let vagter: CaEvent;
        let allVagter = [];

        for (const shift of usersShifts) {
            allVagter.push(vagter = {
                title: `${shift.employeeFirstName} ${shift.employeeLastName} ${shift.employeeNumber}`,
                startTime: new Date(shift.startTime),
                endTime: new Date(shift.endTime)
            })
        }

        console.log(allVagter)

        setEmployee(employeeFromToken)
        setAllEvents(allVagter)
        setAllShifts(usersShifts)

    };

    useEffect(() => {
        getUserShifts()

    }, []);

    useEffect(() => {


    }, [allEvents]);





    return (
        <div className="m-10">


            <div className="my-5">
                <div className="max-w-sm rounded overflow-hidden shadow-lg">
                    <div className="px-6 py-4">
                        <div className="font-bold text-xl mb-2"> FirstName = {employee?.firstName} LastName = {employee?.lastName}</div>
                        <p className="text-gray-700 text-base">
                            vagt i dag
                        </p>
                        <p className="text-gray-700 text-base">
                            clocked in ? status `?
                        </p>
                    </div>
                    <div className="px-6 pt-4 pb-2">
                        <div className="my-2 flex justify-between ">

                            <button
                                type="button"
                                className=" rounded border border-transparent bg-indigo-600 px-6 py-3 text-xs font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
                            >
                                Clock In
                            </button>
                            <button
                                type="button"
                                className=" rounded border border-transparent bg-indigo-600 px-6 py-3 text-xs font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
                            >
                                Clock out
                            </button>
                        </div>
                    </div>
                </div>


            </div>


            <div>
                <Calendar localizer={localize} events={allEvents}
                          startAccessor="startTime" endAccessor="endTime" style={{height: 500}}>

                </Calendar>
            </div>

        </div>

    );
}

export default CalendarUi;