import { TestBed } from '@angular/core/testing';

import { PlayerApiService } from './player.api.service';

describe('PlayerServiceService', () => {
  let service: PlayerApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlayerApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
