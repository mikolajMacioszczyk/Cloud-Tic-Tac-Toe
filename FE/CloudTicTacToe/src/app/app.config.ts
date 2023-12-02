import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { PlayerService } from './services/player.service';
import { provideHttpClient } from '@angular/common/http';
import { GameService } from './services/game.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    PlayerService,
    GameService,
    provideHttpClient()
  ]
};
