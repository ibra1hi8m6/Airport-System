import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteGateComponent } from './delete-gate.component';

describe('DeleteGateComponent', () => {
  let component: DeleteGateComponent;
  let fixture: ComponentFixture<DeleteGateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeleteGateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteGateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
