use [Testing System]
create database [Testing System]

DROP TABLE IF EXISTS Settings;
DROP TABLE IF EXISTS Results;
DROP TABLE IF EXISTS Answers;
DROP TABLE IF EXISTS Questions;
DROP TABLE IF EXISTS Tests;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Roles;


DROP VIEW IF EXISTS UserTestResults;

DROP SEQUENCE IF EXISTS RoleSeq;
DROP SEQUENCE IF EXISTS UserSeq;
DROP SEQUENCE IF EXISTS TestSeq;
DROP SEQUENCE IF EXISTS QuestionSeq;
DROP SEQUENCE IF EXISTS AnswerSeq;
DROP SEQUENCE IF EXISTS ResultSeq;
DROP SEQUENCE IF EXISTS SettingSeq;

DROP INDEX IF EXISTS idx_username ON Users;
DROP INDEX IF EXISTS idx_test_name ON Tests;
DROP INDEX IF EXISTS idx_test_id ON Questions;
DROP INDEX IF EXISTS idx_question_id ON Answers;
DROP INDEX IF EXISTS idx_user_test ON Results;

DROP TRIGGER trg_UpdateTestActiveStatus;

CREATE TABLE Roles (
    role_id INT PRIMARY KEY,
    role_name NVARCHAR(255) NOT NULL UNIQUE,
	BossRoleID INT NULL,
	FOREIGN KEY (BossRoleID) REFERENCES Roles(role_id)
);

CREATE TABLE Users (
    user_id INT PRIMARY KEY,
    username NVARCHAR(255) NOT NULL UNIQUE,
    role INT NOT NULL,
    FOREIGN KEY (role) REFERENCES Roles(role_id)
);

CREATE TABLE Tests (
    test_id INT PRIMARY KEY,
    test_name NVARCHAR(255) NOT NULL,
    creator INT NOT NULL,
    date_created DATETIME NOT NULL DEFAULT GETDATE(),
    is_active BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (creator) REFERENCES Users(user_id)
);

CREATE TABLE Questions (
    question_id INT PRIMARY KEY,
    test_id INT NOT NULL,
    question_text NVARCHAR(255) NOT NULL,
    question_type NVARCHAR(255) NOT NULL,
    FOREIGN KEY (test_id) REFERENCES Tests(test_id)
);

CREATE TABLE Answers (
    answer_id INT PRIMARY KEY,
    question_id INT NOT NULL,
    answer_text NVARCHAR(255) NOT NULL,
    is_correct BIT NOT NULL,
    FOREIGN KEY (question_id) REFERENCES Questions(question_id)
);

CREATE TABLE Results (
    result_id INT PRIMARY KEY,
    user_id INT NOT NULL,
    test_id INT NOT NULL,
    score INT NOT NULL,
    date_taken DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (test_id) REFERENCES Tests(test_id)
);

CREATE TABLE Settings (
    setting_id INT PRIMARY KEY,
    setting_name NVARCHAR(255) NOT NULL UNIQUE,
    setting_value NVARCHAR(255) NOT NULL
);




CREATE VIEW UserTestResults AS
SELECT 
    u.username,
    t.test_name,
    r.score,
    r.date_taken
FROM 
    Results r
JOIN 
    Users u ON r.user_id = u.user_id
JOIN 
    Tests t ON r.test_id = t.test_id;


CREATE SEQUENCE UserSeq
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE RoleSeq
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE TestSeq
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE QuestionSeq
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE AnswerSeq
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE ResultSeq
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE SettingSeq
START WITH 1
INCREMENT BY 1;

CREATE INDEX idx_username ON Users(username);

CREATE INDEX idx_test_name ON Tests(test_name);

CREATE INDEX idx_test_id ON Questions(test_id);

CREATE INDEX idx_question_id ON Answers(question_id);

CREATE INDEX idx_user_test ON Results(user_id, test_id);


CREATE TRIGGER trg_UpdateTestActiveStatus
AFTER DELETE ON Questions
FOR EACH ROW
BEGIN
    DECLARE questionCount INT;

    SELECT COUNT(*)
    INTO questionCount
    FROM Questions
    WHERE test_id = OLD.test_id;

    IF questionCount = 0 THEN
        UPDATE Tests
        SET is_active = 0
        WHERE test_id = OLD.test_id;
    END IF;
END;

SELECT * FROM Roles;
SELECT * FROM Users;
SELECT * FROM Tests;
SELECT * FROM Questions;
SELECT * FROM Answers;
SELECT * FROM Results;
SELECT * FROM Settings;
SELECT * FROM UserTestResults;

