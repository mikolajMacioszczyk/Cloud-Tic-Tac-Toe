import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { PlayerApiService } from './services/player.api.service';
import { provideHttpClient } from '@angular/common/http';
import { GameApiService } from './services/game.api.service';
import { StateService } from './services/state.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    PlayerApiService,
    GameApiService,
    StateService,
    provideHttpClient()
  ]
};
