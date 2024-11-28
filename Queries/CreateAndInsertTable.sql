CREATE DATABASE SQLInjectionDemo;

USE SQLInjectionDemo;

CREATE TABLE Users (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

INSERT INTO Users (Username, Password)
VALUES ('admin', 'password123'),
       ('testuser', 'test123');