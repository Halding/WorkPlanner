import React, {useEffect, useState} from 'react';
import {Department} from "../models/Department";
import {useQuery} from "@tanstack/react-query";
import axios from "axios";
import {Employee} from "../models/Employee";
import {number} from "prop-types";

function TestDepartment() {

    const [department, setDepartment] = useState<Department>()


    const {data, isLoading, isError} = useQuery<Department[], Error>(["department"], () => axios
        .get("https://localhost:7293/api/department")
        .then((res) => res.data));


    const getDepartmentById = async (data: Department) => {
        const {data: departmentGotById} = await axios.get(`https://localhost:7293/api/department/${data.id}`);
        console.log(departmentGotById);
        setDepartment(departmentGotById)

    };


    useEffect(() => {
        getDepartmentById(testDepartment)
    }, []);

    const testDepartment : Department = {
        id: 3,
        departmentName: "Kassen"
    }




    if (isLoading)
        return <div>Loading</div>

    if (isError)
        return <div>Error</div>


    return (

        <div className="flex justify-center my-2 grid-cols-1 mx-5">
            <div className="grid-rows">

                Test Get All Department
                <div className="mb-3 xl:w-96">
                    {data.map(d => (
                        <div key={null} className="grid-cols-1">
                            <label>
                                {d.id} {d.departmentName}
                            </label>
                        </div>

                    ))}
                </div>

                <button
                    onClick={() => {

                        getDepartmentById(testDepartment)
                    }}
                    type="button"
                    className="inline-flex justify-center w-full rounded-md border border-transparent m-2
                                    shadow-sm px-4 py-2 bg-red-600 text-base font-medium text-white hover:bg-red-700 focus:outline-none
                                    focus:ring-2 focus:ring-offset-2 focus:ring-red-500 sm:text-sm disabled:bg-gray-300
                                    disabled:cursor-not-allowed hover:disabled:border-gray-300"
                >
                    Get Department by Id
                </button>

                <label>
                    test {department?.id} {department?.departmentName}
                </label>

            </div>

        </div>
    );
}

export default TestDepartment;