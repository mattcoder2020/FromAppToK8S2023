import { Component } from "@angular/core";
import { dataservice } from "../share/dataservice";
import { Product } from "../share/Product";

@Component({
    selector: "ProductList",
    templateUrl: "./productlist.html", //Move the template out to external html
    styles:[]
    }
)
export class ProductList
{
    public products = [];
    public chosenStarWidth: string;
    constructor(private dataservice: dataservice)
    {
      dataservice.LoadProducts().subscribe
            (
            (success) => { if (success) this.products = dataservice.Products; }
      )
      this.products = dataservice.Products;
    }
    

    AddToCart(product: Product): void
    {
        this.dataservice.AddOrderItem(product);
    }

    ParentClick(event: string): void {
        this.chosenStarWidth = event;
    }
        
}
