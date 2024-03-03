# Online Book Shop
This repo constains a web application for an online book shop

Backend: ASP.NET Core Web API, C#, Microsoft SQL Server database

Frontend: Angular, Typescript, HTML, SCSS


Entities: Book, ApplicationUser, Cart, Order, BookOrder, Payment


Relationships:

Book M - ApplicationUser M => Cart

ApplicationUser 1 - Order M

Book M - Order M => BookOrder

Order 1 - Payment 1


Application Users can have User role or both User and Admin role
