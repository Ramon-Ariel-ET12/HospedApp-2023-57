-- Active: 1691515569821@@127.0.0.1@3306
DROP DATABASE IF EXISTS 5to_HospeddApp2023 ;
CREATE DATABASE 5to_HospeddApp2023 ;
USE 5to_HospeddApp2023 ;


CREATE TABLE Hotel(
hotel SMALLINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
nombre VARCHAR(64) NOT NULL,
domicilio VARCHAR(64) NOT NULL,
email VARCHAR(64) NOT NULL UNIQUE,
contraseña CHAR(64) NOT NULL,
estrella TINYINT UNSIGNED NOT NULL
);


CREATE TABLE Cama (
tipo_de_cama TINYINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
nombre VARCHAR(64) NOT NULL,
pueden_dormir TINYINT UNSIGNED NOT NULL
);


CREATE TABLE Cuarto (
cuarto TINYINT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
cochera BOOL NOT NULL,
noche DOUBLE NOT NULL,
descripcion VARCHAR(60)
);


CREATE TABLE Cuarto_Cama(
cuarto TINYINT UNSIGNED NOT NULL,
tipo_de_cama TINYINT UNSIGNED NOT NULL,
cantidad_de_cama TINYINT UNSIGNED NOT NULL,
PRIMARY KEY (cuarto, tipo_de_cama),
CONSTRAINT FK_CuartoCama_Cuarto FOREIGN KEY (cuarto) REFERENCES Cuarto (cuarto),
CONSTRAINT FK_CuartoCama_Cama FOREIGN KEY (tipo_de_cama) REFERENCES Cama (tipo_de_cama)
);


CREATE TABLE Cliente (
cliente MEDIUMINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
nombre VARCHAR(64) NOT NULL,
apellido VARCHAR(64) NOT NULL,
email VARCHAR(64) NOT NULL,
contraseña CHAR(64) NOT NULL
);


CREATE TABLE Reserva (
reserva BIGINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
hotel SMALLINT UNSIGNED NOT NULL,
inicio DATE NOT NULL,
fin DATE NOT NULL,
cliente MEDIUMINT UNSIGNED NOT NULL,
cuarto TINYINT UNSIGNED NOT NULL,
calificacion_del_cliente TINYINT UNSIGNED,
calificacion_del_hotel TINYINT UNSIGNED,
comentario_del_cliente VARCHAR(60),
CONSTRAINT FK_Reserva_CLiente FOREIGN KEY (cliente) REFERENCES Cliente (cliente),
CONSTRAINT FK_Reserva_Cuarto FOREIGN KEY (cuarto) REFERENCES Cuarto (cuarto),
CONSTRAINT FK_Reserva_Hotel FOREIGN KEY (hotel) REFERENCES Hotel (hotel)
);


CREATE TABLE ReservaCancelado (
reserva BIGINT UNSIGNED NOT NULL,
inicio DATE NOT NULL,
fin DATE NOT NULL,
cliente MEDIUMINT UNSIGNED NOT NULL,
cuarto TINYINT UNSIGNED NOT NULL,
PRIMARY KEY (reserva),
CONSTRAINT FK_ReservaCancelado_Cliente FOREIGN KEY (cliente) REFERENCES Cliente (cliente),
CONSTRAINT FK_ReservaCancelado_Cuarto FOREIGN KEY (cuarto) REFERENCES Cuarto (cuarto)
);


#Realizar los SP para dar de alta todas las entidades menos las tablas Cliente y Reserva.
DELIMITER //
DROP PROCEDURE IF EXISTS AltaHotel //
CREATE PROCEDURE AltaHotel (unnombre VARCHAR(64), undomicilio VARCHAR(64), unemail VARCHAR(64), uncontraseña CHAR(64), unestrella TINYINT UNSIGNED)
BEGIN
INSERT INTO Hotel (nombre, domicilio, email, contraseña, estrella)
VALUES (unnombre, undomicilio, unemail, SHA2(uncontraseña, 256), unestrella);
END //


DELIMITER //
DROP PROCEDURE IF EXISTS AltaCama //
CREATE PROCEDURE AltaCama (unnombre VARCHAR(64), unpueden_dormir TINYINT UNSIGNED)
BEGIN
INSERT INTO Cama (nombre, pueden_dormir)
VALUES (unnombre, unpueden_dormir);
END //


DELIMITER //
DROP PROCEDURE IF EXISTS AltaCuarto //
CREATE PROCEDURE AltaCuarto (uncochera BOOL, unnoche DOUBLE, undescripcion VARCHAR(64))
BEGIN
INSERT INTO Cuarto (cochera, noche, descripcion)
VALUES (uncochera, unnoche, undescripcion);
END //


