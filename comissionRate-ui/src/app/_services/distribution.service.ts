import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Distribution } from '../_models/distribution';

@Injectable({
  providedIn: 'root'
})
export class DistributionService {

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getDistributions() {
    return this.http.get<Distribution[]>(this.baseUrl+'distributions');
  }

  getDistributionsFor(companyId: number) {
    return this.http.get<Distribution[]>(this.baseUrl+'distributions/'+companyId);
  }
}
