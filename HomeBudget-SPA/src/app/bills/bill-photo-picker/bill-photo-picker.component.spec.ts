/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BillPhotoPickerComponent } from './bill-photo-picker.component';

describe('BillPhotoPickerComponent', () => {
  let component: BillPhotoPickerComponent;
  let fixture: ComponentFixture<BillPhotoPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BillPhotoPickerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BillPhotoPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
