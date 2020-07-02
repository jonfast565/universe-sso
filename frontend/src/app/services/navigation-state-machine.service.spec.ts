import { TestBed } from '@angular/core/testing';

import { NavigationStateMachineService } from './navigation-state-machine.service';

describe('NavigationStateMachineService', () => {
  let service: NavigationStateMachineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NavigationStateMachineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
