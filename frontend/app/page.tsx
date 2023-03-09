import FormWrapper from "@/components/FormWrapper"

export default function Home() {

  return (
    <>
      <div className="h-screen lg:flex">
        <div className="w-screen h-1/4 lg:w-1/2 lg:h-screen bg-blue-300 items-center justify-center flex">
          <h1 className="text-4xl font-bold text-white text-center">
            Welcome to the <span className="text-blue-600">AnswerMe</span>
          </h1>
        </div>
        <div className="w-screen  lg:w-1/2 lg:h-screen items-center justify-center flex">
          <FormWrapper />
        </div>
      </div>
    </>

  )
}
