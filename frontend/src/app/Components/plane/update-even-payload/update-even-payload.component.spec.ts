import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateEvenPayloadComponent } from './update-even-payload.component';

describe('UpdateEvenPayloadComponent', () => {
  let component: UpdateEvenPayloadComponent;
  let fixture: ComponentFixture<UpdateEvenPayloadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateEvenPayloadComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateEvenPayloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
