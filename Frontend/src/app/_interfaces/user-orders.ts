import { Book } from "./book";
import { Order } from "./order"

export interface UserOrder {
    date: Date;
    city: string;
    address: string;
    status: string;
    bookOrders: { book: Book; price: number; quantity: number }[];
    payment: { total: number, type: string };
}
