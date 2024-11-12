import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetFlightsWithDurationLessThanComponent } from './get-flights-with-duration-less-than.component';

describe('GetFlightsWithDurationLessThanComponent', () => {
  let component: GetFlightsWithDurationLessThanComponent;
  let fixture: ComponentFixture<GetFlightsWithDurationLessThanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetFlightsWithDurationLessThanComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetFlightsWithDurationLessThanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
