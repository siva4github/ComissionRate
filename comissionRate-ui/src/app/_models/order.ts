export interface Order {
  id: number;
  orderDate: Date;
  requiredDate: Date;
  shipName: string;
  shipAddress: string;
  shipCity: string;
  shipRegion: string;
  shipPostalCode: string;
  shipCountry: string;
  customerId: number;
  customerName: string;
}
