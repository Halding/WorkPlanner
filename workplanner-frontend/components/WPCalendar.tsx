import React, {useEffect, useState} from 'react';
import {Department} from "../models/Department";
import "react-big-calendar/lib/css/react-big-calendar.css"
import axios from "axios";
import {getCookie} from "./Login";
import {Calendar, dateFnsLocalizer} from "react-big-calendar";
import {format, getDay, parse, startOfWeek} from "date-fns";
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


function WpCalendar() {

    const [departmentId, setDepartmentId] = useState<string>()
    const [allDepartments, setAllDepartments] = useState<Department[]>()
    const [allEvents, setAllEvents] = useState<CaEvent[]>()

    const getData = async () => {


        const jwt = getCookie("OurJwt")

        const {data: newAllDepartments} = await axios.get(`https://localhost:7293/api/Department`, {
            headers: {
                Authorization: "Bearer " + jwt
            }
        })

        setAllDepartments(newAllDepartments)
    }

    const getShiftData = async () => {

        const jwt = getCookie("OurJwt")


        if (departmentId != null) {
            const {data: departmentShifts} = await axios.get(`https://localhost:7293/api/shift/department/${departmentId}`, {
                headers: {
                    Authorization: "Bearer " + jwt
                }
            })

            let vagter: CaEvent;
            let allVagter = [];

            for (const shift of departmentShifts) {
                allVagter.push(vagter = {
                    title: `${shift.employeeNumber} ${shift.employeeFirstName}`,
                    startTime: new Date(shift.startTime),
                    endTime: new Date(shift.endTime)
                })
            }
            setAllEvents(allVagter)
        }

    }


    useEffect(() => {
        getData()
    }, []);

    useEffect(() => {
        getShiftData()
    }, [departmentId]);



    return (


        <div className=" m-10">
            <div className="my-5">

                <div>
                    <label htmlFor="countries"
                           className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-400">Select
                        Department</label>
                    <select onChange={e => setDepartmentId(e.target.value.toString())} id="Departments"
                            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500
                     focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600
                      dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
                        <option>Choose a Department</option>
                        {allDepartments?.map((d) => (
                            <option value={d.id} key={d.id}>{d.departmentName}</option>
                        ))}
                    </select>
                </div>

            </div>

            <Calendar localizer={localize} events={allEvents}
                      startAccessor="startTime" endAccessor="endTime" style={{height: 500}}>

            </Calendar>

        </div>
    );
}

export default WpCalendar;