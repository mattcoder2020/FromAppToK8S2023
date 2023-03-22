import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { spyOnClass } from 'jasmine-es6-spies'
import { productservice } from '../productservice';
import { ProductlistComponent } from './productlist.component';
import { IProduct } from '../../entity/IProduct';


describe('ProductlistComponent', () => {
  let component: ProductlistComponent;
  let fixture: ComponentFixture<ProductlistComponent>;
  let productservicefortest: jasmine.SpyObj<productservice>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ProductlistComponent],
      providers: [
        { provide: productservicefortest, useFactory: () => spyOnClass(productservice) }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display 2 products', () => {
    productservicefortest.products = new IProduct[2];

    fixture.detectChanges();
    expect(component).toBeTruthy();
  });
});
