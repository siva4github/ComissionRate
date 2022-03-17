import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginationHeaders, getPaginationResult } from '../_helpers/paginationHelper';
import { Customer } from '../_models/customer';
import { Order } from '../_models/order';
import { CustomerParams } from '../_models/params/customerParams';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  baseUrl = environment.apiUrl;
  custParams: CustomerParams = new CustomerParams();

  constructor(private http: HttpClient) { }

  getCustomers(custParams: CustomerParams) {
    //return this.http.get<Customer[]>(this.baseUrl+'customers');

    let params = getPaginationHeaders(custParams.pageNumber, custParams.pageSize);
    return getPaginationResult<Partial<Customer[]>>(this.baseUrl + 'customers', params, this.http);

  }

  createCustomer(customer: Partial<Customer>) {
    return this.http.post(this.baseUrl + 'customers', customer);
  }

  updateCustomer(id: number, customer: Partial<Customer>) {
    return this.http.put(this.baseUrl + 'customers/'+ id, customer);
  }

  getCustomerOrders(customerId: number) {
    return this.http.get<Order[]>(this.baseUrl + 'orders/OrdersByCustomerId/'+customerId);
  }

  getCustomerParams() {
    return this.custParams;
  }

  setCustomerParams(params: CustomerParams) {
    this.custParams = params;
  }
}
