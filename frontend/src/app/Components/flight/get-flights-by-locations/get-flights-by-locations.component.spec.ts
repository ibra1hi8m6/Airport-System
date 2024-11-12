import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetFlightsByLocationsComponent } from './get-flights-by-locations.component';

describe('GetFlightsByLocationsComponent', () => {
  let component: GetFlightsByLocationsComponent;
  let fixture: ComponentFixture<GetFlightsByLocationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetFlightsByLocationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetFlightsByLocationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
