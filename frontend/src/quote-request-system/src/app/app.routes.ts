import {Routes} from '@angular/router';
import {RegisterComponent} from './components/register/register.component';
import {LoginComponent} from './components/login/login.component';
import {OfferListComponent} from './components/offer-list/offer-list.component';
import {QuoteRequestComponent} from "./components/quote-request/quote-request.component";

export const routes: Routes = [
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: LoginComponent},
  {path: 'quote-request', component: QuoteRequestComponent},
  {path: 'offer-list', component: OfferListComponent},
  {path: '', redirectTo: '/logina', pathMatch: 'full'}
];
