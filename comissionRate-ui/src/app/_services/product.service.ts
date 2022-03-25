import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginationHeaders, getPaginationResult } from '../_helpers/paginationHelper';
import { ProductParams } from '../_models/params/productParams';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl = environment.apiUrl;
  productParams!: ProductParams;
  
  constructor(private http: HttpClient) { this.resetProductParams(); }

  getProducts( productParams: ProductParams) {
    //return this.http.get<Product[]>(this.baseUrl+'products');

    let params = getPaginationHeaders(productParams.pageNumber, productParams.pageSize);
    params = params.append('companyId', productParams.companyId);
    params = params.append('distributionId', productParams.distributionId);
    params = params.append('orderBy', productParams.orderBy);

    return getPaginationResult<Partial<Product[]>>(this.baseUrl + 'products', params, this.http);
  }


  getProductParams() {
    return this.productParams;
  }

  setProductParams(params: ProductParams) {
    this.productParams = params;
  }

  resetProductParams(){
    this.productParams = new ProductParams();
    return this.productParams;
  }

}
