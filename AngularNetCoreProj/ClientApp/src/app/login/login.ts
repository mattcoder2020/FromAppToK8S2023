import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { dataservice } from '../storemodule/share/dataservice';
import { error } from '@angular/compiler/src/util';
import { retry } from 'rxjs/operators';

@Component({
    selector: "login",
    templateUrl: './login.html',
    styles: []
}
)
export class login
{
    constructor(private ds: dataservice, private router: Router) { }
    public creds = { UserName: "", Password: "", RememberMe: false }
    public errormessage: string;
    public onlogin() {
        this.ds.login(this.creds).subscribe(
            success => {
                if (success) {
                    if (this.ds.Order.orderItems.length > 0) { this.router.navigate(["checkout"]) }
                    else { this.router.navigate([""]) }  //return to the product list if cart is empty
                }
            },
            error => this.errormessage = error
        );
    }

    public submitDisable() {
        return !(this.creds.UserName.length > 0 && this.creds.Password.length > 0);
    }
}

