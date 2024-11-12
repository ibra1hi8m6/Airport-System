import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetFlightsByPilotIdComponent } from './get-flights-by-pilot-id.component';

describe('GetFlightsByPilotIdComponent', () => {
  let component: GetFlightsByPilotIdComponent;
  let fixture: ComponentFixture<GetFlightsByPilotIdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetFlightsByPilotIdComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetFlightsByPilotIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
