CREATE DATABASE discount;

CREATE TABLE Coupon (
    ID SERIAL PRIMARY KEY NOT NULL,
    ProductName VARCHAR(100) NOT NULL,
    Description TEXT,
    Amount int
);