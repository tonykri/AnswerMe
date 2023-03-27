'use client'
import { Card } from "flowbite-react";


export default function ProfileInfo(props: any) {
    return (
        <div className="max-w-sm m-auto w-4/5 mt-1">
            <Card>

                <div className="flex flex-col items-center pb-10">
                    <img
                        className="mb-3 h-24 w-24 rounded-full shadow-lg object-fill"
                        src="https://flowbite.com/docs/images/people/profile-picture-3.jpg"
                        alt="Bonnie image"
                    />
                    <h5 className="mb-1 text-xl font-medium text-gray-900 dark:text-white">
                        {props.firstname} {props.lastname}
                    </h5>
                    <span className="text-sm text-gray-500 dark:text-gray-400">
                        Email: {props.email}
                    </span>
                    <span className="text-sm text-gray-500 dark:text-gray-400">
                        BirthDate: {props.birthdate}
                    </span>
                </div>
            </Card>
        </div>
    )
}