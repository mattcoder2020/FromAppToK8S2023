import { IProductCategory } from "./IProductCategory";

export class IProduct {
  name: string;
  productCategoryId: number;

  //create a property with type of float or decimal

  price: number;
  productCategory: IProductCategory;
  id: number;
}

