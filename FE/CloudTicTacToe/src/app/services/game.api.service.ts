import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameBoard } from '../models/game-board';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class GameApiService {
  private baseUrl = environment.apiUrl + '/Game';

  constructor(private http: HttpClient) {}

  getById(gameId: string): Observable<GameBoard> {
    return this.http.get<GameBoard>(this.baseUrl + '/' + gameId);
  }

  initializeWithComputer(body: { playerId: string }): Observable<GameBoard> {
    return this.http.post<GameBoard>(this.baseUrl + '', body);
  }

  initializeOrJoinOnlineGame(body: { playerId: string }): Observable<GameBoard> {
    return this.http.post<GameBoard>(this.baseUrl + '/online', body);
  }

  playTurn(body: { id: string, playerId: string, rowNumber: number, colNumber: number }): Observable<GameBoard> {
    return this.http.put<GameBoard>(this.baseUrl + '/' + body.id + '/actions/play', body);
  }
}
