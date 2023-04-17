import { Injectable } from '@angular/core';
import firebase from 'firebase/compat/app';
import 'firebase/compat/firestore';

import * as config from '../../firebaseconfig.js';
import { timestamp } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FireService {

  firebaseApplication;
  firestore: firebase.firestore.Firestore;
  messages: any[] = [];

  constructor() { 
    this.firebaseApplication = firebase.initializeApp(config.firebaseConfig);
    this.firestore = firebase.firestore();
    this.getMessages();
  }

  sendMessage(sendMessage: any) : void{
    let messageDTO: MessageDTO = {
      content: sendMessage,
      timestamp: new Date(),
      user: 'somebody'
    }
    this.firestore.collection('chat')
    .add(messageDTO)
  }

  getMessages() {
    this.firestore
    .collection('chat')
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
  user: string;
}
