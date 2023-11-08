DROP DATABASE IF EXISTS 5to_HospeddApp2023 ;
CREATE DATABASE 5to_HospeddApp2023 ;
USE 5to_HospeddApp2023 ;


CREATE TABLE Hotel(
IdHotel SMALLINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
Nombre VARCHAR(64) NOT NULL,
Domicilio VARCHAR(64) NOT NULL,
Email VARCHAR(64) NOT NULL UNIQUE,
Contraseña CHAR(64) NOT NULL,
Estrella TINYINT UNSIGNED NOT NULL
);


CREATE TABLE Cama (
Tipo_de_cama TINYINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
Nombre VARCHAR(64) NOT NULL,
Pueden_dormir TINYINT UNSIGNED NOT NULL
);


CREATE TABLE Cuarto (
IdCuarto TINYINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
Cochera BOOL NOT NULL,
Noche DOUBLE NOT NULL,
Descripcion VARCHAR(60)
);


CREATE TABLE Cuarto_Cama(
IdCuarto TINYINT UNSIGNED NOT NULL,
Tipo_de_cama TINYINT UNSIGNED NOT NULL,
Cantidad_de_cama TINYINT UNSIGNED NOT NULL,
PRIMARY KEY (IdCuarto, Tipo_de_cama),
CONSTRAINT FK_CuartoCama_Cuarto FOREIGN KEY (IdCuarto) REFERENCES Cuarto (IdCuarto),
CONSTRAINT FK_CuartoCama_Cama FOREIGN KEY (Tipo_de_cama) REFERENCES Cama (Tipo_de_cama)
);


CREATE TABLE Cliente (
IdCliente MEDIUMINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
Nombre VARCHAR(64) NOT NULL,
Apellido VARCHAR(64) NOT NULL,
Email VARCHAR(64) NOT NULL,
Contraseña CHAR(64) NOT NULL
);


CREATE TABLE Reserva (
IdReserva BIGINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
IdHotel SMALLINT UNSIGNED NOT NULL,
Inicio DATE NOT NULL,
Fin DATE NOT NULL,
IdCliente MEDIUMINT UNSIGNED NOT NULL,
IdCuarto TINYINT UNSIGNED NOT NULL,
Calificacion_del_cliente TINYINT UNSIGNED,
Calificacion_del_hotel TINYINT UNSIGNED,
Comentario_del_cliente VARCHAR(60),
CONSTRAINT FK_Reserva_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente (IdCliente),
CONSTRAINT FK_Reserva_Cuarto FOREIGN KEY (IdCuarto) REFERENCES Cuarto (IdCuarto),
CONSTRAINT FK_Reserva_Hotel FOREIGN KEY (IdHotel) REFERENCES Hotel (IdHotel)
);


CREATE TABLE ReservaCancelado (
IdReserva BIGINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
IdHotel SMALLINT UNSIGNED NOT NULL,
Inicio DATE NOT NULL,
Fin DATE NOT NULL,
IdCliente MEDIUMINT UNSIGNED NOT NULL,
IdCuarto TINYINT UNSIGNED NOT NULL,
CONSTRAINT FK_ReservaCancelado_Hotel FOREIGN KEY (IdHotel) REFERENCES Hotel (IdHotel),
CONSTRAINT FK_ReservaCancelado_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente (IdCliente),
CONSTRAINT FK_ReservaCancelado_Cuarto FOREIGN KEY (IdCuarto) REFERENCES Cuarto (IdCuarto)
);


#Realizar los SP para dar de alta todas las entidades menos las tablas Cliente y Reserva.
DELIMITER //
DROP PROCEDURE IF EXISTS AltaHotel //
CREATE PROCEDURE AltaHotel (unNombre VARCHAR(64), unDomicilio VARCHAR(64), unEmail VARCHAR(64), unContraseña CHAR(64), unEstrella TINYINT UNSIGNED)
BEGIN
INSERT INTO Hotel (Nombre, Domicilio, Email, Contraseña, Estrella)
VALUES (unNombre, unDomicilio, unEmail, SHA2(unContraseña, 256), unEstrella);
END //


DELIMITER //
DROP PROCEDURE IF EXISTS AltaCama //
CREATE PROCEDURE AltaCama (unNombre VARCHAR(64), unPueden_dormir TINYINT UNSIGNED)
BEGIN
INSERT INTO Cama (Nombre, Pueden_dormir)
VALUES (unNombre, unPueden_dormir);
END //


DELIMITER //
DROP PROCEDURE IF EXISTS AltaCuarto //
CREATE PROCEDURE AltaCuarto (unCochera BOOL, unNoche DOUBLE, unDescripcion VARCHAR(64))
BEGIN
INSERT INTO Cuarto (Cochera, Noche, Descripcion)
VALUES (unCochera, unNoche, unDescripcion);
END //


DELIMITER //
DROP PROCEDURE IF EXISTS AltaCuarto_Cama //
CREATE PROCEDURE AltaCuarto_Cama (unIdCuarto TINYINT UNSIGNED, unTipo_de_cama TINYINT UNSIGNED, unCantidad_de_cama TINYINT UNSIGNED)
BEGIN
INSERT INTO Cuarto_Cama (IdCuarto, Tipo_de_cama, Cantidad_de_cama)
VALUES (unIdCuarto, unTipo_de_cama, unCantidad_de_cama);
END //


