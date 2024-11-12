import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetFlightsWithDoctorsComponent } from './get-flights-with-doctors.component';

describe('GetFlightsWithDoctorsComponent', () => {
  let component: GetFlightsWithDoctorsComponent;
  let fixture: ComponentFixture<GetFlightsWithDoctorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetFlightsWithDoctorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetFlightsWithDoctorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
