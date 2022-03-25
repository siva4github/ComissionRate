import { Distribution } from './../_models/distribution';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Company } from '../_models/params/company';
import { ProductParams } from '../_models/params/productParams';
import { Product } from '../_models/product';
import { CompanyService } from '../_services/company.service';
import { DistributionService } from '../_services/distribution.service';
import { ProductService } from '../_services/product.service';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products! : Partial<Product[]>;
  companies! : Partial<Company[]>;
  distributions! : Partial<Distribution[]>;
  prdForm!: FormGroup;
  public pageSizeArray: number[] = [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100];
  public prdOrderByArray: string[] = ['name','location','code'];
  prdParams!: ProductParams;
  pagination!: Pagination;

  constructor(private productService: ProductService, private companyService: CompanyService, private disService: DistributionService,
    private modalService: BsModalService, private tostr: ToastrService, private formBuilder: FormBuilder, ) { }

  ngOnInit(): void {
    this.prdParams = this.productService.getProductParams();
    this.initializeForm();
    this.loadCompanies();
    this.loadDistributions();
    this.loadProducts();
  }

  initializeForm() {
    this.prdForm = this.formBuilder.group({
      company: [this.prdParams.companyId],
      distribution: [this.prdParams.distributionId],
      orderBy: [this.prdParams.orderBy],
      pageSize: [this.prdParams.pageSize]
    });
  }

  loadProducts() {
    this.productService.getProducts(this.prdParams).subscribe({
      next : (response) => { 
        this.products = response.result; 
        this.pagination = response.pagination;
      },
      error : (err) => { this.tostr.error(err); }
    });
  }

  loadCompanies() {
    this.companyService.getCompanies().subscribe({
      next : (response) => { 
        this.companies = response;
      },
      error : (err) => {this.tostr.error(err);}
    });
  }

  loadDistributions() {
    this.disService.getDistributions().subscribe({
      next : (response) => { 
        this.distributions = response;
      },
      error : (err) => {this.tostr.error(err);}
    });
  }

  loadDistributionsFor(companyId: number) {
    this.disService.getDistributionsFor(companyId).subscribe({
      next : (response) => {         
        this.distributions = response;
      },
      error : (err) => {this.tostr.error(err);}
    });
  }

  onCompanySelected() {
    if(this.prdForm.value.company == 0)
    { 
      this.loadDistributions(); 
    }
    else
    { 
      this.loadDistributionsFor(this.prdForm.value.company);
    }

    this.prdForm.patchValue({
      distribution : 'Select Distribution'
    });
  }
  
  pageChanged(event: any) {
    this.prdParams.pageNumber = event.page;
    this.productService.setProductParams(this.prdParams);
    this.loadProducts();
  }

  createProduct() {

  }

  editProduct(product: Product) {

  }

  applyFilter() {
    console.log(this.prdForm.value);
    this.prdParams = this.productService.getProductParams();
    this.prdParams.companyId = this.stringIsNullOrEmpty(this.prdForm.value.company) ? 0 : this.prdForm.value.company;
    this.prdParams.distributionId = this.stringIsNullOrEmpty(this.prdForm.value.distribution) ? 0 : this.prdForm.value.distribution;
    this.prdParams.orderBy = this.stringIsNullOrEmpty(this.prdForm.value.orderBy) ? 0 : this.prdForm.value.orderBy;
    this.prdParams.pageSize = this.prdForm.value.pageSize;
    this.productService.setProductParams(this.prdParams);
    this.loadProducts();
  }

  resetFilter() {

  }

  stringIsNullOrEmpty(value: string): boolean {
    if(value === null || value === undefined || value === '' || value.includes('Select'))
      return true;

    return false;
  }

}
