---Samuel Zea🤡y Karen Jimenez🤡
CREATE DATABASE Recorcholis
GO
USE Recorcholis
GO


---Las tablas:
CREATE TABLE Hoteles(

Id_Hotel INT IDENTITY,
Nombre VARCHAR(20)NOT NULL,
Dueños VARCHAR(30)NOT NULL
);

ALTER TABLE Hoteles 
ADD CONSTRAINT PK_Id_Hotel PRIMARY KEY (Id_Hotel)

INSERT INTO Hoteles ( Nombre, Dueños) 
VALUES ( 'Recochita', 'Samuel y Karen') 


CREATE TABLE Huespedes(

Id_Huesped INT NOT NULL,
Nombre VARCHAR(20)NOT NULL,
Apellido VARCHAR(20)NOT NULL,
Fecha_Naci DATE NOT NULL,
Edad INT NOT NULL,
Sexo CHAR(1) CHECK (Sexo = 'F' or Sexo = 'M') NOT NULL,
Telefono VARCHAR(15)NOT NULL,
Correo VARCHAR(30)NOT NULL,
Historial_Reserva VARCHAR(30)DEFAULT 'No hay historial'
);

ALTER TABLE Huespedes
ADD CONSTRAINT PK_Id_Huesped PRIMARY KEY (Id_Huesped)

GO

CREATE FUNCTION dbo.CalcularEdad (@FechaNaci DATE) 
RETURNS INT 
AS
BEGIN
    RETURN (DATEDIFF(YEAR, @FechaNaci, GETDATE()) - 
        CASE 
            WHEN (MONTH(@FechaNaci) > MONTH(GETDATE())) 
                 OR (MONTH(@FechaNaci) = MONTH(GETDATE()) AND DAY(@FechaNaci) > DAY(GETDATE())) 
            THEN 1 
            ELSE 0 
        END);
END;

GO

INSERT INTO Huespedes (Id_Huesped, Nombre, Apellido, Fecha_Naci, Edad, Sexo, Telefono, Correo)  
VALUES 
(1023888288, 'ZAMUEL', 'ZEA', '2006-06-04', dbo.CalcularEdad('2006-06-04'), 'M', '317849494', 'ZAMUELZEA@gmail.com'),
(1023888299, 'CARLOS', 'LOPEZ', '2002-03-15', dbo.CalcularEdad('2002-03-15'), 'M', '315678123', 'CARLOSLOPEZ@gmail.com'),
(1023888300, 'MARIA', 'GOMEZ', '1998-12-22', dbo.CalcularEdad('1998-12-22'), 'F', '312456789', 'MARIAGOMEZ@gmail.com'),
(1023888311, 'JUAN', 'PEREZ', '1995-07-10', dbo.CalcularEdad('1995-07-10'), 'M', '318765432', 'JUANPEREZ@gmail.com'),
(1023888322, 'LAURA', 'RAMIREZ', '2001-09-30', dbo.CalcularEdad('2001-09-30'), 'F', '310987654', 'LAURARAMIREZ@gmail.com'),
(1023888333, 'PEDRO', 'FERNANDEZ', '1993-05-05', dbo.CalcularEdad('1993-05-05'), 'M', '319654321', 'PEDROFERNANDEZ@gmail.com'),
(1023888344, 'SOFIA', 'MARTINEZ', '1997-11-12', dbo.CalcularEdad('1997-11-12'), 'F', '311222333', 'SOFIAMARTINEZ@gmail.com'),
(1023888355, 'ANDRES', 'GARCIA', '2000-02-18', dbo.CalcularEdad('2000-02-18'), 'M', '314789456', 'ANDRESGARCIA@gmail.com'),
(1023888366, 'FERNANDA', 'RODRIGUEZ', '1996-08-25', dbo.CalcularEdad('1996-08-25'), 'F', '316555777', 'FERNANDARODRIGUEZ@gmail.com'),
(1023888377, 'DANIEL', 'HERRERA', '2003-04-07', dbo.CalcularEdad('2003-04-07'), 'M', '317111222', 'DANIELHERRERA@gmail.com'),
(1023888388, 'VALENTINA', 'TORRES', '1994-10-14', dbo.CalcularEdad('1994-10-14'), 'F', '318999888', 'VALENTINATORRES@gmail.com'),
(1023558880, 'KARMEN', 'JIMENEZ', '2005-06-04', dbo.CalcularEdad('2005-06-04'), 'F', '31577888', 'KARMENJIMENEZ@gmail.com');


