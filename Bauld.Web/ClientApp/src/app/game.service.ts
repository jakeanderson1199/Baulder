import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor( private http: HttpClient) {}

  user: string;
  baseurl = '/api';  
  //baseurl = 'http://192.168.1.19:5000';
  addPlayer (gameId: string, player: string): Observable<any> {
    let url = `${this.baseurl}/games/${gameId}/players`
    let body = {
      "Name": player
    }
    return this.http.post<any>(url, body, httpOptions)
  }
  
  postAnswer (gameId: string, answer: string): Observable<any> {
    let url = `${this.baseurl}/games/${gameId}/players/${this.user}/answer`
    let body = {
     "text": answer,
     "UserName": this.user
   }
    return this.http.post<any>(url, body, httpOptions)
  }
  
  startGame (user: String){
    let g = new GameModel();
    let url = `${this.baseurl}/games/${user}`
    return this.http.post<any>(url, {}, httpOptions)
  }
  showGames () : Observable<GameSummary[]>{
    let url = `${this.baseurl}/games`
    return this.http.get<GameSummary[]>(url,httpOptions)
  }
  getGame (gameId: string){
    let url = `${this.baseurl}/games/${gameId}`
    return this.http.get<any>(url,httpOptions)
  }
  postVote (owner_name: string,player_name: string,vote: string){
    let url = `${this.baseurl}/games/${owner_name}/players/${this.user}/vote`
    let body = {
      "vote": vote
    }
    return this.http.post<any>(url, body, httpOptions)
  }
  newTurn (owner_name: string){
    let url = `${this.baseurl}/games/${owner_name}/turns`
    let body = {}
    return this.http.post<any>(url, body, httpOptions)
  }
  resetState (){
    let url = `${this.baseurl}/games/reset`
    return this.http.post<any>(url, {}, httpOptions)
  }

}

export class GameModel {
ownerName: string;
players: any[];
answers: any[];
votes: any[];
index: number;
turn : Turn;
gameId : string;
}

export interface GameSummary{
  ownerName: string;
  gameId: string;
}
export class Player{
  name: string;
  points: number;
  answer: Answer;
  owner: boolean;
  playerID: string;
  
}
export class Answer{
  userName: string;
  text: string;
  isReal: boolean;
  isUser: boolean;
  
}
export class Turn{
  answers: Answer[];
  votes: Vote[];
  currentQuestion: Question;
  
}
export class Question{
  label: string;
  category: string;
  answer: string;
}
export class Vote {
  answerId: string;  //This is the PlayerID of the person who wrote the answer
  playerId: string;  //This is the PlayerID of the person who voted for the answer specified above
  correctAnswer: boolean;
}