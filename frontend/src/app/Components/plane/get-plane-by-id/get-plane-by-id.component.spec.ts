import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetPlaneByIdComponent } from './get-plane-by-id.component';

describe('GetPlaneByIdComponent', () => {
  let component: GetPlaneByIdComponent;
  let fixture: ComponentFixture<GetPlaneByIdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetPlaneByIdComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetPlaneByIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
