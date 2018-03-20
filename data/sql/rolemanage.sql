/*
Navicat MySQL Data Transfer

Source Server         : my
Source Server Version : 50720
Source Host           : localhost:3306
Source Database       : rolemanage

Target Server Type    : MYSQL
Target Server Version : 50720
File Encoding         : 65001

Date: 2018-03-17 22:47:06
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `limitinfo`
-- ----------------------------
DROP TABLE IF EXISTS `limitinfo`;
CREATE TABLE `limitinfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(255) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `IsEnable` bigint(20) DEFAULT NULL,
  `RoleModel` int(11) DEFAULT NULL,
  `SuperID` int(11) DEFAULT NULL,
  `OrderID` int(11) DEFAULT NULL,
  `LimitName` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of limitinfo
-- ----------------------------

-- ----------------------------
-- Table structure for `manager`
-- ----------------------------
DROP TABLE IF EXISTS `manager`;
CREATE TABLE `manager` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(255) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `FlagNo` varchar(255) DEFAULT NULL,
  `IsEnable` bigint(20) DEFAULT NULL,
  `RoleID` int(11) DEFAULT NULL,
  `LoginName` varchar(255) DEFAULT NULL,
  `LoginPwd` varchar(255) DEFAULT NULL,
  `IsSuperAdmin` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of manager
-- ----------------------------

-- ----------------------------
-- Table structure for `roleinfo`
-- ----------------------------
DROP TABLE IF EXISTS `roleinfo`;
CREATE TABLE `roleinfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(255) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `RoleName` varchar(255) DEFAULT NULL,
  `IsEnable` bigint(20) DEFAULT NULL,
  `RoleCode` varchar(255) DEFAULT NULL,
  `RoleLevel` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roleinfo
-- ----------------------------
