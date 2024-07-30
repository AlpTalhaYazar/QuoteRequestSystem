import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import {OffersResponseDto} from "../../models/offer.model";

@Component({
  selector: 'app-offer-list',
  template: `
    <div class="container mx-auto p-6">
      <h2 class="text-2xl font-bold mb-4">Offer List</h2>
      <nz-table #basicTable>
        <thead>
          <tr>
            <th>ID</th>
            <th>Quote Id</th>
            <th>Offer Currency Type</th>
            <th>Offer Amount</th>
            <th>Creation Date</th>
            <th>Last Update</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let offer of offers">
            <td>{{ offer.id }}</td>
            <td>{{ offer.quote_id }}</td>
            <td>{{ offer.offer_currency_type }}</td>
            <td>{{ offer.offer_amount }}</td>
            <td>{{ offer.created_at }}</td>
            <td>{{ offer.updated_at }}</td>
          </tr>
        </tbody>
      </nz-table>
    </div>
  `,
  styles: []
})
export class OfferListComponent implements OnInit {
  offers: OffersResponseDto[] = [];

  constructor(
    private apiService: ApiService,
    private message: NzMessageService
  ) {}

  ngOnInit() {
    this.loadOffers();
  }

  loadOffers() {
    this.apiService.getOffers().subscribe(
      (response: any) => {
        this.offers = response.data;
        console.log(this.offers);
      },
      error => {
        this.message.error('Failed to load offers: ' + error.message);
      }
    );
  }
}
