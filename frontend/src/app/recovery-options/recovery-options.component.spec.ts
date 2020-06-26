import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecoveryOptionsComponent } from './recovery-options.component';

describe('RecoveryComponent', () => {
  let component: RecoveryOptionsComponent;
  let fixture: ComponentFixture<RecoveryOptionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecoveryOptionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecoveryOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
