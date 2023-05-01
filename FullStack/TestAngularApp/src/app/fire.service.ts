import { Injectable } from '@angular/core';
import firebase from 'firebase/compat/app';
import 'firebase/compat/firestore';
import 'firebase/compat/auth'

import * as config from '../../firebaseconfig.js';
import { timestamp } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FireService {

  firebaseApplication;
  firestore: firebase.firestore.Firestore;
  auth: firebase.auth.Auth;
  messages: any[] = [];
  channels: any[] = [];
  currentChannel: any;

  constructor() { 
    this.firebaseApplication = firebase.initializeApp(config.firebaseConfig);
    this.firestore = firebase.firestore();
    this.auth = firebase.auth();

    this.auth.onAuthStateChanged((user) => {
      if (user){
        this.getChannels(user);
      }
    })

  }

  SignUp(email: string, password: string){
    this.auth.createUserWithEmailAndPassword(email, password);
  }

  SignIn(email: string, password: string){
    this.auth.signInWithEmailAndPassword(email, password);
  }

  SignOut(){
    this.auth.signOut();
  }

  getChannels(user: any){
    this.firestore
    .collection('channels')
    .where('members', 'array-contains', this.auth.currentUser?.uid)
    .orderBy('name', 'asc')
    .onSnapshot( snapshot => {
      snapshot.docChanges().forEach(change => {
        if (change.type == "added"){
          this.channels.push({id: change.doc.id, data: change.doc.data()});
        } else if (change.type == "modified"){
          const index = this.channels.findIndex(document => document.id != change.doc.id);
          this.channels[index] = {
            id: change.doc.id, data: change.doc.data()
          }
        }
      })
    })
  }

  sendMessage(sendMessage: any) : void{
    let messageDTO: MessageDTO = {
      timestamp: new Date(),
      from: {
        uid: this.auth.currentUser?.uid,
        displayName: '',
        email: this.auth.currentUser?.email
      },
      content: sendMessage
    }
    this.firestore
    .collection('channels')
    .doc(this.currentChannel)
    .collection('messages')
    .add(messageDTO)
  }

  q;
  getMessages(channelId: any) {
    this.messages = [];
    if (this.q){
      this.q();
    }
    this.q = this.firestore;

    this.currentChannel = channelId;
    this.firestore
    .collection('channels')
    .doc(channelId)
    .collection('messages')
    .orderBy('timestamp', 'desc')
    .onSnapshot(snapshot => {
      snapshot.docChanges().forEach(change => {
        if (change.type == "added"){
          this.messages.push({id: change.doc.id, data: change.doc.data()});
        } else if (change.type == "modified"){
          const index = this.messages.findIndex(document => document.id != change.doc.id);
          this.messages[index] = {
            id: change.doc.id, data: change.doc.data()
          }
        }
      })
    })
  }

}

export interface MessageDTO{
  content: string;
  timestamp: Date;
  from: Sender;
}

export interface Sender{
  uid: any;
  displayName: string;
  email: any;
}