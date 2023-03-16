'use client'
import { useState } from "react"
import axios from "axios";

export default function CommentCreate(props:any) {
    const [comment, setComment] = useState("");

    function handleSubmit(e:any){
        e.preventDefault();
        axios.post(`https://localhost:8080/api/Comment/create/${props.postId}`,
        {
            "Content": comment
        }, {
            headers: {
              'Authorization': `Bearer ${localStorage.getItem('token')}` 
            }
          }).then((res)=>{
            setComment("")
            props.reloadPage();
            props.viewCreated(true);
          }).catch((err) => {
            console.log(err);
        });
    }

    return (
        <form onSubmit={handleSubmit} className="flex items-center m-2 sticky top-1">
            <div className="relative w-full">
                <input type="text" id="simple-search" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full pl-2 p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" 
                placeholder="Create Comment" required value={comment} onChange={(e)=>setComment(e.target.value)}/>
            </div>
            <button type="submit" className="p-2.5 ml-2 text-sm font-medium text-white bg-blue-700 rounded-lg border border-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" className="w-6 h-6">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M12 4.5v15m7.5-7.5h-15" />
                </svg>
                <span className="sr-only">Create</span>
            </button>
        </form>
    )
}