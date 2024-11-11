CREATE DATABASE PetStoreDB;
GO

USE PetStoreDB;
GO

-- Створення таблиці Employee
CREATE TABLE Employee (
    EmployeeID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Position NVARCHAR(50)
);

-- Створення таблиці Owner
CREATE TABLE Owner (
    OwnerID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Phone NVARCHAR(15)
);

-- Створення таблиці Product
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50),
    Price DECIMAL(10, 2),
    Stock INT
);

-- Створення таблиці Pet
CREATE TABLE Pet (
    PetID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50),
    Species NVARCHAR(50),
    Breed NVARCHAR(50),
    Age INT
);

-- Створення таблиці Order (із зовнішніми ключами на таблиці Owner і Employee)
CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY,
    OrderDate DATE,
    OwnerID INT,
    EmployeeID INT,
    FOREIGN KEY (OwnerID) REFERENCES Owner(OwnerID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

-- Створення таблиці OrderProduct (із зовнішніми ключами на таблиці Order і Product)
CREATE TABLE OrderProduct (
    OrderID INT,
    ProductID INT,
    Quantity INT,
    PRIMARY KEY (OrderID, ProductID),
    FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Наповнення таблиць даними для тестування
-- Додавання даних до таблиці Employee
INSERT INTO Employee (FirstName, LastName, Position) 
VALUES ('John', 'Doe', 'Manager'), 
       ('Jane', 'Smith', 'Cashier'),
       ('Robert', 'Brown', 'Sales');

-- Додавання даних до таблиці Owner
INSERT INTO Owner (FirstName, LastName, Phone) 
VALUES ('Alice', 'Johnson', '1234567890'), 
       ('Bob', 'Williams', '0987654321');

-- Додавання даних до таблиці Product
INSERT INTO Product (Name, Price, Stock) 
VALUES ('Dog Food', 25.99, 100),
       ('Cat Toy', 5.50, 200),
       ('Fish Tank', 100.00, 10);

-- Додавання даних до таблиці Pet
INSERT INTO Pet (Name, Species, Breed, Age) 
VALUES ('Buddy', 'Dog', 'Labrador', 5),
       ('Whiskers', 'Cat', 'Persian', 3),
       ('Goldie', 'Fish', 'Goldfish', 1);

-- Додавання даних до таблиці Order
INSERT INTO [Order] (OrderDate, OwnerID, EmployeeID)
VALUES ('2024-10-01', 1, 1),
       ('2024-10-02', 2, 2);

-- Додавання даних до таблиці OrderProduct
INSERT INTO OrderProduct (OrderID, ProductID, Quantity) 
VALUES (1, 1, 2), 
       (1, 2, 3), 
       (2, 3, 1);
