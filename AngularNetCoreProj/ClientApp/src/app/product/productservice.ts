import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { Observable } from "rxjs";  // reactive js
import { IProduct } from "../entity/IProduct";
import { map } from 'rxjs/operators';
import { params } from "../entity/params";

@Injectable({ providedIn: "root" })
export class productservice  {
  constructor(private http: HttpClient) { }
  public products: any[];

  getbyid(id: number) {
    return this.http.get("http://localhost:5002/api/product/" + id);
  }

  getall() {
    return this.http.get("http://localhost:5002/api/product");
  }

  getallCategory() {
    return this.http.get("http://localhost:5002/api/product/productcategory");
  }

  updateproduct(product: IProduct) {
    return this.http.put("http://localhost:5002/api/product", product);
  }

  createproduct(product: IProduct) {
    return this.http.post("http://localhost:5002/api/product", product);
  }
  getproductsbyfiltration(p: params) {
    let params = new HttpParams();
    //if (p.productcatetoryid != 0)
    //  params = new HttpParams().set('ProductCategoryId', p.productcatetoryid.toString());
    //if (p.orderby.length > 0)
    //  params = new HttpParams().set('orderby', p.orderby.toString());
    if (p.productcatetoryid != 0)
      params = params.append('ProductCategoryId', p.productcatetoryid.toString());
    if (p.orderby.length > 0)
      params = params.append('orderby', p.orderby.toString());

    const options = p.productcatetoryid != 0 ? { params: new HttpParams().set('ProductCategoryId', p.productcatetoryid.toString()) } : {};
    //const options = { params: httpparam };

    //httpparam.append("ProductCategoryId", p.productcatetoryid.toString());
    return this.http.get("http://localhost:5002/api/product", { params });
  }
}
