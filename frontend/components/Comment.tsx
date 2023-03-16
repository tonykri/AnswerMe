'use client'
import { Card } from "flowbite-react"
import { useState } from "react";
import axios from "axios";

export default function Comment(props: any) {
    const [editMode, setEditMode] = useState(false);
    const [wrongInput, setWrongInput] = useState("");
    const [content, setContent] = useState(props.content);
    const [tempContent, setTempContent] = useState(props.content);


    function handleDelete() {
        axios.delete(`https://localhost:8080/api/Comment/${props.id}`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        }).then((res) => {
            props.reloadPage();
            props.viewDeleted(true);
        }).catch((err) => {
            console.log(err);
        });
    }

    function handleEditMode(e: any) {
        e.stopPropagation();
        if (!editMode) {
            setEditMode(!editMode);
            return;
        }
        axios.put(`https://localhost:8080/api/Comment/update/${props.id}`,
            {
                "Content": tempContent
            }, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        }).then((res) => {
            setContent(tempContent);
            setEditMode(false);
            setWrongInput("");
            props.viewUpdated(true);
        }).catch((err) => {
            console.log(err);
            setWrongInput("border-red-800 dark:border-red-800");
        });
    }

    return (
        <Card className="m-2">

            <div className="flex items-center justify-end">

                {(props.user.email === localStorage.getItem('email')) && <div>
                    <button type="button" className="text-white bg-blue-700 w-8 rounded-lg p-1 hover:bg-blue-800 transition-all duration-100 m-1" onClick={handleEditMode}>
                        {editMode ?
                            <svg fill="none" stroke="currentColor" stroke-width="1.5" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M4.5 12.75l6 6 9-13.5"></path>
                            </svg> :
                            <svg fill="none" stroke="currentColor" stroke-width="1.5" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M16.862 4.487l1.687-1.688a1.875 1.875 0 112.652 2.652L6.832 19.82a4.5 4.5 0 01-1.897 1.13l-2.685.8.8-2.685a4.5 4.5 0 011.13-1.897L16.863 4.487zm0 0L19.5 7.125"></path>
                            </svg>}
                    </button>
                    {editMode ?
                        <button type="button" className="text-white bg-red-700 w-8 rounded-lg p-1 hover:bg-red-800 transition-all duration-100" onClick={(e) => { setEditMode(!editMode); setWrongInput("") }}>
                            <svg fill="none" stroke="currentColor" stroke-width="1.5" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"></path>
                            </svg>
                        </button> :
                        <button type="button" className="text-white bg-red-700 w-8 rounded-lg p-1 hover:bg-red-800 transition-all duration-100" onClick={handleDelete}>
                            <svg fill="none" stroke="currentColor" stroke-width="1.5" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0"></path>
                            </svg>
                        </button>}
                </div>}

            </div>
            {editMode ?
                <input className={wrongInput + " block bg-white w-full border border-slate-300 dark:border-gray-500 rounded-md py-2 pl-2 pr-2 shadow-sm focus:outline-none focus:border-sky-500 focus:ring-sky-500 focus:ring-1 sm:text-sm dark:bg-slate-700"} type="text" name="updateContent" value={tempContent} onChange={(e) => setTempContent(e.target.value)} /> :
                <p className="font-normal text-gray-700 dark:text-gray-400">
                    {content}
                </p>}
            <div className="flex justify-between border-t-2">
                <div>
                    <h5 className="text-md tracking-tight font-normal text-gray-700 dark:text-gray-400">
                        {props.user.firstname} {props.user.lastname}
                    </h5>
                </div>

                <div className="items-center flex">
                    <div>
                        <h5 className="text-md tracking-tight font-normal text-gray-700 dark:text-gray-400">
                            Posted: {new Date(props.created).toDateString()}
                        </h5>
                        {props.updated > props.created && <h5 className="text-md tracking-tight font-normal text-gray-700 dark:text-gray-400">
                            Updated: {new Date(props.updated).toDateString()}
                        </h5>}
                    </div>
                </div>
            </div>
        </Card>
    )
}
