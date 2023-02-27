import { Component } from '@angular/core';
import { TicTacToeService } from '../../services/tic.tac.toe.service';

@Component({
  selector: 'app-game-controls',
  templateUrl: './game-controls.component.html',
  styleUrls: ['./game-controls.component.css']
})
export class GameControlsComponent {
  value = '';

  constructor(public ticTacToeService: TicTacToeService) {

  }
}
