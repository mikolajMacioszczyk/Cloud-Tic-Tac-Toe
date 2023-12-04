import { TestBed } from '@angular/core/testing';

import { GameApiService } from './game.api.service';

describe('GameService', () => {
  let service: GameApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GameApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
