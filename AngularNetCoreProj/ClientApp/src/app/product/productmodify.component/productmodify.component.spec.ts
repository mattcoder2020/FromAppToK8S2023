import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductmodifyComponent } from './productmodify.component';
//generate all test cases for the component
  


describe('ProductmodifyComponent', () => {
  let component: ProductmodifyComponent;
  let fixture: ComponentFixture<ProductmodifyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductmodifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductmodifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
