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