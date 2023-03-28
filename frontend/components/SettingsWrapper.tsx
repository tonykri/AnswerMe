'use client'
import ChangePasswordForm from "./ChangePasswordForm";
import SettingsMenu from "./SettingsMenu";
import { useState } from "react";

export default function SettingsWrapper() {
const [showChangePassword, setShowChangePassword] = useState(false);

    return (
        <div className="container h-full max-h-screen mx-auto mt-5 lg:flex">
            <div className="lg:w-2/6 lg:h-full w-full h-2/5 lg:flex">
                <SettingsMenu setShowChangePassword={setShowChangePassword}/>
            </div>
            <div className="lg:w-4/6 lg:h-full w-full h-3/5">
                {showChangePassword && <ChangePasswordForm />}
            </div>
        </div>
    )
}