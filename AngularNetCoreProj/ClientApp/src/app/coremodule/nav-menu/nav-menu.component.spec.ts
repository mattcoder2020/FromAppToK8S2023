import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NavMenuComponent } from './nav-menu.component';
import { SpyOnClass } from 'jasmine-es6-spies';

describe('NavMenuComponent', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [NavMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should display a login', async(() => {
    const titleText = fixture.nativeElement.querySelector('li').textContent;
    expect(titleText).toEqual('login');
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
