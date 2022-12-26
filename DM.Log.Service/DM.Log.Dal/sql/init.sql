CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `LogDotaRun` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Version` bigint NOT NULL,
    `CreateBy` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreateDt` datetime(6) NULL,
    `UpdateBy` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UpdateDt` datetime(6) NULL,
    CONSTRAINT `PK_LogDotaRun` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `LogInterface` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Title` varchar(50) CHARACTER SET utf8mb4 NOT NULL COMMENT 'Scenario Title',
    `Description` varchar(500) CHARACTER SET utf8mb4 NOT NULL COMMENT 'Scenario Description',
    `Version` bigint NOT NULL,
    `CreateBy` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreateDt` datetime(6) NULL,
    `UpdateBy` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UpdateDt` datetime(6) NULL,
    CONSTRAINT `PK_LogInterface` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221226091415_Init', '6.0.1');

COMMIT;

