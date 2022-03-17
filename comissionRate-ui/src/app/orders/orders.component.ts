import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Order } from '../_models/order';
import { Pagination } from '../_models/pagination';
import { OrderParams } from '../_models/params/orderParams';
import { OrderService } from '../_services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  orders! : Partial<Order[]>;
  pagination!: Pagination;
  ordParams!: OrderParams;

  constructor(private tostr: ToastrService, private orderService: OrderService) { }

  ngOnInit(): void {
    this.ordParams = this.orderService.getOrderParams();
    this.loadOrders();
  }

  loadOrders() {
    this.orderService.getOrders(this.ordParams).subscribe({
      next : (response) => { 
        this.orders = response.result; 
        this.pagination = response.pagination;
      },
      error : (err) => { this.tostr.error(err); }
    });
  }

  createOrder () {

  }

  pageChanged(event: any) {
    this.ordParams.pageNumber = event.page;
    this.orderService.setOrderParams(this.ordParams);
    this.loadOrders();
  }

}
