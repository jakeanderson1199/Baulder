import { Component, OnInit } from '@angular/core';
import { GameModel, GameService } from './game.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'bauld';

  resetState(): void {
    GameService.resetState();
  }
}
