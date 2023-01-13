import { api } from "../../axios-config";
import { Icategory } from "./types";
import { AxiosError } from "axios";

async function post(
  parameters: Icategory
): Promise<Icategory | Error | number> {
  const { name, parentCategoryId } = parameters;
  try {
    const { status } = await api.post("category", { name, parentCategoryId });

    if (status) {
      return status;
    }
    return new Error("Erro ao atualizar os as categoria!");
  } catch (error) {
    return error as AxiosError;
  }
}

async function put(
  parameters: Icategory
): Promise<Icategory[] | Error | number> {
  const { id } = parameters;

  try {
    const { status } = await api.put(`category/${id}`, parameters);

    if (status) {
      return status;
    }
    return new Error("Erro ao atualizar os as categoria!");
  } catch (error) {
    return error as AxiosError;
  }
}

async function getAll(): Promise<Icategory[] | Error> {
  try {
    const { data } = await api.get("category");

    if (data) {
      return data;
    }
    return new Error("Erro ao listar os as categorias!");
  } catch (error) {
    return error as AxiosError;
  }
}

async function deleteById(id: number): Promise<any | Error> {
  try {
    const { status } = await api.delete(`category/${id}`);

    if (status) {
      return status;
    }
    return new Error("Erro ao deletar a categoria!");
  } catch (error) {
    return error as AxiosError;
  }
}

export const Category = { getAll, deleteById, put, post };
