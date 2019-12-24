import { Component, OnInit } from '@angular/core';
import { GameModel, GameService } from './game.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'bauld';
  constructor(
    private gameService: GameService,
    private route: ActivatedRoute,
    private router: Router) { }
  resetState(): void {
    this.gameService.resetState().subscribe(r => {
      this.router.navigate(['/dashboard']);
    });
  }
}