CREATE TABLE Sedes(
Id_Sede INT IDENTITY,
Id_Hotel INT DEFAULT '1',
Direccion VARCHAR(30)NOT NULL,
Locacion CHAR(15) CHECK (Locacion = 'Bello' or Locacion = 'Anori' or Locacion = 'Medellin') NOT NULL
);

ALTER TABLE Sedes
ADD CONSTRAINT PK_Id_Sede PRIMARY KEY (Id_Sede)

INSERT INTO Sedes ( Direccion, Locacion) 
VALUES ( 'Calle 44', 'Anori'), ( 'Calle 42', 'Bello'),( 'Calle 556', 'Medellin') 


CREATE TABLE Empleados(

Id_Empleado INT NOT NULL,
Id_Hotel INT DEFAULT '1',
Id_Sede INT CHECK(Id_Sede = 1 or Id_Sede = 2 or Id_Sede = 3)NOT NULL,
Nombre VARCHAR(20)NOT NULL,
Apellido VARCHAR(20)NOT NULL,
Fecha_Naci DATE NOT NULL,
Edad INT NOT NULL,
Sexo CHAR(1) CHECK (Sexo = 'F' or Sexo = 'M') NOT NULL,
Fecha_Contratacion DATE NOT NULL,
Rol VARCHAR(20) CHECK (Rol = 'Recepcionista' or Rol = 'Gerente' or Rol= 'Seguridad' or Rol= 'Piscinero' or Rol= 'Cocinero' or Rol= 'Aseador' or Rol= 'Mantenimiento' ) NOT NULL,
);

ALTER TABLE Empleados
ADD CONSTRAINT PK_Id_Empleado PRIMARY KEY (Id_Empleado)

