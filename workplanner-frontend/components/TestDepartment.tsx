import React, {useEffect, useState} from 'react';
import {Department} from "../models/Department";
import {useQuery} from "@tanstack/react-query";
import axios from "axios";
import {Employee} from "../models/Employee";

function TestDepartment() {

    const {data, isLoading, isError} = useQuery<Department[], Error>(["deployment"], () => axios
        .get("https://localhost:7293/api/department")
        .then((res) => res.data));


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

            </div>
        </div>
    );
}

export default TestDepartment;