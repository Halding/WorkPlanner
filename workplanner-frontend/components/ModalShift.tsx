import React, {Fragment, useEffect, useRef, useState} from 'react'
import {Dialog, Transition} from '@headlessui/react'
import {getCookie} from "./Login";
import axios from "axios";
import {Employee} from "../models/Employee";
import {MakeShift} from "../models/MakeShift";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import { MobileDatePicker } from '@mui/x-date-pickers/MobileDatePicker';



export default function ModalShift({isOpen, setIsOpen}: { isOpen: boolean, setIsOpen: (arg0: boolean) => void }) {

    const cancelButtonRef = useRef(null)
    const [employee, setEmployee] = useState<Employee>()
    const [employeeNumber, setEmployeeNumber] = useState<string>("")
    const [startValue, setStartValue] = useState<Date | null>(new Date());
    const [endValue, setEndValue] = useState<Date | null>(new Date());

    const handleStartTime = (newValue: Date | null) => {
        setStartValue(newValue);
    };

    const handleEndTime = (newValue: Date | null) => {
        setEndValue(newValue);
    };

    useEffect(() => {
        postShift()
    }, [employee]);


    const getData = async () => {
        const jwt = getCookie("OurJwt")

        const {data: employeeFromEmployeeNumber} = await axios.get(`https://localhost:7293/api/employee/employeeNumber/${employeeNumber}`, {
            headers: {
                Authorization: "Bearer " + jwt
            }
        })
        setEmployee(employeeFromEmployeeNumber)

    }

    const postShift = async () => {

        const jwt = getCookie("OurJwt")

        if (employee && startValue != null && endValue != null) {
            let testShift : MakeShift = {
                startTime : startValue,
                endTime : endValue,
                clockInTime : null,
                clockOutTime : null,
                employeeId : employee.id,
                departmentId: employee.departmentId
            }
            const {data: createdShift} = await axios.post(`http://localhost:5293/api/shift/create`,testShift, {
                headers: {
                    Authorization: "Bearer " + jwt
                }
            });

            console.log("123")
            console.log(createdShift)
            console.log("123")
        }

    }
    return (
        <Transition.Root show={isOpen} as={Fragment}>
            <Dialog as="div" className="relative z-10" initialFocus={cancelButtonRef} onClose={() => setIsOpen(false)}>
                <Transition.Child
                    as={Fragment}
                    enter="ease-out duration-300"
                    enterFrom="opacity-0"
                    enterTo="opacity-100"
                    leave="ease-in duration-200"
                    leaveFrom="opacity-100"
                    leaveTo="opacity-0"
                >
                    <div className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"/>
                </Transition.Child>

                <div className="fixed inset-0 z-10 overflow-y-auto">
                    <div className="flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0">
                        <Transition.Child
                            as={Fragment}
                            enter="ease-out duration-300"
                            enterFrom="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                            enterTo="opacity-100 translate-y-0 sm:scale-100"
                            leave="ease-in duration-200"
                            leaveFrom="opacity-100 translate-y-0 sm:scale-100"
                            leaveTo="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                        >
                            <Dialog.Panel
                                className="relative transform overflow-hidden rounded-lg bg-white px-4 pt-5 pb-4 text-left shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg sm:p-6">
                                <form>
                                    <label className="block text-sm font-medium">
                                        Choose Employee by EmployeeNumber
                                    </label>
                                    <input
                                        value={employeeNumber}
                                        onChange={e => setEmployeeNumber(e.target.value)}
                                        type="text"
                                        name="email"
                                        id="email"
                                        className="block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm"
                                        placeholder="1061"
                                    />
                                    <div className="my-2 flex justify-between ">
                                        <LocalizationProvider dateAdapter={AdapterDateFns}>
                                            <Stack spacing={3}>
                                                <DateTimePicker
                                                    label="Time The Shift Start"
                                                    value={startValue}
                                                    inputFormat="dd/MM/yyyy HH:mm"
                                                    ampm={false}
                                                    onChange={handleStartTime}
                                                    renderInput={(params) => <TextField {...params} />}
                                                />
                                            </Stack>
                                        </LocalizationProvider>
                                        <LocalizationProvider dateAdapter={AdapterDateFns}>
                                            <Stack spacing={3}>
                                                <DateTimePicker
                                                    label="Time The Shift End"
                                                    value={endValue}
                                                    inputFormat="dd/MM/yyyy HH:mm"
                                                    ampm={false}
                                                    onChange={handleEndTime}
                                                    renderInput={(params) => <TextField {...params} />}
                                                />
                                            </Stack>
                                        </LocalizationProvider>
                                    </div>
                                </form>

                                <div className="mt-5 sm:mt-6 sm:grid sm:grid-flow-row-dense sm:grid-cols-2 sm:gap-3">
                                    <button
                                        type="button"
                                        className="inline-flex w-full justify-center rounded-md border border-transparent bg-blue-600 px-4 py-2 text-base font-medium text-white shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 sm:col-start-2 sm:text-sm"
                                        onClick={() => { getData(); setIsOpen(false);}}
                                    >
                                        Create Shift
                                    </button>
                                    <button
                                        type="button"
                                        className="mt-3 inline-flex w-full justify-center rounded-md border border-gray-300 bg-white px-4 py-2 text-base font-medium text-gray-700 shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 sm:col-start-1 sm:mt-0 sm:text-sm"
                                        onClick={() => setIsOpen(false)}
                                        ref={cancelButtonRef}
                                    >
                                        Cancel
                                    </button>
                                </div>
                            </Dialog.Panel>
                        </Transition.Child>
                    </div>
                </div>
            </Dialog>
        </Transition.Root>
    )
}
