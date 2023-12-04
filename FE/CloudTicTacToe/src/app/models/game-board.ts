import { Cell } from "./cell";

export interface GameBoard {
  id: string;
  state: 'Waiting' | 'Ongoing' | 'Draw' | 'WinnX' | 'WinnO';
  board: Cell[][];
  nextPlayerId: string;
}
