import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from '../../entity/IProduct';

@Component({
  selector: 'app-productdisplay',
  templateUrl: './productdisplay.component.html',
  styleUrls: ['./productdisplay.component.css']
})


export class ProductDisplayComponent implements OnInit {
   @Input() product: IProduct;

  constructor() { }
  ngOnInit(): void {}

}
