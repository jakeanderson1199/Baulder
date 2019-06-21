import { Component, OnInit } from '@angular/core';
import { GameService, Game, GameSummary }  from '../game.service';

import {ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private gameService: GameService,
              private router: Router) { }

  ngOnInit() {
    //this.showGames();
  }
games: GameSummary[];
tmpUser: string;
  get user(): string {
    return this.gameService.user
  }

  set user(user: string) {
     this.gameService.user = user;
  }

  savePlayer(game: Game): void {
    this.gameService.addPlayer(game.owner_name,this.user)
    .subscribe(r=> {
      console.log(r);
      this.router.navigate([`/games/${game.owner_name}`])
    })
  }
  startGame(): void {
    this.gameService.startGame(this.user)
    .subscribe(r=> {
      console.log(r);
      this.showGames();
    })}

  showGames(): void {
    this.gameService.showGames()
    .subscribe(
    r=> {
      console.log('games', r);
      this.games = r;
    } 
    );
  }


 }

