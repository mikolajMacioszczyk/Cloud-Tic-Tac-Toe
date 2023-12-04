import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameApiService } from '../services/game.api.service';
import { GameBoard } from '../models/game-board';
import { Cell } from '../models/cell';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss',
})
export class BoardComponent implements OnInit {
  private playerIdValue: string = '';
  board: GameBoard | null = null;

  currentPlayer: 'X' | 'O' = 'X';

  get isOngoing(): boolean {
    return this.board?.state === 'Ongoing';
  }

  get stateMessage(): string {
    switch (this.board?.state){
      case 'Draw':
        return 'Game ended with draw'
      case 'WinnO':
        return 'Player O won!'
      case 'WinnX':
        return 'Player X won!'
    }
    return '';
  }

  constructor(private route: ActivatedRoute,
    private gameService: GameApiService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.playerIdValue = params['playerId'];
      this.restartGame();
    });
  }

  cellClick(cell: Cell): void {
    if (cell?.fieldState === 'Empty' && this.isOngoing) {
      this.gameService.playTurn({id: this.board!.id, userMark: this.currentPlayer, rowNumber: cell.rowNumber, colNumber: cell.columnNumber}).subscribe(game => {
        this.board = game;
        console.log('board rows: ', this.board?.board);
    });
    }
  }

  restartGame(): void {
    this.gameService.initializeWithComputer({playerId: this.playerIdValue}).subscribe(game => {
      this.board = game;
      console.log('board rows: ', this.board?.board);
    })
  }
}
