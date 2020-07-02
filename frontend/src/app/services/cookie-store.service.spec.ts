import { TestBed } from '@angular/core/testing';

import { CookieStoreService } from './cookie-store.service';

describe('CookieStoreService', () => {
  let service: CookieStoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CookieStoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
