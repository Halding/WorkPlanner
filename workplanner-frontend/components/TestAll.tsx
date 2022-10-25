import React from 'react';
import {useQuery} from "@tanstack/react-query";
import {Employee} from "../models/Employee";
import TestGetAllEmployee from "./TestGetAllEmployee";
import TestCreateEmployee from "./TestCreateEmployee";
import TestDepartment from "./TestDepartment";





function TestAll() {



    return (
        <div className="flex">

            <TestGetAllEmployee></TestGetAllEmployee>

            <TestCreateEmployee></TestCreateEmployee>

            <TestDepartment></TestDepartment>

        </div>
    );
}

export default TestAll;