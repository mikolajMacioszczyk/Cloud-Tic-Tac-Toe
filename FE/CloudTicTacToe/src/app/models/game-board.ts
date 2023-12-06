import { Cell } from "./cell";
import { Player } from "./player";

export interface GameBoard {
  id: string;
  state: 'Waiting' | 'Ongoing' | 'Draw' | 'WinnX' | 'WinnO';
  board: Cell[][];
  nextPlayerId: string;
  playerX: Player | null;
  playerO: Player | null;
}
