import ButtonTheme from "@/components/ButtonTheme"
import { useState } from "react";
import axios from "axios";

export default function RegisterForm(props: any) {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [birthDate, setBirthDate] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [wrongInputs, setWrongInputs] = useState(false);

  const cssUnit = "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500";


  async function handleSubmit(e: any) {
    e.preventDefault();
    if(!(password === confirmPassword)){
      setWrongInputs(true);
      return;
    }
    setWrongInputs(false);
    await axios.post('https://localhost:8080/api/User/register', {
      FirstName: firstName,
      LastName: lastName,
      Email: email,
      Password: password,
      BirthDate: birthDate
    }).then(res => {
      console.log(res);
      alert('Account created successfully!');
      props.viewLogin(true);
    }).catch(err => {
      console.log(err);
      setWrongInputs(true);
    })
  }

  return (

    <form onSubmit={handleSubmit} className="lg:w-1/2 mt-2">
      <div className="mb-6">
        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
          Firstname:
          <input type="text" id="firstName" className={cssUnit} value={firstName} onChange={(e)=>setFirstName(e.target.value)} required />
        </label>
      </div>
      <div className="mb-6">
        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
          Lastname:
          <input type="text" id="lastName" className={cssUnit} value={lastName} onChange={(e)=>setLastName(e.target.value)} required />
        </label>
      </div>
      <div className="mb-6">
        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
          Birth Date:
          <input type="date" id="birthDate" className={cssUnit} value={birthDate} onChange={(e)=>setBirthDate(e.target.value)} required />
        </label>
      </div>
      <div className="mb-6">
        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
          Email:
          <input type="email" id="email" className={cssUnit} placeholder="email@example.com" value={email} onChange={(e)=>setEmail(e.target.value)} required />
        </label>
      </div>
      <div className="mb-6">
        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
          Password:
          <input type="password" id="password" className={cssUnit} value={password} onChange={(e)=>setPassword(e.target.value)} required />
        </label>
      </div>
      <div className="mb-6">
        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
          Confirm Password:
          <input type="password" id="confirmPassword" className={cssUnit} value={confirmPassword} onChange={(e)=>setConfirmPassword(e.target.value)} required />
        </label>
      </div>
      <button type="submit" className="text-white bg-blue-600 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Sign up</button>
      {wrongInputs && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mt-4" role="alert">
        <strong className="font-bold">Oups!</strong>
        <span className="block sm:inline"> Check your data</span>
      </div>}
      <div className="mb-6 mt-6 w-100 flex justify-between">
        <h3 className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Already a user? <span className="underline cursor-pointer hover:text-blue-600 dark:hover:text-blue-600 dark:text-white" onClick={()=>props.viewLogin(true)}>Sign in</span></h3>
        <ButtonTheme />
      </div>
    </form>
  )
}