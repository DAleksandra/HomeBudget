/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EditOutgoingComponent } from './edit-outgoing.component';

describe('EditOutgoingComponent', () => {
  let component: EditOutgoingComponent;
  let fixture: ComponentFixture<EditOutgoingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditOutgoingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditOutgoingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
