import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetAllPlanesComponent } from './get-all-planes.component';

describe('GetAllPlanesComponent', () => {
  let component: GetAllPlanesComponent;
  let fixture: ComponentFixture<GetAllPlanesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetAllPlanesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetAllPlanesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
