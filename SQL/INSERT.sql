-- Active: 1723664382511@@127.0.0.1@3306@5to_hospeddapp2023
CALL AltaHotel (@Hoteldeprueba, "Hoteldeprueba","En el Hotel", "Hotel de prueba@gmail.com", 'Hoteldeprueba', 5);
CALL AltaCama (@Cama, "Cama", 2);
CALL AltaCuarto (@cuarto1, TRUE, 24.99, "Un cuarto con cochera, cuesta 24.99 pesos :v");
CALL AltaCuarto (@cuarto2, FALSE, 19.99, "Un cuarto sin cochera pipipi, cuesta 19.99 pesos :v");
CALL AltaCuarto_Cama (@cuarto1, @Cama, 2);
CALL AltaHotel_Cuarto (@Hoteldeprueba, @cuarto1);
CALL AltaHotel_Cuarto (@Hoteldeprueba, @cuarto2);
CALL RegistrarCliente (12345678, "Leonel", "Messi", "Quemirabobo@gmail.com","Andapalla");
CALL RegistrarCliente (87654321, "Roberto", "Bolaños", "RobertoBolaños777@gmail.com","contraseñadeRobertoBolaños");
CALL AltaReserva (@Hoteldeprueba,'2023-02-01', '2023-04-01', 12345678, @cuarto1);
CALL AltaReserva (@Hoteldeprueba,'2023-02-06', '2023-03-22', 87654321, @cuarto2);
CALL CerrarEstadiaHotel (1,1,10);
CALL CerrarEstadiaHotel (1,2,10);
CALL CerrarEstadiaCliente (12345678, 10, "Bueno muchachos nos vemos en miami");
CALL CerrarEstadiaCliente (87654321, 10, "Me arrepiento asi que se cancela :v");