import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { CityCreateModel, CityEditModel, CityModel } from '../../../shared/classes/city';
import { CityService } from '../../../shared/services/city.service';
declare var $: any;

@Component({
  selector: 'app-dashboard-cities-dialog',
  templateUrl: './dashboard-cities-dialog.component.html'
})
export class DashboardCitiesDialogComponent implements OnInit, OnDestroy {

  @Input() city: CityModel;

  public cityForm: FormGroup;
  public counter            :   number = 1;
  public variantImage       :   any = '';
  public selectedColor      :   any = '';
  public selectedSize       :   any = '';

  constructor(private fb: FormBuilder, private cityService: CityService) {
    this.cityForm = this.fb.group({
      id: [this.city?.id, Validators.required],
      countryId: [this.city?.country?.id, Validators.required],
      name: [this.city?.name, [Validators.required, Validators.pattern('[a-zA-Z][a-zA-Z ]+[a-zA-Z]$')]],
      postcode: [this.city?.postcode, Validators.required]
    })
  }

  ngOnInit() {
  }

  onSubmit() {
    var model = new CityEditModel();

    model.id = this.cityForm.controls.id.value;
    model.name = this.cityForm.controls.name.value;
    model.postcode = this.cityForm.controls.postcode.value;
    model.countryId = this.cityForm.controls.countryId.value;

    if (this.cityForm.controls.name.errors == null && this.cityForm.controls.postcode.errors == null) {
      this.cityService.updateItem(model);
      $('.quickviewm').modal('hide');
    }
  }

  ngOnDestroy() {
    $('.quickviewm').modal('hide');
  }
}
