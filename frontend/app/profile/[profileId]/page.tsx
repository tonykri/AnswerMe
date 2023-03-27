import NavBar from "@/components/NavBar"
import ProfilePageWrapper from "@/components/ProfilePageWrapper"
export default function Home({ params }: any) {

    return (
        <>
            <NavBar />
            <ProfilePageWrapper profileId={params.profileId}/>
        </>

    )
}
