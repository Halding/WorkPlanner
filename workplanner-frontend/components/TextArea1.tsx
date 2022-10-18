import React from 'react';
import {type} from "os";
import {number} from "prop-types";
import {useQuery} from "@tanstack/react-query";
import {list} from "postcss";
import {Employee} from "../models/Employee";





function TextArea1() {



    const {data, isLoading, isError} = useQuery<Employee[],Error>(["employee"],() =>
        fetch(`${process.env.NEXT_PUBLIC_BASEURL}employee`).then((res) => res.json()))



    if (isLoading)
    return <div>Loading</div>

    if (isError)
        return <div>Error</div>

    return (
        <div className="flex justify-center">
            <div className="mb-3 xl:w-96">
                {data.map(e =>(
                    <div className="grid-cols-1">
                        <label>
                            {e.id} {e.firstName} {e.lastName}
                        </label>
                    </div>

                ))}


            </div>
        </div>
    );
}




export default TextArea1;