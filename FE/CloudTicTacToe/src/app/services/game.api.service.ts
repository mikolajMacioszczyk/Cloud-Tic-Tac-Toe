import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameBoard } from '../models/game-board';

@Injectable({
  providedIn: 'root',
})
export class GameApiService {
  // TODO: Get api url from variable
  private apiUrl = 'http://localhost:8080/api/Game';

  constructor(private http: HttpClient) {}

  getById(gameId: string): Observable<GameBoard> {
    return this.http.get<GameBoard>(this.apiUrl + '/' + gameId);
  }

  initializeWithComputer(body: { playerId: string }): Observable<GameBoard> {
    return this.http.post<GameBoard>(this.apiUrl + '', body);
  }

  playTurn(body: { id: string, userMark: string, rowNumber: number, colNumber: number }): Observable<GameBoard> {
    return this.http.put<GameBoard>(this.apiUrl + '/' + body.id + '/actions/play', body);
  }
}
