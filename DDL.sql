DROP Database if exists 5to_HostedApp2022;
CREATE Database 5to_HostedApp2022;
USE 5to_HostedApp2022;

CREATE TABLE Hotel(
idHotel SMALLINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
domicilio VARCHAR(20) not null,
email VARCHAR(20) not null,
contraseña CHAR(64) not null,
estrella TINYINT UNSIGNED not null,
CONSTRAINT UK_contra UNIQUE(contraseña)
);  

CREATE TABLE Cliente(
idCliente MEDIUMINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
email VARCHAR(20) not null,
nombre VARCHAR(20) not null,
apellido VARCHAR(20) not null,
contraseña CHAR(64) not null
);

CREATE TABLE Cama(
idCama TINYINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
nombre VARCHAR(20) not null,
pueden_dormir TINYINT not null
);

CREATE TABLE Cuarto(
cuarto TINYINT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
idCama TINYINT UNSIGNED not null,
cochera char(2) not null,
costo_noche SMALLINT UNSIGNED not null,
descripcion VARCHAR(60) not null,
CONSTRAINT FK_CUARTO_CAMA FOREIGN KEY (idCama)
REFERENCES Cama(idCama)
);

CREATE TABLE cuarto_cama(
cuarto TINYINT UNSIGNED not null,
idCama TINYINT UNSIGNED not null,
cant_camas TINYINT UNSIGNED not null,
PRIMARY KEY (cuarto, idCama), 
CONSTRAINT FK_HOTEL_CUARTO FOREIGN KEY (cuarto)
REFERENCES Cuarto(cuarto),
CONSTRAINT FK_HOTEL_CAMA FOREIGN KEY (idCama)
REFERENCES Cama(idCama)
);

CREATE TABLE Reserva(
idReserva INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
idHotel SMALLINT UNSIGNED not null,
inicio DATE not null,
fin DATE not null,
idCliente MEDIUMINT UNSIGNED not null,
cuarto TINYINT UNSIGNED not null,
calificacion_C TINYINT UNSIGNED not null,
calificacion_H TINYINT UNSIGNED not null,
comentario_C VARCHAR(60),
habilitado BOOL not null,
CONSTRAINT FK_HOTEL_ID FOREIGN KEY (idHotel)
REFERENCES Hotel(idHotel),
CONSTRAINT FK_HOTEL_CLIENTE FOREIGN KEY (idCliente)
REFERENCES Cliente(idCliente),
CONSTRAINT FK_HOTEL_RESERVA FOREIGN KEY (cuarto)
REFERENCES Cuarto(cuarto)
);