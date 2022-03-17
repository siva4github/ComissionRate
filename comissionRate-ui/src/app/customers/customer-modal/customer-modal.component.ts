import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Customer } from 'src/app/_models/customer';

@Component({
  selector: 'app-customer-modal',
  templateUrl: './customer-modal.component.html',
  styleUrls: ['./customer-modal.component.css']
})
export class CustomerModalComponent implements OnInit {

  @Input() customerModalEmitter = new EventEmitter();
  mode!: string;
  customerForm!: FormGroup;
  customer!: Partial<Customer>;

  constructor(public bsModalRef: BsModalRef, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.customerForm = this.formBuilder.group({
      name: [this.customer.name, [Validators.required]],
      companyName: [this.customer.companyName, [Validators.required]],
      address: [this.customer.address],
      city: [this.customer.city],
      region: [this.customer.region],
      postalCode: [this.customer.postalCode, ],
      country: [this.customer.country, ],
      phone: [this.customer.phone, [Validators.required]],
    });
  }

  customerAction() {
    // this.customer.name = this.customerForm.value.name;
    // this.customer.companyName = this.customerForm.value.companyName;
    // this.customer.address = this.customerForm.value.address;
    // this.customer.city = this.customerForm.value.city;
    // this.customer.region = this.customerForm.value.region;
    // this.customer.postalCode = this.customerForm.value.postalCode;
    // this.customer.country = this.customerForm.value.country;
    // this.customer.phone = this.customerForm.value.phone;
    this.customer = this.customerForm.value;
    this.customerModalEmitter.emit(this.customer);
    this.bsModalRef.hide();
  }


}
