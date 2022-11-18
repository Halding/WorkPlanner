import React, {useEffect, useState} from 'react';
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import { MobileDatePicker } from '@mui/x-date-pickers/MobileDatePicker';

function CustomPicker({label} : {label : string}) {
    const [value, setValue] = useState<Date |null>(new Date());

    const handleChange = (newValue: Date | null) => {
        setValue(newValue);
    };

    useEffect(() => {

    }, [value]);


    return (
        <LocalizationProvider dateAdapter={AdapterDateFns}>
            <Stack spacing={3}>
                {/*<DesktopDatePicker*/}
                {/*    label="Date desktop"*/}
                {/*    inputFormat="dd/mm/yyyy"*/}
                {/*    value={value}*/}
                {/*    onChange={handleChange}*/}
                {/*    renderInput={(params) => <TextField {...params} />}*/}
                {/*/>*/}
                {/*<MobileDatePicker*/}
                {/*    label="Date mobile"*/}
                {/*    inputFormat="MM/DD/YYYY"*/}
                {/*    value={value}*/}
                {/*    onChange={handleChange}*/}
                {/*    renderInput={(params) => <TextField {...params} />}*/}
                {/*/>*/}
                {/*<TimePicker*/}
                {/*    label="Time"*/}
                {/*    value={value}*/}
                {/*    onChange={handleChange}*/}
                {/*    renderInput={(params) => <TextField {...params} />}*/}
                {/*/>*/}
                <DateTimePicker
                    label={label}
                    value={value}
                    ampm={false}
                    onChange={handleChange}
                    renderInput={(params) => <TextField {...params} />}
                />
            </Stack>
        </LocalizationProvider>
    );
}

export default CustomPicker;