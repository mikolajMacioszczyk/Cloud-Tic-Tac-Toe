import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameBoard } from '../models/game-board';
import { environment } from '../../environments/environment';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { StateService } from './state.service';

@Injectable({
  providedIn: 'root',
})
export class GameApiService {
  private baseUrl = environment.apiUrl + '/Game';
  private chatConnection?: HubConnection;
  gameBoard: GameBoard | null = null;

  constructor(private http: HttpClient, private stateService: StateService) {}

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

  surrender(body: { id: string, playerId: string }): Observable<GameBoard> {
    return this.http.put<GameBoard>(this.baseUrl + '/' + body.id + '/actions/surrender', body);
  }

  createChatConnection(){
    this.chatConnection = new HubConnectionBuilder()
      .withUrl(`${environment.hubUrl}/hubs/game`)
      .withAutomaticReconnect()
      .build();

    this.chatConnection.start().catch(error => {
      console.log(error);
    });

    this.chatConnection.on('UserConnected', () => {
      this.addGameConnnectionId();
    });

    this.chatConnection.on('BoardUpdated', board => {
      this.gameBoard = board;
      console.log('got: ', board);
    });
  }

  stopChatConnection(){
    this.chatConnection?.stop().catch(error => console.log(error));
  }

  private async addGameConnnectionId() {
    return this.chatConnection?.invoke('AddGameConnectionId', this.stateService.GetActiveGameId())
      .catch(error => console.log(error));
  }
}
