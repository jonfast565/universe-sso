import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SetRecoveryOptionsComponent } from './set-recovery-options.component';

describe('RecoveryComponent', () => {
  let component: SetRecoveryOptionsComponent;
  let fixture: ComponentFixture<SetRecoveryOptionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetRecoveryOptionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetRecoveryOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
