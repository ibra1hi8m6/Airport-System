import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePlaneComponent } from './delete-plane.component';

describe('DeletePlaneComponent', () => {
  let component: DeletePlaneComponent;
  let fixture: ComponentFixture<DeletePlaneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeletePlaneComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeletePlaneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
