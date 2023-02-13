import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { Observable } from "rxjs";  // reactive js
import { IProduct } from "../entity/IProduct";
import { map } from 'rxjs/operators';

@Injectable({ providedIn: "root" })
export class productservice  {
  constructor(private http: HttpClient) { }
  public products: any[];

  getall() {
    return this.http.get("http://localhost:5002/api/product");
    }

}