#Se pide hacer el SP ‘registrarCliente’ que reciba los datos del cliente. Es importante guardar encriptada la contraseña del cliente usando SHA256.
DELIMITER //
DROP PROCEDURE IF EXISTS RegistrarCliente //
CREATE PROCEDURE RegistrarCliente (unNombre VARCHAR(64), unApellido VARCHAR(64), unEmail VARCHAR(64), unContraseña CHAR(64))
BEGIN
INSERT INTO Cliente (Nombre, Apellido, Email, Contraseña)
VALUES (unNombre, unApellido, unEmail, SHA2(unContraseña, 256));
END //


#Se pide hacer el SP ‘altaReserva’ que reciba los datos no opcionales y haga el alta de una estadia. Hacer el SP ‘cerrarEstadiaHotel’ que reciba los parámetros necesarios y una calificación por parte del hotel. Hacer el SP ‘cerrarEstadiaCliente’ que reciba los parámetros necesarios, una calificación por parte del cliente y un comentario.
DELIMITER //
DROP PROCEDURE IF EXISTS AltaReserva //
CREATE PROCEDURE AltaReserva (unIdHotel SMALLINT UNSIGNED,unInicio DATE, unFin DATE, unIdCliente MEDIUMINT UNSIGNED, unIdCuarto TINYINT UNSIGNED)
BEGIN
INSERT INTO Reserva (IdHotel, Inicio, Fin, IdCliente, IdCuarto)
VALUES (unIdHotel, unInicio, unFin, unIdCliente, unIdCuarto);
END //


DELIMITER //
DROP PROCEDURE IF EXISTS CerrarEstadiaHotel //
CREATE PROCEDURE CerrarEstadiaHotel (unIdHotel SMALLINT UNSIGNED, unIdCuarto TINYINT UNSIGNED, unCalificacion_del_hotel TINYINT UNSIGNED)
BEGIN
UPDATE Reserva
SET Calificacion_del_hotel = unCalificacion_del_hotel
WHERE IdHotel = unIdHotel AND IdCuarto = unIdCuarto;
END //

DELIMITER //
DROP PROCEDURE IF EXISTS CerrarEstadiaCliente //
CREATE PROCEDURE CerrarEstadiaCliente (unIdCliente MEDIUMINT UNSIGNED, unCalificacion_del_cliente TINYINT UNSIGNED, unComentario_del_cliente VARCHAR(60))
BEGIN
UPDATE Reserva
SET Calificacion_del_cliente = unCalificacion_del_cliente , Comentario_del_cliente = unComentario_del_cliente
WHERE IdCliente = unIdCliente;
END //
#Se pide hacer el SF ‘CantidadPersonas’ que reciba por parámetro un identificador de cuarto, la función tiene que devolver la cantidad de personas que pueden dormir en el cuarto: CANT PERSONAS = SUMATORIA(PERSONAS POR CAMA)


DELIMITER //
DROP FUNCTION IF EXISTS CantidadPersonas //
CREATE FUNCTION CantidadPersonas (unIdCliente TINYINT UNSIGNED) RETURNS INT READS SQL DATA
BEGIN
DECLARE suma INT;
SELECT SUM(Pueden_dormir * Cantidad_de_cama) INTO suma
FROM Cuarto_Cama
JOIN Cama USING (Tipo_de_cama)
WHERE IdCuarto = unIdCuarto;
RETURN suma;
END //

  -- Se pide desarrollar un trigger para que al momento de ingresar una reserva, si la fecha de inicio se encuentra entre la fecha de inicio y fin de otra reserva para el mismo cuarto con reserva no cancelada, no se debe permitir el INSERT mostrando la leyenda “Fecha Superpuesta”. También se tiene que tener en cuenta que un cliente no puede tener propias sin cancelar de manera superpuestas, es decir, al momento de reservar se tiene que verificar que ese mismo cliente no posea reservas propias no canceladas en otros lados, en ese caso también se tiene que mostrar la leyenda “El cliente ya posee otra reserva para esa fecha”.
DELIMITER $$
DROP TRIGGER IF EXISTS BefInsReserva $$
CREATE TRIGGER BefInsReserva BEFORE INSERT ON Reserva
FOR EACH ROW
BEGIN
IF (EXISTS(SELECT * FROM Reserva WHERE IdHotel = NEW.IdHotel AND IdCuarto = NEW.IdCuarto AND Inicio <= NEW.Inicio AND Fin >= NEW.Fin))THEN
SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Fecha Superpuesta';
END IF;
END $$

CALL AltaHotel ("Hoteldeprueba","En el Hotel", "Hotel de prueba@gmail.com", 'Hoteldeprueba', 5);
CALL AltaCama ("Cama", 2);
CALL AltaCuarto (TRUE, 24.99, "Un cuarto con cochera, cuesta 24.99 pesos :v");
CALL AltaCuarto (FALSE, 19.99, "Un cuarto sin cohera pipipi, cuesta 19.99 pesos :v");
CALL RegistrarCliente ("Leonel", "Messi", "Quemirabobo@gmail.com","Andapalla");
CALL RegistrarCliente ("Roberto", "Bolaños", "RobertoBolaños777@gmail.com","contraseñadeRobertoBolaños");
CALL AltaReserva (1,'2023-02-01', '2023-04-01', 1, 1);
CALL AltaReserva (1,'2023-02-06', '2023-03-22', 2, 2);
CALL CerrarEstadiaHotel (1,1,10);
CALL CerrarEstadiaHotel (1,2,10);
CALL CerrarEstadiaCliente (1, 10, "Bueno muchachos nos vemos en miami");
CALL CerrarEstadiaCliente (2, 10, "Me arrepiento asi que se cancela :v");
