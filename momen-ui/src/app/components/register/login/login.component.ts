import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CredentialService } from 'src/app/core/services';
import { Router } from '@angular/router';
import { LoginSliderList } from 'src/app/core/lists';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  slideConfig = { slidesToShow: 1, slidesToScroll: 1, autoplay: true, autoplaySpeed: 1000, dots: false, arrows: false };

  sliderList: any[] = LoginSliderList;

  constructor(
    private credentialService: CredentialService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.createForm();
  }

  createForm() {
    this.loginForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      password: ['', Validators.required],
      rememberMe: ['']
    });
  }

  ngOnInit() {
    if (localStorage.getItem('clinic-username')) {
      this.loginForm.patchValue({});
      this.loginForm.setValue({
        name: localStorage.getItem('clinic-username'),
        password: localStorage.getItem('clinic-password'),
        rememberMe: true
      });
    }
  }

  public errorHandling = (control: string, error: string) => {
    return this.loginForm.controls[control].hasError(error);
  }

  login() {
    // this.repository.post('account/login', this.loginForm.value).subscribe(
    //   (res: any) => {

    //     if (this.loginForm.value.rememberMe) {
    //       localStorage.setItem('clinic-username', this.loginForm.value.name);
    //       localStorage.setItem('clinic-password', this.loginForm.value.password);
    //     } else {
    //       localStorage.removeItem('clinic-username');
    //       localStorage.removeItem('clinic-password');
    //     }

    //     localStorage.setItem('momen-token', res.token);
    //     if (this.credentialService.isDoctor()) {
    //       this.router.navigate(['/home']);
    //     } else if (this.credentialService.isNurse()) {
    //       this.router.navigate(['/home/ar']);
    //     }
    //   });
  }

}
