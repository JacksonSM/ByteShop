import { AxiosError } from "axios";
import { api } from "../../axios-config";
import { IProductGet, IProductPost, IResponseProduct } from "./types";

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

export const Product = { get, post, getById };
