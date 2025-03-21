-- Eliminar tablas si existen
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.People', 'U') IS NOT NULL DROP TABLE dbo.People;
IF OBJECT_ID('dbo.Quotes', 'U') IS NOT NULL DROP TABLE dbo.Quotes;
IF OBJECT_ID('dbo.Recipes', 'U') IS NOT NULL DROP TABLE dbo.Recipes;

-- Crear tabla Users
CREATE TABLE dbo.Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

-- Insertar datos en Users
INSERT INTO dbo.Users (Username, Password) VALUES ('admin', '123456');

-- Crear tabla People
CREATE TABLE dbo.People (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    DocumentType NVARCHAR(50) NOT NULL,
    DocumentNumber NVARCHAR(50) NOT NULL,
    DateOfBirth DATETIME NOT NULL,
    PhoneNumber NVARCHAR(50),
    Email NVARCHAR(255),
    PersonType INT NOT NULL
);

-- Insertar datos en People
INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES 
('John Doe', 'PS', '123456789', '1980-01-01', '555-1234', 'john.doe@example.com', 0),
('Jane Smith', 'PI', '987654321', '1990-02-02', '555-5678', 'jane.smith@example.com', 1),
('Alice Johnson', 'PR', '123123123', '1985-03-03', '555-8765', 'alice.johnson@example.com', 0),
('Bob Brown', 'PC', '321321321', '1975-04-04', '555-4321', 'bob.brown@example.com', 1),
('Charlie Davis', 'PL', '456456456', '1982-05-05', '555-6789', 'charlie.davis@example.com', 0),
('Diana Evans', 'PP', '654654654', '1992-06-06', '555-9876', 'diana.evans@example.com', 1),
('Eve Foster', 'PE', '789789789', '1988-07-07', '555-3456', 'eve.foster@example.com', 0),
('Frank Green', 'PS', '987987987', '1978-08-08', '555-6543', 'frank.green@example.com', 1),
('Grace Harris', 'PS', '111222333', '1983-09-09', '555-7890', 'grace.harris@example.com', 0),
('Henry Irving', 'PS', '333222111', '1993-10-10', '555-0987', 'henry.irving@example.com', 1);

-- Crear tabla Quotes
CREATE TABLE dbo.Quotes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Date DATETIME NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    Status INT NOT NULL
);

-- Insertar datos en Quotes
INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES 
('2025-03-10', 'Clinic A', 1, 1, 0),
('2025-03-11', 'Clinic B', 2, 2, 1),
('2025-03-12', 'Clinic C', 3, 3, 2),
('2025-03-13', 'Clinic D', 4, 4, 0),
('2025-03-14', 'Clinic E', 5, 5, 1),
('2025-03-15', 'Clinic F', 6, 6, 2),
('2025-03-16', 'Clinic G', 7, 7, 0),
('2025-03-17', 'Clinic H', 8, 8, 1),
('2025-03-18', 'Clinic I', 9, 9, 2),
('2025-03-19', 'Clinic J', 10, 10, 0);

-- Crear tabla Recipes
CREATE TABLE dbo.Recipes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL,
    PatientId INT NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    Status INT NOT NULL
);
