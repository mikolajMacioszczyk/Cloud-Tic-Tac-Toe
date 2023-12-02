import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameBoard } from '../models/game-board';

@Injectable({
  providedIn: 'root'
})
export class GameService {
// TODO: Get api url from variable
private apiUrl = 'http://localhost:8080/api/Game';

constructor(private http: HttpClient) {}

initializeWithComputer(body: {playerId: string}): Observable<GameBoard> {
  return this.http.post<GameBoard>(this.apiUrl + '', body);
}
}