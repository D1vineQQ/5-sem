CREATE OR REPLACE TYPE Role_obj AS OBJECT (
    role_id NUMBER,
    role_name NVARCHAR2(255),

    CONSTRUCTOR FUNCTION Role_obj(role_id NUMBER, role_name NVARCHAR2) RETURN SELF AS RESULT
);
/
CREATE OR REPLACE TYPE BODY Role_obj AS
    CONSTRUCTOR FUNCTION Role_obj(role_id NUMBER, role_name NVARCHAR2) RETURN SELF AS RESULT IS
    BEGIN
        SELF.role_id := role_id;
        SELF.role_name := role_name;
        RETURN;
    END;
END;
/

-- Объектный тип для пользователя
CREATE OR REPLACE TYPE User_obj AS OBJECT (
    user_id NUMBER,
    username NVARCHAR2(255),
    role_id NUMBER,

    CONSTRUCTOR FUNCTION User_obj(user_id NUMBER, username NVARCHAR2, role_id NUMBER) RETURN SELF AS RESULT
);
/
CREATE OR REPLACE TYPE BODY User_obj AS
    CONSTRUCTOR FUNCTION User_obj(user_id NUMBER, username NVARCHAR2, role_id NUMBER) RETURN SELF AS RESULT IS
    BEGIN
        SELF.user_id := user_id;
        SELF.username := username;
        SELF.role_id := role_id;
        RETURN;
    END;
END;
/

-- Объектный тип для теста
CREATE OR REPLACE TYPE Test_obj AS OBJECT (
    test_id NUMBER,
    test_name NVARCHAR2(255),
    creator_id NUMBER,
    date_created DATE,
    is_active NUMBER(1),

    CONSTRUCTOR FUNCTION Test_obj(test_id NUMBER, test_name NVARCHAR2, creator_id NUMBER, date_created DATE, is_active NUMBER) RETURN SELF AS RESULT
);
/
CREATE OR REPLACE TYPE BODY Test_obj AS
    CONSTRUCTOR FUNCTION Test_obj(test_id NUMBER, test_name NVARCHAR2, creator_id NUMBER, date_created DATE, is_active NUMBER) RETURN SELF AS RESULT IS
    BEGIN
        SELF.test_id := test_id;
        SELF.test_name := test_name;
        SELF.creator_id := creator_id;
        SELF.date_created := date_created;
        SELF.is_active := is_active;
        RETURN;
    END;
END;
/

-- Объектный тип для вопроса
CREATE OR REPLACE TYPE Question_obj AS OBJECT (
    question_id NUMBER,
    test_id NUMBER,
    question_text NVARCHAR2(255),
    question_type NVARCHAR2(255),

    CONSTRUCTOR FUNCTION Question_obj(question_id NUMBER, test_id NUMBER, question_text NVARCHAR2, question_type NVARCHAR2) RETURN SELF AS RESULT
);
/
CREATE OR REPLACE TYPE BODY Question_obj AS
    CONSTRUCTOR FUNCTION Question_obj(question_id NUMBER, test_id NUMBER, question_text NVARCHAR2, question_type NVARCHAR2) RETURN SELF AS RESULT IS
    BEGIN
        SELF.question_id := question_id;
        SELF.test_id := test_id;
        SELF.question_text := question_text;
        SELF.question_type := question_type;
        RETURN;
    END;
END;
/

-- Объектный тип для ответа
CREATE OR REPLACE TYPE Answer_obj AS OBJECT (
    answer_id NUMBER,
    question_id NUMBER,
    answer_text NVARCHAR2(255),
    is_correct NUMBER(1),

    CONSTRUCTOR FUNCTION Answer_obj(answer_id NUMBER, question_id NUMBER, answer_text NVARCHAR2, is_correct NUMBER) RETURN SELF AS RESULT
);
/
CREATE OR REPLACE TYPE BODY Answer_obj AS
    CONSTRUCTOR FUNCTION Answer_obj(answer_id NUMBER, question_id NUMBER, answer_text NVARCHAR2, is_correct NUMBER) RETURN SELF AS RESULT IS
    BEGIN
        SELF.answer_id := answer_id;
        SELF.question_id := question_id;
        SELF.answer_text := answer_text;
        SELF.is_correct := is_correct;
        RETURN;
    END;
