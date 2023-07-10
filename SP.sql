USE 5to_HostedApp2023;
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCuarto $$
CREATE PROCEDURE AltaCuarto (unaCochera TINYINT, unCosto SMALLINT UNSIGNED, unaDesc VARCHAR(60))
BEGIN
   INSERT INTO Cuarto(cochera, costo_noche, descripcion)
   VALUES (unaCochera, unCosto, unaDesc);
END $$


CALL AltaCuarto(2, 1900, "Vale por una descripcion");




DELIMITER $$
DROP PROCEDURE IF EXISTS AltaHotel $$
CREATE PROCEDURE AltaHotel (unDomicilio VARCHAR(20), unMail VARCHAR(20), unaContra CHAR(64), estrellas TINYINT UNSIGNED)
BEGIN
   INSERT INTO Hotel(domicilio, email, contraseña, estrella)
   VALUES (unDomicilio, unMail, SHA2(unaContra, 256), estrellas);
END $$


CALL AltaHotel("Libertador 238", "hostedapp@gmail.com", "root", 5);
CALL AltaHotel("San Martin 1275", "sheraton@gmail.com", "SuperHotel", 5);
CALL AltaHotel("Far far away", "telonashe@gmail.com", "contraseña", 2);
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCama $$
CREATE PROCEDURE AltaCama (unNombre VARCHAR(20), cantidad TINYINT UNSIGNED)
BEGIN
   INSERT INTO Cama(nombre, pueden_dormir)
   VALUES (unNombre, cantidad);
END $$
CALL AltaCama ("Matrimonial", 2);
CALL AltaCama ("Simple", 1);
CALL AltaCama ("Cucheta", 2);
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaCamaCuarto $$
CREATE PROCEDURE AltaCamaCuarto (unCuarto TINYINT UNSIGNED, unaCama TINYINT UNSIGNED, cantidad TINYINT UNSIGNED)
BEGIN
   INSERT INTO cuarto_cama(cuarto, idCama, cant_camas)
   VALUES (unCuarto, unaCama, cantidad);
END $$
DELIMITER $$
DROP PROCEDURE IF EXISTS RegistrarCliente $$
CREATE PROCEDURE RegistrarCliente (unMail VARCHAR(20),unNombre VARCHAR(20), unApellido VARCHAR(20), unaContra CHAR(64))
BEGIN
   INSERT INTO Cliente(email, nombre, apellido, contraseña)
   VALUES (unMail, unNombre, unApellido, SHA2(unaContra, 256));
END $$

CALL `RegistrarCliente` ("ramonarielet12d1@gmail.com", "Ramón", "Lugones", 46912644);
CALL `RegistrarCliente` ("leivajenifer796@gmail.com", "Jenifer", "Leiva", 123456789);
CALL `RegistrarCliente` ("franco.salinaset12d1@gmail.com", "Franco", "Salinas", 987654321);


DELIMITER $$
DROP PROCEDURE IF EXISTS AltaReserva $$
CREATE PROCEDURE AltaReserva (unHotel SMALLINT UNSIGNED, Inicio DATE, fin DATE, unCliente MEDIUMINT, unCuarto TINYINT)
BEGIN
   INSERT INTO Reserva (idHotel, inicio, fin, idCliente, cuarto, habilitado)
       VALUES (unHotel, inicio, fin, unCliente , unCuarto, TRUE);
END $$
DELIMITER $$
DROP PROCEDURE IF EXISTS CerrarEstadiaHotel $$
CREATE PROCEDURE CerrarEstadiaHotel (unHotel SMALLINT UNSIGNED, fin DATE, unCliente MEDIUMINT, unCuarto TINYINT, unaCalif TINYINT UNSIGNED, unComentario VARCHAR (60))
BEGIN
   UPDATE Reserva
   SET habilitado = FALSE,
   calificacion_h = unaCalif,
   comentario = unComentario
   WHERE idHotel = unHotel &&
   fin = fin &&
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