export interface QuoteCreateRequestDto {
  mode: string;
  movement_type: string;
  incoterms: string;
  country_id: number;
  city_id: number;
  package_type: string;
  package_dimension_unit: string;
  width: number;
  length: number;
  height: number;
  package_amount: number;
  weight_unit: string;
  weight_value: number;
  currency: string;
}
