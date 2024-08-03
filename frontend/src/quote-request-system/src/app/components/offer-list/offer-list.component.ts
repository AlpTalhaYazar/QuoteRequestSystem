import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import {OffersResponseDto} from "../../models/offer.model";

@Component({
  selector: 'app-offer-list',
  templateUrl: `./offer-list.component.html`,
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
      },
      error => {
        this.message.error('Failed to load offers: ' + error.message);
      }
    );
  }
}