INSERT INTO Empleados (Id_Empleado, Id_Sede, Nombre, Apellido, Fecha_Naci, Edad, Sexo, Fecha_Contratacion, Rol)  
VALUES  
(1001, 1, 'Maria', 'Garza', '1989-02-01', DATEDIFF(YEAR, '1989-02-01', GETDATE()), 'F', '2024-06-04', 'Recepcionista'),  
(1002, 2, 'Carlos', 'Mendoza', '1995-07-10', DATEDIFF(YEAR, '1995-07-10', GETDATE()), 'M', '2024-07-15', 'Recepcionista'),  
(1003, 3, 'Laura', 'Ramirez', '1992-11-20', DATEDIFF(YEAR, '1992-11-20', GETDATE()), 'F', '2024-08-30', 'Recepcionista'),  
(2001, 1, 'Roberto', 'Fernández', '1980-03-25', DATEDIFF(YEAR, '1980-03-25', GETDATE()), 'M', '2024-09-10', 'Gerente'),  
(2002, 2, 'Ana', 'Lopez', '1985-06-18', DATEDIFF(YEAR, '1985-06-18', GETDATE()), 'F', '2025-01-12', 'Gerente'),  
(2003, 3, 'Javier', 'Gutierrez', '1978-10-05', DATEDIFF(YEAR, '1978-10-05', GETDATE()), 'M', '2025-02-20', 'Gerente'),  
(3001, 1, 'Juan', 'Hernandez', '1990-05-30', DATEDIFF(YEAR, '1990-05-30', GETDATE()), 'M', '2024-03-22', 'Seguridad'),  
(3002, 2, 'Pedro', 'Ortega', '1993-12-15', DATEDIFF(YEAR, '1993-12-15', GETDATE()), 'M', '2024-04-01', 'Seguridad'),  
(3003, 3, 'Sofía', 'Martínez', '1991-09-08', DATEDIFF(YEAR, '1991-09-08', GETDATE()), 'F', '2024-05-10', 'Seguridad'),  
(4001, 1, 'Luis', 'Suarez', '1987-04-12', DATEDIFF(YEAR, '1987-04-12', GETDATE()), 'M', '2024-06-05', 'Piscinero'),  
(4002, 2, 'Antonio', 'Vargas', '1992-08-29', DATEDIFF(YEAR, '1992-08-29', GETDATE()), 'M', '2024-07-18', 'Piscinero'),  
(4003, 3, 'Fernando', 'Diaz', '1990-01-22', DATEDIFF(YEAR, '1990-01-22', GETDATE()), 'M', '2024-08-08', 'Piscinero'),  
(5001, 1, 'Marta', 'Reyes', '1985-07-19', DATEDIFF(YEAR, '1985-07-19', GETDATE()), 'F', '2024-09-20', 'Cocinero'),  
(5002, 2, 'Elena', 'Torres', '1988-11-25', DATEDIFF(YEAR, '1988-11-25', GETDATE()), 'F', '2025-10-05', 'Cocinero'),  
(5003, 3, 'Hugo', 'Gómez', '1990-03-10', DATEDIFF(YEAR, '1990-03-10', GETDATE()), 'M', '2025-02-15', 'Cocinero'),  
(6001, 1, 'Gloria', 'Santos', '1975-06-14', DATEDIFF(YEAR, '1975-06-14', GETDATE()), 'F', '2024-03-10', 'Aseador'),  
(6002, 2, 'Pablo', 'Navarro', '1982-10-09', DATEDIFF(YEAR, '1982-10-09', GETDATE()), 'M', '2024-07-03', 'Aseador'),  
(6003, 3, 'Daniela', 'Rojas', '1991-12-03', DATEDIFF(YEAR, '1991-12-03', GETDATE()), 'F', '2024-08-25', 'Aseador'),  
(7001, 1, 'Jorge', 'Pérez', '1978-02-18', DATEDIFF(YEAR, '1978-02-18', GETDATE()), 'M', '2024-11-12', 'Mantenimiento'),  
(7002, 2, 'Oscar', 'Hidalgo', '1986-05-20', DATEDIFF(YEAR, '1986-05-20', GETDATE()), 'M', '2025-04-17', 'Mantenimiento'),  
(7003, 3, 'Verónica', 'Castillo', '1994-07-22', DATEDIFF(YEAR, '1994-07-22', GETDATE()), 'F', '2025-05-10', 'Mantenimiento');


CREATE TABLE Habitaciones(

Id_Habitacion INT IDENTITY,
Id_Sede INT CHECK(Id_Sede = 1 or Id_Sede = 2 or Id_Sede = 3)NOT NULL,
Id_Hotel INT DEFAULT '1',
Numero_Habitacion VARCHAR(4)NOT NULL,
Precio_Habitacion DECIMAL(10,3)NOT NULL,
Camas INT NOT NULL,
Estado VARCHAR(15) CHECK (Estado = 'Disponible' or Estado = 'Ocupado' or Estado= 'Reservada') NOT NULL,
Capacidad VARCHAR(20) DEFAULT 'Maximo 6 Huespedes'
);

ALTER TABLE Habitaciones
ADD CONSTRAINT PK_Id_Habitacion PRIMARY KEY (Id_Habitacion)

INSERT INTO Habitaciones ( Id_Sede, Numero_Habitacion, Precio_Habitacion, Camas, Estado) 
VALUES
(1, '101', 100.000, 2, 'Disponible'),
(2, '102', 120.000, 3, 'Disponible'),
(3, '103', 150.000, 4, 'Disponible'),
(1, '101', 100.000, 2, 'Disponible'),
(2, '102', 120.000, 3, 'Disponible'),
(3, '103', 150.000, 4, 'Disponible'),
(1, '101', 100.000, 2, 'Disponible'),
(2, '102', 120.000, 3, 'Disponible'),
(3, '103', 150.000, 4, 'Disponible'),
(1, '104', 80.000, 1, 'Ocupado'),
(2, '104', 80.000, 1, 'Ocupado'),
(3, '104', 80.00, 1, 'Ocupado');


