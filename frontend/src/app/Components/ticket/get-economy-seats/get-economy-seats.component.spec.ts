import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetEconomySeatsComponent } from './get-economy-seats.component';

describe('GetEconomySeatsComponent', () => {
  let component: GetEconomySeatsComponent;
  let fixture: ComponentFixture<GetEconomySeatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetEconomySeatsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetEconomySeatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
