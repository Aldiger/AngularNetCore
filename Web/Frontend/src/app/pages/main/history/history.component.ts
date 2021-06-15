import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {ProductService} from "../../../services/product.service";

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  history: any;

  constructor(@Inject(MAT_DIALOG_DATA) public data:any, private productService: ProductService) { }

  ngOnInit(): void {
    this.fetchHistory()
  }

  fetchHistory(){
    this.productService.getHistory(this.data.recordId).subscribe(history => this.history = history );
  }

}
