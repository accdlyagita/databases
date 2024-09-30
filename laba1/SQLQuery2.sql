CREATE DATABASE PetStoreDB;
GO
USE PetStoreDB;
GO
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL
);

CREATE TABLE Pets (
    PetID INT PRIMARY KEY IDENTITY(1,1),
    PetName NVARCHAR(50) NOT NULL,
    CategoryID INT,
    Price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    OrderDate DATE NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    PetID INT,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (PetID) REFERENCES Pets(PetID)
);
GO

-- Заповнення таблиці Categories
INSERT INTO Categories (CategoryName) VALUES
('Dogs'),
('Cats'),
('Fish');

-- Заповнення таблиці Pets
INSERT INTO Pets (PetName, CategoryID, Price) VALUES
('Golden Retriever', 1, 500.00),
('Persian Cat', 2, 300.00),
('Goldfish', 3, 10.00);

-- Заповнення таблиці Customers
INSERT INTO Customers (CustomerName, Phone) VALUES
('John Doe', '123-456-7890'),
('Jane Smith', '098-765-4321');

-- Заповнення таблиці Orders
INSERT INTO Orders (CustomerID, OrderDate) VALUES
(1, '2023-09-01'),
(2, '2023-09-05');

-- Заповнення таблиці OrderDetails
INSERT INTO OrderDetails (OrderID, PetID, Quantity) VALUES
(1, 1, 1),
(2, 3, 2);
GO
