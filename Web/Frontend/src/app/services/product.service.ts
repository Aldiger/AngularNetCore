import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Subject} from "rxjs";

const PRODUCT_URL = '/products';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
    private updatedProductSource = new Subject<any>();
    // Observable string streams
    updatedProduct$ = this.updatedProductSource.asObservable();

  constructor(private http: HttpClient) { }

     productList(){
     return  this.http.get(PRODUCT_URL);
     }

    addProduct(product: any) {
        return this.http.post(PRODUCT_URL,product);
    }

    readProduct(id: any){
      return this.http.get(`${PRODUCT_URL}/${id}`);
    }

    deleteProduct(id: any) {
        return this.http.delete(`${PRODUCT_URL}/${id}`);
    }

    updateProduct(id: any, product: any) {
        return this.http.put(`${PRODUCT_URL}/${id}`, product);
    }

    productUpdated(newProduct: any){
        this.updatedProductSource.next(newProduct);
    }

    getHistory(id: any){
        return this.http.get(`${PRODUCT_URL}/audit/${id}`);
    }
}
