import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {
  temp: object;
  constructor(private http: HttpClient, private toastr: ToastrService) {
    
  }

  ngOnInit() {
  }

  apinotfoundclick() {
    return this.http.get(environment.apiurlProduct + "/buggy/servererror1").subscribe(response => {
      this.temp = response;
      this.toastr.success('Hello world!', 'Toastr fun!');
    }, error => this.toastr.error(error.error.message, error.error.statuscode));
  }

  badrequestclick() {
    return this.http.get(environment.apiurlProduct + "/buggy/badrequest").subscribe(response => {
      this.temp = response;
      this.toastr.success('Hello world!', 'Toastr fun!');
    }, error => this.toastr.error(error.error.message, error.error.statuscode));
  }

  serverissueclick() {
    return this.http.get(environment.apiurlProduct+ "/buggy/servererror").subscribe(response => {
      this.temp = response;
      this.toastr.success('Hello world!', 'Toastr fun!');
    }, error => this.toastr.error(error.error.message, error.error.statuscode));
  }

}
