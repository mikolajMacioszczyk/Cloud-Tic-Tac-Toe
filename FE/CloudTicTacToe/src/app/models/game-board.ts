import { Cell } from "./cell";

export interface GameBoard {
  id: string;
  state: 'Ongoing' | 'Draw' | 'WinnX' | 'WinnO'
  cells: Cell[];
}
