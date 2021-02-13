import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { CityCreateModel, CityEditModel, CityModel } from '../../../shared/classes/city';
import { CityService } from '../../../shared/services/city.service';
import { DashboardCitiesComponent } from '../dashboard-cities.component';
declare var $: any;

@Component({
  selector: 'app-dashboard-cities-add-dialog',
  templateUrl: './dashboard-cities-add-dialog.component.html'
})
export class DashboardCitiesAddDialogComponent implements OnInit, OnDestroy {

  public city: CityModel;
  @Input() countryId: string;

  public cityForm: FormGroup;
  public counter            :   number = 1;
  public variantImage       :   any = '';
  public selectedColor      :   any = '';
  public selectedSize       :   any = '';

  constructor(private fb: FormBuilder, private cityService: CityService, private dashboardCitiesComponent: DashboardCitiesComponent) {
    this.cityForm = this.fb.group({
      name: ['', [Validators.required, Validators.pattern('[a-zA-Z][a-zA-Z ]+[a-zA-Z]$')]],
      postcode: ['', Validators.required]
    });
  }

  ngOnInit() {
  }

  onSubmit() {
    var model = new CityCreateModel();

    model.name = this.cityForm.controls.name.value;
    model.postcode = this.cityForm.controls.postcode.value;
    model.countryId = this.countryId;

    if (this.cityForm.controls.name.errors == null && this.cityForm.controls.postcode.errors == null) {

      this.cityService.addItem(model);

      this.city = new CityModel();
      this.cityForm = this.fb.group({
        name: ['', [Validators.required, Validators.pattern('[a-zA-Z][a-zA-Z ]+[a-zA-Z]$')]],
        postcode: ['', Validators.required]
      });

      $('.quickviewm').modal('hide');
    }
  }

  ngOnDestroy() {
    $('.quickviewm').modal('hide');
  }
}
