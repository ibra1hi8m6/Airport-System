import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetAllGatesComponent } from './get-all-gates.component';

describe('GetAllGatesComponent', () => {
  let component: GetAllGatesComponent;
  let fixture: ComponentFixture<GetAllGatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetAllGatesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetAllGatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
