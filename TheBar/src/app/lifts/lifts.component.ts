import { Component, OnInit } from '@angular/core';
import { LiftsService, Joint } from './lifts.service';

@Component({
  selector: 'app-lifts',
  templateUrl: './lifts.component.html',
  styleUrls: ['./lifts.component.css']
})
export class LiftsComponent implements OnInit {

  joints : Joint[];

  lifts = [{ title: "Lift1", type: "Clean and Jerk", date: "21-07-17", duration: "5:00", link: "/lift1" }, { title: "Lift2", type: "Clean and Jerk", date: "21-07-17", duration: "3:00", link: "/lift2" },
  { title: "Lift3", type: "Squat", date: "21-07-17", duration: "7:00", link: "/lift3" }, { title: "Lift5", type: "Snatch", date: "21-07-17", duration: "5:00", link: "/lift5" }
  ];

  constructor(private liftsService: LiftsService) {

  }

  ngOnInit() {
  }

  buttonClicked() {
    this.liftsService.getLifts();
  }

  getDetails() {
    this.liftsService.getLiftDetails(23).subscribe(response => {
      console.log(response["entity"].details.bodyData.details.orderedFrames[0].details.joints);
      this.joints = response["entity"].details.bodyData.details.orderedFrames[0].details.joints
    });    
  }

  draw(){
    this.joints.sort((left, right): number => {
      if (left.jointTypeID < right.jointTypeID) return -1;
      if (left.jointTypeID > right.jointTypeID) return 1;
      return 0;
    });
    console.dir(JSON.stringify(this.joints));
  }

  joints_static = [{"id":484,"jointTypeID":1,"x":0.130986437,"y":0.3896627,"z":1.85242927,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":1,"value":0,"name":"SpineBase"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":483,"jointTypeID":2,"x":0.116613463,"y":0.6935067,"z":1.83557522,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":2,"value":1,"name":"SpineMid"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":482,"jointTypeID":3,"x":0.120345168,"y":0.8520425,"z":1.79535675,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":3,"value":2,"name":"Neck"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":481,"jointTypeID":4,"x":-0.0513348,"y":0.565499663,"z":1.84780145,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":4,"value":3,"name":"Head"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":480,"jointTypeID":5,"x":-0.0613022745,"y":0.3459578,"z":1.830164,"jointTrackingStateTypeID":1,"bodyDataFrameID":35,"details":{"jointType":{"id":5,"value":4,"name":"ShoulderLeft"},"jointTrackingStateType":{"id":1,"value":0,"name":"NotTracked"}}},{"id":479,"jointTypeID":6,"x":-0.022639785,"y":0.173819572,"z":1.751959,"jointTrackingStateTypeID":1,"bodyDataFrameID":35,"details":{"jointType":{"id":6,"value":5,"name":"EldowLeft"},"jointTrackingStateType":{"id":1,"value":0,"name":"NotTracked"}}},{"id":478,"jointTypeID":7,"x":0.0423422754,"y":0.109492168,"z":1.73513222,"jointTrackingStateTypeID":1,"bodyDataFrameID":35,"details":{"jointType":{"id":7,"value":6,"name":"WristLeft"},"jointTrackingStateType":{"id":1,"value":0,"name":"NotTracked"}}},{"id":477,"jointTypeID":8,"x":0.286538035,"y":0.575086832,"z":1.81876242,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":8,"value":7,"name":"HandLeft"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":476,"jointTypeID":9,"x":0.329528749,"y":0.346964121,"z":1.79833519,"jointTrackingStateTypeID":1,"bodyDataFrameID":35,"details":{"jointType":{"id":9,"value":8,"name":"ShoulderRight"},"jointTrackingStateType":{"id":1,"value":0,"name":"NotTracked"}}},{"id":475,"jointTypeID":10,"x":0.250339657,"y":0.158474162,"z":1.71548283,"jointTrackingStateTypeID":1,"bodyDataFrameID":35,"details":{"jointType":{"id":10,"value":9,"name":"ElbowRight"},"jointTrackingStateType":{"id":1,"value":0,"name":"NotTracked"}}},{"id":485,"jointTypeID":11,"x":0.186049923,"y":0.09741454,"z":1.71677959,"jointTrackingStateTypeID":1,"bodyDataFrameID":35,"details":{"jointType":{"id":11,"value":10,"name":"WristRight"},"jointTrackingStateType":{"id":1,"value":0,"name":"NotTracked"}}},{"id":474,"jointTypeID":12,"x":0.06436688,"y":0.0670570657,"z":1.82553494,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":12,"value":11,"name":"HandRight"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":472,"jointTypeID":13,"x":0.07528437,"y":-0.282332063,"z":1.84443581,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":13,"value":12,"name":"HipLeft"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":471,"jointTypeID":14,"x":0.09541458,"y":-0.62606883,"z":1.93837678,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":14,"value":13,"name":"KneeLeft"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":470,"jointTypeID":15,"x":0.0892334,"y":-0.714355469,"z":1.84472656,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":15,"value":14,"name":"AnkleLeft"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":469,"jointTypeID":16,"x":0.219813108,"y":0.07145134,"z":1.81320167,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":16,"value":15,"name":"FootLeft"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":468,"jointTypeID":17,"x":0.2433775,"y":-0.282623857,"z":1.81962609,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":17,"value":16,"name":"HipRight"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":467,"jointTypeID":18,"x":0.22885859,"y":-0.6474175,"z":1.92634046,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":18,"value":17,"name":"KneeRight"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":466,"jointTypeID":19,"x":0.210327148,"y":-0.711914063,"z":1.82324219,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":19,"value":18,"name":"AnkleRight"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":465,"jointTypeID":20,"x":0.120285235,"y":0.6195874,"z":1.84187174,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":20,"value":19,"name":"FootRight"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":464,"jointTypeID":21,"x":0.08275109,"y":0.03652343,"z":1.66480982,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":21,"value":20,"name":"SpineShoulder"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":463,"jointTypeID":22,"x":0.08839635,"y":0.174257711,"z":1.71461093,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":22,"value":21,"name":"HandTipLeft"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":473,"jointTypeID":23,"x":0.118754849,"y":0.0298233815,"z":1.66975868,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":23,"value":22,"name":"ThumbLeft"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}},{"id":486,"jointTypeID":24,"x":0.125166133,"y":0.161115587,"z":1.70988107,"jointTrackingStateTypeID":2,"bodyDataFrameID":35,"details":{"jointType":{"id":24,"value":23,"name":"HandTipRight"},"jointTrackingStateType":{"id":2,"value":1,"name":"Inferred"}}}]
}


