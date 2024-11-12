import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetPlanesByPayloadComponent } from './get-planes-by-payload.component';

describe('GetPlanesByPayloadComponent', () => {
  let component: GetPlanesByPayloadComponent;
  let fixture: ComponentFixture<GetPlanesByPayloadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetPlanesByPayloadComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetPlanesByPayloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
