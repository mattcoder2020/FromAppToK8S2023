import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IOrder } from '../entity/IOrder';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }
  createorder(order: IOrder) {
    return this.http.post("http://localhost:5010/api/order", order);
  } 
}
