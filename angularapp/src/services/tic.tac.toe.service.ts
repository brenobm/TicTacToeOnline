import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GameType } from "../app/type-game-dialog/type-game-dialog.component";
import { Game } from "../models/Game";
import { MakeMoveRequest } from "../models/MakeMoveRequest";
import { NewGameMultiPlayerRequest } from "../models/NewGameMultiPlayerRequest";
import { NewGameSinglePlayerRequest } from "../models/NewGameSinglePlayerRequest";

@Injectable()
export class TicTacToeService {
  public game?: Game;
  public gameType?: GameType;

  constructor(private http: HttpClient) {

  }

  public newGame() {
    switch (this.gameType) {
      case GameType.SinglePlayer:
        this.newSinglePlayerGame();
        break;
      case GameType.MultiPlayer:
        this.newMultiPlayerGame();
        break;
    }
  }

  public makeMove(row: number, col: number) {
    if (this.game!!.status != 1) {
      return;
    }

    let gameMoveRequest: MakeMoveRequest = {
      row: row,
      column: col,
      playerId: this.game!!.turn.id
    };

    this.http.put<Game>(`/api/v1/game/${this.game!!.id}/move`, gameMoveRequest).subscribe(result => {
      this.game = result;
    }, error => console.error(error));
  }

  public getStatusName() {
    let statusName = "";

    switch (this.game!!.status) {
      case 0:
        statusName = "Waiting";
        break;
      case 1:
        statusName = "Running";
        break;
      case 2:
        statusName = "Draw";
        break;
      case 3:
        statusName = "Win";
        break;
    }
    return statusName;
  }

  public getGameTypeName() {
    switch (this.gameType) {
      case GameType.SinglePlayer:
        return "Single Player";
      case GameType.MultiPlayer:
        return "Multi Player";
    }
    return "";
  }

  private newSinglePlayerGame() {
    let newGameRequest: NewGameSinglePlayerRequest = {
      playerXId: "X",
      playerXName: "X",
      playerOId: "O",
      playerOName: "O"
    };

    this.http.post<Game>('/api/v1/game/singleplayer', newGameRequest).subscribe(result => {
      this.game = result;
    }, error => console.error(error));
  }

  private newMultiPlayerGame() {
    let newGameRequest: NewGameMultiPlayerRequest = {
      playerXId: "X",
      playerXName: "X"
    };

    this.http.post<Game>('/api/v1/game/multiplayer', newGameRequest).subscribe(result => {
      this.game = result;
    }, error => console.error(error));
  }
}
