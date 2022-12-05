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
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import {CaEvent} from "../models/CaEvent";
import {Shift} from "../models/Shift";
import {CreateShift} from "../models/CreateShift";
import {end} from "@popperjs/core";




export default function ModalEdit({isOpen, setIsOpen, shiftData}: {shiftData: CaEvent | undefined,isOpen: boolean, setIsOpen: (arg0: boolean) => void}) {

    const cancelButtonRef = useRef(null)
    const [shift, setShift] = useState<Shift>()
    const [startValue, setStartValue] = useState<Date | null>(new Date());
    const [endValue, setEndValue] = useState<Date | null>(new Date());

    const handleStartTime = (newValue: Date | null) => {
        setStartValue(newValue);
    };

    const handleEndTime = (newValue: Date | null) => {
        setEndValue(newValue);
    };

    useEffect(() => {
     getShift()
    }, [shiftData]);




    const postData = async () => {
        const jwt = getCookie("OurJwt")

        getShift()

        if (shift && startValue != null && endValue != null) {
            console.log("se mig ")
            let testShift : CreateShift = {
                id : shift.id,
                startTime : startValue,
                endTime : endValue,
                clockInTime : null,
                clockOutTime : null,
                employeeId : shift.employeeId,
                departmentId: shift.departmentId

            }
            const {data: createdShift} = await axios.patch(`${process.env.NEXT_PUBLIC_BASEURL}shift/update/${shift.id}`,testShift, {
                headers: {
                    Authorization: "Bearer " + jwt
                }
            });

        }

    }

    const DeleteShift = async () => {
        const jwt = getCookie("OurJwt")

        getShift()

        if (shift && startValue != null && endValue != null) {
            let testShift : CreateShift = {
                id : shift.id,
                startTime : startValue,
                endTime : endValue,
                clockInTime : null,
                clockOutTime : null,
                employeeId : shift.employeeId,
                departmentId: shift.departmentId

            }
            const {data: createdShift} = await axios.delete(`${process.env.NEXT_PUBLIC_BASEURL}shift/delete/${shift.id}`,{
                headers: {
                    Authorization: "Bearer " + jwt
                }
            });

        }

    }

    const getShift = async () => {

        const jwt = getCookie("OurJwt")

        if (shiftData) {
            const {data: shiftFromId} = await axios.get(`${process.env.NEXT_PUBLIC_BASEURL}shift/${shiftData.id}`, {
                headers: {
                    Authorization: "Bearer " + jwt
                }
            })
            setShift(shiftFromId)
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
                                        Edit Time on Shift Or Delete.
                                    </label>
                                    <div className="my-2 flex justify-between ">
                                        <LocalizationProvider dateAdapter={AdapterDateFns}>
                                            <Stack spacing={3}>
                                                <DateTimePicker
                                                    label="Time The Shift Start"
                                                    value={shiftData?.startTime}
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
                                                    value={shiftData?.endTime}
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
                                        className="inline-flex w-full justify-center rounded-md border border-transparent bg-red-600 px-4 py-2 text-base font-medium text-white shadow-sm hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-offset-2 sm:col-start-2 sm:text-sm"
                                        onClick={async () => {
                                            await DeleteShift();
                                            setIsOpen(false);
                                        }}
                                    >
                                        Delete Shift
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
                                <div className="mt-5 sm:mt-6">
                                    <button
                                        type="button"
                                        className="inline-flex w-full justify-center rounded-md border border-transparent bg-blue-600 px-4 py-2 text-base font-medium text-white shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 sm:col-start-2 sm:text-sm"
                                        onClick={async () => {
                                            await postData();
                                            setIsOpen(false);
                                        }}
                                    >
                                        Update Shift
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
