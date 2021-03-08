/*
 Navicat Premium Data Transfer

 Source Server         : 127.0.0.1_3306
 Source Server Type    : MySQL
 Source Server Version : 80022
 Source Host           : 127.0.0.1:3306
 Source Schema         : courseware

 Target Server Type    : MySQL
 Target Server Version : 50799
 File Encoding         : 65001

 Date: 08/03/2021 15:38:00
*/

SET NAMES utf8;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cw_courseware
-- ----------------------------
DROP TABLE IF EXISTS `cw_courseware`;
CREATE TABLE `cw_courseware`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `price` decimal(10, 2) NOT NULL DEFAULT 0.00,
  `count` int(0) NULL DEFAULT 0,
  `url` varchar(10000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `create_time` timestamp(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  `cover` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `is_carousel` int(0) NULL DEFAULT 0,
  `carousel_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 21 CHARACTER SET = utf8 COLLATE = utf8_general_ci;

-- ----------------------------
-- Records of cw_courseware
-- ----------------------------
BEGIN;
INSERT INTO `cw_courseware` VALUES (1, '测试ppt', 0.02, 1, '/resource/20210220045854mark.pptx', '2021-02-18 13:55:11', '/resource/20210218135442链表.png', 1, '/resource/20210220045823q.jpg'), (4, '微服务架构', 9.99, 0, '/resource/20210220045620微服务架构.pdf', '2021-02-18 13:55:11', '/resource/20210220050408noorder.png', 2, '/resource/20210218185458starSky.png'), (20, 'wps网盘使用帮助', 0.01, 0, '/resource/20210220050216WPS网盘使用帮助.doc', '2021-02-20 21:56:14', '/resource/20210220050158log.png', 0, NULL);
COMMIT;

-- ----------------------------
-- Table structure for cw_exchange_key
-- ----------------------------
DROP TABLE IF EXISTS `cw_exchange_key`;
CREATE TABLE `cw_exchange_key`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `ex_key` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `cw_id` int(0) NOT NULL,
  `is_used` tinyint(1) NULL DEFAULT 0,
  `create_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0),
  `use_time` timestamp(0) NULL DEFAULT NULL,
  `user_id` int(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `cw_exchange_key_cw_courseware_id_fk`(`cw_id`) USING BTREE,
  INDEX `cw_exchange_key_user_id_fk`(`user_id`) USING BTREE,
  CONSTRAINT `cw_exchange_key_cw_courseware_id_fk` FOREIGN KEY (`cw_id`) REFERENCES `cw_courseware` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `cw_exchange_key_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `sys_user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_general_ci;

-- ----------------------------
-- Records of cw_exchange_key
-- ----------------------------
BEGIN;
INSERT INTO `cw_exchange_key` VALUES (2, '5daca046-2e63-4454-b673-751909de4b55', 4, 1, '2021-02-20 10:46:50', '2021-02-20 10:48:34', 4), (3, 'fd0dfb2a-dd1e-402c-9017-c2f3d6a53389', 1, 1, '2021-02-20 21:12:15', '2021-02-20 21:13:57', 4);
COMMIT;

-- ----------------------------
-- Table structure for cw_order
-- ----------------------------
DROP TABLE IF EXISTS `cw_order`;
CREATE TABLE `cw_order`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `order_sn` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `cw_id` int(0) NULL DEFAULT NULL,
  `user_id` int(0) NOT NULL,
  `price` decimal(10, 2) NOT NULL,
  `create_time` timestamp(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  `pay_time` timestamp(0) NULL DEFAULT NULL,
  `is_pay` tinyint(1) NULL DEFAULT 0,
  `pay_type` int(0) NULL DEFAULT NULL COMMENT '0->小程序',
  `wx_order` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `cw_order_order_sn_index`(`order_sn`) USING BTREE,
  INDEX `cw_order_cw_courseware_id_fk`(`cw_id`) USING BTREE,
  INDEX `cw_order_user_id_fk`(`user_id`) USING BTREE,
  CONSTRAINT `cw_order_cw_courseware_id_fk` FOREIGN KEY (`cw_id`) REFERENCES `cw_courseware` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `cw_order_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `sys_user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8 COLLATE = utf8_general_ci;

-- ----------------------------
-- Records of cw_order
-- ----------------------------
BEGIN;
INSERT INTO `cw_order` VALUES (6, 'c933cbd4-a784-4ed9-8aeb-6344f3899064', 1, 4, 0.02, '2021-02-20 10:44:23', NULL, 0, NULL, NULL), (7, '58fac7bc-022d-4dc3-9123-d32fa7376536', 1, 4, 0.02, '2021-02-20 10:53:58', NULL, 0, NULL, NULL);
COMMIT;

-- ----------------------------
-- Table structure for cw_user_courseware
-- ----------------------------
DROP TABLE IF EXISTS `cw_user_courseware`;
CREATE TABLE `cw_user_courseware`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `user_id` int(0) NOT NULL,
  `cw_id` int(0) NOT NULL,
  `create_time` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `cw_user_courseware_cw_courseware_id_fk`(`cw_id`) USING BTREE,
  INDEX `cw_user_courseware_user_id_fk`(`user_id`) USING BTREE,
  CONSTRAINT `cw_user_courseware_cw_courseware_id_fk` FOREIGN KEY (`cw_id`) REFERENCES `cw_courseware` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `cw_user_courseware_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `sys_user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci;

-- ----------------------------
-- Records of cw_user_courseware
-- ----------------------------
BEGIN;
INSERT INTO `cw_user_courseware` VALUES (4, 4, 4, '2021-02-20 10:48:34'), (5, 4, 1, '2021-02-20 21:13:57');
COMMIT;

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `role_id` int(0) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `level` int(0) NULL DEFAULT NULL COMMENT '角色级别',
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `data_scope` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '数据权限',
  `create_by` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建者',
  `update_by` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '更新者',
  `create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建日期',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`role_id`) USING BTREE,
  UNIQUE INDEX `uniq_name`(`name`) USING BTREE,
  INDEX `role_name_index`(`name`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色表';

-- ----------------------------
-- Records of sys_role
-- ----------------------------
BEGIN;
INSERT INTO `sys_role` VALUES (1, '管理员', NULL, NULL, NULL, NULL, NULL, NULL, NULL), (2, '接单者', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
COMMIT;

-- ----------------------------
-- Table structure for sys_role_permission
-- ----------------------------
DROP TABLE IF EXISTS `sys_role_permission`;
CREATE TABLE `sys_role_permission`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `role_id` int(0) NOT NULL,
  `permission` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `sys_role_permission_sys_role_role_id_fk`(`role_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci;

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `nickname` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `uuid` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `username` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `gender` enum('男','女','保密') CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '保密',
  `portrait` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像',
  `background` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '背景图片',
  `phone_number` varchar(11) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `user_username_uindex`(`username`) USING BTREE,
  UNIQUE INDEX `user_uuid_uindex`(`uuid`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
BEGIN;
INSERT INTO `sys_user` VALUES (1, '起凡', '43dcbc5b-8776-429e-b12b-2cae6bd97020', '起凡', '25d55ad283aa400af464c76d713c07ad', '男', '/resource/img/1612235895184132.png', '', ''), (2, '珂朵莉永生', '43dcbc5b-8776-429e-b12b-2cae6bd97021', '游客', '25d55ad283aa400af464c76d713c07ad', '保密', '/resource/20201105012059-7892016800fa31cd.jpg', NULL, NULL), (3, '管理员', '43dcbc5b-8776-439e-b12b-2cae6bd97021', 'admin', '25d55ad283aa400af464c76d713c07ad', '保密', NULL, NULL, NULL), (4, '起凡', 'fbca158c-722b-45d8-ad63-ddf44ad3b422', '0m4lJ60b6w', 'j47izilk5b', '男', 'https://thirdwx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTLVvJDehJiaEMKnDpKzet1Fy2QMCATHaUdb6Lc6v0WZapn67IyTEGr8UrE16FeC6rvpcwibIylVu8Zg/132', '', '13656987994');
COMMIT;

-- ----------------------------
-- Table structure for sys_users_roles
-- ----------------------------
DROP TABLE IF EXISTS `sys_users_roles`;
CREATE TABLE `sys_users_roles`  (
  `user_id` int(0) NOT NULL COMMENT '用户ID',
  `role_id` int(0) NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`user_id`, `role_id`) USING BTREE,
  INDEX `FKq4eq273l04bpu4efj0jd0jb98`(`role_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户角色关联';

-- ----------------------------
-- Records of sys_users_roles
-- ----------------------------
BEGIN;
INSERT INTO `sys_users_roles` VALUES (3, 1);
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;
