import { CommonModule } from '@angular/common';
import { Player } from '../models/player';
import { PlayerServiceService } from './../services/player-service.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  player: Player | null = null;

  constructor(private playerService: PlayerServiceService)
  {}

  public register(){
    this.playerService.registerPlayer().subscribe(p => {
      this.player = p;
    });
  }
}
