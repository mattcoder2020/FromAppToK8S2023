export interface IProductCategory {
  description: string;
  id: number;
}

export interface IProduct {
  name: string;
  productCategoryId: number;
  price: number;
  productCategory: IProductCategory;
  id: number;
}
