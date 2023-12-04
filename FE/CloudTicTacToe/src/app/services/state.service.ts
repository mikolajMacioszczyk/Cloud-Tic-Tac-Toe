import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  private prefix: string = 'tictactoe_';

  constructor() { }

  GetLoggedPlayerId() : string | null {
    return localStorage.getItem(this.prefix + 'playerId');
  }

  SetLoggedPlayerId(playerId: string) : void {
    return localStorage.setItem(this.prefix + 'playerId', playerId);
  }

  RemovePlayerId() : void {
    return localStorage.removeItem(this.prefix + 'playerId');
  }

  GetActiveGameId() : string | null {
    return localStorage.getItem(this.prefix + 'gameId');
  }

  SetActiveGameId(gameId: string) : void {
    return localStorage.setItem(this.prefix + 'gameId', gameId);
  }
}
