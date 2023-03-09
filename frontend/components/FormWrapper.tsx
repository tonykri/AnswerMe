'use client';

import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";
import { useState } from "react";

export default function FormWrapper() {
    const [viewLoginPage, setViewLoginPage] = useState(true);
    return (
        <>
            {viewLoginPage===true? <LoginForm viewLogin={setViewLoginPage}/> : <RegisterForm viewLogin={setViewLoginPage}/>}
        </>
    );
}