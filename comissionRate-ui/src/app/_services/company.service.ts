import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Company } from '../_models/params/company';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getCompanies() {
    return this.http.get<Company[]>(this.baseUrl+'companies');
  }
}
