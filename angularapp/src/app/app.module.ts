import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { TypeGameDialogComponent } from './type-game-dialog/type-game-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { GameBoardComponent } from './game-board/game-board.component';
import { TicTacToeService } from '../services/tic.tac.toe.service';
import { GameSummaryComponent } from './game-summary/game-summary.component';
import { JoinMultiPlayerDialogComponent } from './join-multi-player-dialog/join-multi-player-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    TypeGameDialogComponent,
    GameBoardComponent,
    GameSummaryComponent,
    JoinMultiPlayerDialogComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, BrowserAnimationsModule, MatDialogModule
  ],
  providers: [TicTacToeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
