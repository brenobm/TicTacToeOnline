import { Component } from '@angular/core';
import { TicTacToeService } from '../services/tic.tac.toe.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public ticTacToeService: TicTacToeService) {
  }

  title = 'Tic Tac Toe Online';
}

