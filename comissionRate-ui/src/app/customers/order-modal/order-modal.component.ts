import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Customer } from 'src/app/_models/customer';
import { Order } from 'src/app/_models/order';

@Component({
  selector: 'app-order-modal',
  templateUrl: './order-modal.component.html',
  styleUrls: ['./order-modal.component.css']
})
export class OrderModalComponent implements OnInit {
  
  customer!: Partial<Customer>;
  orders! : Partial<Order[]>;

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

}
