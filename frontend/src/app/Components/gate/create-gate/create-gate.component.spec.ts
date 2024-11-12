import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateGateComponent } from './create-gate.component';

describe('CreateGateComponent', () => {
  let component: CreateGateComponent;
  let fixture: ComponentFixture<CreateGateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateGateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateGateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
