import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ApiService} from '../../services/api.service';
import {NzMessageService} from 'ng-zorro-antd/message';
import {tr} from "date-fns/locale";
import {QuoteCreateRequestDto} from "../../models/quote.model";

@Component({
  selector: 'app-offer',
  template: `
    <form nz-form [formGroup]="quoteRequestForm" (ngSubmit)="submitForm()" class="max-w-3xl mx-auto mt-8">
      <h2 class="text-2xl font-bold mb-6">Create New Offer</h2>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Mode</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-radio-group formControlName="mode">
            <label nz-radio-button nzValue="LCL">LCL</label>
            <label nz-radio-button nzValue="FCL">FCL</label>
            <label nz-radio-button nzValue="Air">Air</label>
          </nz-radio-group>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Movement Type</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-select formControlName="movementType">
            <nz-option nzValue="Door to Door" nzLabel="Door to Door"></nz-option>
            <nz-option nzValue="Port to Door" nzLabel="Port to Door"></nz-option>
            <nz-option nzValue="Door to Port" nzLabel="Door to Port"></nz-option>
            <nz-option nzValue="Port to Port" nzLabel="Port to Port"></nz-option>
          </nz-select>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Incoterms</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-select formControlName="incoterms">
            <nz-option nzValue="DDP" nzLabel="Delivered Duty Paid (DDP)"></nz-option>
            <nz-option nzValue="DAT" nzLabel="Delivered At Place (DAT)"></nz-option>
          </nz-select>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Country-City</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-select formControlName="countryCity" nzShowSearch nzAllowClear nzPlaceHolder="Select country and city">
            <nz-option-group *ngFor="let country of countriesAndCities" [nzLabel]="country.name">
              <nz-option *ngFor="let city of country.cities" [nzValue]="'country_id=' + country.id + '~' + 'city_id=' + city.id"
                         [nzLabel]="city.name"></nz-option>
            </nz-option-group>
          </nz-select>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Package Type</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-select formControlName="packageType" (ngModelChange)="onPackageTypeChange($event)">
            <nz-option nzValue="Pallets" nzLabel="Pallets"></nz-option>
            <nz-option nzValue="Boxes" nzLabel="Boxes"></nz-option>
            <nz-option nzValue="Cartons" nzLabel="Cartons"></nz-option>
          </nz-select>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Package Dimension Unit</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-radio-group formControlName="packageDimensionUnit" (ngModelChange)="onPackageDimensionUnitChange($event)">
            <label nz-radio-button nzValue="CM">CM</label>
            <label nz-radio-button nzValue="IN">IN</label>
          </nz-radio-group>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Dimensions</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <div class="flex space-x-2">
            <nz-input-number formControlName="width" [nzDisabled]="true" [nzPlaceHolder]="'Width'"></nz-input-number>
            <nz-input-number formControlName="length" [nzDisabled]="true" [nzPlaceHolder]="'Length'"></nz-input-number>
            <nz-input-number formControlName="height" [nzDisabled]="true" [nzPlaceHolder]="'Height'"></nz-input-number>
          </div>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Package Amount</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-input-number formControlName="packageAmount"></nz-input-number>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Weight Unit</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-radio-group formControlName="weightUnit" (ngModelChange)="onWeightUnitChange($event)">
            <label nz-radio-button nzValue="KG">KG</label>
            <label nz-radio-button nzValue="LB">LB</label>
          </nz-radio-group>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Weight Value</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-input-number formControlName="weight"></nz-input-number>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-label [nzSpan]="6">Currency</nz-form-label>
        <nz-form-control [nzSpan]="14">
          <nz-select formControlName="currency">
            <nz-option nzValue="USD" nzLabel="USD"></nz-option>
            <nz-option nzValue="EUR" nzLabel="EUR"></nz-option>
            <nz-option nzValue="TRY" nzLabel="TRY"></nz-option>
          </nz-select>
        </nz-form-control>
      </nz-form-item>

      <nz-form-item>
        <nz-form-control [nzOffset]="6" [nzSpan]="14">
          <button nz-button nzType="primary" [disabled]="!quoteRequestForm.valid">Submit</button>
        </nz-form-control>
      </nz-form-item>
    </form>
  `,
  styles: []
})
export class QuoteRequestComponent implements OnInit {
  title = 'ForceGet Quote App';
  quoteRequestForm: FormGroup = new FormGroup({});
  countriesAndCities: Country[] = [];

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private message: NzMessageService
  ) {
  }

  ngOnInit() {
    this.apiService.getCountriesAndCities().subscribe(
      (response) => {
        this.countriesAndCities = response.data;
      },
      error => {
        this.message.error('Failed to load countries and cities: ' + error.message)
      }
    );

    this.quoteRequestForm = this.fb.group({
      mode: ['', Validators.required],
      movementType: ['', Validators.required],
      incoterms: ['', Validators.required],
      countryCity: ['', Validators.required],
      packageType: ['', Validators.required],
      packageDimensionUnit: ['', Validators.required],
      width: [null, Validators.required],
      length: [null, Validators.required],
      height: [null, Validators.required],
      packageAmount: [null, Validators.required],
      weightUnit: ['', Validators.required],
      weight: [null, Validators.required],
      currency: ['', Validators.required]
    });

    this.quoteRequestForm.patchValue({packageDimensionUnit: 'CM'});
  }

  submitForm() {
    const palletCount = this.calculatePalletCount();
    let validationResult = this.isPalletCountValidForMode(palletCount);

    if (!validationResult.isValid) {
      this.message.error(validationResult.message);
      return;
    }

    if (this.quoteRequestForm.valid) {
      let quoteRequestModel: QuoteCreateRequestDto = {
        mode: this.quoteRequestForm.get('mode')?.value,
        movement_type: this.quoteRequestForm.get('movementType')?.value,
        incoterms: this.quoteRequestForm.get('incoterms')?.value,
        country_id: parseInt(this.quoteRequestForm.get('countryCity')?.value.split('~')[0].split('=')[1]),
        city_id: parseInt(this.quoteRequestForm.get('countryCity')?.value.split('~')[1].split('=')[1]),
        package_type: this.quoteRequestForm.get('packageType')?.value,
        package_dimension_unit: this.quoteRequestForm.get('packageDimensionUnit')?.value,
        width: this.quoteRequestForm.get('width')?.value,
        length: this.quoteRequestForm.get('length')?.value,
        height: this.quoteRequestForm.get('height')?.value,
        package_amount: this.quoteRequestForm.get('packageAmount')?.value,
        weight_unit: this.quoteRequestForm.get('weightUnit')?.value,
        weight_value: this.quoteRequestForm.get('weight')?.value,
        currency: this.quoteRequestForm.get('currency')?.value
      }
      this.apiService.createQuote(quoteRequestModel).subscribe(
        response => {
          this.message.success('Quote request created successfully');
        },
        error => {
          this.message.error('Failed to create quote request: ' + error.message);
        }
      );
    }
  }

  onPackageTypeChange(value: string) {
    let dimensions = value === 'Pallets' ? {width: 40, length: 48, height: 60}
      : value === 'Boxes' ? {width: 24, length: 16, height: 12}
        : {width: 12, length: 12, height: 12};

    if (this.quoteRequestForm.get('packageDimensionUnit')?.value === 'IN') {
      dimensions = {
        width: this.convertCmToInch(dimensions.width),
        length: this.convertCmToInch(dimensions.length),
        height: this.convertCmToInch(dimensions.height)
      };
    }

    if (dimensions) {
      const {width, length, height} = dimensions;
      this.quoteRequestForm.patchValue({width, length, height});
    }

    const palletCount = this.calculatePalletCount();
    let validationResult = this.isPalletCountValidForMode(palletCount);

    if (!validationResult.isValid) {
      this.message.error(validationResult.message);
    }
  }

  calculatePalletCount(): number {
    const packageType = this.quoteRequestForm.get('packageType')?.value;
    const width = this.quoteRequestForm.get('width')?.value;
    const length = this.quoteRequestForm.get('length')?.value;
    const height = this.quoteRequestForm.get('height')?.value;
    const packageAmount = this.quoteRequestForm.get('packageAmount')?.value;

    let palletCount = 0;
    if (packageType && width && length && height && packageAmount) {

      if (packageType === 'Cartons') {
        const boxDimensions = {width: 24, length: 16, height: 12};
        if (boxDimensions) {
          const boxCount = Math.floor(boxDimensions.width / width) * Math.floor(boxDimensions.length / length) * Math.floor(boxDimensions.height / height);
          const palletDimensions = {width: 40, length: 48, height: 60};
          if (palletDimensions) {
            palletCount = Math.floor(palletDimensions.width / boxDimensions.width) * Math.floor(palletDimensions.length / boxDimensions.length) * Math.floor(palletDimensions.height / boxDimensions.height);
            palletCount = Math.ceil(packageAmount / (boxCount * palletCount));
          }
        }
      } else if (packageType === 'Boxes') {
        const palletDimensions = {width: 40, length: 48, height: 60};
        if (palletDimensions) {
          palletCount = Math.floor(palletDimensions.width / width) * Math.floor(palletDimensions.length / length) * Math.floor(palletDimensions.height / height);
          palletCount = Math.ceil(packageAmount / palletCount);
        }
      } else if (packageType === 'Pallets') {
        palletCount = packageAmount;
      }
    }

    return palletCount;
  }


  isPalletCountValidForMode(palletCount: number): ValidationResult {
    const mode = this.quoteRequestForm.get('mode')?.value;

    if (mode === 'LCL' && palletCount >= 24) {
      return {isValid: false, message: 'Please choose FCL mode for 24 or more pallets'};
    } else if (mode === 'FCL' && palletCount > 24) {
      return {isValid: false, message: 'You cannot ship more than 24 pallets with FCL'};
    }

    return {isValid: true, message: ''};
  }

  convertInchToCm(inches: number): number {
    return inches * 2.54;
  }

  convertCmToInch(cm: number): number {
    return cm / 2.54;
  }

  onPackageDimensionUnitChange(value: string) {
    console.log(value);
    if (value === 'CM') {
      this.convertDimensionsFromInchToCm();
    } else {
      this.converDimensionsFromCmToInch();
    }
  }

  private convertDimensionsFromInchToCm() {
    const width = this.quoteRequestForm.get('width')?.value;
    const length = this.quoteRequestForm.get('length')?.value;
    const height = this.quoteRequestForm.get('height')?.value;
    if (width && length && height) {
      this.quoteRequestForm.patchValue({
        width: this.convertInchToCm(width),
        length: this.convertInchToCm(length),
        height: this.convertInchToCm(height)
      });
    }
  }

  private converDimensionsFromCmToInch() {
    const width = this.quoteRequestForm.get('width')?.value;
    const length = this.quoteRequestForm.get('length')?.value;
    const height = this.quoteRequestForm.get('height')?.value;
    if (width && length && height) {
      this.quoteRequestForm.patchValue({
        width: this.convertCmToInch(width),
        length: this.convertCmToInch(length),
        height: this.convertCmToInch(height)
      });
    }
  }

  convertKgToLb(kg: number): number {
    return kg * 2.20462;
  }

  convertLbToKg(lb: number): number {
    return lb / 2.20462;
  }

  onWeightUnitChange(value: string) {
    if (value === 'KG') {
      this.convertWeightFromLbToKg();
    } else {
      this.convertWeightFromKgToLb();
    }
  }

  private convertWeightFromLbToKg() {
    const weight = this.quoteRequestForm.get('weight')?.value;
    if (weight) {
      this.quoteRequestForm.patchValue({weight: this.convertLbToKg(weight)});
    }
  }

  private convertWeightFromKgToLb() {
    const weight = this.quoteRequestForm.get('weight')?.value;
    if (weight) {
      this.quoteRequestForm.patchValue({weight: this.convertKgToLb(weight)});
    }
  }
}

interface ValidationResult {
  isValid: boolean;
  message: string
}

interface Country {
  id: number;
  name: string;
  cities: City[];
  created_at: string;
  updated_at: string;

}

interface City {
  id: number;
  name: string;
  created_at: string;
  updated_at: string;
}
