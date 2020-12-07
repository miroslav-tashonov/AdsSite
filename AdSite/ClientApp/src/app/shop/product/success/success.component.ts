import { Component, OnInit } from '@angular/core';
import { Order } from '../../../shared/classes/order';
import { OrderService } from '../../../shared/services/order.service';

@Component({
  selector: 'app-order-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.scss']
})
export class SuccessComponent implements OnInit {
  
  public orderDetails : Order = {};

  constructor(private orderService: OrderService) { }

  ngOnInit() {
  	this.orderDetails = this.orderService.getOrderItems();
  }

}
