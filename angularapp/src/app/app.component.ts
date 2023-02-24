import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TicTacToeService } from '../services/tic.tac.toe.service';
import { TypeGameDialogComponent } from './type-game-dialog/type-game-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public ticTacToeService: TicTacToeService, public dialog: MatDialog) {
    this.openNewGameDialog();
  }

  public openNewGameDialog() {
    const dialogRef = this.dialog.open(TypeGameDialogComponent, {});

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.ticTacToeService.gameType = result;

      this.ticTacToeService.newGame();
    });
  }

  title = 'Tic Tac Toe Online';
}

