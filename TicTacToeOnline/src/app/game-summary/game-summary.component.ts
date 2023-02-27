import { Component } from '@angular/core';
import { TicTacToeService } from '../../services/tic.tac.toe.service';

@Component({
  selector: 'app-game-summary',
  templateUrl: './game-summary.component.html',
  styleUrls: ['./game-summary.component.css']
})
export class GameSummaryComponent {
  constructor(public ticTacToeService: TicTacToeService) {

  }
}
