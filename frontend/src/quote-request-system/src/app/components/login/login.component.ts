import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ApiService} from '../../services/api.service';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {NzMessageService} from 'ng-zorro-antd/message';

@Component({
  selector: 'app-login',
  template: `
    <div class="flex justify-center items-center h-screen bg-gray-100">
      <form nz-form [formGroup]="loginForm" (ngSubmit)="submitForm()" class="bg-white !p-8 rounded shadow-md w-96">
        <h2 class="text-2xl mb-6 text-center">Login</h2>

        <nz-form-item>
          <nz-form-control nzErrorTip="Please input your email!">
            <nz-input-group nzPrefixIcon="mail">
              <input type="email" nz-input formControlName="email" placeholder="Email"/>
            </nz-input-group>
          </nz-form-control>
        </nz-form-item>

        <nz-form-item>
          <nz-form-control nzErrorTip="Please input your password!">
            <nz-input-group nzPrefixIcon="lock">
              <input type="password" nz-input formControlName="password" placeholder="Password"/>
            </nz-input-group>
          </nz-form-control>
        </nz-form-item>

        <button nz-button nzType="primary" [disabled]="!loginForm.valid" class="w-full">Login</button>

        <div class="mt-4 text-center">
          Don't have an account? <a routerLink="/register" class="text-blue-500">Register</a>
        </div>
      </form>
    </div>
  `,
  styles: []
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private authService: AuthService,
    private router: Router,
    private message: NzMessageService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  async submitForm() {
    if (this.loginForm.valid) {
      const {email, password} = this.loginForm.value;
      this.authService.login(email, password).subscribe(response => {
        this.message.success('Login successful');
        this.router.navigate(['/offer']);
      }, error => {
        this.message.error('Login failed: ' + error.message);
      });
    }
  }
}
