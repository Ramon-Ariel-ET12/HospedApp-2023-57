USE HostedApp2023;
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