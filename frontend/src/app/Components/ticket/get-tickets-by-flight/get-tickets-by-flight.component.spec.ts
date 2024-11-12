import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetTicketsByFlightComponent } from './get-tickets-by-flight.component';

describe('GetTicketsByFlightComponent', () => {
  let component: GetTicketsByFlightComponent;
  let fixture: ComponentFixture<GetTicketsByFlightComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetTicketsByFlightComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetTicketsByFlightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
