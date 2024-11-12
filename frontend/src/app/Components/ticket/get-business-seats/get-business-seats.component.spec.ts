import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetBusinessSeatsComponent } from './get-business-seats.component';

describe('GetBusinessSeatsComponent', () => {
  let component: GetBusinessSeatsComponent;
  let fixture: ComponentFixture<GetBusinessSeatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetBusinessSeatsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetBusinessSeatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
