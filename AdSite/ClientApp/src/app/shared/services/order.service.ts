import { Injectable } from '@angular/core';
import { Product } from '../classes/product';
import { CartItem } from '../classes/cart-item';
import { Order } from '../classes/order';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class OrderService {
  
  // Array
  public OrderDetails;

  constructor(private router: Router) { }

  // Get order items
  public getOrderItems() : Order {
    return this.OrderDetails;
  }

  // Create order
  public createOrder(product: any, details: any, orderId: any, amount: any) {
    var item = {
        shippingDetails: details,
        product: product,
        orderId: orderId,
        totalAmount: amount
    };
    this.OrderDetails = item;
    this.router.navigate(['/home/checkout/success']);
  }

}
