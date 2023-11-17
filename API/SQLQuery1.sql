USE master;
CREATE DATABASE EmployeeDatabase;
USE EmployeeDatabase;
CREATE TABLE Employees
(
GPN NVARCHAR(50) PRIMARY KEY,
Email NVARCHAR(100) NOT NULL,
ContactNumber NVARCHAR(20) NOT NULL,
address NVARCHAR (300) NOT NULL
);
SELECT * FROM Employees;
INSERT INTO Employees (GPN, ContactNumber, Email, Address)
VALUES
    ('IN010178815', '1234567890', 'example@email.com', '123 Street, City','Name1'),
    ('IN010178816', '9876543210', 'another@example.com', '456 Avenue, Town', 'Name2');

ALTER TABLE Employees ADD Name NVARCHAR(50);

update Employees add name ="ABCD" where GPN="IN010178813"

UPDATE Employees
SET Name = 'ABCD'