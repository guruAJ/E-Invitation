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
-- Table structure for table `addgestlists`
--

DROP TABLE IF EXISTS `addgestlists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `addgestlists` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OcassionId` int NOT NULL,
  `UserId` int NOT NULL,
  `EnclosureId` int NOT NULL,
  `RankId` int NOT NULL,
  `ArmyNo` text NOT NULL,
  `IndlName` text NOT NULL,
  `Unit` text NOT NULL,
  `Fmn` text,
  `EmailId` text NOT NULL,
  `PhoneNo` text NOT NULL,
  `NameOfGest` text NOT NULL,
  `Gender` text NOT NULL,
  `Relation` text NOT NULL,
  `Dob` text NOT NULL,
  `AdhaorNo` text,
  `Photo` text,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `addgestlists`
--

LOCK TABLES `addgestlists` WRITE;
/*!40000 ALTER TABLE `addgestlists` DISABLE KEYS */;
INSERT INTO `addgestlists` VALUES (48,14,11,2,2,'1233','Renjit','22 Unit','22 Fmn','sshs@Gmail.com','9876543210','Renjit','Male','Self','2022-03-30','32344','5634c3b7-0830-4bb5-aa24-47184842321a.jpeg',1),(49,15,13,1,14,'123','Rahul','22 Unm','22 Fmn','sds@mil.com','9876543218','Rahul','Male','Self','2022-03-29','212','2502a83d-61d5-4209-b7f1-96e1f1c4ef92.jpeg',1),(50,15,13,1,14,'123','Rahul','22 Unm','22 Fmn','sds@mil.com','9876543218','Meera','Male','Wife','2022-04-06','2233','f52dc1c4-1179-49d7-8ee5-3c2e2105e922.jpeg',1),(51,16,13,1,1,'1233','ARUN KUMAR','22 AAD','22 FMN','FDF@GMAIL.COM','2323232323','ARUN KUMAR','Male','Self','2022-04-20','43243324','69dba732-8d4d-496d-a727-80dfe25b0056.png',1),(52,16,9,1,1,'1245','DGIS','KK','3241','K@GMAIL.COM','2636011092','KAPOOR DGIS','Male','Self','2022-04-13','324234',NULL,1),(53,16,9,1,1,'1245','DGIS','KK','3241','K@GMAIL.COM','2636011092','KAPOOR 2 DGIS','Male','Self','2022-04-13','45646',NULL,1),(54,16,13,1,1,'1233','ARUN KUMAR','22 AAD','22 FMN','FDF@GMAIL.COM','2323232323','KAPOOR DGMO','Male','Self','2022-04-15','3242342313','null',1);
/*!40000 ALTER TABLE `addgestlists` ENABLE KEYS */;
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
