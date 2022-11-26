import { Component } from '@angular/core'
import { Router } from '@angular/router'
import { dataservice } from '../share/dataservice';

@Component(
    {
        selector: "checkout",
        templateUrl: "./checkout.html"
    })
export class checkout {
    public errMessage: string;
    constructor(private router: Router, private ds: dataservice)
    {
        if (ds.Token!=null && ds.Token.length > 0) {  }
        else { router.navigate(['login']);}
    }

    public SubmitOrder(): void {
        this.ds.SubmitOrder().subscribe(
            (success) => this.errMessage="order has been made successfully",
            (error) => this.errMessage = error.message
        )}

    public NavigateToStore(): void {
        this.router.navigate([""]);
    }
}

   