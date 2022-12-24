import axios, { AxiosError } from "axios";
import { api } from "../../axios-config";
import { IProductGet, IProductPost } from "./types";

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

async function getById(id: number): Promise<IProductGet[] | Error> {
  try {
    const { data } = await api.get(`product/${id}`);

    if (data) {
      return data.data;
    }
    return new Error("Erro ao listar os as categorias!");
  } catch (error) {
    return error as AxiosError;
  }
}

async function post({ ...attribute }: IProductPost): Promise<any | Error> {
  const values: IProductPost | any = {
    name: attribute.name,
    description: attribute.description,
    sku: attribute.sku,
    price: attribute.price,
    costPrice: attribute.costPrice,
    stock: attribute.stock,
    warranty: attribute.warranty,
    brand: attribute.brand,
    weight: attribute.weight,
    height: attribute.weight,
    length: attribute.length,
    width: attribute.width,
    categoryId: attribute.categoryId,
    mainImageBase64: attribute.mainImageBase64,
    secondaryImagesBase64: attribute.secondaryImagesBase64,
  };
  try {
    const { data } = await api.post("product", values);
    if (data) {
      return { data };
    }
    return new Error("Erro ao cadastrar o produto");
  } catch (error) {
    return new Error(
      (error as { message: string }).message || "Erro ao cadastrar o produto"
    );
  }
}

export const Product = { getByParameter, post, getById };
