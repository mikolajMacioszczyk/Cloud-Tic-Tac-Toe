import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Player } from '../models/player';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PlayerApiService {
  private baseUrl = environment.apiUrl + '/Player';

  constructor(private http: HttpClient) {}

  getAllPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(this.baseUrl);
  }

  registerPlayer(body: {name: string}): Observable<Player> {
    return this.http.post<Player>(this.baseUrl + '', body);
  }
}
