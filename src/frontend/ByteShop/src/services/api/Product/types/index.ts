export interface IProductGet {
  id?: number;
  sku?: string;
  name?: string;
  description?: string;
  price?: number;
  costPrice?: number;
  brand?: string;
  weight?:  number,
  height?:  number,
  length?:  number,
  width?: number,
  mainImageUrl?: string;
  secondaryImageUrl?: Array<string>;
  category?: {
    id: number,
    name: string,
    parentCategoryId?: number;
  }
  pagination?: {
    actualPage: number,
    itemsPerPage: number,
    itemsTotal: number,
    totalPage: number
  }

  warranty?: number;
  stock?: number;
  actualPage?: number;
  itemsPerPage?: number;
  createdOn?: Date;
}


export interface IDataProductList {
  content: IProductGet[];
  pagination?: {
    actualPage: number,
    itemsPerPage: number,
    itemsTotal: number,
    totalPage: number,
  }
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
