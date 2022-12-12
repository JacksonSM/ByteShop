import { api } from "../../axios-config";

async function getAll(): Promise<void | Error> {
  try {
    const { data } = await api.get("category");

    if (data) return data.data;
  } catch (error) {
    throw new Error(
      (error as { message: string }).message || "Erro ao listar as categorias!"
    );
  }
}

export const Category = { getAll };
