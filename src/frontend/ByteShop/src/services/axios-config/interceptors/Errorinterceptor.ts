import {AxiosError} from "axios";

export const errorinterceptor =(error: AxiosError) =>{
    if (error.message ==='Network Erro'){
        return Promise.reject(new Error('Erro de conexão'));
    }
    Promise.reject(error);
};