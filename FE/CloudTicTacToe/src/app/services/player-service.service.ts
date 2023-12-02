import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Player } from '../models/player';

@Injectable({
  providedIn: 'root',
})
export class PlayerServiceService {
  // TODO: Get api url from variable
  private apiUrl = 'http://localhost:8080/api/Player';

  constructor(private http: HttpClient) {}

  registerPlayer(): Observable<Player> {
    return this.http.post<Player>(this.apiUrl + '', {});
  }
}
