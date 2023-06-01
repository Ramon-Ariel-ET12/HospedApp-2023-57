#El sistema va a admitir un máximo de 1000 hoteles.
delimiter //
Create procedure altaHotel (idHotel int)
begin
	if(exists(select idHotel
				from Hotel
				where idHotel > 100))then
				delete idHotel
				from Hotel
				where idHotel > 100;
	end if;
end //
#Existen 7 tipos de camas distintas.
Delimiter //
Create procedure AltaCama (idCama int )
begin
	if(exists(select idCama
				from Cama
                where idCama = 8))then
                delete 
				from Cama
                where idCama = 8;
	end if ;
end //
#No existen más de 100 cuartos por hotel.
Delimiter //
Create Procedure AltaCuarto (Cuarto int)
Begin
	if(exists(select Cuarto
				from Cuarto
                where Cuarto > 100)) then
                delete Cuarto
				from Cuarto
                where Cuarto > 100;
	end if ;
End //
#El sistema tiene que soportar hasta 100.000 clientes.
Delimiter // 
Create Procedure AltaCliente (idCLiente int)
Begin
	if(exists(select idCliente
				from Cliente
                where idCliente > 100000))then
                delete idCliente
				from Cliente
                where idCliente > 100000;
	end if ;
End //
#El sistema debe soportar 10 millones de reservas y cancelaciones.
Delimiter //
Create procedure AltaReserva (idReserva int)
Begin
	if(exists(select idReserva
				from Reserva
                where idReserva > 10000000))then
                delete idReserva
				from Reserva
                where idReserva > 10000000;
	end if ;
End //