import { Component, OnInit } from '@angular/core';
import { StateService } from '../../services/state.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

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
  constructor(private stateService: StateService, private router: Router)
  {}

  ngOnInit(): void {
    const playerId = this.stateService.GetLoggedPlayerId();
    // TODO: If player id exists and game id exists - navigate to game
    if (!playerId){
      this.router.navigate(['/register']);
    }
  }

  initializeGameWithComputer(){
    this.router.navigate(['/game']);
  }
}
