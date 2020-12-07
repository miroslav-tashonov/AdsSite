import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
declare var $: any;

@Component({
  selector: 'app-age-verification',
  templateUrl: './age-verification.component.html',
  styleUrls: ['./age-verification.component.scss']
})
export class AgeVerificationComponent implements OnInit {
  
  public authForm   :  FormGroup;
  public currdate
  public setDate

  constructor(private fb: FormBuilder) { 
  	this.authForm = this.fb.group({
      day: ['', Validators.required],
      month: ['', Validators.required],
      year: ['', Validators.required],
    })
  }

  ngOnInit() {
    if(localStorage.getItem('popState') != 'shown'){
    	$('.agem').modal({
         backdrop: 'static',
         keyboard: false
      });
    }
  }

  ageForm() {
    

        var day = this.authForm.value.day;
        var month = this.authForm.value.month;
        var year = this.authForm.value.year;
        var age = 18;
        var mydate = new Date();
        mydate.setFullYear(year, month-1, day);

        var currdate = new Date();
        this.currdate = currdate;
        var setDate = new Date();         
        this.setDate = setDate.setFullYear(mydate.getFullYear() + age, month-1, day);

        if ((this.currdate - this.setDate) > 0){
          $('.agem').modal('hide');
          localStorage.setItem('popState','shown')
        } else {
          window.location.href = "https://www.google.com/";
        }

  }

}
