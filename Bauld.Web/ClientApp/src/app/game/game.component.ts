import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GameModel, GameService, Player } from '../game.service';
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
  ui: any = {};

  constructor(
    private gameService: GameService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.gameId = this.route.snapshot.paramMap.get('gameId');
    this.refresh();
    if (this.user == null) {
      this.router.navigate(['/dashboard'])
    }
    ;
  }
  get user(): string {
    return this.gameService.user;
  }
  set user(user: string) {
    this.gameService.user = user;
  }
  saveAnswer(): void {

    this.gameService.postAnswer(this.gameId, this.answer)
      .subscribe(r => {
        this.onGameUpdated(r);
      });
  }
  refresh(): void {

    this.gameService.getGame(this.gameId)
      .subscribe(r => {
        console.log('got game', r);
        this.onGameUpdated(r);
      });

  }

  getInstructions(): string {
    if (!this.game || !this.game.turn) {
      return 'not set yet';
    }

    const q = this.game.turn.currentQuestion;
    const c = q.category;
    if (c === 'word') {
      return 'What is the definitiion of the word ' + q.label;
    } else if (c === 'initials') {
      return `What do the initials ${q.label} stand for?`;
    } else if (c === 'people') {
      return `What is ${q.label} famous for?`;
    } else {
      return '';
    }
  }

  private onGameUpdated(game: GameModel) {
    this.game = game;
    this.allAnswered = this.game.allAnswered
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
        this.userVoted = false;
        this.onGameUpdated(r)

        this.allAnswered = false;
        this.answer = null;
      })
  }
  joinNextTurn(): void {
    this.userVoted = false;
    this.refresh()

  }

  get answers(): Answer[] {
    let answers: Answer[] = [];
    this.game.turn.answers.forEach(a => {
      let answer = new Answer();
      answer.text = a.text;
      answer.playerID = a.playerID;
      answer.userName = a.userName;
      a.isUser = a.userName === this.user;
      answers.push(answer);
    });
    //todo sort randomly
    //let real: Answer = { userName: "REAL_ANSWER", text: this.game.turn.currentQuestion.answer, playerID: "REAL_ANSWER", isUser: false };
    //answers.push(real);
    answers = shuffle(answers);
    return answers;
  }

  get userAnswered(): boolean {
    if (!this.game || !this.game.players) return false;
    let userplayer = this.game.players.find(p => p.name == this.user);
    return userplayer && userplayer.answer;
  }

  playerHadAnswer(player: Player): boolean {
    if (!this.game || !this.game.turn) {
      return false;
    }

    const answers = this.game.turn.answers;

    const playerAnswer = answers.find(a => a.playerID === player.playerID);

    if (!playerAnswer) {
      return false;
    }

    return playerAnswer.text === this.game.turn.currentQuestion.answer;


  }
}

export class Answer {
  userName: string;
  text: string;
  playerID: string;
  isUser: boolean
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
