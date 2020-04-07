/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { OutgoingsService } from './outgoings.service';

describe('Service: Outgoings', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OutgoingsService]
    });
  });

  it('should ...', inject([OutgoingsService], (service: OutgoingsService) => {
    expect(service).toBeTruthy();
  }));
});
