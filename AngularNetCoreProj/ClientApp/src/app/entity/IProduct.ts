import { IProductCategory } from "./IProductCategory";

export class IProduct {
  name: string;
  productCategoryId: number;
  price: number;
  productCategory: IProductCategory;
  id: number;
}

