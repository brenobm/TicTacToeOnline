export interface Game {
    id: string;
    board: number[][];
    turn?: string;
    status: number;
    playerX: string;
    playerY?: string;
    winner?: string;
}
