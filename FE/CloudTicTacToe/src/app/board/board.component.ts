import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameService } from '../services/game.service';
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
  board: GameBoard | null = null;

  currentPlayer: 'X' | 'O' = 'X';
  winner: 'Empty' | 'X' | 'O' | null = null;

  constructor(private route: ActivatedRoute,
    private gameService: GameService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const playerIdValue = params['playerId'];

      this.gameService.initializeWithComputer({playerId: playerIdValue}).subscribe(game => {
        this.board = game;
        console.log('board rows: ', this.board?.board);
      })
    });
  }

  cellClick(cell: Cell): void {
    if (cell?.fieldState === 'Empty' && !this.winner) {
      this.gameService.playTurn({id: this.board!.id, userMark: this.currentPlayer, rowNumber: cell.rowNumber, colNumber: cell.columnNumber}).subscribe(game => {
        this.board = game;
        this.checkWinner();
        console.log('board rows: ', this.board?.board);
    });
    }
  }

  checkWinner(): void {
    for (let i = 0; i < 3; i++) {
      const row = this.board?.board[i];
      // Check rows
      if (
        row![0].fieldState !== 'Empty' &&
        row![0].fieldState === row![1].fieldState &&
        row![1].fieldState === row![2].fieldState
      ) {
        this.winner = row![0].fieldState;
        return;
      }
      // Check columns
      if (
        this.board?.board[0][i].fieldState !== 'Empty' &&
        this.board?.board[0][i].fieldState === this.board?.board[1][i].fieldState &&
        this.board?.board[1][i].fieldState === this.board?.board[2][i].fieldState
      ) {
        this.winner = this.board!.board[0][i].fieldState;
        return;
      }
    }

    // Check diagonals
    if (
      this.board?.board[0][0].fieldState !== 'Empty' &&
      this.board?.board[0][0].fieldState ===this.board?.board[1][1].fieldState &&
      this.board?.board[1][1].fieldState === this.board?.board[2][2].fieldState
    ) {
        this.winner = this.board!.board[0][0].fieldState;
      return;
    }

    if (
      this.board?.board[0][2].fieldState !== 'Empty' &&
      this.board!.board[0][2].fieldState === this.board!.board[1][1].fieldState &&
      this.board!.board[1][1].fieldState === this.board!.board[2][0].fieldState
    ) {
        this.winner = this.board!.board[0][2].fieldState;
      return;
    }
  }
}
