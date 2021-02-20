import { TestBed } from '@angular/core/testing';

import { BaseRestService } from './base-rest.service';

describe('BaseRestService', () => {
  let service: BaseRestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BaseRestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
