import { TestBed } from '@angular/core/testing';

import { WebSettingsService } from './web-settings.service';

describe('WebSettingsService', () => {
  let service: WebSettingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WebSettingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
