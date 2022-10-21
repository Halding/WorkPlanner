import React, {useEffect, useState} from 'react';
import {useQuery} from "@tanstack/react-query";
import {Employee} from "../models/Employee";
import axios from "axios";


function TestGetAllEmployee() {

    const [employee, setEmployee] = useState<Employee>();

    const {data, isLoading, isError} = useQuery<Employee[], Error>(["employee"], () => axios
        .get("https://localhost:44396/api/employee")
        .then((res) => res.data));


    const getEmployeeById = async (data: Employee) => {
        const {data: response} = await axios.get(`https://localhost:7293/api/employee/${data.id}`);
        console.log(response);
        setEmployee(response)
        return response

    };

    useEffect( () => {
        getEmployeeById(testEmployee)
    }, []);



    function itWorks() {
        console.log("It works")
    }

    const testEmployee: Employee = {
        id: 7,
        firstName: "Svend",
        lastName: "123",
        departmentId: null,
        role: "123",
        password: "123"
    }


    if (isLoading)
        return <div>Loading</div>

    if (isError)
        return <div>Error</div>

    return (

        <div className="flex justify-center my-2 grid-cols-1 mx-5">

            <div className="grid-rows">

                Test Get All Employee
                <div className="mb-3 xl:w-96">
                    {data.map(e => (
                        <div key={null} className="grid-cols-1">
                            <label>
                                {e.firstName} {e.lastName}
                            </label>
                        </div>

                    ))}
                </div>

                <button
                    onClick={() => {
                        itWorks();
                        getEmployeeById(testEmployee)
                    }}
                    type="button"
                    className="inline-flex justify-center w-full rounded-md border border-transparent m-2
                                    shadow-sm px-4 py-2 bg-red-600 text-base font-medium text-white hover:bg-red-700 focus:outline-none
                                    focus:ring-2 focus:ring-offset-2 focus:ring-red-500 sm:text-sm disabled:bg-gray-300
                                    disabled:cursor-not-allowed hover:disabled:border-gray-300"
                >
                    Get Employee by Id
                </button>

                <label>
                    test {employee?.id}
                </label>

            </div>
        </div>
    )
}

export default TestGetAllEmployee;