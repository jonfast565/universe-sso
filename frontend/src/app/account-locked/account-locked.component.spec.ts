import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountLockedComponent } from './account-locked.component';

describe('AccountLockedComponent', () => {
  let component: AccountLockedComponent;
  let fixture: ComponentFixture<AccountLockedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountLockedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountLockedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
