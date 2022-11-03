import React from 'react';
import {signIn} from "next-auth/react";
import TestAll from "../../components/TestAll";

function Test() {
    return (
        <div>
            <button onClick={() => {
                signIn()
            }}> login
            </button>
            <TestAll></TestAll>
        </div>
    );
}

export default Test;