END;
/

-- Объектный тип для результата теста
CREATE OR REPLACE TYPE Result_obj AS OBJECT (
    result_id NUMBER,
    user_id NUMBER,
    test_id NUMBER,
    score NUMBER,
    date_taken DATE,

    CONSTRUCTOR FUNCTION Result_obj(result_id NUMBER, user_id NUMBER, test_id NUMBER, score NUMBER, date_taken DATE) RETURN SELF AS RESULT
);
/
CREATE OR REPLACE TYPE BODY Result_obj AS
    CONSTRUCTOR FUNCTION Result_obj(result_id NUMBER, user_id NUMBER, test_id NUMBER, score NUMBER, date_taken DATE) RETURN SELF AS RESULT IS
    BEGIN
        SELF.result_id := result_id;
        SELF.user_id := user_id;
        SELF.test_id := test_id;
        SELF.score := score;
        SELF.date_taken := date_taken;
        RETURN;
    END;
END;
/

-- Объектный тип для настройки системы
CREATE OR REPLACE TYPE Setting_obj AS OBJECT (
    setting_id NUMBER,
    setting_name NVARCHAR2(255),
    setting_value NVARCHAR2(255),

    CONSTRUCTOR FUNCTION Setting_obj(setting_id NUMBER, setting_name NVARCHAR2, setting_value NVARCHAR2) RETURN SELF AS RESULT
);
/
CREATE OR REPLACE TYPE BODY Setting_obj AS
    CONSTRUCTOR FUNCTION Setting_obj(setting_id NUMBER, setting_name NVARCHAR2, setting_value NVARCHAR2) RETURN SELF AS RESULT IS
    BEGIN
        SELF.setting_id := setting_id;
        SELF.setting_name := setting_name;
        SELF.setting_value := setting_value;
        RETURN;
    END;
END;
/
DROP TABLE Settings;
DROP TABLE Results;
DROP TABLE Answers;
DROP TABLE Questions;
DROP TABLE Tests;
DROP TABLE Users;
DROP TABLE Roles;

CREATE TABLE ROLES OF Role_obj;
CREATE TABLE USERS OF User_obj;
CREATE TABLE TESTS OF Test_obj;
CREATE TABLE QUESTIONS OF Question_obj;
CREATE TABLE ANSWERS OF Answer_obj;
CREATE TABLE RESULTS OF Result_obj;
CREATE TABLE SETTINGS OF Setting_obj;


INSERT INTO ROLES VALUES (Role_obj(1, 'Admin'));
INSERT INTO ROLES VALUES (Role_obj(2, 'Teacher'));
INSERT INTO ROLES VALUES (Role_obj(3, 'Student'));

INSERT INTO USERS VALUES (User_obj(1, 'admin_user', 1));
INSERT INTO USERS VALUES (User_obj(2, 'teacher_user', 2));
INSERT INTO USERS VALUES (User_obj(3, 'student_user', 3));

INSERT INTO TESTS VALUES (Test_obj(1, 'Math Test', 2, SYSDATE, 1));

INSERT INTO QUESTIONS VALUES (Question_obj(1, 1, 'What is 2+2?', 'Single Choice'));
INSERT INTO QUESTIONS VALUES (Question_obj(2, 1, 'What is 3+3?', 'Single Choice'));

INSERT INTO ANSWERS VALUES (Answer_obj(1, 1, '4', 1));
INSERT INTO ANSWERS VALUES (Answer_obj(2, 1, '5', 0));
INSERT INTO ANSWERS VALUES (Answer_obj(3, 2, '6', 1));
INSERT INTO ANSWERS VALUES (Answer_obj(4, 2, '7', 0));

INSERT INTO RESULTS VALUES (Result_obj(1, 3, 1, 100, SYSDATE));

INSERT INTO SETTINGS VALUES (Setting_obj(1, 'Max Attempts', '3'));
INSERT INTO SETTINGS VALUES (Setting_obj(2, 'Time Limit', '60 Minutes'));



SELECT * FROM ROLES;
SELECT * FROM USERS;
SELECT * FROM TESTS;
SELECT * FROM QUESTIONS;
SELECT * FROM ANSWERS;
SELECT * FROM RESULTS;
SELECT * FROM SETTINGS;



  --2
