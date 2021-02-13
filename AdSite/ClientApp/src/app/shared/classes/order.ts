import { CartItem } from './cart-item';

// Order
export interface Order {
    shippingDetails?: any;
    product?: CartItem;
    orderId?: any;
    totalAmount?: any;
}