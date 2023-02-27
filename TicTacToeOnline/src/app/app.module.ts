import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { GameBoardComponent } from './game-board/game-board.component';
import { TicTacToeService } from '../services/tic.tac.toe.service';
import { GameSummaryComponent } from './game-summary/game-summary.component';
import { GameControlsComponent } from './game-controls/game-controls.component';

@NgModule({
  declarations: [
    AppComponent,
    GameBoardComponent,
    GameSummaryComponent,
    GameControlsComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, FormsModule, BrowserAnimationsModule, MatDialogModule, MatInputModule, MatIconModule
  ],
  providers: [TicTacToeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
