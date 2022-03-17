import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginationHeaders, getPaginationResult } from '../_helpers/paginationHelper';
import { Order } from '../_models/order';
import { OrderParams } from '../_models/params/orderParams';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = environment.apiUrl;
  ordParams: OrderParams = new OrderParams();

  constructor(private http: HttpClient) { }

  getOrders(ordParams: OrderParams) {
    //return this.http.get<Customer[]>(this.baseUrl+'customers');

    let params = getPaginationHeaders(ordParams.pageNumber, ordParams.pageSize);
    return getPaginationResult<Partial<Order[]>>(this.baseUrl + 'orders', params, this.http);

  }

  createOrder(customer: Partial<Order>) {
    return this.http.post(this.baseUrl + 'customers', customer);
  }

  updateOrder(id: number, customer: Partial<Order>) {
    return this.http.put(this.baseUrl + 'customers/'+ id, customer);
  }

  getCustomerOrders(customerId: number) {
    return this.http.get<Order[]>(this.baseUrl + 'orders/OrdersByCustomerId/'+customerId);
  }

  getOrderParams() {
    return this.ordParams;
  }

  setOrderParams(params: OrderParams) {
    this.ordParams = params;
  }
}
