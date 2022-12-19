import { AxiosError } from "axios";
import { api } from "../../axios-config";
import { IProductGet } from "./types";

async function getByParameter(
  parameterName:
    | "sku"
    | "name"
    | "brand"
    | "category"
    | "actualPage"
    | "itemsPerPage",
  { ...parameterValue }: IProductGet
): Promise<IProductGet[] | Error> {
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
