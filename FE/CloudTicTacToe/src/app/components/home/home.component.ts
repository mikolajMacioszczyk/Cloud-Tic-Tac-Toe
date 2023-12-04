import { Component, OnInit } from '@angular/core';
import { StateService } from '../../services/state.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { GameApiService } from '../../services/game.api.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ 
    CommonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  constructor(
    private stateService: StateService, 
    private gameService: GameApiService,
    private router: Router)
  {}

  ngOnInit(): void {
    const playerId = this.stateService.GetLoggedPlayerId();
    // TODO: If player id exists and game id exists - navigate to game
    if (!playerId){
      this.router.navigate(['/register']);
    }
  }

  initializeGameWithComputer(){
    this.gameService.initializeWithComputer({playerId: this.stateService.GetLoggedPlayerId()!}).subscribe(game => {
      this.stateService.SetActiveGameId(game.id);
      this.router.navigate(['/game']);
    });
  }
}