EXEC GetRoleHierarchy @RoleID = 1;
EXEC GetRoleHierarchy @RoleID = 2;

EXEC MoveSubtreeRole @SourceRoleId = 6, @TargetRoleId = 2;


INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (NEXT VALUE FOR RoleSeq, 'Admin', NULL);
INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (NEXT VALUE FOR RoleSeq, 'Moderator', 1);
INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (NEXT VALUE FOR RoleSeq, 'Teacher', 2);
INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (NEXT VALUE FOR RoleSeq, 'aaa', 2);
INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (NEXT VALUE FOR RoleSeq, 'bbb', 3);
INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (NEXT VALUE FOR RoleSeq, 'adb', 1);
INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (NEXT VALUE FOR RoleSeq, '666', 6);

INSERT INTO Users (user_id, username, role) VALUES (NEXT VALUE FOR UserSeq, 'admin_user', 1);
INSERT INTO Users (user_id, username, role) VALUES (NEXT VALUE FOR UserSeq, 'regular_user', 2);

INSERT INTO Tests (test_id, test_name, creator) VALUES (NEXT VALUE FOR TestSeq, 'Math Test', 1);
INSERT INTO Tests (test_id, test_name, creator) VALUES (NEXT VALUE FOR TestSeq, 'Science Test', 1);
INSERT INTO Tests (test_id, test_name, creator) VALUES (NEXT VALUE FOR TestSeq, 'Science Test', 1);


INSERT INTO Questions (question_id, test_id, question_text, question_type) VALUES (NEXT VALUE FOR QuestionSeq, 1, 'What is 2 + 2?', 'Multiple Choice');
INSERT INTO Questions (question_id, test_id, question_text, question_type) VALUES (NEXT VALUE FOR QuestionSeq, 1, 'What is the capital of France?', 'Multiple Choice');
INSERT INTO Questions (question_id, test_id, question_text, question_type) VALUES (NEXT VALUE FOR QuestionSeq, 2, 'What is the chemical formula for water?', 'Multiple Choice');

INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (NEXT VALUE FOR AnswerSeq, 1, '3', 0);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (NEXT VALUE FOR AnswerSeq, 1, '4', 1);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (NEXT VALUE FOR AnswerSeq, 2, 'Berlin', 0);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (NEXT VALUE FOR AnswerSeq, 2, 'Paris', 1);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (NEXT VALUE FOR AnswerSeq, 3, 'H2O', 1);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (NEXT VALUE FOR AnswerSeq, 3, 'O2', 0);

INSERT INTO Results (result_id, user_id, test_id, score) VALUES (NEXT VALUE FOR ResultSeq, 2, 1, 80);
INSERT INTO Results (result_id, user_id, test_id, score) VALUES (NEXT VALUE FOR ResultSeq, 2, 2, 90);

INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES
    (NEXT VALUE FOR ResultSeq, 1, 1, 100, DATEADD(MONTH, -1, GETDATE())),  -- За последний месяц
    (NEXT VALUE FOR ResultSeq, 1, 2, 1, DATEADD(MONTH, -1, GETDATE())),  -- За последний месяц
    (NEXT VALUE FOR ResultSeq, 2, 1, 100, DATEADD(MONTH, -2, GETDATE())),  -- За последний месяц
    (NEXT VALUE FOR ResultSeq, 2, 2, 100, DATEADD(MONTH, -3, GETDATE())),  -- За последний квартал
    (NEXT VALUE FOR ResultSeq, 1, 1, 100, DATEADD(MONTH, -4, GETDATE())),  -- За последний квартал
    (NEXT VALUE FOR ResultSeq, 2, 2, 100, DATEADD(MONTH, -5, GETDATE())),  -- За последние полгода
    (NEXT VALUE FOR ResultSeq, 1, 1, 100, DATEADD(MONTH, -6, GETDATE())),  -- За последние полгода
    (NEXT VALUE FOR ResultSeq, 2, 1, 1, DATEADD(YEAR, -1, GETDATE())),   -- За последний год
    (NEXT VALUE FOR ResultSeq, 2, 2, 1, DATEADD(YEAR, -1, GETDATE())),   -- За последний год
    (NEXT VALUE FOR ResultSeq, 1, 2, 1, DATEADD(YEAR, -1, GETDATE()))    -- За последний год
