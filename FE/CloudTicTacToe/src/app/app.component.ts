import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { StateService } from './services/state.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'CloudTicTacToe';

  get hasLoggedInUser(): boolean {
    return this.stateService.GetLoggedPlayerId() != null;
  }

  constructor(
    private stateService: StateService,
    private router: Router)
  {}

  logout(): void {
    this.stateService.RemovePlayerId();
    this.router.navigate(['/register']);
  }
}
