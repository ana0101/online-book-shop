import { Book } from "./book";

export interface UserOrder {
    id: number;
    date: Date;
    city: string;
    address: string;
    status: string;
    bookOrders: { book: Book; price: number; quantity: number }[];
    payment: { total: number, type: string };
}
