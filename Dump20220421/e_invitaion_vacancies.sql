-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: localhost    Database: e_invitaion
-- ------------------------------------------------------
-- Server version	8.0.28

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
-- Table structure for table `vacancies`
--

DROP TABLE IF EXISTS `vacancies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vacancies` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OcassionId` int NOT NULL,
  `EnclosureId` int NOT NULL,
  `CategoryId` int NOT NULL,
  `RankId` int NOT NULL,
  `Total` int NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedOn` datetime NOT NULL,
  `IsActive` int NOT NULL,
  `CreatedBy` int NOT NULL,
  `OcassionName` text,
  `EnclosureName` text,
  `EnclosureColor` text,
  `CategoryName` text,
  `RankName` text,
  `IsLock` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vacancies`
--

LOCK TABLES `vacancies` WRITE;
/*!40000 ALTER TABLE `vacancies` DISABLE KEYS */;
INSERT INTO `vacancies` VALUES (46,14,1,1,14,10,'0001-01-01 00:00:00','2022-04-19 16:44:54',1,0,NULL,NULL,NULL,NULL,NULL,0),(47,14,2,2,2,10,'0001-01-01 00:00:00','2022-04-19 16:45:09',1,0,NULL,NULL,NULL,NULL,NULL,0),(48,14,2,2,3,10,'0001-01-01 00:00:00','2022-04-19 16:45:21',1,0,NULL,NULL,NULL,NULL,NULL,0),(49,14,3,2,5,10,'0001-01-01 00:00:00','2022-04-19 16:45:35',1,0,NULL,NULL,NULL,NULL,NULL,0),(50,15,1,1,14,10,'0001-01-01 00:00:00','2022-04-19 17:11:05',1,0,NULL,NULL,NULL,NULL,NULL,0),(51,15,2,2,2,10,'0001-01-01 00:00:00','2022-04-19 17:11:30',1,0,NULL,NULL,NULL,NULL,NULL,0),(52,15,2,2,3,10,'0001-01-01 00:00:00','2022-04-19 17:11:48',1,0,NULL,NULL,NULL,NULL,NULL,0),(53,15,2,3,8,10,'0001-01-01 00:00:00','2022-04-19 17:12:17',1,0,NULL,NULL,NULL,NULL,NULL,0),(54,15,3,4,14,10,'0001-01-01 00:00:00','2022-04-19 17:12:56',1,0,NULL,NULL,NULL,NULL,NULL,0),(55,16,1,1,1,10,'0001-01-01 00:00:00','2022-04-20 10:45:39',1,0,NULL,NULL,NULL,NULL,NULL,0),(56,16,1,2,9,10,'0001-01-01 00:00:00','2022-04-20 15:32:24',1,0,NULL,NULL,NULL,NULL,NULL,0);
/*!40000 ALTER TABLE `vacancies` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-04-21 16:14:56