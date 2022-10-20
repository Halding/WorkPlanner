import React from 'react';
import {useQuery} from "@tanstack/react-query";
import {Employee} from "../models/Employee";
import axios from "axios";


function TestGetAllEmployee() {

    const {data, isLoading, isError} = useQuery<Employee[], Error>(["employee"], () => axios
        .get("https://localhost:7293/api/employee")
        .then((res) => res.data));


    if (isLoading)
        return <div>Loading</div>

    if (isError)
        return <div>Error</div>

    return (


        <div className="flex justify-center my-2 grid-cols-1">

            {/*Test Create Employee*/}


            {/*Test Get All Employee*/}
            <div className="mb-3 xl:w-96">
                {data.map(e => (
                    <div key={null} className="grid-cols-1">
                        <label>
                            {e.firstName} {e.lastName}
                        </label>
                    </div>

                ))}
            </div>
        </div>
    )
}

export default TestGetAllEmployee;