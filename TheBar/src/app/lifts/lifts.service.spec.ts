import { TestBed, inject } from '@angular/core/testing';

import { LiftsService } from './lifts.service';

describe('LiftsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LiftsService]
    });
  });

  it('should be created', inject([LiftsService], (service: LiftsService) => {
    expect(service).toBeTruthy();
  }));
});