CREATE TABLE Reservas(

Id_Reserva INT IDENTITY,
Id_Huesped INT NOT NULL,
Id_Sede INT CHECK (Id_Sede = '1' or Id_Sede = '2' or Id_Sede = '3') NOT NULL,
Estado_Actual VARCHAR(15) CHECK (Estado_Actual = 'Pendiente' or Estado_Actual = 'Confirmado' or Estado_Actual= 'Cancelado')NOT NULL,
Fecha_Reserva DATE NOT NULL,
Numero_Huespedes VARCHAR(15)NOT NULL
);

ALTER TABLE Reservas
ADD CONSTRAINT PK_Id_Reserva PRIMARY KEY (Id_Reserva)

INSERT INTO Reservas( Id_Huesped, Id_Sede, Estado_Actual, Fecha_Reserva, Numero_Huespedes)
VALUES 
(1023888288, 1, 'Confirmado', GETDATE(), 2),
(1023888288, 1, 'Pendiente', GETDATE(), 3),
(1023888299, 2, 'Confirmado', GETDATE(), 1),
(1023888300, 3, 'Confirmado', GETDATE(), 2),
(1023888311, 1, 'Pendiente', GETDATE(), 4),
(1023888322, 2, 'Confirmado', GETDATE(), 3),
(1023888333, 3, 'Pendiente', GETDATE(), 2),
(1023888344, 1, 'Confirmado', GETDATE(), 5),
(1023888355, 2, 'Pendiente', GETDATE(), 3),
(1023888366, 3, 'Confirmado', GETDATE(), 6),
(1023888377, 1, 'Confirmado', GETDATE(), 1),
(1023888388, 2, 'Pendiente', GETDATE(), 4),
(1023558880, 3, 'Confirmado', GETDATE(), 2);


CREATE TABLE Reservas_Habitaciones(

Id_Reserva_Habitacion INT IDENTITY,
Id_Reserva INT NOT NULL,
Id_Habitacion INT NOT NULL,
Precio_Dia DECIMAL(10,3) NOT NULL
);

ALTER TABLE Reservas_Habitaciones
ADD CONSTRAINT PK_Id_Reserva_Habitacion PRIMARY KEY (Id_Reserva_Habitacion)

INSERT INTO Reservas_Habitaciones( Id_Reserva, Id_Habitacion, Precio_Dia)
VALUES 
(1, 1, 100.000),
(2, 2, 150.000),
(3, 3, 200.000),
(4, 1, 100.000),
(5, 2, 150.000),
(6, 3, 200.000),
(7, 4, 100.000),
(8, 1, 150.000),
(9, 2, 200.000),
(10, 3, 100.000),
(11, 4, 150.000),
(12, 1, 200.000),
(13, 2, 100.000);


CREATE TABLE ServiciosExtras(

Id_ServicioExtra INT IDENTITY,
Id_Sede INT CHECK(Id_Sede = 1 or Id_Sede = 2 or Id_Sede = 3) NOT NULL,
Piscina VARCHAR(5) CHECK (Piscina = 'Si' or Piscina = 'No') NOT NULL,
Restaurante VARCHAR(5) CHECK (Restaurante = 'Si' or Restaurante = 'No') NOT NULL,
Limpieza VARCHAR(5) CHECK (Limpieza = 'Si' or Limpieza = 'No') NOT NULL,
Mantenimiento VARCHAR(5) CHECK (Mantenimiento = 'Si' or Mantenimiento = 'No') NOT NULL,
Gimnasio VARCHAR(5) CHECK (Gimnasio = 'Si' or Gimnasio = 'No') NOT NULL,
Jacuzzi VARCHAR(5) CHECK (Jacuzzi = 'Si' or Jacuzzi = 'No') NOT NULL
);

