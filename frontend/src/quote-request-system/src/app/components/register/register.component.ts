import {Component} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ApiService} from '../../services/api.service';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {NzMessageService} from 'ng-zorro-antd/message';

@Component({
  selector: 'app-register',
  templateUrl: `./register.component.html`,
  styles: []
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private authService: AuthService,
    private router: Router,
    private message: NzMessageService
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30), Validators.pattern(/^[a-zA-Z]+$/)]],
      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30), Validators.pattern(/^[a-zA-Z]+$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/)]],
      confirmPassword: ['', [Validators.required, this.confirmPasswordValidator]]
    });
  }

  confirmPasswordValidator = (control: AbstractControl): { [key: string]: boolean } | null => {
    if (!control.parent) {
      return null;
    }
    const password = control.parent.get('password');
    const confirmPassword = control.parent.get('confirmPassword');
    if (!password || !confirmPassword) {
      return null;
    }
    return password.value === confirmPassword.value ? null : {'mismatch': true};
  }

  submitForm() {
    if (this.registerForm.valid) {
      const {firstName, lastName, email, password} = this.registerForm.value;
      this.authService.register(email, password, firstName, lastName)
        .subscribe(
          response => {
            this.message.success('User registered successfully');
            this.router.navigate(['/login']);
          },
          error => {
            this.message.error('An error occurred');
          }
        );
    }
  }
}
