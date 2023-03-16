'use client'
import { useEffect, useState } from "react";
import axios from "axios";
import PostInComments from "./PostInComments";
import Comment from "./Comment";
import CommentCreate from "./CommentCreate";

export default function PostCommentsWrapper(props: any) {
    interface Provider {
        id: string;
      }
    const [data, setData] = useState({ id: 1 })
    const [comments, setComments] = useState<Provider[]>([])
    const [readyPost, setReadyPost] = useState(false)
    const [viewUpdated, setViewUpdated] = useState(false);
    const [viewDeleted, setViewDeleted] = useState(false);
    const [viewCreated, setViewCreated] = useState(false);
    const [reload, setReload] = useState(false);

    function handleReload(){
        setReload(!reload);
    }


    useEffect(() => {
        axios.get(`https://localhost:8080/api/Post/${props.postId}`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        }).then((res) => {
            setData(res.data)
            setComments(res.data.comments)
            setReadyPost(true)
        }).catch((err) => {
            console.log(err);
        });
    }, [reload]);

    return (
        <div >
            <div className="container mx-auto md:mt-10 mt-5 lg:flex">
                <div className="mx-2 my-2 lg:w-1/2 h-full">
                    {readyPost && <PostInComments {...data} />}
                    {viewCreated && <div id="toast-success" className=" m-auto mt-5 flex items-center w-full max-w-xs p-4 mb-4 text-gray-500 bg-white rounded-lg shadow dark:text-gray-400 dark:bg-gray-800" role="alert">
                        <div className="inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-green-500 bg-green-100 rounded-lg dark:bg-green-800 dark:text-green-200">
                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clip-rule="evenodd"></path></svg>
                            <span className="sr-only">Check icon</span>
                        </div>
                        <div className="ml-3 text-sm font-normal">Comment Created successfully.</div>
                        <button type="button" className="ml-auto -mx-1.5 -my-1.5 bg-white text-gray-400 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 hover:bg-gray-100 inline-flex h-8 w-8 dark:text-gray-500 dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700" data-dismiss-target="#toast-success" aria-label="Close" onClick={() => setViewCreated(false)}>
                            <span className="sr-only">Close</span>
                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path></svg>
                        </button>
                    </div>}
                    {viewUpdated && <div id="toast-success" className=" m-auto mt-5 flex items-center w-full max-w-xs p-4 mb-4 text-gray-500 bg-white rounded-lg shadow dark:text-gray-400 dark:bg-gray-800" role="alert">
                        <div className="inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-green-500 bg-green-100 rounded-lg dark:bg-green-800 dark:text-green-200">
                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clip-rule="evenodd"></path></svg>
                            <span className="sr-only">Check icon</span>
                        </div>
                        <div className="ml-3 text-sm font-normal">Comment updated successfully.</div>
                        <button type="button" className="ml-auto -mx-1.5 -my-1.5 bg-white text-gray-400 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 hover:bg-gray-100 inline-flex h-8 w-8 dark:text-gray-500 dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700" data-dismiss-target="#toast-success" aria-label="Close" onClick={() => setViewUpdated(false)}>
                            <span className="sr-only">Close</span>
                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path></svg>
                        </button>
                    </div>}
                    {viewDeleted && <div id="toast-danger" className="m-auto mt-5 flex items-center w-full max-w-xs p-4 mb-4 text-gray-500 bg-white rounded-lg shadow dark:text-gray-400 dark:bg-gray-800" role="alert">
                        <div className="inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-red-500 bg-red-100 rounded-lg dark:bg-red-800 dark:text-red-200">
                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path></svg>
                            <span className="sr-only">Error icon</span>
                        </div>
                        <div className="ml-3 text-sm font-normal">Comment has been deleted.</div>
                        <button type="button" className="ml-auto -mx-1.5 -my-1.5 bg-white text-gray-400 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 hover:bg-gray-100 inline-flex h-8 w-8 dark:text-gray-500 dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700" data-dismiss-target="#toast-danger" aria-label="Close" onClick={() => setViewDeleted(false)}>
                            <span className="sr-only">Close</span>
                            <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path></svg>
                        </button>
                    </div>}
                </div>

                <div className="lg:w-1/2 lg:h-full lg:max-h-screen dark:bg-slate-500 bg-slate-100 mt-8 py-1 lg:my-2 rounded-lg overflow-auto">
                    <CommentCreate postId={data.id}  reloadPage={handleReload} viewCreated={setViewCreated}/>
                    {comments.length>0? comments.map(com => (<Comment key={com.id} {...com} viewUpdated={setViewUpdated} viewDeleted={setViewDeleted} reloadPage={handleReload}/>)):
                    <h1 className="text-3xl my-20 text-center">No Comments</h1>}
                </div>
            </div>
        </div>

    )
}