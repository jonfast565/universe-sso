import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetCredentialComponent } from './reset-credential.component';

describe('ResetCredentialComponent', () => {
  let component: ResetCredentialComponent;
  let fixture: ComponentFixture<ResetCredentialComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResetCredentialComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetCredentialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
