-- Active: 1726545379907@@127.0.0.1@3306@5to_hospedapp2023
#Realizar los SP para dar de alta todas las entidades menos las tablas Cliente y Reserva.
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaHotel $$
CREATE PROCEDURE AltaHotel (OUT unIdHotel SMALLINT UNSIGNED, unNombre VARCHAR(64), unDomicilio VARCHAR(64), unEmail VARCHAR(64), unContrasena CHAR(64), unEstrella TINYINT UNSIGNED)
BEGIN
	INSERT INTO Hotel (Nombre, Domicilio, Email, Contrasena, Estrella)
	VALUES (unNombre, unDomicilio, unEmail, SHA2(unContrasena, 256), unEstrella);
	SET unIdHotel = LAST_INSERT_ID();
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS ModificarHotel $$
CREATE PROCEDURE ModificarHotel (unIdHotel SMALLINT UNSIGNED, unNombre VARCHAR(64), unDomicilio VARCHAR(64), unEmail VARCHAR(64), unContrasena CHAR(64), unEstrella TINYINT UNSIGNED)
BEGIN
	UPDATE Hotel
	SET 
		Nombre = COALESCE(unNombre, Nombre), 
		Domicilio = COALESCE(unDomicilio, Domicilio), 
		Email = COALESCE(unEmail, Email), 
		Contrasena = COALESCE(SHA2(unContrasena, 256), Contrasena),
		Estrella = COALESCE(unEstrella, Estrella)
	WHERE IdHotel = unIdHotel;
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
DROP PROCEDURE IF EXISTS ModificarCama $$
CREATE PROCEDURE ModificarCama (unIdCama TINYINT UNSIGNED, unNombre VARCHAR(64), unPueden_dormir TINYINT UNSIGNED)
BEGIN
		UPDATE Cama
		SET 
			Nombre = COALESCE(unNombre, Nombre),
			Pueden_dormir = COALESCE(unPueden_dormir, Pueden_dormir)
		WHERE IdCama = unIdCama;
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
DROP PROCEDURE IF EXISTS ModificarCuarto $$
CREATE PROCEDURE ModificarCuarto (unIdCuarto TINYINT UNSIGNED, unCochera BOOL, unNoche DOUBLE, unDescripcion VARCHAR(64))
BEGIN
		UPDATE Cuarto
		SET 
			Cochera = COALESCE(unCochera, Cochera), 
			Noche = COALESCE(unNoche, Noche), 
			Descripcion = COALESCE(unDescripcion, Descripcion)
		WHERE IdCuarto = unIdCuarto;
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

#Se pide hacer el SP ‘registrarCliente’ que reciba los datos del cliente. Es importante guardar encriptada la contrasena del cliente usando SHA256.
DELIMITER $$
DROP PROCEDURE IF EXISTS RegistrarCliente $$
CREATE PROCEDURE RegistrarCliente (unDni INT UNSIGNED, unNombre VARCHAR(64), unApellido VARCHAR(64), unEmail VARCHAR(64), unContrasena CHAR(64))
BEGIN
		INSERT INTO Cliente (Dni, Nombre, Apellido, Email, Contrasena)
		VALUES (unDni, unNombre, unApellido, unEmail, unContrasena);
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS ModificarCliente $$
CREATE PROCEDURE ModificarCliente (unDni INT UNSIGNED, unNombre VARCHAR(64), unApellido VARCHAR(64), unEmail VARCHAR(64), unContrasena CHAR(64))
BEGIN
		UPDATE Cliente
		SET 
			Nombre = COALESCE(unNombre, Nombre),
			Apellido = COALESCE(unApellido, Apellido), 
			Email = COALESCE(unEmail, Email), 
			Contrasena = COALESCE(SHA2(unContrasena, 256), Contrasena)
		WHERE Dni = unDni;
END $$

