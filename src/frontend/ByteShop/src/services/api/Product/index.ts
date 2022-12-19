import { AxiosError } from "axios";
import { api } from "../../axios-config";
import { IProduct } from "./types";

async function getByParameter(
  parameterName:
    | "sku"
    | "name"
    | "brand"
    | "category"
    | "actualPage"
    | "itemsPerPage",
  { ...parameterValue }: IProduct
): Promise<IProduct[] | Error> {
  try {
    const { data } = await api.get(
      `product?${parameterName}=${parameterValue[parameterName]}`
    );

    if (data) {
      return data.data;
    }
    return new Error("Erro ao listar os as categorias!");
  } catch (error) {
    return error as AxiosError;
  }
}

export const Product = { getByParameter };
