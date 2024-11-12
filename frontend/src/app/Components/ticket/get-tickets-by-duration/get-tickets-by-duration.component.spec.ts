import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetTicketsByDurationComponent } from './get-tickets-by-duration.component';

describe('GetTicketsByDurationComponent', () => {
  let component: GetTicketsByDurationComponent;
  let fixture: ComponentFixture<GetTicketsByDurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetTicketsByDurationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetTicketsByDurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
