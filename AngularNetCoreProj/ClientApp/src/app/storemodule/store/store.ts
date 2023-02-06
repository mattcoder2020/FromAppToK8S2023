import { Component } from '@angular/core';

@Component({
  selector: 'FrontPage',  // The element / markup name where this componenet with data can be place on html 
  templateUrl:'./Store.html',
  styles: []
})
export class Store {
    //the class has the componennt and the data like title, they are glue together and provide a AppComponent type
    title = 'Matt-app';
}

