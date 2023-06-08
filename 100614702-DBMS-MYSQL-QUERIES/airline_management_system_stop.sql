-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: airline_management_system
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `stop`
--

DROP TABLE IF EXISTS `stop`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stop` (
  `Stop_Id` int NOT NULL,
  `Airport_Id` int DEFAULT NULL,
  `Arrival` datetime DEFAULT NULL,
  `Departure` datetime DEFAULT NULL,
  `Flight_Id` int DEFAULT NULL,
  PRIMARY KEY (`Stop_Id`),
  KEY `Airline_FK_idx` (`Airport_Id`),
  KEY `flight_Stop_Id_idx` (`Flight_Id`),
  CONSTRAINT `Airline_FK` FOREIGN KEY (`Airport_Id`) REFERENCES `airport` (`Airport_Id`) ON DELETE CASCADE,
  CONSTRAINT `flight_Stop_Id` FOREIGN KEY (`Flight_Id`) REFERENCES `flight` (`Flight_Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stop`
--

LOCK TABLES `stop` WRITE;
/*!40000 ALTER TABLE `stop` DISABLE KEYS */;
INSERT INTO `stop` VALUES (1,1,'2023-01-18 10:00:00','2023-01-18 10:00:00',1),(2,2,'2023-01-18 18:30:00','2023-01-18 20:00:00',1),(3,3,'2023-01-19 13:00:00','2023-01-19 13:00:00',1),(4,2,'2023-01-19 08:00:00','2023-01-19 08:00:00',2),(5,3,'2023-01-20 01:00:00','2023-01-20 01:00:00',2),(6,1,'2023-01-20 10:00:00','2023-01-20 10:00:00',3),(7,2,'2023-01-20 18:30:00','2023-01-20 20:00:00',3),(8,3,'2023-01-21 13:00:00','2023-01-21 13:00:00',3);
/*!40000 ALTER TABLE `stop` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-01-26 11:35:27