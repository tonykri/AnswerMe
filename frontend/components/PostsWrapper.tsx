'use client'
import Post from "./Post"
import { useState, useEffect } from "react"
import axios from "axios"
import CreatePost from "./CreatePost"

export default function PostsWrapper() {
    interface Provider {
        id: string;
    }
    const [posts, setPosts] = useState<Provider[]>([]);
    const [viewCreatePost, setViewCreatePost] = useState(false);
    const [viewDeleted, setViewDeleted] = useState(false);
    const [viewUpdated, setViewUpdated] = useState(false);
    const [reload, setReload] = useState(false);
    const [loadData, setLoadData] = useState("Loading");


    function deletePost(id: any) {
        axios.delete(`https://localhost:8080/api/Post/delete/${id}`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        }).then((res) => {
            let temp = posts.filter(post => post.id !== id);
            setPosts(temp);
            setViewDeleted(true);
        }).catch((err) => {
            console.log(err);
        });
    }

    function handleReload() {
        setReload(!reload);
    }

    useEffect(() => {
        setLoadData("Loading");
        axios.get(`https://localhost:8080/api/Post/viewAllPosts`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        }).then((res) => {
            setPosts(res.data);
            setLoadData("No Posts");
        }).catch((err) => {
            console.log(err);
            setLoadData("Error, Try again");
        });
    }, [reload]);

    return (
        <div className="container mx-auto md:mt-10 mt-5">
            {viewDeleted && <div id="toast-danger" className="sticky m-auto top-5 flex items-center w-full max-w-xs p-4 mb-4 text-gray-500 bg-white rounded-lg shadow dark:text-gray-400 dark:bg-gray-800" role="alert">
                <div className="inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-red-500 bg-red-100 rounded-lg dark:bg-red-800 dark:text-red-200">
                    <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path></svg>
                    <span className="sr-only">Error icon</span>
                </div>
                <div className="ml-3 text-sm font-normal">Item has been deleted.</div>
                <button type="button" className="ml-auto -mx-1.5 -my-1.5 bg-white text-gray-400 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 hover:bg-gray-100 inline-flex h-8 w-8 dark:text-gray-500 dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700" data-dismiss-target="#toast-danger" aria-label="Close" onClick={() => setViewDeleted(false)}>
                    <span className="sr-only">Close</span>
                    <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path></svg>
                </button>
            </div>}
            {viewUpdated && <div id="toast-success" className="sticky m-auto top-5 flex items-center w-full max-w-xs p-4 mb-4 text-gray-500 bg-white rounded-lg shadow dark:text-gray-400 dark:bg-gray-800" role="alert">
                <div className="inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-green-500 bg-green-100 rounded-lg dark:bg-green-800 dark:text-green-200">
                    <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clip-rule="evenodd"></path></svg>
                    <span className="sr-only">Check icon</span>
                </div>
                <div className="ml-3 text-sm font-normal">Item updated successfully.</div>
                <button type="button" className="ml-auto -mx-1.5 -my-1.5 bg-white text-gray-400 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 hover:bg-gray-100 inline-flex h-8 w-8 dark:text-gray-500 dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700" data-dismiss-target="#toast-success" aria-label="Close" onClick={() => setViewUpdated(false)}>
                    <span className="sr-only">Close</span>
                    <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path></svg>
                </button>
            </div>}
            {viewCreatePost ?
                <CreatePost viewCreateForm={setViewCreatePost} reloadPage={handleReload} /> :
                <button type="button" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 ml-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800" onClick={() => setViewCreatePost(true)}>Create Post</button>
            }

            {posts.length > 0 ? posts.map(item => (<Post key={item.id} {...item} deletePost={deletePost} viewUpdatedToast={setViewUpdated} />)) :
                <h1 className="text-3xl mt-20 text-center">{loadData}</h1>
            }

        </div>
    )
}