ALTER TABLE ServiciosExtras
ADD CONSTRAINT PK_Id_ServicioExtra PRIMARY KEY (Id_ServicioExtra)

INSERT INTO ServiciosExtras( Id_Sede, Piscina, Restaurante, Limpieza, Mantenimiento, Gimnasio, Jacuzzi)
VALUES ( 1, 'Si', 'No', 'No', 'No', 'Si', 'Si'), ( 2, 'No', 'Si', 'No', 'No', 'No', 'Si'), ( 3, 'Si', 'Si', 'Si', 'No', 'No', 'No'),
( 1, 'No', 'No', 'No', 'Si', 'No', 'Si'), ( 2, 'Si', 'Si', 'Si', 'No', 'Si', 'No'), ( 3, 'No', 'No', 'No', 'No', 'No', 'Si')


CREATE TABLE Empleados_ServiciosExtras(

Id_Empleado_ServicioExtra INT IDENTITY,
Id_empleado INT NOT NULL,
Id_ServicioExtra INT NOT NULL,
Precio_Servicio DECIMAL(10,3)NOT NULL,
Pago_Servicio DECIMAL(10,3)NOT NULL
);

ALTER TABLE Empleados_ServiciosExtras
ADD CONSTRAINT PK_Id_Empleado_ServicioExtra PRIMARY KEY (Id_Empleado_ServicioExtra)

INSERT INTO Empleados_ServiciosExtras( Id_empleado, Id_ServicioExtra, Precio_Servicio, Pago_Servicio)
VALUES 
(4001, 1, 55.000, 22.000),
(4003, 3, 55.000, 22.000),
(5002, 2, 60.000, 25.000),
(5003, 3, 60.000, 25.000),
(5002, 4, 60.000, 25.000),
(6003, 3, 70.000, 30.000),
(6002, 5, 70.000, 28.000),
(7001, 4, 70.000, 30.000);


CREATE TABLE Sedes_ServiciosExtras(

Id_Sedes_ServiciosExtras INT IDENTITY,
Id_Sede INT CHECK(Id_Sede = 1 or Id_Sede = 2 or Id_Sede = 3) NOT NULL,
Id_ServicioExtra INT NOT NULL,
Descuento_Sede DECIMAL(10,2)NOT NULL
);

ALTER TABLE Sedes_ServiciosExtras
ADD CONSTRAINT PK_Id_Sedes_ServiciosExtras PRIMARY KEY (Id_Sedes_ServiciosExtras)

INSERT INTO Sedes_ServiciosExtras (Id_Sede, Id_ServicioExtra, Descuento_Sede)
VALUES
(1, 1, 0.20),
(2, 2, 0.10),
(3, 3, 0.20),
(1, 4, 0.05),
(2, 5, 0.10),
(3, 6, 0.05);


CREATE TABLE Estadias(

Id_Estadia INT IDENTITY,
Id_Reserva INT NOT NULL ,
Fecha_Entrada DATE NOT NULL,
Fecha_Salida DATE NOT NULL,
);

ALTER TABLE Estadias
ADD CONSTRAINT PK_Id_Estadia PRIMARY KEY (Id_Estadia)

INSERT INTO Estadias( Id_Reserva, Fecha_Entrada, Fecha_Salida)
VALUES
(1, GETDATE(), DATEADD(DAY, 3, GETDATE())),
(2, GETDATE(), DATEADD(DAY, 2, GETDATE())),
(3, GETDATE(), DATEADD(DAY, 1, GETDATE())),
(4, GETDATE(), DATEADD(DAY, 1, GETDATE())),
(5, GETDATE(), DATEADD(DAY, 2, GETDATE())),
(6, GETDATE(), DATEADD(DAY, 3, GETDATE())),
(7, GETDATE(), DATEADD(DAY, 2, GETDATE())),
(8, GETDATE(), DATEADD(DAY, 5, GETDATE())),
(9, GETDATE(), DATEADD(DAY, 3, GETDATE())),
(10, GETDATE(), DATEADD(DAY, 1, GETDATE())),
(11, GETDATE(), DATEADD(DAY, 2, GETDATE())),
(12, GETDATE(), DATEADD(DAY, 4, GETDATE())),
(13, GETDATE(), DATEADD(DAY, 3, GETDATE()));


