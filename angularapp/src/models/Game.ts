import { Player } from "./Player";

export interface Game {
    id: string;
    board: number[][];
    turn: Player;
    status: number;
    playerX: Player;
    playerY: Player;
    winner?: Player;
}
