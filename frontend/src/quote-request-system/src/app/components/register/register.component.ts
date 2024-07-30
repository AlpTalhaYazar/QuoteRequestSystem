import {Component} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ApiService} from '../../services/api.service';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {NzMessageService} from 'ng-zorro-antd/message';

@Component({
  selector: 'app-register',
  template: `
    <div class="flex justify-center items-center h-screen bg-gray-100">
      <form nz-form [formGroup]="registerForm" (ngSubmit)="submitForm()" class="bg-white !p-8 rounded shadow-md w-96">
        <h2 class="text-2xl mb-6 text-center">Register</h2>

        <nz-form-item>
          <nz-form-control [nzErrorTip]="firstNameErrorTpl">
            <nz-input-group nzPrefixIcon="user">
              <input type="text" nz-input formControlName="firstName" placeholder="John"/>
            </nz-input-group>
          </nz-form-control>
        </nz-form-item>

        <ng-template #firstNameErrorTpl let-control>
          <ng-container *ngIf="control.hasError('required')">
            First Name is required.
          </ng-container>
          <ng-container *ngIf="control.hasError('minlength')">
            First Name must be at least 2 characters long.
          </ng-container>
          <ng-container *ngIf="control.hasError('maxlength')">
            First Name must be at most 30 characters long.
          </ng-container>
          <ng-container *ngIf="control.hasError('pattern')">
            First Name must contain only letters.
          </ng-container>
        </ng-template>

        <nz-form-item>
          <nz-form-control [nzErrorTip]="lastNameErrorTpl">
            <nz-input-group nzPrefixIcon="user">
              <input type="text" nz-input formControlName="lastName" placeholder="Doe"/>
            </nz-input-group>
          </nz-form-control>
        </nz-form-item>

        <ng-template #lastNameErrorTpl let-control>
          <ng-container *ngIf="control.hasError('required')">
            Last Name is required.
          </ng-container>
          <ng-container *ngIf="control.hasError('minlength')">
            Last Name must be at least 2 characters long.
          </ng-container>
          <ng-container *ngIf="control.hasError('maxlength')">
            Last Name must be at most 30 characters long.
          </ng-container>
          <ng-container *ngIf="control.hasError('pattern')">
            Last Name must contain only letters.
          </ng-container>
        </ng-template>

        <nz-form-item>
          <nz-form-control [nzErrorTip]="emailErrorTpl">
            <nz-input-group nzPrefixIcon="mail">
              <input type="email" nz-input formControlName="email" placeholder="Email"/>
            </nz-input-group>
          </nz-form-control>
        </nz-form-item>

        <ng-template #emailErrorTpl let-control>
          <ng-container *ngIf="control.hasError('required')">
            Email is required.
          </ng-container>
          <ng-container *ngIf="control.hasError('email')">
            Email is invalid.
          </ng-container>
        </ng-template>

        <nz-form-item>
          <nz-form-control [nzErrorTip]="passwordErrorTpl">
            <nz-input-group nzPrefixIcon="lock">
              <input type="password" nz-input formControlName="password" placeholder="Password"/>
            </nz-input-group>
          </nz-form-control>
        </nz-form-item>

        <ng-template #passwordErrorTpl let-control>
          <ng-container *ngIf="control.hasError('required')">
            Password is required.
          </ng-container>
          <ng-container *ngIf="control.hasError('minlength')">
            Password must be at least 8 characters long.
          </ng-container>
          <ng-container *ngIf="control.hasError('pattern')">
            Password must contain at least one uppercase letter, one lowercase letter, and one number.
          </ng-container>
        </ng-template>

        <nz-form-item>
          <nz-form-control [nzErrorTip]="confirmPasswordErrorTpl">
            <nz-input-group nzPrefixIcon="lock">
              <input type="password" nz-input formControlName="confirmPassword" placeholder="Confirm Password"/>
            </nz-input-group>
          </nz-form-control>
        </nz-form-item>

        <ng-template #confirmPasswordErrorTpl let-control>
          <ng-container *ngIf="control.hasError('required')">
            Confirm Password is required.
          </ng-container>
          <ng-container *ngIf="control.hasError('mismatch')">
            Passwords do not match.
          </ng-container>
        </ng-template>

        <button nz-button nzType="primary" [disabled]="!registerForm.valid" class="w-full">Register</button>

        <div class="mt-4 text-center">
          Already have an account? <a routerLink="/login" class="text-blue-500">Login</a>
        </div>
      </form>
    </div>
  `,
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
