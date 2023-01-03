export interface Icategory {
  id: number;
  name: string;
  parentCategoryId: number;
}

export interface IcategoryPutAndpost {
  id?: number;
  name: string;
  parentCategoryId: number;
}
