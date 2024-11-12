import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetPlanesBySeatsComponent } from './get-planes-by-seats.component';

describe('GetPlanesBySeatsComponent', () => {
  let component: GetPlanesBySeatsComponent;
  let fixture: ComponentFixture<GetPlanesBySeatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetPlanesBySeatsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetPlanesBySeatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