#Se pide hacer el SP ‘altaReserva’ que reciba los datos no opcionales y haga el alta de una estadia.
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaReserva $$
CREATE PROCEDURE AltaReserva (OUT unIdReserva SMALLINT UNSIGNED, unIdHotel SMALLINT UNSIGNED,unInicio DATE, unFin DATE, unDni INT UNSIGNED, unNumero TINYINT UNSIGNED)
BEGIN
	DECLARE unIdCuarto TINYINT UNSIGNED;
	SELECT IdCuarto INTO unIdCuarto
	FROM Hotel_Cuarto
	WHERE IdHotel = unIdHotel AND Numero = unNumero;
	INSERT INTO Reserva (IdHotel, Inicio, Fin, Dni, IdCuarto, Numero)
	VALUES (unIdHotel, unInicio, unFin, unDni, unIdCuarto, unNumero);
	SET unIdReserva = LAST_INSERT_ID();
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS ModificarReserva $$
CREATE PROCEDURE ModificarReserva (unIdReserva SMALLINT UNSIGNED, unIdHotel SMALLINT UNSIGNED,unInicio DATE, unFin DATE, unDni INT UNSIGNED, unIdCuarto TINYINT UNSIGNED)
BEGIN
		UPDATE Reserva
		SET 
			IdHotel = COALESCE(unIdHotel, IdHotel), 
			Inicio = COALESCE(unInicio, Inicio), 
			Fin = COALESCE(unFin, Fin), 
			Dni = COALESCE(unDni, Dni), 
			IdCuarto = COALESCE(unIdCuarto, IdCuarto)
		WHERE IdReserva = unIdReserva;
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

DELIMITER $$
DROP PROCEDURE IF EXISTS BuscarCliente $$
CREATE PROCEDURE BuscarCliente (buscar VARCHAR(255))
BEGIN
    SELECT * FROM Cliente
    WHERE Dni LIKE CONCAT('%', buscar, '%')
    OR Nombre LIKE CONCAT('%', buscar, '%')
    OR Apellido LIKE CONCAT('%', buscar, '%')
    OR LEFT(Email, LOCATE('@', Email) - 1) LIKE CONCAT('%', buscar, '%');
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS BuscarCama $$
CREATE PROCEDURE BuscarCama (buscar VARCHAR(255))
BEGIN
    SELECT * FROM Cama
    WHERE Nombre LIKE CONCAT('%', buscar, '%')
    OR Pueden_dormir LIKE CONCAT('%', buscar, '%');
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS BuscarCuarto $$
CREATE PROCEDURE BuscarCuarto (buscar VARCHAR(255))
BEGIN
    SELECT * FROM Cuarto
    WHERE Cochera LIKE CONCAT('%', buscar, '%')
    OR Noche LIKE CONCAT('%', buscar, '%')
    OR Descripcion LIKE CONCAT('%', buscar, '%');
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS BuscarHotel $$
CREATE PROCEDURE BuscarHotel (buscar VARCHAR(255))
BEGIN
    SELECT * FROM Hotel
    WHERE Nombre LIKE CONCAT('%', buscar, '%')
    OR Domicilio LIKE CONCAT('%', buscar, '%')
    OR Email LIKE CONCAT('%', buscar, '%')
    OR Contrasena LIKE CONCAT('%', buscar, '%')
    OR Estrella LIKE CONCAT('%', buscar, '%');
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS BuscarReserva $$
CREATE PROCEDURE BuscarReserva (buscar VARCHAR(255))
BEGIN
    SELECT r.*, h.*, c.*, cl.*, hc.Numero
    FROM Reserva r
    INNER JOIN Hotel h ON r.IdHotel = h.IdHotel
    INNER JOIN Cuarto c ON r.IdCuarto = c.IdCuarto
    INNER JOIN Cliente cl ON r.Dni = cl.Dni
    INNER JOIN Hotel_Cuarto hc ON r.IdHotel = hc.IdHotel AND r.IdCuarto = hc.IdCuarto
    WHERE r.IdReserva LIKE CONCAT('%', buscar, '%')
    OR r.IdHotel LIKE CONCAT('%', buscar, '%')
    OR r.Inicio LIKE CONCAT('%', buscar, '%')
    OR r.Fin LIKE CONCAT('%', buscar, '%')
    OR r.Dni LIKE CONCAT('%', buscar, '%')
    OR r.IdCuarto LIKE CONCAT('%', buscar, '%')
    OR r.Calificacion_del_cliente LIKE CONCAT('%', buscar, '%')
    OR r.Calificacion_del_hotel LIKE CONCAT('%', buscar, '%')
    OR r.Comentario_del_cliente LIKE CONCAT('%', buscar, '%')
    OR h.Nombre LIKE CONCAT('%', buscar, '%')
    OR h.Domicilio LIKE CONCAT('%', buscar, '%') 
    OR c.Descripcion LIKE CONCAT('%', buscar, '%')
    OR cl.Nombre LIKE CONCAT('%', buscar, '%')
    OR cl.Apellido LIKE CONCAT('%', buscar, '%')
    OR hc.Numero LIKE CONCAT('%', buscar, '%')
	ORDER BY r.IdReserva ASC;
END $$
