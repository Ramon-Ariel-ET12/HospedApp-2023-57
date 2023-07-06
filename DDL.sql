-- Active: 1684371989218@@127.0.0.1@3306
DROP DATABASE IF EXISTS HostedApp2023;
CREATE DATABASE HostedApp2023;
USE HostedApp2023;

CREATE TABLE Hotel(
idHotel SMALLINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
domicilio VARCHAR(20) NOT NULL,
email VARCHAR(50) NOT NULL,
contraseña CHAR(64) NOT NULL,
estrella TINYINT UNSIGNED NOT NULL,
CONSTRAINT UK_contra UNIQUE(contraseña)
);  

CREATE TABLE Cliente(
idCliente MEDIUMINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
email VARCHAR(50) NOT NULL,
nombre VARCHAR(20) NOT NULL,
apellido VARCHAR(20) NOT NULL,
contraseña CHAR(64) NOT NULL
);

CREATE TABLE Cama(
idCama TINYINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
nombre VARCHAR(20) NOT NULL,
pueden_dormir TINYINT NOT NULL
);

CREATE TABLE Cuarto(
cuarto TINYINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
idCama TINYINT UNSIGNED NOT NULL,
cochera char(2) NOT NULL,
costo_noche SMALLINT UNSIGNED NOT NULL,
descripcion VARCHAR(60) NOT NULL,
CONSTRAINT FK_CUARTO_CAMA FOREIGN KEY (idCama)
REFERENCES Cama(idCama)
);

CREATE TABLE cuarto_cama(
cuarto TINYINT UNSIGNED NOT NULL,
idCama TINYINT UNSIGNED NOT NULL,
cant_camas TINYINT UNSIGNED NOT NULL,
PRIMARY KEY (cuarto, idCama), 
CONSTRAINT FK_HOTEL_CUARTO FOREIGN KEY (cuarto)
REFERENCES Cuarto(cuarto),
CONSTRAINT FK_HOTEL_CAMA FOREIGN KEY (idCama)
REFERENCES Cama(idCama)
);

CREATE TABLE Reserva(
idReserva INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
idHotel SMALLINT UNSIGNED NOT NULL,
inicio DATE NOT NULL,
fin DATE NOT NULL,
idCliente MEDIUMINT UNSIGNED NOT NULL,
cuarto TINYINT UNSIGNED NOT NULL,
calificacion_C TINYINT UNSIGNED NOT NULL,
calificacion_H TINYINT UNSIGNED NOT NULL,
comentario_C VARCHAR(60),
habilitado BOOL NOT NULL,
CONSTRAINT FK_HOTEL_ID FOREIGN KEY (idHotel)
REFERENCES Hotel(idHotel),
CONSTRAINT FK_HOTEL_CLIENTE FOREIGN KEY (idCliente)
REFERENCES Cliente(idCliente),
CONSTRAINT FK_HOTEL_RESERVA FOREIGN KEY (cuarto)
REFERENCES Cuarto(cuarto)
);
<<<<<<< HEAD
#STORED PROCEDURE
#El sistema va a admitir un máximo de 1000 hoteles.
delimiter //
CREATE PROCEDURE altaHotel (idHotel INT)
BEGIN
	IF(EXISTS(SELECT idHotel
				FROM Hotel
				WHERE idHotel > 1000))THEN
				DELETE
				FROM Hotel
				WHERE idHotel > 1000;
	END IF;
END //
#Existen 7 tipos de camas distintas.
Delimiter //
CREATE PROCEDURE AltaCama (idCama INT )
BEGIN
	IF(EXISTS(SELECT idCama
				FROM Cama
                WHERE idCama > 7))THEN
                DELETE
				FROM Cama
                WHERE idCama > 7;
	END IF ;
END //
#No existen más de 100 cuartos por hotel.
Delimiter //
CREATE PROCEDURE AltaCuarto (Cuarto INT)
BEGIN
	IF(EXISTS(SELECT Cuarto
				FROM Cuarto
                WHERE Cuarto > 100)) THEN
                DELETE
				FROM Cuarto
                WHERE Cuarto > 100;
	END IF  ;
