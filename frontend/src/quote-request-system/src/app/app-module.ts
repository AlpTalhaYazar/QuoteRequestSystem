// src/app/app.module.ts

import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {RouterModule} from '@angular/router';
import {AuthInterceptor} from './interceptors/auth.interceptor';

import {NzFormModule} from 'ng-zorro-antd/form';
import {NzInputModule} from 'ng-zorro-antd/input';
import {NzInputNumberModule} from 'ng-zorro-antd/input-number'; // Add this import
import {NzButtonModule} from 'ng-zorro-antd/button';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzRadioModule} from 'ng-zorro-antd/radio';
import {NzAutocompleteModule} from 'ng-zorro-antd/auto-complete';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzPopoverModule} from 'ng-zorro-antd/popover';
import {NzTableModule} from 'ng-zorro-antd/table';
import {NzMessageModule} from 'ng-zorro-antd/message';
import * as AllIcons from '@ant-design/icons-angular/icons';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {RegisterComponent} from './components/register/register.component';
import {LoginComponent} from './components/login/login.component';
import {QuoteRequestComponent} from "./components/quote-request/quote-request.component";
import {OfferListComponent} from './components/offer-list/offer-list.component';
import {IconDefinition} from "@ant-design/icons-angular";

const antDesignIcons = AllIcons as {
  [key: string]: IconDefinition;
};
const icons: IconDefinition[] = Object.keys(antDesignIcons).map(key => antDesignIcons[key])

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    QuoteRequestComponent,
    OfferListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule,
    NzFormModule,
    NzInputModule,
    NzInputNumberModule, // Add this line
    NzButtonModule,
    NzSelectModule,
    NzRadioModule,
    NzAutocompleteModule,
    NzIconModule,
    NzIconModule.forRoot(icons),
    NzPopoverModule,
    NzTableModule,
    NzMessageModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
