CREATE DATABASE VideojuegosDB;

USE VideojuegosDB;

-- Creación de la tabla Videojuegos
CREATE TABLE Videojuegos (
    VideojuegoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(MAX),
    Compania NVARCHAR(MAX),
    AnoLanzamiento INT,
    Precio DECIMAL(10, 2),
    PuntajePromedio DECIMAL(4, 2) DEFAULT 0.00,
    FechaActualizacion DATETIME DEFAULT GETDATE(),
    Usuario NVARCHAR(MAX)
);

-- Creación de la tabla Calificaciones
CREATE TABLE Calificaciones (
    CalificacionID UNIQUEIDENTIFIER PRIMARY KEY,
    NicknameJugador NVARCHAR(MAX),
    VideojuegoID INT,
    Puntuacion DECIMAL(4, 2),
    FechaActualizacion DATETIME DEFAULT GETDATE(),
    Usuario NVARCHAR(MAX),
    FOREIGN KEY (VideojuegoID) REFERENCES Videojuegos(VideojuegoID)
);

-- Creación de la tabla Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(256) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    [Password] NVARCHAR(MAX) NOT NULL
);	

-- Creación de la tabla Usuarios Token
CREATE TABLE UsuariosToken (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(256) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    [Password] NVARCHAR(MAX) NOT NULL,
	Rol NVARCHAR(MAX) NOT NULL
);	
---------------------------------------------------- RETO 2 ---------------------------------------------------------
-- Insertar datos semilla para los registros 1 - 20 en la tabla Videojuegos
INSERT INTO Videojuegos (Nombre, Compania, AnoLanzamiento, Precio, PuntajePromedio, Usuario)
VALUES
    ('Dark Souls', 'From Software', 2011, 39.99, 0.00, 'Admin'),
    ('Sekiro: Shadows Die Twice', 'From Software', 2019, 59.99, 0.00, 'Admin'),
    ('Bloodborne', 'From Software', 2015, 19.99, 0.00, 'Admin'),
    ('Demon''s Souls', 'From Software', 2009, 39.99, 0.00, 'Admin'),
    ('Cuphead', 'StudioMDHR', 2017, 19.99, 0.00, 'Admin'),
    ('Contra', 'Konami', 1987, 6.99, 0.00, 'Admin'),
    ('Nioh', 'Koei Tecmo', 2017, 19.99, 0.00, 'Admin'),
    ('Celeste', 'Extremely OK Games', 2018, 7.99, 0.00, 'Admin'),
    ('Battletoads', 'Rare', 1991, 5.99, 0.00, 'Admin'),
    ('Blasphemous', 'Team17', 2019, 24.99, 0.00, 'Admin'),
    ('Teenage Mutant Ninja Turtles', 'Konami', 1989, 12.99, 0.00, 'Admin'),
    ('Ninja Gaiden Black', 'Koei Tecmo', 2005, 25.99, 0.00, 'Admin'),
    ('Ghosts ''n Goblins', 'Capcom', 1985, 6.99, 0.00, 'Admin'),
    ('Salt and Sanctuary', 'Ska Studios', 2016, 17.99, 0.00, 'Admin'),
    ('Dark Souls III', 'From Software', 2016, 59.99, 0.00, 'Admin'),
    ('Super Meat Boy', 'Direct2Drive', 2010, 5.99, 0.00, 'Admin'),
    ('Dark Souls II', 'From Software', 2014, 39.99, 0.00, 'Admin'),
    ('Hollow Knight', 'Team Cherry', 2017, 14.99, 0.00, 'Admin'),
    ('Super Mario Maker 2', 'Nintendo', 2019, 59.99, 0.00, 'Admin'),
    ('Elden Ring', 'From Software', 2022, 69.99, 0.00, 'Admin');

INSERT INTO UsuariosToken (Username, Email, Password, Rol)
VALUES
		('Sergio Lopez', 'sergiolopezm@misena.edu.co', '123Sergio', 'Admin')

INSERT INTO UsuariosToken (Username, Email, Password, Rol)
VALUES
		('Andres', 'andres@correoprueba.com', 'andres123', 'operario')

SELECT * FROM Videojuegos
SELECT * FROM Usuarios
SELECT * FROM UsuariosToken