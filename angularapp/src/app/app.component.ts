import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public game?: Game;
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;

    this.newGame();
  }

  public makeMove(row: number, col: number) {
    if (this.game!!.status != 0) {
      return;
    }

    let gameMoveRequest: MakeMoveRequest = {
      row: row,
      column: col,
      playerId: this.game!!.turn.id
    };

    this.http.put<Game>(`https://tic-tac-toe-online.azurewebsites.net/api/v1/game/${this.game!!.id}/move`, gameMoveRequest).subscribe(result => {
      this.game = result;
    }, error => console.error(error));
  }

  public getStatusName(status: number) {
    let statusName = "";

    switch (status) {
      case 0:
        statusName = "Running";
        break;
      case 1:
        statusName = "Draw";
        break;
      case 2:
        statusName = "Win";
        break;
    }
    return statusName;
  }

  public newGame() {
    let newGameRequest: NewGameRequest = {
      playerXId: "X",
      playerXName: "X",
      playerOId: "O",
      playerOName: "O"
    };

    this.http.post<Game>('https://tic-tac-toe-online.azurewebsites.net/api/v1/game', newGameRequest).subscribe(result => {
      this.game = result;
    }, error => console.error(error));
  }

  title = 'Tic Tac Toe Online';
}

interface Game {
  id: string;
  board: number[][];
  turn: Player;
  status: number;
  playerX: Player;
  playerY: Player;
  winner?: Player;
}

interface Player {
  id: string;
  name: string;
}

interface NewGameRequest {
  playerXId: string;
  playerXName: string;
  playerOId: string;
  playerOName: string;
}

interface MakeMoveRequest {
  playerId: string;
  row: number;
  column: number;
}