DELIMITER //
DROP PROCEDURE IF EXISTS AltaCuarto_Cama //
CREATE PROCEDURE AltaCuarto_Cama (uncuarto TINYINT UNSIGNED, untipo_de_cama TINYINT UNSIGNED, uncantidad_de_cama TINYINT UNSIGNED)
BEGIN
INSERT INTO Cuarto_Cama (cuarto, tipo_de_cama, cantidad_de_cama)
VALUES (uncuarto, untipo_de_cama, uncantidad_de_cama);
END //


#Se pide hacer el SP ‘registrarCliente’ que reciba los datos del cliente. Es importante guardar encriptada la contraseña del cliente usando SHA256.
DELIMITER //
DROP PROCEDURE IF EXISTS RegistrarCliente //
CREATE PROCEDURE RegistrarCliente (unnombre VARCHAR(64), unapellido VARCHAR(64), unemail VARCHAR(64), uncontraseña CHAR(64))
BEGIN
INSERT INTO Cliente (nombre, apellido, email, contraseña)
VALUES (unnombre, unapellido, unemail, SHA2(uncontraseña, 256));
END //


#Se pide hacer el SP ‘altaReserva’ que reciba los datos no opcionales y haga el alta de una estadia. Hacer el SP ‘cerrarEstadiaHotel’ que reciba los parámetros necesarios y una calificación por parte del hotel. Hacer el SP ‘cerrarEstadiaCliente’ que reciba los parámetros necesarios, una calificación por parte del cliente y un comentario.
DELIMITER //
DROP PROCEDURE IF EXISTS AltaReserva //
CREATE PROCEDURE AltaReserva (unhotel SMALLINT UNSIGNED,uninicio DATE, unfin DATE, uncliente MEDIUMINT UNSIGNED, uncuarto TINYINT UNSIGNED)
BEGIN
INSERT INTO Reserva (hotel, inicio, fin, cliente, cuarto)
VALUES (unhotel, uninicio, unfin, uncliente, uncuarto);
END //


DELIMITER //
DROP PROCEDURE IF EXISTS CerrarEstadiaHotel //
CREATE PROCEDURE CerrarEstadiaHotel (unhotel SMALLINT UNSIGNED, uncuarto TINYINT UNSIGNED, uncalificacion_del_hotel TINYINT UNSIGNED)
BEGIN
UPDATE Reserva
SET calificacion_del_hotel = uncalificacion_del_hotel
WHERE hotel = unhotel AND cuarto = uncuarto;
END //


DELIMITER //
DROP PROCEDURE IF EXISTS CerrarEstadiaCliente //
CREATE PROCEDURE CerrarEstadiaCliente (uncliente MEDIUMINT UNSIGNED, uncalificacion_del_cliente TINYINT UNSIGNED, uncomentario_del_cliente VARCHAR(60))
BEGIN
UPDATE Reserva
SET calificacion_del_cliente = uncalificacion_del_cliente , comentario_del_cliente = uncomentario_del_cliente
WHERE cliente = uncliente ;
END //
#Se pide hacer el SF ‘CantidadPersonas’ que reciba por parámetro un identificador de cuarto, la función tiene que devolver la cantidad de personas que pueden dormir en el cuarto: CANT PERSONAS = SUMATORIA(PERSONAS POR CAMA)


DELIMITER //
DROP FUNCTION IF EXISTS CantidadPersonas //
CREATE FUNCTION CantidadPersonas (uncuarto TINYINT UNSIGNED) RETURNS INT READS SQL DATA
BEGIN
DECLARE suma INT;
SELECT SUM(pueden_dormir * cantidad_de_cama) INTO suma
FROM Cuarto_Cama
JOIN Cama USING (tipo_de_cama)
WHERE cuarto = uncuarto;
RETURN suma;
END //

  -- Se pide desarrollar un trigger para que al momento de ingresar una reserva, si la fecha de inicio se encuentra entre la fecha de inicio y fin de otra reserva para el mismo cuarto con reserva no cancelada, no se debe permitir el INSERT mostrando la leyenda “Fecha Superpuesta”. También se tiene que tener en cuenta que un cliente no puede tener propias sin cancelar de manera superpuestas, es decir, al momento de reservar se tiene que verificar que ese mismo cliente no posea reservas propias no canceladas en otros lados, en ese caso también se tiene que mostrar la leyenda “El cliente ya posee otra reserva para esa fecha”.
DELIMITER $$
DROP TRIGGER IF EXISTS BefInsReserva $$
CREATE TRIGGER BefInsReserva BEFORE INSERT ON Reserva
FOR EACH ROW
BEGIN
IF (EXISTS(SELECT * FROM Reserva WHERE hotel = NEW.hotel AND cuarto = NEW.hotel AND inicio <= NEW.inicio AND fin >= NEW.fin))THEN
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
