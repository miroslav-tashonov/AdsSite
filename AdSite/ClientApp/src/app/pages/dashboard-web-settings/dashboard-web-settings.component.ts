import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { WebSettingsModel } from '../../shared/classes/web-settings';
import { CountryService } from '../../shared/services/country.service';
import { WebSettingsService } from '../../shared/services/web-settings.service';

@Component({
  selector: 'app-dashboard-web-settings',
  templateUrl: './dashboard-web-settings.component.html',
  styleUrls: ['./dashboard-web-settings.component.scss']
})
export class DashboardWebSettingsComponent implements OnInit {

  //public webSettings$: Observable<WebSettingsModel> = of();
  public countryId: string;
  public webSettingsForm: FormGroup;

  constructor(private fb: FormBuilder, private webSettingsService: WebSettingsService, private countryService: CountryService, private locationStrategy: LocationStrategy) {
    this.webSettingsForm = this.fb.group({
      title: ['', Validators.required],
      logoimage: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', Validators.required],
      address: ['', Validators.required],
      fbSocialLink: [''],
      instaSocialLink: [''],
      twitterSocialLink: [''],
      vkSocialLink: [''],
    });


    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.webSettingsService.getWebSettingsModel(country.id).subscribe(
          webSettings => {
            this.webSettingsForm = this.fb.group({
              title: [webSettings.title, Validators.required],
              logoimage: [webSettings.logoImagePath, Validators.required],
              phone: [webSettings.phone, Validators.required],
              email: [webSettings.email, Validators.required],
              address: [webSettings.address, Validators.required],
              fbSocialLink: [webSettings.facebookSocialLink],
              instaSocialLink: [webSettings.instagramSocialLink],
              twitterSocialLink: [webSettings.twitterSocialLink],
              vkSocialLink: [webSettings.vkSocialLink],
            });
          }
        );
        this.countryId = country.id;
      }
    });
  }

  ngOnInit() {
  }

  onSubmit() {
    var model = new WebSettingsModel();

    model.title = this.webSettingsForm.controls.title.value;
    model.logoImagePath = this.webSettingsForm.controls.logoimage.value;
    model.phone = this.webSettingsForm.controls.phone.value;
    model.email = this.webSettingsForm.controls.email.value;
    model.address = this.webSettingsForm.controls.address.value;
    model.facebookSocialLink = this.webSettingsForm.controls.fbSocialLink.value;
    model.instagramSocialLink = this.webSettingsForm.controls.instaSocialLink.value;
    model.twitterSocialLink = this.webSettingsForm.controls.twitterSocialLink.value;
    model.vkSocialLink = this.webSettingsForm.controls.vkSocialLink.value;
    model.countryId = this.countryId;

    if (this.webSettingsForm.controls.title.errors == null && this.webSettingsForm.controls.logoimage.errors == null &&
      this.webSettingsForm.controls.phone.errors == null && this.webSettingsForm.controls.email.errors == null &&
      this.webSettingsForm.controls.address.errors == null)
    {
      this.webSettingsService.updateItem(model);
    }
  }

}
