-- Active: 1726541820216@@127.0.0.1@3306@5to_hospedapp2023


  -- Se pide desarrollar un trigger para que al momento de ingresar una reserva, si la fecha de inicio se encuentra entre la fecha de inicio y fin de otra reserva para el mismo cuarto con reserva no cancelada, no se debe permitir el INSERT mostrando la leyenda “Fecha Superpuesta”. También se tiene que tener en cuenta que un cliente no puede tener propias sin cancelar de manera superpuestas, es decir, al momento de reservar se tiene que verificar que ese mismo cliente no posea reservas propias no canceladas en otros lados, en ese caso también se tiene que mostrar la leyenda “El cliente ya posee otra reserva para esa fecha”.
DELIMITER $$
DROP TRIGGER IF EXISTS BefInsReserva $$
CREATE TRIGGER BefInsReserva BEFORE INSERT ON Reserva
FOR EACH ROW
BEGIN
	IF (NOT EXISTS(SELECT * FROM Hotel_Cuarto WHERE IdHotel = NEW.IdHotel AND Numero = NEW.Numero))THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'EL cuarto no existe en el hotel';
	END IF;
	IF (EXISTS(SELECT * FROM Reserva WHERE IdHotel = NEW.IdHotel AND Numero = NEW.Numero AND Inicio <= NEW.Inicio AND Fin >= NEW.Fin))THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Fecha Superpuesta';
	END IF;
END $$

DELIMITER $$
DROP TRIGGER IF EXISTS befInsCliente $$
CREATE TRIGGER befInsCliente BEFORE INSERT ON Cliente
FOR EACH ROW
BEGIN
    SET NEW.Contrasena = SHA2(NEW.Contrasena, 256);
	IF (CHAR_LENGTH(NEW.Dni) != 8)THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'El dni tiene que ser de 8 digitos';
	END IF;
END $$


DELIMITER $$
DROP TRIGGER IF EXISTS befUpdCliente $$
CREATE TRIGGER befUpdCliente BEFORE UPDATE ON Cliente
FOR EACH ROW
BEGIN
    SET NEW.Contrasena = SHA2(NEW.Contrasena, 256);
END $$

DELIMITER $$
DROP TRIGGER IF EXISTS befInsHotel_Cuarto $$
 CREATE TRIGGER befInsHotel_Cuarto BEFORE INSERT ON Hotel_Cuarto
 FOR EACH ROW
 BEGIN
	DECLARE suma int;
	SELECT COUNT(IdCuarto) INTO suma
	FROM Hotel_Cuarto
	WHERE IdHotel = NEW.IdHotel;
	SET suma = suma + 1;
	SET NEW.Numero = suma;
 END $$