import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
export class RankingComponent {
  players: Player[] = [
    { id: 'id', name: "PLayer 1", isComputer: false, points: 1 },
    { id: 'id', name: "PLayer 2", isComputer: false, points: 25 },
  ]
}
