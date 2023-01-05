export interface Icategory {
  id: number;
  name: string;
  categoryLevel?: number;
  parentCategoryId: number;
}

export interface IcategoryPutAndpost {
  id?: number;
  name: string;
  categoryLevel?: number;
  parentCategoryId: number;
}
