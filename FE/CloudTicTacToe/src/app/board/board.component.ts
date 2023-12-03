import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameService } from '../services/game.service';

type FieldType = 'X' | 'O' | null;
type UserChoice = 'X' | 'O';

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
  board: FieldType[][] = [];

  currentPlayer: UserChoice = 'X';
  winner: FieldType = null;

  constructor(private route: ActivatedRoute,
    private gameService: GameService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const playerIdValue = params['playerId'];

      this.gameService.initializeWithComputer({playerId: playerIdValue}).subscribe(game => {
        console.log('created game: ', game);
        this.board = [
          [null, null, null],
          [null, null, null],
          [null, null, null],
        ];
      })
    });
  }

  cellClick(row: number, col: number): void {
    if (!this.board[row][col] && !this.winner) {
      this.board[row][col] = this.currentPlayer;
      this.checkWinner();
      this.currentPlayer = this.currentPlayer === 'X' ? 'O' : 'X';
    }
  }

  checkWinner(): void {
    for (let i = 0; i < 3; i++) {
      // Check rows
      if (
        this.board[i][0] !== null &&
        this.board[i][0] === this.board[i][1] &&
        this.board[i][1] === this.board[i][2]
      ) {
        this.winner = this.board[i][0];
        return;
      }

      // Check columns
      if (
        this.board[0][i] !== null &&
        this.board[0][i] === this.board[1][i] &&
        this.board[1][i] === this.board[2][i]
      ) {
        this.winner = this.board[0][i];
        return;
      }
    }

    // Check diagonals
    if (
      this.board[0][0] !== null &&
      this.board[0][0] === this.board[1][1] &&
      this.board[1][1] === this.board[2][2]
    ) {
      this.winner = this.board[0][0];
      return;
    }

    if (
      this.board[0][2] !== null &&
      this.board[0][2] === this.board[1][1] &&
      this.board[1][1] === this.board[2][0]
    ) {
      this.winner = this.board[0][2];
      return;
    }
  }
}
