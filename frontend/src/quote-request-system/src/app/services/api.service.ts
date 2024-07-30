import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {QuoteCreateRequestDto} from "../models/quote.model";
import {OffersResponseDto} from "../models/offer.model";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = `${environment.apiUrl}`;
  private token = sessionStorage.getItem('token');

  constructor(private http: HttpClient) {
  }

  register(userData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData);
  }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials);
  }

  submitOffer(offerData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/offer`, offerData);
  }

  getOffers(): Observable<OffersResponseDto> {
    return this.http.get<OffersResponseDto>(`${this.apiUrl}/offer`);
  }

  getCountries(): Observable<any> {
    return this.http.get(`${this.apiUrl}/country`);
  }

  getCities(countryId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/city`);
  }

  getCountriesAndCities(): Observable<any> {
    return this.http.get(`${this.apiUrl}/country/with-cities`);
  }

  createQuote(quoteData: QuoteCreateRequestDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/quote`, quoteData);
  }
}
