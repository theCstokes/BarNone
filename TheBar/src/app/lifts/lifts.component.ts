import { Component, OnInit } from '@angular/core';
import {LiftsService} from './lifts.service';

@Component({
  selector: 'app-lifts',
  templateUrl: './lifts.component.html',
  styleUrls: ['./lifts.component.css']
})
export class LiftsComponent implements OnInit {
  
  lifts = [{title: "Lift1", type:"Clean and Jerk", date:"21-07-17", duration: "5:00", link:"/lift1"}, {title: "Lift2", type:"Clean and Jerk", date:"21-07-17", duration: "3:00",link:"/lift2"}, 
  {title: "Lift3", type:"Squat", date:"21-07-17", duration: "7:00", link:"/lift3"},  {title: "Lift5", type:"Snatch", date:"21-07-17", duration: "5:00", link:"/lift5"}
  ];
  
  constructor(private liftsService : LiftsService) {

   }

  ngOnInit() {
  }

  buttonClicked(){
    this.liftsService.getLifts();
  }
}
