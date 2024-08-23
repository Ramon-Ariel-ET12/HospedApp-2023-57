-- Active: 1723664382511@@127.0.0.1@3306@5to_hospeddapp2023
#Realizar los SP para dar de alta todas las entidades menos las tablas Cliente y Reserva.
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaHotel $$
CREATE PROCEDURE AltaHotel (OUT unIdHotel SMALLINT UNSIGNED, unNombre VARCHAR(64), unDomicilio VARCHAR(64), unEmail VARCHAR(64), unContraseña CHAR(64), unEstrella TINYINT UNSIGNED)
BEGIN
	INSERT INTO Hotel (Nombre, Domicilio, Email, Contraseña, Estrella)
	VALUES (unNombre, unDomicilio, unEmail, SHA2(unContraseña, 256), unEstrella);
	SET unIdHotel = LAST_INSERT_ID();
END $$


DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCama $$
CREATE PROCEDURE AltaCama (OUT unIdCama TINYINT UNSIGNED, unNombre VARCHAR(64), unPueden_dormir TINYINT UNSIGNED)
BEGIN
	INSERT INTO Cama (Nombre, Pueden_dormir)
	VALUES (unNombre, unPueden_dormir);
	SET unIdCama = LAST_INSERT_ID();
END $$


DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCuarto $$
CREATE PROCEDURE AltaCuarto (OUT unIdCuarto TINYINT UNSIGNED, unCochera BOOL, unNoche DOUBLE, unDescripcion VARCHAR(64))
BEGIN
	INSERT INTO Cuarto (Cochera, Noche, Descripcion)
	VALUES (unCochera, unNoche, unDescripcion);
	SET unIdCuarto = LAST_INSERT_ID();
END $$


DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCuarto_Cama $$
CREATE PROCEDURE AltaCuarto_Cama (unIdCuarto TINYINT UNSIGNED, unIdCama TINYINT UNSIGNED, unCantidad_de_cama TINYINT UNSIGNED)
BEGIN
	INSERT INTO Cuarto_Cama (IdCuarto, IdCama, Cantidad_de_cama)
	VALUES (unIdCuarto, unIdCama, unCantidad_de_cama);
END $$


DELIMITER $$
DROP PROCEDURE IF EXISTS AltaHotel_Cuarto $$
CREATE PROCEDURE AltaHotel_Cuarto (unIdHotel SMALLINT UNSIGNED, unIdCuarto TINYINT UNSIGNED)
BEGIN
	INSERT INTO Hotel_Cuarto (IdHotel, IdCuarto)
					VALUES	(unIdHotel, unIdCuarto);
END $$

#Se pide hacer el SP ‘registrarCliente’ que reciba los datos del cliente. Es importante guardar encriptada la contraseña del cliente usando SHA256.
DELIMITER $$
DROP PROCEDURE IF EXISTS RegistrarCliente $$
CREATE PROCEDURE RegistrarCliente (unDni INT UNSIGNED, unNombre VARCHAR(64), unApellido VARCHAR(64), unEmail VARCHAR(64), unContraseña CHAR(64))
BEGIN
		INSERT INTO Cliente (Dni, Nombre, Apellido, Email, Contraseña)
		VALUES (unDni, unNombre, unApellido, unEmail, unContraseña);
END $$


#Se pide hacer el SP ‘altaReserva’ que reciba los datos no opcionales y haga el alta de una estadia.
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaReserva $$
CREATE PROCEDURE AltaReserva (OUT unIdReserva SMALLINT UNSIGNED, unIdHotel SMALLINT UNSIGNED,unInicio DATE, unFin DATE, unDni INT UNSIGNED, unIdCuarto TINYINT UNSIGNED)
BEGIN
	INSERT INTO Reserva (IdHotel, Inicio, Fin, Dni, IdCuarto)
	VALUES (unIdHotel, unInicio, unFin, unDni, unIdCuarto);
END $$
 #Hacer el SP ‘cerrarEstadiaHotel’ que reciba los parámetros necesarios y una calificación por parte del hotel. 

DELIMITER $$
DROP PROCEDURE IF EXISTS CerrarEstadiaHotel $$
CREATE PROCEDURE CerrarEstadiaHotel (unIdHotel SMALLINT UNSIGNED, unIdCuarto TINYINT UNSIGNED, unCalificacion_del_hotel TINYINT UNSIGNED)
BEGIN
	UPDATE Reserva
	SET Calificacion_del_hotel = unCalificacion_del_hotel
	WHERE IdHotel = unIdHotel AND IdCuarto = unIdCuarto;
END $$

#Hacer el SP ‘cerrarEstadiaCliente’ que reciba los parámetros necesarios, una calificación por parte del cliente y un comentario.

DELIMITER $$
DROP PROCEDURE IF EXISTS CerrarEstadiaCliente $$
CREATE PROCEDURE CerrarEstadiaCliente (unDni INT UNSIGNED, unCalificacion_del_cliente TINYINT UNSIGNED, unComentario_del_cliente VARCHAR(60))
BEGIN
	UPDATE Reserva
	SET Calificacion_del_cliente = unCalificacion_del_cliente , Comentario_del_cliente = unComentario_del_cliente
	WHERE Dni = unDni;
END $$
#Se pide hacer el SF ‘CantidadPersonas’ que reciba por parámetro un identificador de cuarto, la función tiene que devolver la cantidad de personas que pueden dormir en el cuarto: CANT PERSONAS = SUMATORIA(PERSONAS POR CAMA)


DELIMITER $$
DROP FUNCTION IF EXISTS CantidadPersonas $$
CREATE FUNCTION CantidadPersonas (unIdCuarto TINYINT UNSIGNED) RETURNS INT READS SQL DATA
BEGIN
	DECLARE suma INT;
	SELECT SUM(Pueden_dormir * Cantidad_de_cama) INTO suma
	FROM Cuarto_Cama
	JOIN Cama USING (IdCama)
	WHERE IdCuarto = unIdCuarto;
	RETURN suma;
END $$