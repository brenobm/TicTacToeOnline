import { Component } from '@angular/core';
import { TicTacToeService } from '../../services/tic.tac.toe.service';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent {
  constructor(public ticTacToeService: TicTacToeService) {

  }
}
