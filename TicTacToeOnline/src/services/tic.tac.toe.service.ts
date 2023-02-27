import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GameType } from "../models/game-type.enum";
import { Game } from "../models/game.model";
import { JoinGameMultiPlayerRequest } from "../models/join-game-multiplayer-request.model";
import { MakeMoveRequest } from "../models/make-move-request.model";
import { NewGameMultiPlayerRequest } from "../models/new-game-multiplayer-request.model";
import { NewGameSinglePlayerRequest } from "../models/new-game-singleplayer-request.model";
import { HubConnectionBuilder } from "@aspnet/signalr";
import { v4 as uuidv4 } from "uuid";
import { HubConnection } from "@aspnet/signalr/dist/esm/HubConnection";

@Injectable()
export class TicTacToeService {
  public game?: Game;
  public gameType?: GameType;
  public currentPlayer?: string;
  public currentPlayerName?: string;
  public connection?: HubConnection;
  public error?: string;

  constructor(private http: HttpClient) {
    this.currentPlayer = uuidv4();
  }

  public makeMove(row: number, col: number) {
    if (this.game!!.status != 1) {
      return;
    }

    if (this.gameType == GameType.MultiPlayer && this.currentPlayer != this.game?.turn)
      return;

    let gameMoveRequest: MakeMoveRequest = {
      row: row,
      column: col,
      player: this.game!!.turn!!
    };

    this.http.put<Game>(
      `/api/v1/game/${this.game!!.id}/move`,
      gameMoveRequest
    ).subscribe(result => {
      this.game = result;
      this.error = undefined;
    }, error => {
      if (error.status === 412) {
        this.error = error.error;
        console.info(error);
      } else {
        this.error = error.message;
        console.error(error);
      }      
    });
  }

  public getGameStatus() {
    let statusName = "";
    let playerName = this.game!!.turn === this.currentPlayer
      ? this.currentPlayerName
      : this.currentPlayerName === 'X' ? 'O' : 'X';
    let winnerPlayerName = this.game!!.turn === this.currentPlayer
      ? this.currentPlayerName
      : this.currentPlayerName === 'X' ? 'O' : 'X';
 
    switch (this.game!!.status) {
      case 0:
        statusName = "Waiting";
        break;
      case 1:
        statusName = `Player ${playerName} turn`;
        break;
      case 2:
        statusName = "Draw";
        break;
      case 3:
        statusName = `Player ${winnerPlayerName} wins`;
        break;
    }
    return statusName;
  }

  public getGameType() {
    switch (this.gameType) {
      case GameType.SinglePlayer:
        return "Tic Tac Toe in a Single Player mode";
      case GameType.MultiPlayer:
        return "Tic Tac Toe in a Multi Player mode";
    }
    return "";
  }

  public newSinglePlayerGame() {
    this.gameType = GameType.SinglePlayer;
    let newGameRequest: NewGameSinglePlayerRequest = {
      playerX: "X",
      playerO: "O"
    };

    this.http.post<Game>(
      '/api/v1/game/singleplayer',
      newGameRequest
    ).subscribe(result => {
      this.game = result;
      this.error = undefined;
    }, error => {
      if (error.status === 405) {
        this.error = error.error;
        console.info(error);
      } else {
        this.error = error.message;
        console.error(error);
      }
    });
  }

  public newMultiPlayerGame() {
    this.setUpSignalR();
    this.gameType = GameType.MultiPlayer;
    this.currentPlayerName = "X";
    let newGameRequest: NewGameMultiPlayerRequest = {
      player: this.currentPlayer!
    };

    this.http.post<Game>(
      '/api/v1/game/multiplayer',
      newGameRequest
    ).subscribe(result => {
      this.game = result
      this.error = undefined;
    }, error => {
      if (error.status === 405) {
        this.error = error.error;
        console.info(error);
      } else {
        this.error = error.message;
        console.error(error);
      }
    });
  }

  public joinMultiPlayerGame(gameId: string) {
    this.setUpSignalR();
    this.gameType = GameType.MultiPlayer;
    this.currentPlayerName = "O";
    let newGameRequest: JoinGameMultiPlayerRequest = {
      player: this.currentPlayer!
    };

    this.http.put<Game>(
      `/api/v1/game/${gameId}/join`,
      newGameRequest
    ).subscribe(result => {
      this.game = result;
      this.error = undefined;
    }, error => {
      if (error.status === 412) {
        this.error = error.error;
        console.info(error);
      } else {
        this.error = error.message;
        console.error(error);
      }
    });
  }

  public getGame(gameId: string) {
    console.log(`Getting game ${gameId}`);
    this.http.get<Game>(
      `/api/v1/game/${gameId}`
    ).subscribe(result => {
      this.game = result;
      this.error = undefined;
    }, error => {
      if (error.status === 404) {
        this.error = error.error;
        console.info(error);
      } else {
        this.error = error.message;
        console.error(error);
      }
    });
  }

  private setUpSignalR() {
    this.connection = new HubConnectionBuilder().withUrl("https://localhost:7150/gameHub").build();
    let self = this;

    this.connection.on("Game", function (user: string, game: Game) {
      console.log(`${user} says ${game.id}`);
      if (user === self.currentPlayer)
        self.getGame(game.id);
    });

    this.connection.start().then(function () {
      console.log('SignalR connected')
    }).catch(function (err) {
      return console.error(err.toString());
    });
  }
}
