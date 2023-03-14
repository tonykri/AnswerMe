import { useRouter } from "next/navigation";
import axios from "axios";


export default function ActiveToken(){
    const router = useRouter();

    if (!localStorage.getItem('token')) {
        router.push('/');
    } else {
        axios.get('https://localhost:8080/api/User/activeToken', {
            headers: {
              'Authorization': `Bearer ${localStorage.getItem('token')}` 
            }
          }).catch(() => {
            router.push('/');
        });
    }
}