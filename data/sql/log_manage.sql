/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50720
Source Host           : localhost:3306
Source Database       : log_manage

Target Server Type    : MYSQL
Target Server Version : 50720
File Encoding         : 65001

Date: 2018-03-23 11:57:27
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_log_debug
-- ----------------------------
DROP TABLE IF EXISTS `t_log_debug`;
CREATE TABLE `t_log_debug` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(255) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Content` varchar(255) DEFAULT NULL,
  `IsDeal` tinyint(1) DEFAULT NULL,
  `ParamInfo` varchar(255) DEFAULT NULL,
  `DealIntro` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_log_debug
-- ----------------------------

-- ----------------------------
-- Table structure for t_log_sys
-- ----------------------------
DROP TABLE IF EXISTS `t_log_sys`;
CREATE TABLE `t_log_sys` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(255) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Content` varchar(255) DEFAULT NULL,
  `CreaterID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_log_sys
-- ----------------------------

-- ----------------------------
-- Table structure for t_log_upload
-- ----------------------------
DROP TABLE IF EXISTS `t_log_upload`;
CREATE TABLE `t_log_upload` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(255) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Content` varchar(255) DEFAULT NULL,
  `CreaterID` int(11) DEFAULT NULL,
  `FileName` varchar(255) DEFAULT NULL,
  `SaveType` varchar(255) DEFAULT NULL,
  `ResultStatus` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_log_upload
-- ----------------------------
