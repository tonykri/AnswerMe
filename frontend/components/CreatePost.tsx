'use client'
import { useState } from "react";
import axios from "axios";

export default function CreatePost(props: any) {
    const [wrongInput, setWrongInput] = useState(false);
    const [title, setTitle] = useState("");
    const [content, setContent] = useState("");

    const cssUnit = "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500";

    function createPost(e: any){
        e.preventDefault();
        axios.post(`https://localhost:8080/api/Post/create`,
        {
            "Title": title,
            "Content": content
        }, {
            headers: {
              'Authorization': `Bearer ${localStorage.getItem('token')}` 
            }
          }).then((res)=>{
            setTitle("");
            setContent("");
            setWrongInput(false);
            props.viewCreateForm(false);
            props.reloadPage();
          }).catch((err) => {
            console.log(err);
            setWrongInput(true);
        });
    }

    return (
        <form onSubmit={createPost} className="lg:w-1/2 m-2 mb-5 dark:bg-slate-500 bg-slate-100 p-5 rounded-lg">
            <div className="mb-6">
                <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
                    Title:
                    <input type="text" id="title" className={cssUnit} value={title} onChange={(e) => setTitle(e.target.value)} required />
                </label>
            </div>
            <div className="mb-6">
                <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
                    Content:
                    <input type="text" id="content" className={cssUnit} value={content} onChange={(e) => setContent(e.target.value)} required />
                </label>
            </div>
            {wrongInput && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mt-4" role="alert">
                <strong className="font-bold">Oups!</strong>
                <span className="block sm:inline"> Your input is small</span>
            </div>}
            <button type="submit" className="text-white bg-blue-600 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 my-2">Create</button>

            <button type="button" className="text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-800 my-2 sm:mx-2" onClick={() => props.viewCreateForm(false)}>Cancel</button>

        </form>
    )
}