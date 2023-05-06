import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { Observable } from "rxjs";  // reactive js
import { IProduct } from "../entity/IProduct";
import { map } from 'rxjs/operators';
import { params } from "../entity/params";
import { IInventoryProduct } from "../entity/IInventoryProduct";

@Injectable({ providedIn: "root" })
export class inventoryservice  {
  constructor(private http: HttpClient) { }
  public products: any[];

  getbyid(id: number) {
    return this.http.get("http://localhost:7000/api/product/" + id);
  }

  getall() {
    return this.http.get("http://localhost:7000/api/product");
  }

  getallCategory() {
    return this.http.get("http://localhost:5002/api/product/productcategory");
  }

  updateproduct(product: IInventoryProduct) {
    return this.http.put("http://localhost:7000/api/product", product);
  }

  getproductsbyfiltration(p: params) {
    let params = new HttpParams();
    if (p.productcatetoryid != null && p.productcatetoryid != 0)
      params = params.append('ProductCategoryId', p.productcatetoryid.toString());
    if (p.id != null && p.id != 0)
      params = params.append('ProductId', p.id.toString());

    //const options = p.productcatetoryid != 0 ? { params: new HttpParams().set('ProductCategoryId', p.productcatetoryid.toString()) } : {};
    //const options = { params: httpparam };

    //httpparam.append("ProductCategoryId", p.productcatetoryid.toString());
    return this.http.get("http://localhost:7000/api/product", { params });
  }
}
