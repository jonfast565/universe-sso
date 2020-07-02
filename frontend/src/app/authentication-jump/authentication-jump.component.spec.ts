import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthenticationJumpComponent } from './authentication-jump.component';

describe('AuthenticationJumpComponent', () => {
  let component: AuthenticationJumpComponent;
  let fixture: ComponentFixture<AuthenticationJumpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthenticationJumpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthenticationJumpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
