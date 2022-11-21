export type CreateShift = {
    id : number,
    startTime: Date,
    endTime: Date,
    clockInTime: null | Date,
    clockOutTime: null | Date,
    employeeId: number,
    departmentId: number,

};