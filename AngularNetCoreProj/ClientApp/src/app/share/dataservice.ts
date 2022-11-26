import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";  // reactive js
import { Product } from "./Product";
import { Order } from './Order';
import { OrderItem } from './OrderItem';
import { map } from 'rxjs/operators';

@Injectable()   //Add Injectable to inject the HttpClient 
                //which registered at module as service to be added at constructor
export class dataservice
{
    //which registered at module as service to be added at constructor
    constructor(private http: HttpClient) { };
    public Order: Order = new Order();
    public Products: Product[];
    public Token: string;
    public TokenValidTo: Date;
    login(creds): Observable<boolean> {
        return this.http.post("/login/CreateToken", creds).pipe
            (
            map((data: any) => {
                this.Token = data.token;
                this.TokenValidTo = data.expire;
                return true;
            })
            );
    }
    LoadProducts(): Observable<boolean>
    {
        return this.http.get("/api/products")
            .pipe(
              map((data: any[]) =>
              { this.Products = data; return true }));
     }
    AddOrderItem(product: Product): void {
        let _item: OrderItem;
        if (this.Order.orderItems != undefined) {
            _item = this.Order.orderItems.find(e => e.productId == product.id)
        }
        if (_item) {
            _item.quantity++;
        }
        else {
            var item = new OrderItem();
            item.orderId = 0;
            item.product = product;
            item.productId = product.id;
            item.quantity = 1;
            this.Order.orderItems.push(item);
        }
    }
    SubmitOrder() {
        //if (this.Order.orderItems != undefined) {
        this.Order.orderItems.forEach(i => i.product = null);
        return this.http.post("/api/Orders", this.Order
            , { headers: new HttpHeaders({ "authorization": "Bearer " + this.Token }) }
        ).pipe
            (
            map(
            (data: any) => {
                alert(data.id),
                this.Order = new Order();
                return true;
            })
            )
        // }
    }
    
} 