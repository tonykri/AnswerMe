'use client'
import ProfileInfo from "./ProfileInfo"
import { useEffect, useState } from "react";
import axios from "axios";
import ProfilePosts from "./ProfilePosts";
import { useRouter } from "next/navigation";

export default function ProfilePageWrapper(props: any) {
    const router = useRouter();
    interface Provider {
        firstname: string;
        email: string;
        lastname: string;
        birthdate: string;
        posts: Object[];
    }
    const [data, setData] = useState<Provider>(Object)
    useEffect(() => {
        axios.get(`https://localhost:8080/api/User/profile/${props.profileId}`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        }).then((res) => {
            setData(res.data)
        }).catch((err) => {
            router.push('/home');
            alert("User not found");
        });
    }, []);

    return (
        <div className="container h-full max-h-screen mx-auto mt-5 lg:flex">
            <div className="lg:w-1/2 lg:h-full w-full h-1/2 lg:flex">
                <ProfileInfo firstname={data.firstname} lastname={data.lastname} email={data.email} birthdate={data.birthdate} />
            </div>
            <div className="lg:w-1/2 lg:h-full w-full h-1/2">
                <ProfilePosts posts={data.posts} profileId={props.profileId}/>
            </div>
        </div>
    )

}