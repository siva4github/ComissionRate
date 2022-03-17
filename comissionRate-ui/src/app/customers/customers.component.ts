import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Customer } from '../_models/customer';
import { Order } from '../_models/order';
import { Pagination } from '../_models/pagination';
import { CustomerParams } from '../_models/params/customerParams';
import { CustomerService } from '../_services/customer.service';
import { CustomerModalComponent } from './customer-modal/customer-modal.component';
import { OrderModalComponent } from './order-modal/order-modal.component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  customers! : Partial<Customer[]>;
  orders! : Partial<Order[]>;
  bsModelRef!: BsModalRef;
  pagination!: Pagination;
  custParams!: CustomerParams;

  constructor(private customerService: CustomerService, private modalService: BsModalService, private tostr: ToastrService) { }

  ngOnInit(): void {
    this.custParams = this.customerService.getCustomerParams();
    this.loadCustomers();
  }

  loadCustomers() {
    this.customerService.getCustomers(this.custParams).subscribe({
      next : (response) => { 
        this.customers = response.result; 
        this.pagination = response.pagination;
      },
      error : (err) => { this.tostr.error(err); }
    });
  }

  createCustomer() {
    let cust : Partial<Customer> = { };

    const config = {
      class: 'modal-dialog-center',
      initialState: {
        mode: 'add',
        customer: cust
      }
    };

    this.bsModelRef = this.modalService.show(CustomerModalComponent, config);
    this.bsModelRef.content.customerModalEmitter.subscribe((value: any) => {
      
      this.customerService.createCustomer(value).subscribe({
        next : () => { 
          this.loadCustomers(); 
          this.tostr.success('Customer created successfully');
        },
        error: (err) => { this.tostr.error(err); }
      });
    });
  }

  editCustomer(cust: Partial<Customer>) {
    
    const config = {
      class: 'modal-dialog-center',
      initialState: {
        mode: 'update',
        customer: cust
      }
    };

    this.bsModelRef = this.modalService.show(CustomerModalComponent, config);
    this.bsModelRef.content.customerModalEmitter.subscribe((value: any) => {
      
      this.customerService.updateCustomer(cust.id!, value).subscribe({
        next : () => { 
          this.loadCustomers(); 
          this.tostr.success('Customer updated successfully');
        },
        error: (err) => { this.tostr.error(err); }
      });

    });
  }

  viewOrders(cust: Partial<Customer>) {
    this.customerService.getCustomerOrders(cust.id!).subscribe({
      next : (response) => { 
        this.orders = response;

        const config = {
          class: 'modal-dialog-center modal-xl',
          initialState: {
            customer: cust,
            orders: this.orders
          }
        };
    
        this.bsModelRef = this.modalService.show(OrderModalComponent, config);

      },
      error : () => {}
    });
  }

  pageChanged(event: any) {
    this.custParams.pageNumber = event.page;
    this.customerService.setCustomerParams(this.custParams);
    this.loadCustomers();
  }

}
