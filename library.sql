-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'books'
--
-- ---

DROP TABLE IF EXISTS `books`;

CREATE TABLE `books` (
  `book_id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `title` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`book_id`)
);

-- ---
-- Table 'authors'
--
-- ---

DROP TABLE IF EXISTS `authors`;

CREATE TABLE `authors` (
  `author_id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`author_id`)
);

-- ---
-- Table 'authors_books'
--
-- ---

DROP TABLE IF EXISTS `authors_books`;

CREATE TABLE `authors_books` (
  `author_id` INTEGER NULL DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL
);

-- ---
-- Table 'copies'
--
-- ---

DROP TABLE IF EXISTS `copies`;

CREATE TABLE `copies` (
  `copy_id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`copy_id`)
);

-- ---
-- Table 'patrons'
--
-- ---

DROP TABLE IF EXISTS `patrons`;

CREATE TABLE `patrons` (
  `patron_id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`patron_id`)
);

-- ---
-- Table 'checkouts'
--
-- ---

DROP TABLE IF EXISTS `checkouts`;

CREATE TABLE `checkouts` (
  `patron_id` INTEGER NULL DEFAULT NULL,
  `copy_id` INTEGER NULL DEFAULT NULL,
  `check_out` DATETIME NULL DEFAULT NULL,
  `due_date` DATETIME NULL DEFAULT NULL
);

-- ---
-- Table 'history'
--
-- ---

DROP TABLE IF EXISTS `history`;

CREATE TABLE `history` (
  `patron_id` INTEGER NULL DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  `check_out` DATETIME NULL DEFAULT NULL,
  `check_in` DATETIME NULL DEFAULT NULL
);

-- ---
-- Foreign Keys
-- ---

-- ALTER TABLE `authors_books` ADD FOREIGN KEY (author_id) REFERENCES `authors` (`author_id`);
-- ALTER TABLE `authors_books` ADD FOREIGN KEY (book_id) REFERENCES `books` (`book_id`);
-- ALTER TABLE `copies` ADD FOREIGN KEY (book_id) REFERENCES `books` (`book_id`);
-- ALTER TABLE `checkouts` ADD FOREIGN KEY (patron_id) REFERENCES `patrons` (`patron_id`);
-- ALTER TABLE `checkouts` ADD FOREIGN KEY (copy_id) REFERENCES `copies` (`copy_id`);
-- ALTER TABLE `history` ADD FOREIGN KEY (patron_id) REFERENCES `patrons` (`patron_id`);
-- ALTER TABLE `history` ADD FOREIGN KEY (book_id) REFERENCES `books` (`book_id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `authors` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `authors_books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `copies` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `patrons` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `checkouts` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `history` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `books` (`book_id`,`title`) VALUES
-- ('','');
-- INSERT INTO `authors` (`author_id`,`name`) VALUES
-- ('','');
-- INSERT INTO `authors_books` (`author_id`,`book_id`) VALUES
-- ('','');
-- INSERT INTO `copies` (`copy_id`,`book_id`) VALUES
-- ('','');
-- INSERT INTO `patrons` (`patron_id`,`name`) VALUES
-- ('','');
-- INSERT INTO `checkouts` (`patron_id`,`copy_id`,`check_out`,`due_date`) VALUES
-- ('','','','');
-- INSERT INTO `history` (`patron_id`,`book_id`,`check_out`,`check_in`) VALUES
-- ('','','','');
