import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {
  temp: object;
  constructor(private http:HttpClient ) { }

  ngOnInit() {
  }

  apinotfoundclick() {
    return this.http.get(environment.apiurlProduct + "/buggy/servererror1").subscribe(response => {
      this.temp = response;
    }, error => console.error(error));
  }

  badrequestclick() {
    return this.http.get(environment.apiurlProduct + "/buggy/badrequest").subscribe(response => {
      this.temp = response;
    }, error => console.error(error));
  }

  serverissueclick() {
    return this.http.get(environment.apiurlProduct+ "/buggy/servererror").subscribe(response => {
      this.temp = response;
    }, error => console.error(error));
  }

}
