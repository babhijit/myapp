SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `myapp` ;
CREATE SCHEMA IF NOT EXISTS `myapp` DEFAULT CHARACTER SET utf8 ;
USE `myapp` ;

-- -----------------------------------------------------
-- table student
-- -----------------------------------------------------

CREATE  TABLE IF NOT EXISTS `myapp`.`Student` (
  `ID` INT NOT NULL AUTO_INCREMENT ,
  `StudentName` VARCHAR(45) NOT NULL ,
  `StudentAge` INT NULL ,
  `StudentStandard` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`ID`) 
  ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  
-- -----------------------------------------------------
-- procedure InsertStudent
-- -----------------------------------------------------

USE `myapp`;
DROP procedure IF EXISTS `myapp`.`InsertUpdateStudent`;

DELIMITER $$
USE `myapp`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertUpdateStudent`(IN studentID INT , IN studentName VARCHAR(45), IN studentAge INT, IN studentStandard VARCHAR(45), OUT insertedID INT)
BEGIN
	DECLARE recordExists INT;
	
	SELECT COUNT(*) INTO recordExists FROM Student WHERE ID = studentID;

	IF recordExists > 0 && studentID > 0 THEN 
		UPDATE Student 
		SET StudentName = studentName, StudentAge = studentAge, StudentStandard = studentStandard
		WHERE ID = studentID;

	ELSE
		INSERT INTO student (StudentName, StudentAge, StudentStandard)
			VALUES (studentName, studentAge, studentStandard);

	SELECT LAST_INSERT_ID() INTO insertedID;

END IF;
			
END$$

DELIMITER ;


-- -----------------------------------------------------
-- data insertion
-- -----------------------------------------------------

INSERT INTO `myapp`.`student` (`StudentName`, `StudentAge`, `StudentStandard`)
			VALUES ('Add New', 0, 'NA');
UPDATE `myapp`.`student` SET `ID`= 0 WHERE `ID`=1;

-- -------------------------------------------------------------

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

