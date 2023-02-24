import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-type-game-dialog',
  templateUrl: './type-game-dialog.component.html',
  styleUrls: ['./type-game-dialog.component.css']
})
export class TypeGameDialogComponent {
  public singlePlayer: GameType = GameType.SinglePlayer;
  public multiPlayer: GameType = GameType.MultiPlayer;

  constructor(
    public dialogRef: MatDialogRef<TypeGameDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) { }

}

export interface DialogData {
  gameType: GameType;
}

export enum GameType {
  SinglePlayer,
  MultiPlayer
}