CREATE TABLE Facturas(

Id_Factura INT IDENTITY,
Id_ServicioExtra INT NOT NULL,
Id_reserva INT NOT NULL,
Id_Estadia INT NOT NULL,
Total DECIMAL(10,3) DEFAULT 50.000 NOT NULL,
Metodo_Pago VARCHAR(30) CHECK(Metodo_Pago = 'Tarjeta de credito' or Metodo_Pago = 'Efectivo' or Metodo_Pago = 'Transferencia') NOT NULL,
Cargos_Extra VARCHAR(30) CHECK(Cargos_Extra = 'Ninguno' or Cargos_Extra = 'Minibar' or Cargos_Extra = 'Servicio de lavanderia' )  DEFAULT 'Ninguno',
Reseña VARCHAR(15) CHECK(Reseña = 'Excelente' or Reseña = 'Buena' or Reseña = 'Regular' or Reseña = 'Mala') DEFAULT 'Excelente'
);

ALTER TABLE Facturas
ADD CONSTRAINT PK_Id_Factura PRIMARY KEY (Id_Factura)

INSERT INTO Facturas( Id_ServicioExtra, Id_Reserva, Id_Estadia, Total, Metodo_Pago, Cargos_Extra, Reseña)
VALUES
(1, 1, 3, 50.000*3, 'Tarjeta de credito', 'Ninguno', 'Excelente'),
(2, 2, 2, 50.000*2,'Efectivo', 'Minibar', 'Buena'),
(3, 3, 1, 50.000*5, 'Transferencia', 'Ninguno', 'Excelente'),
(4, 4, 1, 50.000*4, 'Tarjeta de credito', 'Ninguno', 'Regular'),
(5, 5, 2, 50.000*1, 'Efectivo', 'Minibar', 'Mala'),
(6, 6, 1, 50.000*2,'Transferencia', 'Servicio de lavanderia', 'Buena'),
(1, 7, 3, 50.000*3,'Tarjeta de credito', 'Ninguno', 'Buena'),
(2, 8, 2, 50.000*2,'Efectivo', 'Minibar', 'Regular'),
(3, 9, 3, 50.000*2,'Transferencia', 'Ninguno', 'Excelente'),
(4, 10, 1, 50.000*1,'Tarjeta de credito', 'Ninguno', 'Buena'),
(5, 11, 2, 50.000*3, 'Efectivo', 'Minibar', 'Buena'),
(6, 12, 4, 50.000*4,'Transferencia', 'Servicio de lavanderia', 'Excelente'),
(1, 13, 3, 50.000*4,'Tarjeta de credito', 'Ninguno', 'Regular');

CREATE TABLE Auditorias(

Id_Auditoria INT IDENTITY,
Usuario VARCHAR(10) NOT NULL,
Accion VARCHAR(20) NOT NULL,
Fecha DATETIME
);
ALTER TABLE Auditorias
ADD CONSTRAINT PK_Id_Auditoria PRIMARY KEY (Id_Auditoria)

INSERT INTO Auditorias(Usuario, Accion, Fecha)
VALUES('Karmen', 'Recocho', DATETIME.NOW);

CREATE TABLE Usuarios(

Id_Usuario INT IDENTITY,
Nombre VARCHAR(10) NOT NULL,
Contraseña INT NOT NULL
);
ALTER TABLE Usuarios
ADD CONSTRAINT PK_Id_Usuario PRIMARY KEY (Id_Usuario)

INSERT INTO Usuarios(Nombre, Contraseña)
VALUES('Zamuel', 777);

---Las FK
ALTER TABLE Empleados
ADD CONSTRAINT FK_Empleados_Hotel FOREIGN KEY (Id_Hotel) REFERENCES Hoteles(Id_Hotel);

