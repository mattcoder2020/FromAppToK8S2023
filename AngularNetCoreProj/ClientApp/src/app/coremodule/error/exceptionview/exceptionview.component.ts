import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-exceptionview',
  templateUrl: './exceptionview.component.html',
  styleUrls: ['./exceptionview.component.scss']
})
export class ExceptionviewComponent {
  error: any;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.['error'];
  }
}
