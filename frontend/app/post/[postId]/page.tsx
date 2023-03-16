import NavBar from "@/components/NavBar"
import PostCommentsWrapper from "@/components/PostCommentsWrapper"
import ActiveToken from "@/helper/ActiveToken"

export default async function Home({ params }: any) {

    return (
        <>
            <ActiveToken />
            <NavBar />
            <PostCommentsWrapper postId={params.postId} />
        </>
    )
}
