import { PlayerApiService } from './../../services/player.api.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Player } from '../../models/player';

@Component({
  selector: 'app-ranking',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './ranking.component.html',
  styleUrl: './ranking.component.scss'
})
export class RankingComponent implements OnInit {
  players: Player[] = []

  constructor(private playerService: PlayerApiService)
  {}

  ngOnInit(): void {
    this.playerService.getAllPlayers().subscribe(players => {
      this.players = players
        .filter(p => !p.isComputer)
        .sort((p1, p2) => p2.points - p1.points);
    })
  }
}
