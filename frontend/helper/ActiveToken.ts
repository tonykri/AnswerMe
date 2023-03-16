'use client'
import { useRouter } from "next/navigation";
import axios from "axios";

export default function ActiveToken() {
  const router = useRouter();
  const token = localStorage.getItem('token');

  axios.get('https://localhost:8080/api/User/activeToken', {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  }).catch(() => {
    router.push('/');
  });

  return (null);
}