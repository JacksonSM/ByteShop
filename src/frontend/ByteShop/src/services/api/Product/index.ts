import { AxiosError } from "axios";
import { api } from "../../axios-config";
import { IProductGet, IProductPost, IProductPut, IResponseProduct } from "./types";

type IParameters = {
  [key: string]: IProductGet;
  value?: any;
};

async function get(
  parameters: IParameters | ""
): Promise<IResponseProduct | Error> {
  try {
    let queryString = "";

    for (const [key, value] of Object.entries(parameters)) {
      queryString += `${key}=${value[key]}&`;
    }
    queryString = queryString.slice(0, -1); // remove o último "&"

    const { data } = await api.get(`product?${queryString}`);

    if (data) {
      return data;
    }
    return new Error("Erro ao listar os as categorias!");
  } catch (error) {
    return error as AxiosError;
  }
}

async function getById(id: number): Promise<IProductGet | Error> {
  try {
    const { data } = await api.get(`product/${id}`);

    if (data) {
      return data;
    }
    return new Error("Erro ao listar os as categorias!");
  } catch (error) {
    return error as AxiosError;
  }
}

async function post(parameters: IProductPost): Promise<IResponseProduct | Error> {
  try {
    const { data } = await api.post("product", parameters);
    if (data) {
      return data;
    }
    return new Error("Erro ao cadastrar o produto");
  } catch (error) {
    return new Error(
      (error as { message: string }).message || "Erro ao cadastrar o produto"
    );
  }
}

async function put(parameters: IProductPut): Promise<IResponseProduct | Error> {
  const {id} = parameters;
  try {
    const { data } = await api.put(`product/${id}`, parameters);
    if (data) {
      return data;
    }
    return new Error("Erro ao atualizar o produto");
  } catch (error) {
    return new Error(
      (error as { message: string }).message || "Erro ao atualizar o produto"
    );
  }
}

async function InativeById(id: number): Promise<number | Error> {
  try {
    const { status } = await api.delete(`product/${id}`);

    if (status) {
      return status;
    }
    return new Error("Erro ao inativar o produto!");
  } catch (error) {
    return error as AxiosError;
  }
}

export const Product = { get, post, getById, put, InativeById };
