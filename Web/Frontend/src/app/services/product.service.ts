import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

const PRODUCT_URL = '/products';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  productList(){
   return  this.http.get(PRODUCT_URL);
  }

    addProduct(product: any) {
        return this.http.post(PRODUCT_URL,product);
    }

    deleteProduct(id: any) {
        return this.http.delete(`${PRODUCT_URL}/${id}`);
    }
}
