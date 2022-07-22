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
-- Table structure for table `ocassions`
--

DROP TABLE IF EXISTS `ocassions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ocassions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OcassionName` varchar(200) NOT NULL,
  `OcassionDate` datetime NOT NULL,
  `ChiefName` varchar(200) NOT NULL,
  `Venue` varchar(200) NOT NULL,
  `Time` varchar(200) NOT NULL,
  `Dress` varchar(200) NOT NULL,
  `Dress1` varchar(200) NOT NULL,
  `ContactName` varchar(200) NOT NULL,
  `IssueBranch` varchar(200) NOT NULL,
  `PhoneNo` varchar(10) NOT NULL,
  `ASCON` text NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedOn` datetime NOT NULL,
  `IsActive` int NOT NULL,
  `CreatedBy` int NOT NULL,
  `IsLock` tinyint(1) NOT NULL,
  `IsFinish` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ocassions`
--

LOCK TABLES `ocassions` WRITE;
/*!40000 ALTER TABLE `ocassions` DISABLE KEYS */;
INSERT INTO `ocassions` VALUES (14,'15 August A','2022-08-15 16:44:00','MM Naravane','Cariappa Parade Ground, Delhi Cant','1030 h (To be Seated by 0950h)','Dress No-1, Winter Ceremonial','National Dress / Lounge Suit','RSVP','AG/CW-1','9876543210','35140','2022-04-19 16:44:35','2022-04-19 16:44:35',1,1,1,1),(15,'15 August B','2022-04-01 17:07:00','MM Naravane','Cariappa Parade Ground, Delhi Cant','1030 h (To be Seated by 0950h)','Dress No-1, Winter Ceremonial','National Dress / Lounge Suit','RSVP','AG/CW-1','9876543210','35140','2022-04-19 17:08:46','2022-04-19 17:08:46',1,1,1,0),(16,'15 August C','2022-08-15 00:00:00','MM Naravane','Cariappa Parade Ground, Delhi Cant','1030 h (To be Seated by 0950h)','Dress No-1, Winter Ceremonial','National Dress / Lounge Suit','RSVP','AG/CW-1','9876543218','35140','2022-04-20 16:56:55','2022-04-20 16:56:55',1,1,0,0);
/*!40000 ALTER TABLE `ocassions` ENABLE KEYS */;
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
