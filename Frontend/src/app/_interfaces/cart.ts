import { Book } from "./book";

export interface Cart {
    quantity: number;
    book: Book;
}