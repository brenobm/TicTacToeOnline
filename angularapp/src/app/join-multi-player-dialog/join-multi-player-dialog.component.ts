import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from '../type-game-dialog/type-game-dialog.component';

@Component({
  selector: 'app-join-multi-player-dialog',
  templateUrl: './join-multi-player-dialog.component.html',
  styleUrls: ['./join-multi-player-dialog.component.css']
})
export class JoinMultiPlayerDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<JoinMultiPlayerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) { }
}
