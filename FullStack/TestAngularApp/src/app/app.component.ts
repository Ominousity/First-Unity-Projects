import { Component, OnInit } from '@angular/core';
import { FireService } from './fire.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  message: any;

  state: string = "SignIn";

  displayName: string = "";
  email: string = "";
  password: string = "";

  constructor(public fireservice: FireService, public dialog: MatDialog) {
    
  }

  StateChangeSignIn(){
    this.state = "SignIn";
  }

  StateChangeSignUp(){
    this.state = "SignUp"
  }

  LoginOrSignup(email: string, password: string){
    if (this.state == "SignIn"){
      this.fireservice.SignIn(email, password);
    } else if (this.state == "SignUp"){
      this.fireservice.SignUp(email, password);
    }
  }
  
}
