-- phpMyAdmin SQL Dump
-- version 4.8.2
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 13-12-2018 a las 10:03:47
-- Versión del servidor: 10.1.34-MariaDB
-- Versión de PHP: 7.2.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `thaidersusers`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE `users` (
  `id` int(5) NOT NULL,
  `Nombre` varchar(20) NOT NULL,
  `Apellidos` varchar(40) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Nacimiento` varchar(12) NOT NULL,
  `Nacionalidad` varchar(12) NOT NULL,
  `Nick` text NOT NULL,
  `Password` varchar(20) NOT NULL,
  `ban` tinyint(4) NOT NULL,
  `FriendsList` text NOT NULL,
  `hash` varchar(100) NOT NULL,
  `salt` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`id`, `Nombre`, `Apellidos`, `Email`, `Nacimiento`, `Nacionalidad`, `Nick`, `Password`, `ban`, `FriendsList`, `hash`, `salt`) VALUES
(1, 'Alejandro', 'Rivas Carrillo', 'anfi04@hotmail.es', '24/07/95', 'Española', 'Alejandrator95', 'MyPaswroef', 0, '', '', ''),
(2, '11111111111', '111111111111111', '1111111111', '11/Enero/111', 'EspaÃ±a', '1111111111', '$5$rounds=5000$steam', 0, '', '', ''),
(3, 'alex', 'prueba', 'loginprueba', '21/Enero/22', 'EspaÃ±a', 'prueba', '$5$rounds=5000$steam', 0, '', '', ''),
(4, 'aaaaaaaaa', 'aaaaaaaaaaaaa', 'prueba', '22/Enero/112', 'EspaÃ±a', 'prueber', '', 0, '', '$5$rounds=5000$steamehamsprueba$HUvTrXnDpPagXT.OeyUwnUJiXC36owvMZNQbo3KxtCD', '$5$rounds=5000$steamehamsprueba$'),
(5, 'aaaaaaaaa', 'aaaaaaaaaaaaa', 'pruebaa', '22/Enero/112', 'EspaÃ±a', 'prueber', 'prueba', 0, '1,2,3,5,4', '$5$rounds=5000$steamehamsprueba$HUvTrXnDpPagXT.OeyUwnUJiXC36owvMZNQbo3KxtCD', '$5$rounds=5000$steamehamspruebaa$');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `users`
--
ALTER TABLE `users`
  ADD UNIQUE KEY `id` (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `users`
--
ALTER TABLE `users`
  MODIFY `id` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
