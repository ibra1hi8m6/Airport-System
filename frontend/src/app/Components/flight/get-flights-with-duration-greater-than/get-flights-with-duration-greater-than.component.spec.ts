import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetFlightsWithDurationGreaterThanComponent } from './get-flights-with-duration-greater-than.component';

describe('GetFlightsWithDurationGreaterThanComponent', () => {
  let component: GetFlightsWithDurationGreaterThanComponent;
  let fixture: ComponentFixture<GetFlightsWithDurationGreaterThanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetFlightsWithDurationGreaterThanComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetFlightsWithDurationGreaterThanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
