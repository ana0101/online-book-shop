# OnlineBookShop
Web application for an online book shop using .net web core api for backend and angular for frontend

Entities: Book, ApplicationUser, Cart, Order, BookOrder, Payment

Relationships:
Book M - ApplicationUser M => Cart
ApplicationUser 1 - Order M
Book M - Order M => BookOrder
Order 1 - Payment 1

Application Users can have User role or both User and Admin role
