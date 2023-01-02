import { api } from "../../axios-config";
import { Icategory, IcategoryPut } from "./types";
import { AxiosError } from "axios";


type TCategory = {
  data: Icategory[];
};


async function put({id, name, parentCategoryId}:Icategory): Promise<Icategory[] | Error| number> {
  const parameters:IcategoryPut = {
    name: name,
    parentCategoryId: parentCategoryId
  }

  try {
    const { status } = await api.put(`category/${id}`,parameters);

    if (status) {
      return status
    };
    return new Error("Erro ao atualizar os as categoria!");
  } catch (error) {
    return error as AxiosError;
  }
}


async function getAll(): Promise<Icategory[] | Error> {
  try {
    const { data } = await api.get("category");

    if (data) {
      return data.data
    };
    return new Error("Erro ao listar os as categorias!");
  } catch (error) {
    return error as AxiosError;
  }
}

export const Category = { getAll, put };