;
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES
    (NEXT VALUE FOR ResultSeq, 1, 1, 1, DATEADD(MONTH, -1, GETDATE())),  -- За последний месяц
    (NEXT VALUE FOR ResultSeq, 1, 2, 1, DATEADD(MONTH, -1, GETDATE())),  -- За последний месяц
    (NEXT VALUE FOR ResultSeq, 2, 1, 1, DATEADD(MONTH, -2, GETDATE()))  -- За последний месяц
;

TRUNCATE TABLE Results;
select * from Results;

INSERT INTO Settings (setting_id, setting_name, setting_value) VALUES (NEXT VALUE FOR SettingSeq, 'Max Score', '100');
INSERT INTO Settings (setting_id, setting_name, setting_value) VALUES (NEXT VALUE FOR SettingSeq, 'Pass Score', '60');


CREATE PROCEDURE GetRoleHierarchy
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;

    WITH SubordinateRoles AS (
        SELECT
			@RoleId AS role_id,
            @RoleId AS BossRoleID,
            role_name,
            CAST(CAST(@RoleId AS NVARCHAR(10)) AS NVARCHAR(MAX)) AS HierarchyPath,
            0 AS Level
        FROM
            Roles
        WHERE
            role_id = @RoleId

        UNION ALL

        SELECT
            r.role_id,
            r.BossRoleID,
            r.role_name,
            CAST(s.HierarchyPath + '/' + CAST(r.role_id AS NVARCHAR(10)) AS NVARCHAR(MAX)) AS HierarchyPath,
            s.Level + 1
        FROM
            Roles r
        INNER JOIN
            SubordinateRoles s ON r.BossRoleID = s.role_id
    )
    SELECT
        role_id,
        HierarchyPath
    FROM
        SubordinateRoles
    ORDER BY
        role_id, BossRoleID, Level;
END;







CREATE PROCEDURE AddSubordinateRole
    @BossRoleId INT,
    @RoleName NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewRoleId INT;

    -- �������� ����� ���������� ������������� ��� ����
    SELECT @NewRoleId = NEXT VALUE FOR RoleSeq;

    -- ��������� ����� ���� � ��������� BossRoleId
    INSERT INTO Roles (role_id, role_name, BossRoleID)
    VALUES (@NewRoleId, @RoleName, @BossRoleId);
END;



EXEC AddSubordinateRole @BossRoleId = 2, @RoleName = 'Pupil';

SELECT * FROM Roles;










DROP PROCEDURE MoveSubtreeRole;

CREATE PROCEDURE MoveSubtreeRole
	@SourceRoleId INT,
    @TargetRoleId INT
AS
BEGIN
    UPDATE Roles
    SET BossRoleID = @TargetRoleId
    WHERE role_id = @SourceRoleId;
END;




CREATE PROCEDURE MoveSubtreeRole
    @SourceRoleId INT,
    @TargetRoleId INT
AS
BEGIN
    SET NOCOUNT ON;

    WITH Subtree AS (
        SELECT role_id
        FROM Roles
        WHERE role_id = @SourceRoleId
        
        UNION ALL
        
        SELECT r.role_id
        FROM Roles r
        INNER JOIN Subtree s ON r.BossRoleID = s.role_id
    )
    UPDATE Roles
    SET BossRoleID = @TargetRoleId
    WHERE role_id IN (SELECT role_id FROM Subtree);
END;

EXEC GetRoleHierarchy @RoleID = 4;


Select * from Roles;

DROP TABLE IF EXISTS Roles;
DROP SEQUENCE IF EXISTS RoleSeq;
CREATE TABLE Roles (
    role_id INT PRIMARY KEY,
    role_name NVARCHAR(255) NOT NULL UNIQUE,
	BossRoleID INT NULL,
	FOREIGN KEY (BossRoleID) REFERENCES Roles(role_id)
);
CREATE SEQUENCE RoleSeq
START WITH 1
INCREMENT BY 1;

INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (NEXT VALUE FOR RoleSeq, 'Admin', NULL);
EXEC AddSubordinateRole @BossRoleId = 1, @RoleName = 'Moderator';
EXEC AddSubordinateRole @BossRoleId = 1, @RoleName = 'Teacher';
EXEC AddSubordinateRole @BossRoleId = 3, @RoleName = 'HeadMan';
EXEC AddSubordinateRole @BossRoleId = 4, @RoleName = 'Pupil';

select * from roles;

EXEC GetRoleHierarchy @RoleID = 1;

EXEC MoveSubtreeRole @SourceRoleId = 3, @TargetRoleId = 2;



