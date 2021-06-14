import {ChangeDetectorRef, Component, OnInit, ViewChild} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ProductService} from "../../../services/product.service";
import {AppAuthService} from "../../../services/auth.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {Product} from "../../../models/product";

@Component({
    selector: "app-form",
    templateUrl: "./form.component.html",
    styleUrls: ['./form.component.css']
})
export class AppFormComponent implements  OnInit{
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

    form = this.formBuilder.group({
        userId: ["", Validators.required],
        postId: ["", Validators.required],
        commentId: ["", Validators.required]
    });


    constructor(private readonly formBuilder: FormBuilder,
                private productService: ProductService,
                private auth: AppAuthService,
                private changeDetectorRefs: ChangeDetectorRef,
                ) {
        this.productForm = this.formBuilder.group({
            name: ["", Validators.required],
            description: ["", Validators.required],
            price: ["", Validators.required],
            userId: ["", Validators.required]
        });
    }

    ngOnInit() {
        this.getProducts();
        this.currentUser = this.auth.getUser();
        console.log(this.auth.getUser());
        this.currentUser.role = "admin";
        console.log(this.currentUser);
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    getProducts(){
        this.productService.productList().subscribe(products => {
            console.log(products);
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
        this.productService.addProduct(product).subscribe(newProduct => {
            console.log(newProduct);
        this.getProducts();
        })
    }

    set() {
        this.form.patchValue({ userId: "10", postId: "100", commentId: "500" });
    }

// tslint:disable-next-line:typedef
    applyFilter($event: KeyboardEvent) {
        this.filterValue = ($event.target as HTMLInputElement).value;
        this.filterValue = this.filterValue.trim(); // Remove whitespace
        this.filterValue = this.filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
        this.dataSource.filter = this.filterValue;
    }

    // editProduct(id) {
    //
    // }

    deleteProduct(id: any) {
        this.productService.deleteProduct(id).subscribe(() =>
        {
            this.getProducts();
            alert("Successfully Deleted");
        }
        );
    }
}
