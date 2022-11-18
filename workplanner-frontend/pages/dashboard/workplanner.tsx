import React from 'react';
import Layout from "../../components/Layout";
import WpCalendar from "../../components/WPCalendar";
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns'
import {LocalizationProvider} from "@mui/x-date-pickers";

function Workplanner() {
    return (
        <Layout>
            <WpCalendar></WpCalendar>
        </Layout>
    );
}

export default Workplanner;