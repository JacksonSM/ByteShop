export interface Icategory {
  id: number;
  name: string;
  parentCategoryId: number;
}

export interface IcategoryPut {
  id?: number; 
  name: string;
  parentCategoryId: number;
}
