/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { IncomesService } from './incomes.service';

describe('Service: Incomes', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [IncomesService]
    });
  });

  it('should ...', inject([IncomesService], (service: IncomesService) => {
    expect(service).toBeTruthy();
  }));
});
