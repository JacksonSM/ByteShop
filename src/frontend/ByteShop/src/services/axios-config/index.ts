import axios from "axios"
import { errorinterceptor } from "./interceptors/Errorinterceptor";
import { responseinterceptor } from "./interceptors/Responseinterceptor";


const api = axios.create({
    headers:{ 'Content-Type': 'application/json'},
    baseURL:"https://localhost:7069/api/"}
    );

 api.interceptors.response.use(
    (reponse) => responseinterceptor(reponse),
    (error) => errorinterceptor(error),
 )   


export {api};