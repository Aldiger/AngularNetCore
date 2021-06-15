import {AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ProductService} from "../../../services/product.service";
import {AppAuthService} from "../../../services/auth.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {Product} from "../../../models/product";
import {MatDialog} from "@angular/material/dialog";
import {UpdateProductFormComponent} from "../update-product-form/update-product-form.component";
import {Subscription} from "rxjs";
import {HistoryComponent} from "../history/history.component";

@Component({
    selector: "app-form",
    templateUrl: "./form.component.html",
    styleUrls: ['./form.component.css']
})
export class AppFormComponent implements  OnInit, AfterViewInit, OnDestroy{
    disabled = false;
    products: Product[] = [];
    productForm: FormGroup;
    currentUser: any;
    dataSource = new MatTableDataSource<Product[]>();
    filterValue!: string;
    loading = false;
    displayedColumns: string[] = ['name', 'description', 'price', 'actions'];
    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;
    postUpdated: Subscription;


    constructor(private readonly formBuilder: FormBuilder,
                private productService: ProductService,
                private auth: AppAuthService,
                public dialog: MatDialog,
                private changeDetectorRefs: ChangeDetectorRef,
                ) {
        this.productForm = this.formBuilder.group({
            name: ["", Validators.required],
            description: ["", Validators.required],
            price: ["", Validators.required],
            userId: ["", Validators.required]
        });

         this.postUpdated = productService.updatedProduct$.subscribe(
            newProduct => {
                console.log(newProduct);
    });}

    ngOnInit() {
        this.getProducts();
        this.currentUser = this.auth.getUser();
        if (this.currentUser.name == 'Administrator Administrator'){
            this.currentUser.role = "admin";
        } else {
            this.currentUser.role = 'worker';
        }

    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    getProducts(){
        this.productService.productList().subscribe(products => {
            // @ts-ignore
            this.dataSource.data = products;
            this.dataSource.sort = this.sort;
        });
        this.changeDetectorRefs.detectChanges();
    }

    addProduct(){
        const product = {
            name: this.productForm.value.name,
            description: this.productForm.value.description,
            price: this.productForm.value.price,
            userId: this.currentUser.id
        };
        this.productService.addProduct(product).subscribe(() => {
        this.getProducts();
        })
    }

// tslint:disable-next-line:typedef
    applyFilter($event: KeyboardEvent) {
        this.filterValue = ($event.target as HTMLInputElement).value;
        this.filterValue = this.filterValue.trim(); // Remove whitespace
        this.filterValue = this.filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
        this.dataSource.filter = this.filterValue;
    }

    deleteProduct(id: any) {
        this.productService.deleteProduct(id).subscribe(() =>
        {
            this.getProducts();
            alert("Successfully Deleted");
        }
        );
    }

    editProduct(id: any) {
        this.dialog.open(UpdateProductFormComponent, {
            data: {recordId: id, paginator: this.paginator, dataSource: this.dataSource}
        });
    }

    showHistory(id: any) {
        this.dialog.open(HistoryComponent, {
            data: {recordId: id, paginator: this.paginator, dataSource: this.dataSource}
        });    }

    ngOnDestroy() {
        // prevent memory leak when component destroyed
        this.postUpdated.unsubscribe();
    }
}
