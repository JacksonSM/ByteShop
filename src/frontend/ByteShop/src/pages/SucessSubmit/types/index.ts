import { IImgsJson } from "services/api/Product/types";

export interface IpropsSucessSubmit {
  name?: string;
  description?: string;
  sku?: string;
  price?: number;
  costPrice?: number;
  stock?: number;
  warranty?: number;
  brand?: string;
  weight?: number;
  height?: number;
  length?: number;
  width?: number;
  categoryId?: number;
  mainImageUrl?: IImgsJson;
  secondaryImageUrl?: IImgsJson[];
}
