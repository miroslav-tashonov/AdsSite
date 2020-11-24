import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryViewModel } from '../models/CategoryViewModel';

@Component({
  selector: 'app-categories-menu',
  templateUrl: './categories-menu.component.html',
  styleUrls: ['./categories-menu.component.css']
})

export class CategoriesMenuComponent implements OnInit {
  @Input() componentCategories$?: CategoryViewModel[];
  constructor() {
  }
  ngOnInit(): void {
  }
}