End //
#El sistema tiene que soportar hasta 100.000 clientes.
Delimiter // 
CREATE PROCEDURE AltaCliente (idCLiente INT)
BEGIN
	IF(EXISTS(SELECT idCliente
				FROM Cliente
                WHERE idCliente > 100000))THEN
                DELETE 
				FROM Cliente
                WHERE idCliente > 100000;
	END IF ;
End //
#El sistema debe soportar 10 millones de reservas y cancelaciones.
Delimiter //
CREATE PROCEDURE AltaReserva (idReserva INT)
BEGIN
	IF(EXISTS(SELECT idReserva
				FROM Reserva
                WHERE idReserva > 10000000))THEN
                DELETE 
				FROM Reserva
                WHERE idReserva > 10000000;
	END IF ;
End //

#Iteración Uno

USE 5to_HostedApp2023;
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCuarto $$
CREATE PROCEDURE AltaCuarto (unaCochera TINYINT, unCosto SMALLINT UNSIGNED, unaDesc VARCHAR(60))
BEGIN
   INSERT INTO Cuarto(cochera, costo_noche, descripcion)
   VALUES (unaCochera, unCosto, unaDesc);
END $$
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaHotel $$
CREATE PROCEDURE AltaHotel (unDomicilio VARCHAR(20), unMail VARCHAR(50), unaContra CHAR(64), estrellas TINYINT UNSIGNED)
BEGIN
   INSERT INTO Hotel(domicilio, email, contraseña, estrella)
   VALUES (unDomicilio, unMail, SHA2(unaContra, 256), estrellas);
END $$
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCama $$
CREATE PROCEDURE AltaCama (unNombre VARCHAR(20), cantidad TINYINT UNSIGNED)
BEGIN
   INSERT INTO Cama(nombre, pueden_dormir)
   VALUES (unNombre, cantidad);
END $$
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCamaCuarto $$
CREATE PROCEDURE AltaCamaCuarto (unCuarto TINYINT UNSIGNED, unaCama TINYINT UNSIGNED, cantidad TINYINT UNSIGNED)
BEGIN
   INSERT INTO cuarto_cama(cuarto, idCama, cant_camas)
   VALUES (unCuarto, unaCama, cantidad);
END $$
DELIMITER $$
DROP PROCEDURE IF EXISTS RegistrarCliente $$
CREATE PROCEDURE RegistrarCliente (unMail VARCHAR(50),unNombre VARCHAR(20), unApellido VARCHAR(20), unaContra CHAR(64))
BEGIN
   INSERT INTO Cliente(email, nombre, apellido, contraseña)
   VALUES (unMail, unNombre, unApellido, SHA2(unaContra, 256));
END $$
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaReserva $$
CREATE PROCEDURE AltaReserva (unHotel SMALLINT UNSIGNED, Inicio DATE, fin DATE, unCliente MEDIUMINT, unCuarto TINYINT)
BEGIN
   INSERT INTO Reserva (idHotel, inicio, fin, idCliente, cuarto, habilitado)
       VALUES (unHotel, inicio, fin, unCliente , unCuarto, TRUE);
END $$
DELIMITER $$
DROP PROCEDURE IF EXISTS CerrarEstadiaHotel $$
CREATE PROCEDURE CerrarEstadiaHotel (unHotel SMALLINT UNSIGNED, unfin DATE, unCliente MEDIUMINT, unCuarto TINYINT, unaCalif TINYINT UNSIGNED, unComentario VARCHAR (60))
BEGIN
   UPDATE Reserva
   SET habilitado = FALSE,
   calificacion_h = unaCalif,
   comentario = unComentario
   WHERE idHotel = unHotel &&
   fin = unfin &&
   idCliente = unCliente &&
   cuarto = unCuarto;
END $$
DELIMITER $$
DROP FUNCTION IF EXISTS CantidadPersonas $$
CREATE FUNCTION CantidadPersonas (unCuarto TINYINT)
RETURNS INT READS SQL DATA
BEGIN
   DECLARE suma INT;
 
   SELECT SUM(pueden_dormir * cant_camas) INTO suma
   FROM Cuarto_cama
   JOIN Cama USING (idCama)
   WHERE idCuarto = unCuarto;
 
   RETURN suma;
END $$



=======
>>>>>>> 160c17351be8c5d01e152191accd266325fa7642
