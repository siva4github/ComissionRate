import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Distribution } from 'src/app/_models/distribution';
import { Company } from 'src/app/_models/params/company';
import { Product } from 'src/app/_models/product';
import { DistributionService } from 'src/app/_services/distribution.service';

@Component({
  selector: 'app-product-modal',
  templateUrl: './product-modal.component.html',
  styleUrls: ['./product-modal.component.css']
})
export class ProductModalComponent implements OnInit {

  @Input() productModalEmitter = new EventEmitter();
  mode!: string;
  productForm!: FormGroup;
  companies!: Partial<Company[]>;
  distributions!: Partial<Distribution[]>;
  product!: Partial<Product>;
  companyId!: number;
  distributionId!: number;

  constructor(public bsModalRef: BsModalRef, private formBuilder: FormBuilder, private disService: DistributionService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.productForm = this.formBuilder.group({
      name: [this.product.name, [Validators.required]],
      location: [this.product.location, [Validators.required]],
      code: [this.product.code, [Validators.required]],
      company: [this.getIndex(this.companies,this.companyId), [Validators.required]],
      distribution: [this.getIndex(this.distributions,this.distributionId), [Validators.required]],
    });
  }

  productAction() {
    console.log(this.productForm);

  }

  // Access formcontrols getter
  getFormControl(name: string) {
    return this.productForm.get(name);
  }
  
  getIndex(array: any[], id: number){
    let index = 0;
    if(id !=0 && id != undefined) {
      index = array.findIndex(c => c?.id == id);
      return array[index]?.id;
    }
    else
    { 
      return '';
    }
  }

  onCompanySelected() {
    if(this.productForm.value.company != 0)
    { 
      this.loadDistributionsFor(this.productForm.value.company);
    }
    else
    { 
      this.distributions = [];
      this.productForm.patchValue({
        distribution : this.getIndex(this.distributions,this.distributionId)
      });
    }
  }

  loadDistributionsFor(companyId: number) {
    this.disService.getDistributionsFor(companyId).subscribe({
      next : (response) => {       
        this.distributions = response;
      },
      error : (err) => { }
    });
  }

}
