import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GameModel, GameService } from '../game.service';
import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {

  game: GameModel;
  answer: string;
  gameId: string;
  allAnswered: boolean;
  userVoted: boolean;
  ui:any = {}

  constructor(
    private gameService: GameService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.gameId = this.route.snapshot.paramMap.get('gameId');
    this.refresh();
   
  }
  get user(): string {
    return this.gameService.user
  }
  set user(user: string) {
    this.gameService.user = user;
  }
  saveAnswer(): void {

    this.gameService.postAnswer(this.gameId, this.answer)
      .subscribe(r => {
        this.onGameUpdated(r);
      })
  }
  refresh(): void {

    this.gameService.getGame(this.gameId)
      .subscribe(r => {
        console.log('got game',r);
        this.onGameUpdated(r);
      });

  }

  private onGameUpdated(game: GameModel) {
    this.game = game;
    this.allAnswered = false
    if (this.game.players.length == this.game.turn.answers.length){
      this.allAnswered = true;
    };
  }

  vote(a): void {
    this.gameService.postVote(this.gameId, this.user, a)
      .subscribe(r => {
        this.onGameUpdated(r);
        this.userVoted = true;
      }
      )
  }
  nextTurn(): void {
    this.gameService.newTurn(this.gameId)
    .subscribe(r => {
      this.onGameUpdated(r)
      this.userVoted = false;
      this.allAnswered = false;
      this.answer = null;
    })
  }

  get answers(): Answer[] {
    let answers: Answer[] = [];
    this.game.turn.answers.forEach(a => {
      let answer = new Answer();
      answer.answer = a.text;
      a.isUser = a.userName === this.user;
      answers.push(answer);
    });
    //todo sort randomly
    let real: Answer = { answer: this.game.turn.currentQuestion.answer, isUser: false, isReal: true };
    answers.push(real);
    answers = shuffle(answers);
    return answers;
  }

  get userAnswered(): boolean {
    if (!this.game || !this.game.players) return false;
    let userplayer = this.game.players.find(p => p.name == this.user);
    return userplayer && userplayer.answer;
  }

}

export class Answer {
  isReal: boolean;
  isUser: boolean;
  answer: string
}
function shuffle(a) {
  var j, x, i;
  for (i = a.length - 1; i > 0; i--) {
    j = Math.floor(Math.random() * (i + 1));
    x = a[i];
    a[i] = a[j];
    a[j] = x;
  }
  return a;
}
