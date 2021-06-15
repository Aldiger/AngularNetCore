import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {ProductService} from "../../../services/product.service";
import {AppAuthService} from "../../../services/auth.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-update-product-form',
  templateUrl: './update-product-form.component.html',
  styleUrls: ['./update-product-form.component.css']
})
export class UpdateProductFormComponent implements OnInit {

  recordId!: number;
  paginator: any;
  datasource: any;
  currentUser!: any;
  updateProductForm!: FormGroup;

  constructor( private productService: ProductService,
               private fb: FormBuilder,
               private auth: AppAuthService,
               public dialogRef: MatDialogRef<UpdateProductFormComponent>,
               @Inject(MAT_DIALOG_DATA) public data:any) {
  }

  ngOnInit(): void {
    this.currentUser = this.auth.getUser();
    this.updateProductForm = this.fb.group({
      id: [''],
      name: [''],
      description: [''],
      price: [''],
      dateAdded: [''],
      dateModified: [''],
      userId: [''],
    });
    this.fetchRecord();
  }

   public fetchRecord(): void{
    this.recordId = this.data.recordId;
    this.paginator = this.data.paginator;
    this.datasource = this.data.datasource;

    this.productService.readProduct(this.recordId).subscribe(product => {
      this.updateProductForm.patchValue(product);
    });
  }

  updateProduct() {
    this.productService.productUpdated(this.updateProductForm.value);
    this.productService.updateProduct(this.updateProductForm.value.id, this.updateProductForm.value).subscribe(() => {
      window.location.reload();
      alert("Successfully Updated!");
    });
    this.dialogRef.close();
  }

}
