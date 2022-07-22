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
-- Table structure for table `vacancyplans`
--

DROP TABLE IF EXISTS `vacancyplans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vacancyplans` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OcassionId` int NOT NULL,
  `UserId` int NOT NULL,
  `EnclosureId` int NOT NULL,
  `RankId` int NOT NULL,
  `Total` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=184 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vacancyplans`
--

LOCK TABLES `vacancyplans` WRITE;
/*!40000 ALTER TABLE `vacancyplans` DISABLE KEYS */;
INSERT INTO `vacancyplans` VALUES (147,14,9,2,2,4),(148,14,9,2,3,3),(149,14,9,1,14,2),(150,14,9,3,5,1),(151,14,10,2,2,1),(152,14,10,2,3,2),(153,14,10,1,14,3),(154,14,10,3,5,4),(155,14,11,2,2,1),(156,14,11,2,3,0),(157,14,11,1,14,0),(158,14,11,3,5,0),(159,14,12,2,3,2),(160,14,12,1,14,2),(161,14,12,3,5,4),(162,14,12,2,2,1),(163,15,12,2,2,0),(164,15,12,2,3,0),(165,15,12,2,8,0),(166,15,12,1,14,0),(167,15,12,3,14,0),(168,15,13,2,2,0),(169,15,13,2,3,0),(170,15,13,2,8,0),(171,15,13,1,14,2),(172,15,13,3,14,2),(173,15,9,2,2,0),(174,15,9,2,3,0),(175,15,9,2,8,0),(176,15,9,1,14,0),(177,15,9,3,14,0),(178,16,13,1,1,3),(179,16,9,1,1,3),(180,16,11,1,1,3),(181,16,9,1,9,1),(182,16,11,1,9,2),(183,16,13,1,9,1);
/*!40000 ALTER TABLE `vacancyplans` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-04-21 16:14:57
