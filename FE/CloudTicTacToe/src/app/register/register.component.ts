import { CommonModule } from '@angular/common';
import { Player } from '../models/player';
import { PlayerServiceService } from './../services/player-service.service';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  formData = {
    name: ''
  };
  player: Player | null = null;

  constructor(
    private playerService: PlayerServiceService,
    private router: Router)
  {}

  public onSubmit(){
    this.playerService.registerPlayer(this.formData).subscribe(p => {
      this.router.navigate(['/game'], { queryParams: { playerId: p.id } });
    });
  }
}
