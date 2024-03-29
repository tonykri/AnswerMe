import PostsWrapper from "@/components/PostsWrapper"
import NavBar from "@/components/NavBar"
import ActiveToken from "@/helper/ActiveToken"

export default function Home() {

    return (
        <>
            <ActiveToken />
            <NavBar />
            <PostsWrapper />
        </>
    )
}
