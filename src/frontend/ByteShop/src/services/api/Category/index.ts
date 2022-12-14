import { api } from "../../axios-config";
import { Icategory } from "./types";
import { AxiosError } from "axios";


type TCategory = {
  data: Icategory[];
};

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

export const Category = { getAll };
