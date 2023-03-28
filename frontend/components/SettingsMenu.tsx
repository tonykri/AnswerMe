'use client'
import { Button, Modal } from "flowbite-react";
import React from "react";
import { useState } from "react";
import axios from "axios";
import { useRouter } from "next/navigation";


export default function SettingsMenu(props: any) {
    const [show, setShow] = useState(false);
    const router = useRouter();

    async function handleDelete(event: any) {
        await axios.delete('https://localhost:8080/api/User/deleteAccount', {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res => {
            alert("Account deleted successfully");
            localStorage.removeItem('token');
            localStorage.removeItem('firstname');
            localStorage.removeItem('lastname');
            localStorage.removeItem('email');
            localStorage.removeItem('birthdate');
            router.push('/');
        }).catch(err => {
            console.log(err);
            alert(err);
        })
    }

    return (
        <div className="p-5">
            <div className="m-5">
                <Button onClick={() => props.setShowChangePassword(true)}>
                    Change Password
                </Button>
            </div>
            <div className="p-5">
                <React.Fragment>
                    <Button onClick={() => setShow(true)} color="failure">
                        Delete Account
                    </Button>
                    <Modal
                        show={show}
                        size="md"
                        popup={true}
                        onClose={() => setShow(false)}
                    >
                        <Modal.Header />
                        <Modal.Body>
                            <div className="text-center">
                                <h3 className="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
                                    Are you sure you want to delete your account?
                                </h3>
                                <div className="flex justify-center gap-4">
                                    <Button
                                        color="failure"
                                        onClick={handleDelete}
                                    >
                                        Yes, I'm sure
                                    </Button>
                                    <Button
                                        color="gray"
                                        onClick={() => setShow(false)}
                                    >
                                        No, cancel
                                    </Button>
                                </div>
                            </div>
                        </Modal.Body>
                    </Modal>
                </React.Fragment>
            </div>

        </div>
    )
}