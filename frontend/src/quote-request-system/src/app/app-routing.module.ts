import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {LoginComponent} from './components/login/login.component';
import {QuoteRequestComponent} from "./components/quote-request/quote-request.component";
import {OfferListComponent} from './components/offer-list/offer-list.component';
import {AuthGuard} from './guards/auth.guard';
import {RegisterComponent} from "./components/register/register.component";

const routes: Routes = [
  {path: 'register', component: RegisterComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent, canActivate: [AuthGuard]},
  {path: 'quote-request', component: QuoteRequestComponent, canActivate: [AuthGuard]},
  {path: 'offer-list', component: OfferListComponent, canActivate: [AuthGuard]},
  {path: '', redirectTo: '/quote-request', pathMatch: 'full'},
  {path: '**', redirectTo: '/quote-request'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
