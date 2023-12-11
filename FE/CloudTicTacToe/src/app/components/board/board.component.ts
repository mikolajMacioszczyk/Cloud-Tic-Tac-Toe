import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameApiService } from '../../services/game.api.service';
import { GameBoard } from '../../models/game-board';
import { Cell } from '../../models/cell';
import { StateService } from '../../services/state.service';
import { Subscription, timer } from 'rxjs';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss',
})
export class BoardComponent implements OnInit, OnDestroy {
  private subscription: Subscription | null = null;
  private gameId: string = '';
  playerId: string = '';
  board: GameBoard | null = null;

  get isOngoing(): boolean {
    return this.board?.state === 'Ongoing';
  }

  get isWaiting(): boolean {
    return this.board?.state === 'Waiting';
  }

  get isYourTurn(): boolean {
    return this.board?.nextPlayerId == this.playerId;
  }

  get isCompleted(): boolean {
    return (
      this.board?.state === 'Draw' ||
      this.board?.state === 'WinnO' ||
      this.board?.state === 'WinnX'
    );
  }

  get stateMessage(): string {
    switch (this.board?.state) {
      case 'Draw':
        return 'Game ended with draw';
      case 'WinnO':
        return 'Player O won!';
      case 'WinnX':
        return 'Player X won!';
    }
    return '';
  }

  constructor(
    private stateService: StateService,
    private gameService: GameApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (
      !this.stateService.GetLoggedPlayerId() ||
      !this.stateService.GetActiveGameId()
    ) {
      this.router.navigate(['/']);
    }
    this.gameId = this.stateService.GetActiveGameId()!;
    this.playerId = this.stateService.GetLoggedPlayerId()!;

    // this.subscription = timer(0, 500).subscribe((_) => {
    //   this.gameService.getById(this.gameId).subscribe((board) => {
    //     this.board = board;
    //   });
    // });

    this.gameService.createChatConnection();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
    this.gameService.stopChatConnection();
  }

  cellClick(cell: Cell): void {
    if (cell?.fieldState === 'Empty' && this.isOngoing && this.isYourTurn) {
      this.gameService
        .playTurn({
          id: this.board!.id,
          playerId: this.playerId,
          rowNumber: cell.rowNumber,
          colNumber: cell.columnNumber,
        })
        .subscribe((game) => {
          this.board = game;
        });
    }
  }

  backToMenu(): void {
    this.router.navigate(['/']);
  }

  surrender(): void {
    this.gameService
      .surrender({ id: this.board!.id, playerId: this.playerId })
      .subscribe((game) => {
        this.board = game;
      });
  }
}
