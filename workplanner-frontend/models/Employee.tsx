import {number} from "prop-types";

export type Employee = {
    id: number,
    employeeNumber: number,
    "firstName": string,
    lastName: string,
    departmentId: null,
    department: null,
    role: string,
    passwordSalt: null,
    passwordHash: null
};