ALTER TABLE Empleados
ADD CONSTRAINT FK_Empleados_Sede FOREIGN KEY (Id_Sede) REFERENCES Sedes(Id_Sede);

ALTER TABLE Habitaciones
ADD CONSTRAINT FK_Habitaciones_Sede FOREIGN KEY (Id_Sede) REFERENCES Sedes(Id_Sede);

ALTER TABLE Habitaciones
ADD CONSTRAINT FK_Habitaciones_Hotel FOREIGN KEY (Id_Hotel) REFERENCES Hoteles(Id_Hotel);

ALTER TABLE Reservas
ADD CONSTRAINT FK_Reservas_Sede FOREIGN KEY (Id_Sede) REFERENCES Sedes(Id_Sede);

ALTER TABLE Reservas
ADD CONSTRAINT FK_Reservas_Huesped FOREIGN KEY (Id_Huesped) REFERENCES Huespedes(Id_Huesped);

ALTER TABLE Estadias
ADD CONSTRAINT FK_Estadias_Reserva FOREIGN KEY (Id_Reserva) REFERENCES Reservas(Id_Reserva);

ALTER TABLE Sedes
ADD CONSTRAINT FK_Sedes_Hotel FOREIGN KEY (Id_Hotel) REFERENCES Hoteles(Id_Hotel);

ALTER TABLE ServiciosExtras
ADD CONSTRAINT FK_ServiciosExtras_Sede FOREIGN KEY (Id_Sede) REFERENCES Sedes(Id_Sede);

ALTER TABLE Facturas
ADD CONSTRAINT FK_Facturas_Estadia FOREIGN KEY (Id_Estadia) REFERENCES Estadias(Id_Estadia);

ALTER TABLE Facturas
ADD CONSTRAINT FK_Facturas_ServicioExtra FOREIGN KEY (Id_ServicioExtra) REFERENCES ServiciosExtras(Id_ServicioExtra);

ALTER TABLE Facturas
ADD CONSTRAINT FK_Facturas_Reserva FOREIGN KEY (Id_Reserva) REFERENCES Reservas(Id_Reserva);

ALTER TABLE Reservas_Habitaciones
ADD CONSTRAINT FK_Reservas_Habitaciones_Reserva FOREIGN KEY (Id_Reserva) REFERENCES Reservas (Id_Reserva);

ALTER TABLE Reservas_Habitaciones
ADD CONSTRAINT FK_Reservas_Habitaciones_Habitacion FOREIGN KEY (Id_Habitacion) REFERENCES Habitaciones (Id_Habitacion);

ALTER TABLE Empleados_ServiciosExtras
ADD CONSTRAINT FK_Auditoria_Empleado FOREIGN KEY (Id_Empleado) REFERENCES Empleados (Id_Empleado);

ALTER TABLE Empleados_ServiciosExtras
ADD CONSTRAINT FK_Auditoria_ServicioExtra FOREIGN KEY (Id_ServicioExtra) REFERENCES ServiciosExtras (Id_ServicioExtra);

ALTER TABLE Sedes_ServiciosExtras
ADD CONSTRAINT FK_Sedes_ServiciosExtras_ServicioExtra FOREIGN KEY (Id_ServicioExtra) REFERENCES ServiciosExtras (Id_ServicioExtra);

ALTER TABLE Sedes_ServiciosExtras
ADD CONSTRAINT FK_Sedes_ServiciosExtras_Sede FOREIGN KEY (Id_Sede) REFERENCES Sedes (Id_Sede);

---Los select:
SELECT * FROM Hoteles
SELECT * FROM Huespedes
SELECT * FROM Sedes
SELECT * FROM Empleados
SELECT * FROM Habitaciones
SELECT * FROM Reservas
SELECT * FROM Reservas_Habitaciones
SELECT * FROM ServiciosExtras
SELECT * FROM Auditoria
SELECT * FROM Sedes_ServiciosExtras
SELECT * FROM Estadias
SELECT * FROM Facturas