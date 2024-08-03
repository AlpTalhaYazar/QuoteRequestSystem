import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ApiService} from '../../services/api.service';
import {NzMessageService} from 'ng-zorro-antd/message';
import {tr} from "date-fns/locale";
import {QuoteCreateRequestDto} from "../../models/quote.model";

@Component({
  selector: 'app-offer',
  templateUrl: `./quote-request.component.html`,
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
