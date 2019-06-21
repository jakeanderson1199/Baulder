import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  constructor (private http: HttpClient){};
  public currentCount = 0;
  ngOnInit(): void {
    
    
  }
  public incrementCounter() {
    this.currentCount++;
    this.http.post('api/question',{label: 'jake'}).subscribe(r => this.question = r,e => {alert("error");
    console.log("helloooo", e);
  })
  }
  question: any;
}
