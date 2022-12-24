export interface IProductGet {
  sku?: string;
  name?: string;
  brand?: string;
  category?: string;
  actualPage?: number;
  itemsPerPage?: number;
}

export interface IImgsJson {
  id?: string;
  base64: string;
  extension: string;
}

export interface IProductPost {
  name: string;
  description: string;
  sku: string;
  price: number;
  costPrice: number;
  stock: number;
  warranty: number;
  brand: string;
  weight: number;
  height: number;
  length: number;
  width: number;
  categoryId: number;
  mainImageBase64: IImgsJson;
  secondaryImagesBase64: IImgsJson[];
}
