import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent {
  baseUrl = 'https://localhost:44352/api/';
  validationsErrors: string[] = [];
  constructor(private http: HttpClient) {


  }
  get404Error(){
    this.http.get(this.baseUrl + 'Buggy/not-found').subscribe({
      next: response => console.log(console.log(response)),
      error: error => console.log(error)
    })
  }

  get400Error(){
    this.http.get(this.baseUrl + 'Buggy/bad-request').subscribe({
      next: response => console.log(console.log(response)),
      error: error => console.log(error)
    })
  }

  get500Error(){
    this.http.get(this.baseUrl + 'Buggy/server-error').subscribe({
      next: response => console.log(console.log(response)),
      error: error => console.log(error)
    })
  }

  get401Error(){
    this.http.get(this.baseUrl + 'Buggy/auth').subscribe({
      next: response => console.log(console.log(response)),
      error: error => console.log(error)
    })
  }

  get400ValidationError(){
    this.http.post(this.baseUrl + 'Account/register', {}).subscribe({
      next: response => console.log(console.log(response)),
      error: error => {
        console.log(error);
        this.validationsErrors = error;

      }
    })
  }
}
