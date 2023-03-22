//Navmenu bar should display correct number of basket item quantity count
//Navmenu bar should display login linkage if user did not login

import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NavMenuComponent } from './nav-menu.component';
import { spyOnClass } from 'jasmine-es6-spies';
import { of } from 'rxjs';
import { BasketService } from '../../product/basket.service';
import { IBasketTotal } from '../../entity/IBasketTotal';

describe('NavMenuComponent', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;
  let basketservice : jasmine.SpyObj<BasketService>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [NavMenuComponent],
      providers: [
        { provide: BasketService, useFactory: () => spyOnClass(BasketService) }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    basketservice = TestBed.get(BasketService);
  })

  beforeEach(() => {
    fixture = TestBed.createComponent(NavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  //it('should display a login linkage', async(() => {
  //  const titleText = fixture.nativeElement.querySelector('login').textContent;
  //  expect(titleText).toEqual('login');
  //}));

  it('should display a coorrect basket item quantity', async(() => {
   // const basket = require("./basket.json");
   // basketservice.calculateTotalQuantity(basket);
   

    let baskettotal: IBasketTotal = new IBasketTotal();
    baskettotal.total = 3;
    basketservice.basket$.basketTotalSource.next(baskettotal);
    fixture.detectChanges();
    const titleText = fixture.nativeElement.querySelector('basket').textContent;
    expect(titleText).toEqual(3);

  }));
  //it('should start with count 0, then increments by 1 when clicked', async(() => {
  //  const countElement = fixture.nativeElement.querySelector('strong');
  //  expect(countElement.textContent).toEqual('0');

  //  const incrementButton = fixture.nativeElement.querySelector('button');
  //  incrementButton.click();
  //  fixture.detectChanges();
  //  expect(countElement.textContent).toEqual('1');
  //}));
});
