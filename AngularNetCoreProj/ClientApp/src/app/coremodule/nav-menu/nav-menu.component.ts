import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasketTotal } from "../../entity/IBasketTotal";
import { BasketService } from '../../product/basket.service';
//import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  basketTotalQuantity$: Observable<IBasketTotal>;
  
  constructor(private basketservice: BasketService) {
    this.basketTotalQuantity$ = basketservice.basketTotal$;
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
