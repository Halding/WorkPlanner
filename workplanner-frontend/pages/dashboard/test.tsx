import React from 'react';
import {signIn} from "next-auth/react";
import TestAll from "../../components/TestAll";

function Test() {
    return (
        <div>
            <TestAll></TestAll>
        </div>
    );
}

export default Test;