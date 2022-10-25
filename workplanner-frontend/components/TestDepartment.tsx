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


    const deleteDepartmenteById = async (data: Department) => {
        const {data: departmentDeletedById} = await axios.delete(`https://localhost:7293/api/Department/delete/${data.id}`);
        console.log(departmentDeletedById)
        setDepartment(departmentDeletedById);
    }

    const getDepartmentById = async (data: Department) => {
        const {data: departmentGotById} = await axios.get(`https://localhost:7293/api/department/${data.id}`);
        console.log(departmentGotById);
        setDepartment(departmentGotById)

    };

    const updateDepartment = async (data: Department) => {
        const {data: updatedDepartment} = await axios.patch(`https://localhost:7293/api/department/update/${data.id}`, data);
        console.log(updatedDepartment)
        setDepartment(updatedDepartment)
        return updatedDepartment.data;
    };



    useEffect(() => {


    }, [department]);

    const testDepartment : Department = {
        id: 2,
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




                <button
                    onClick={() => {

                        updateDepartment(testDepartment)
                    }}
                    type="button"
                    className="inline-flex justify-center w-full rounded-md border border-transparent m-2
                                    shadow-sm px-4 py-2 bg-red-600 text-base font-medium text-white hover:bg-red-700 focus:outline-none
                                    focus:ring-2 focus:ring-offset-2 focus:ring-red-500 sm:text-sm disabled:bg-gray-300
                                    disabled:cursor-not-allowed hover:disabled:border-gray-300"
                >
                    Update Department
                </button>

                <label>
                    test {department?.id} {department?.departmentName}
                </label>




                <button
                    onClick={() => {

                        deleteDepartmenteById(testDepartment)
                    }}
                    type="button"
                    className="inline-flex justify-center w-full rounded-md border border-transparent m-2
                                    shadow-sm px-4 py-2 bg-red-600 text-base font-medium text-white hover:bg-red-700 focus:outline-none
                                    focus:ring-2 focus:ring-offset-2 focus:ring-red-500 sm:text-sm disabled:bg-gray-300
                                    disabled:cursor-not-allowed hover:disabled:border-gray-300"
                >
                    Delete Department
                </button>



            </div>

        </div>
    );
}

export default TestDepartment;