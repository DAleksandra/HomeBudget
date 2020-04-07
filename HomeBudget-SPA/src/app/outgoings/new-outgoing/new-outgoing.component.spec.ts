/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NewOutgoingComponent } from './new-outgoing.component';

describe('NewOutgoingComponent', () => {
  let component: NewOutgoingComponent;
  let fixture: ComponentFixture<NewOutgoingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewOutgoingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewOutgoingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
