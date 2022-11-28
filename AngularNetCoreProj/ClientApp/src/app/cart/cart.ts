import { Component, Input } from '@angular/core';
import { templateSourceUrl } from '@angular/compiler';
import { dataservice } from '../share/dataservice';
import { OrderItem } from '../share/OrderItem';
import { Order } from '../share/Order';
import { Router } from '@angular/router';

@Component({
        selector : "the-cart",
        templateUrl :'./cart.html',
        styles:[]
    }
)
export class cart {
    order: Order;
    @Input('showButton') displayCheckOutButton ="true";
    constructor(private ds: dataservice, private router: Router) {
        this.order = ds.Order;
    }

    Checkout(): void
    { this.router.navigate(["checkout"]);
    }
}
