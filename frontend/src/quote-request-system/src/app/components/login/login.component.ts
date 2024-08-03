import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ApiService} from '../../services/api.service';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {NzMessageService} from 'ng-zorro-antd/message';

@Component({
  selector: 'app-login',
  templateUrl: `./login.component.html`,
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
