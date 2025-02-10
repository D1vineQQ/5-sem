DROP TABLE Settings;
DROP TABLE Results;
DROP TABLE Answers;
DROP TABLE Questions;
DROP TABLE Tests;
DROP TABLE Users;
DROP TABLE Roles;


CREATE TABLE Roles (
    role_id NUMBER PRIMARY KEY,
    role_name NVARCHAR2(255) NOT NULL UNIQUE
);

CREATE TABLE Users (
    user_id NUMBER PRIMARY KEY,
    username NVARCHAR2(255) NOT NULL UNIQUE,
    role NUMBER NOT NULL,
    FOREIGN KEY (role) REFERENCES Roles(role_id)
);

CREATE TABLE Tests (
    test_id NUMBER PRIMARY KEY,
    test_name NVARCHAR2(255) NOT NULL,
    creator NUMBER NOT NULL,
    date_created DATE DEFAULT CURRENT_DATE,
    is_active NUMBER(1) DEFAULT 1,
    FOREIGN KEY (creator) REFERENCES Users(user_id)
);

CREATE TABLE Questions (
    question_id NUMBER PRIMARY KEY,
    test_id NUMBER NOT NULL,
    question_text NVARCHAR2(255) NOT NULL,
    question_type NVARCHAR2(255) NOT NULL,
    FOREIGN KEY (test_id) REFERENCES Tests(test_id)
);

CREATE TABLE Answers (
    answer_id NUMBER PRIMARY KEY,
    question_id NUMBER NOT NULL,
    answer_text NVARCHAR2(255) NOT NULL,
    is_correct NUMBER(1) NOT NULL,
    FOREIGN KEY (question_id) REFERENCES Questions(question_id)
);

CREATE TABLE Results (
    result_id NUMBER PRIMARY KEY,
    user_id NUMBER NOT NULL,
    test_id NUMBER NOT NULL,
    score NUMBER NOT NULL,
    date_taken DATE DEFAULT CURRENT_DATE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (test_id) REFERENCES Tests(test_id)
);

CREATE TABLE Settings (
    setting_id NUMBER PRIMARY KEY,
    setting_name NVARCHAR2(255) NOT NULL UNIQUE,
    setting_value NVARCHAR2(255) NOT NULL
);


SELECT * FROM Roles;
SELECT * FROM Users;
SELECT * FROM Tests;
SELECT * FROM Questions;
SELECT * FROM Answers;
SELECT * FROM Results;
SELECT * FROM Settings;
SELECT * FROM UserTestResults;



DROP VIEW UserTestResults;

DROP SEQUENCE RoleSeq;
DROP SEQUENCE UserSeq;
DROP SEQUENCE TestSeq;
DROP SEQUENCE QuestionSeq;
DROP SEQUENCE AnswerSeq;
DROP SEQUENCE ResultSeq;
DROP SEQUENCE SettingSeq;


DROP INDEX idx_test_name;
DROP INDEX idx_test_id;
DROP INDEX idx_question;
DROP INDEX idx_user_test;



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


CREATE INDEX idx_test_name ON Tests(test_name);
CREATE INDEX idx_test_id ON Questions(test_id);
CREATE INDEX idx_question_id ON Answers(question_id);
CREATE INDEX idx_user_test ON Results(user_id, test_id);



-- Вставка ролей
INSERT INTO Roles (role_id, role_name) VALUES (RoleSeq.NEXTVAL, 'Admin');
INSERT INTO Roles (role_id, role_name) VALUES (RoleSeq.NEXTVAL, 'Moderator');

-- Вставка ролей
-- INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (RoleSeq.NEXTVAL, 'Admin', NULL);
-- INSERT INTO Roles (role_id, role_name, BossRoleID) VALUES (RoleSeq.NEXTVAL, 'Moderator', 1);

-- ALTER TABLE Roles
-- ADD HierarchyID HIERARCHYID;

-- EXEC AddChildRole('Admin', NULL);
-- EXEC AddChildRole('Moderator', 1);
-- EXEC AddChildRole('User', 1);
-- EXEC AddChildRole('Guest', 3);
-- Вставка пользователей
INSERT INTO Users (user_id, username, role) VALUES (UserSeq.NEXTVAL, 'admin_user', 1);
INSERT INTO Users (user_id, username, role) VALUES (UserSeq.NEXTVAL, 'regular_user', 2);

-- Вставка тестов
INSERT INTO Tests (test_id, test_name, creator) VALUES (TestSeq.NEXTVAL, 'Math Test', 1);
INSERT INTO Tests (test_id, test_name, creator) VALUES (TestSeq.NEXTVAL, 'Science Test', 1);

-- Вставка вопросов
INSERT INTO Questions (question_id, test_id, question_text, question_type) VALUES (QuestionSeq.NEXTVAL, 1, 'What is 2 + 2?', 'Multiple Choice');
INSERT INTO Questions (question_id, test_id, question_text, question_type) VALUES (QuestionSeq.NEXTVAL, 1, 'What is the capital of France?', 'Multiple Choice');
INSERT INTO Questions (question_id, test_id, question_text, question_type) VALUES (QuestionSeq.NEXTVAL, 2, 'What is the chemical formula for water?', 'Multiple Choice');

-- Вставка ответов
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (AnswerSeq.NEXTVAL, 1, '3', 0);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (AnswerSeq.NEXTVAL, 1, '4', 1);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (AnswerSeq.NEXTVAL, 2, 'Berlin', 0);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (AnswerSeq.NEXTVAL, 2, 'Paris', 1);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (AnswerSeq.NEXTVAL, 3, 'H2O', 1);
INSERT INTO Answers (answer_id, question_id, answer_text, is_correct) VALUES (AnswerSeq.NEXTVAL, 3, 'O2', 0);

-- Вставка результатов
INSERT INTO Results (result_id, user_id, test_id, score) VALUES (ResultSeq.NEXTVAL, 2, 1, 80);
INSERT INTO Results (result_id, user_id, test_id, score) VALUES (ResultSeq.NEXTVAL, 2, 2, 90);

INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 1, 80, TO_DATE('2023-11-01', 'YYYY-MM-DD'));
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 1, 85, TO_DATE('2023-11-02', 'YYYY-MM-DD'));
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 1, 90, TO_DATE('2023-11-03', 'YYYY-MM-DD'));
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 1, 70, TO_DATE('2023-11-04', 'YYYY-MM-DD'));
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 1, 2, 75, TO_DATE('2023-11-01', 'YYYY-MM-DD'));
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 1, 2, 80, TO_DATE('2023-11-02', 'YYYY-MM-DD'));
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 1, 2, 70, TO_DATE('2023-11-03', 'YYYY-MM-DD'));


INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 1, 1, 80, DATE '2024-01-01');
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 1, 1, 85, DATE '2024-01-02');
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 1, 1, 75, DATE '2024-01-03');
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 1, 70, DATE '2024-01-01');
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 1, 90, DATE '2024-01-02');
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 1, 80, DATE '2024-01-03');
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 2, 60, DATE '2024-01-01');
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 2, 90, DATE '2024-01-02');
INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 2, 2, 70, DATE '2024-01-03');
commit;

-- Вставка настроек
INSERT INTO Settings (setting_id, setting_name, setting_value) VALUES (SettingSeq.NEXTVAL, 'Max Score', '100');
INSERT INTO Settings (setting_id, setting_name, setting_value) VALUES (SettingSeq.NEXTVAL, 'Pass Score', '60');

COMMIT;