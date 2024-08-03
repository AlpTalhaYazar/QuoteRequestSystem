import {Component, OnInit} from '@angular/core';
import {AuthService} from './services/auth.service';
import {NzMessageService} from "ng-zorro-antd/message";
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  template: `
    <div class="min-h-screen bg-gray-100">
      <nav class="bg-white shadow-md">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div class="flex justify-between h-16">
            <div class="flex">
              <div class="flex-shrink-0 flex items-center">
                <span class="text-2xl font-bold text-blue-600">ForceGet</span>
              </div>
              <div class="hidden sm:ml-6 sm:flex sm:space-x-8">
                <a *ngIf="this.authService.isLoggedInValue.value" routerLink="/quote-request"
                   routerLinkActive="text-gray-900 border-b-2 border-blue-500"
                   class="border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700 inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium">
                  New Quote Request
                </a>
                <a *ngIf="this.authService.isLoggedInValue.value" routerLink="/offer-list"
                   routerLinkActive="text-gray-900 border-b-2 border-blue-500"
                   class="border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700 inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium">
                  Offer List
                </a>
                <a *ngIf="this.authService.isLoggedInValue.value" (click)="this.authService.checkSession()"
                   routerLinkActive="text-gray-900 border-b-2 border-blue-500"
                   class="border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700 inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium">
                  check session
                </a>
              </div>
            </div>
            <div class="hidden sm:ml-6 sm:flex sm:items-center">
              <ng-container *ngIf="(!this.authService.isLoggedInValue.value); else loggedInTemplate">
                <a routerLink="/login"
                   class="text-gray-500 hover:text-gray-700 px-3 py-2 rounded-md text-sm font-medium">Login</a>
                <a routerLink="/register"
                   class="text-gray-500 hover:text-gray-700 px-3 py-2 rounded-md text-sm font-medium">Register</a>
              </ng-container>
              <ng-template #loggedInTemplate>
                <button (click)="logout()"
                        class="text-gray-500 hover:text-gray-700 px-3 py-2 rounded-md text-sm font-medium">Logout
                </button>
              </ng-template>
            </div>
          </div>
        </div>
      </nav>

      <main>
        <div class="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
          <router-outlet></router-outlet>
        </div>
      </main>
    </div>
  `,
  styles: []
})
export class AppComponent {
  title = 'ForceGet Quote App';

  constructor(protected authService: AuthService, private message: NzMessageService, private router: Router) {

  }

  logout() {
    this.authService.logout().subscribe({
      next: (response) => {
        this.message.success('Logout successful');
        // asynch call to navigate to login page after 1 second
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 1000);
      },
      error: (error) => {
        this.message.error('Failed to logout: ' + error.message);
      }
    });
  }
}
