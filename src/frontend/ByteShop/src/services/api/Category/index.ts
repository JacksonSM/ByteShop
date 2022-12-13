import { api } from "../../axios-config";
import { Icategory } from "./types";

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
    throw new Error(
      (error as { message: string }).message || "Erro ao listar as categorias!"
    );
  }
}

export const Category = { getAll };
