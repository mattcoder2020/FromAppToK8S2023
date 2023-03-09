import { IProductCategory } from "./IProductCategory";

export interface IProduct {
  name: string;
  productCategoryId: number;
  price: number;
  productCategory: IProductCategory;
  id: number;
}

