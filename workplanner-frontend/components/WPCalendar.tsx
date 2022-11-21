import React, {useEffect, useState} from 'react';
import {Department} from "../models/Department";
import "react-big-calendar/lib/css/react-big-calendar.css"
import axios from "axios";
import {getCookie} from "./Login";
import {Calendar, dateFnsLocalizer} from "react-big-calendar";
import {format, getDay, parse, startOfWeek} from "date-fns";
import {CaEvent} from "../models/CaEvent";
import ModalShift from "./ModalShift";
import ModalEdit from "./ModalEdit";




function WpCalendar() {

    const [departmentId, setDepartmentId] = useState<string>()
    const [allDepartments, setAllDepartments] = useState<Department[]>()
    const [allEvents, setAllEvents] = useState<CaEvent[]>()
    const [isOpenShift, setIsOpenShift] = useState<boolean>(false)
    const [isOpenEdit, setIsOpenEdit] = useState<boolean>(false)
    const [selectedEvent, setSelectedAllEvent] = useState<CaEvent>()


    useEffect(() => {
        getData()
    }, []);

    useEffect(() => {
        getShiftData()
    }, [departmentId, isOpenEdit,isOpenShift]);


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

    const handleSelected = (event: React.SetStateAction<CaEvent | undefined>) => {
            setSelectedAllEvent(event);
            console.log(event)
            setIsOpenEdit(true)
    };


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
                    id: shift.id,
                    title: `${shift.employeeFirstName}`,
                    startTime: new Date(shift.startTime),
                    endTime: new Date(shift.endTime),
                    clockInTime: new Date(shift.clockInTime),
                    clockOutTime: new Date(shift.clockOutTime)
                })
            }
            setAllEvents(allVagter)
        }

    }

    return (
        <>
            <div>
                <div className=" m-10">
                    <div className="my-5">
                        <button
                            onClick={() => setIsOpenShift(true)}
                            type="button"
                            className=" rounded border border-transparent bg-blue-600 px-6 py-3 text-xs font-medium text-white shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                        >
                            Create Shift
                        </button>
                        <div className="my-2">
                            <label htmlFor="departments"
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
                             selected={selectedEvent} startAccessor="startTime" endAccessor="endTime" style={{height: 500}} onSelectEvent={(event) => {handleSelected(event);}}>

                    </Calendar>
                </div>
            </div>
            <ModalShift isOpen={isOpenShift} setIsOpen={setIsOpenShift}/>
            <ModalEdit shiftData={selectedEvent} isOpen={isOpenEdit} setIsOpen={setIsOpenEdit} />
        </>
    );
}

export default WpCalendar;