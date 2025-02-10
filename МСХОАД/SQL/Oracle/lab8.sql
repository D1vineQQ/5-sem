--2
-- Tип "Тест"
CREATE OR REPLACE TYPE TestObj AS OBJECT (
    TestID NUMBER,
    TestName NVARCHAR2(255),
    CreatorID NUMBER,
    DateCreated DATE,
    IsActive NUMBER(1),
    CONSTRUCTOR FUNCTION TestObj(
        self IN OUT NOCOPY TestObj,
        TestName NVARCHAR2,
        CreatorID NUMBER,
        IsActive NUMBER
    ) RETURN SELF AS RESULT,
    MEMBER FUNCTION get_full_name RETURN NVARCHAR2,
    MAP MEMBER FUNCTION test_is_active RETURN NUMBER
);

-- Тип "Ответ"
CREATE OR REPLACE TYPE AnswerObj AS OBJECT (    
    AnswerID NUMBER,
    QuestionID NUMBER,
    AnswerText NVARCHAR2(255),
    IsCorrect NUMBER(1),
    CONSTRUCTOR FUNCTION AnswerObj(
        self IN OUT NOCOPY AnswerObj,
        QuestionID NUMBER,
        AnswerText NVARCHAR2,
        IsCorrect NUMBER
    ) RETURN SELF AS RESULT,
    MEMBER FUNCTION get_answer_summary RETURN NVARCHAR2,
    MEMBER FUNCTION is_correct_answer RETURN NVARCHAR2
);


--------------------------------------------------------

-- Тело типа "Тест"
CREATE OR REPLACE TYPE BODY TestObj AS

    CONSTRUCTOR FUNCTION TestObj(
        self IN OUT NOCOPY TestObj,
        TestName NVARCHAR2,
        CreatorID NUMBER,
        IsActive NUMBER
    ) RETURN SELF AS RESULT IS
    BEGIN
        self.TestID := NULL; -- Предполагается, что существует последовательность TestID_seq
        self.TestName := TestName;
        self.CreatorID := CreatorID;
        self.DateCreated := SYSDATE;
        self.IsActive := IsActive;
        RETURN;
    END;

    MEMBER FUNCTION get_full_name RETURN NVARCHAR2 IS
    BEGIN
        RETURN 'Test: ' || self.TestName;
    END;

    MAP MEMBER FUNCTION test_is_active RETURN NUMBER IS
    BEGIN
        IF self.IsActive = 1 THEN
            RETURN 1; -- Активный тест
        ELSE
            RETURN 0; -- Неактивный тест
        END IF;
    END;

END;

-- Тело типа "Ответ"
CREATE OR REPLACE TYPE BODY AnswerObj AS
    CONSTRUCTOR FUNCTION AnswerObj(
        self IN OUT NOCOPY AnswerObj,
        QuestionID NUMBER,
        AnswerText NVARCHAR2,
        IsCorrect NUMBER
    ) RETURN SELF AS RESULT IS
    BEGIN
        self.AnswerID := NULL;
        self.QuestionID := QuestionID;
        self.AnswerText := AnswerText;
        self.IsCorrect := IsCorrect;
        RETURN;
    END;

    MEMBER FUNCTION get_answer_summary RETURN NVARCHAR2 IS
    BEGIN
        RETURN 'Answer: ' || self.AnswerText || ', Correct: ' || CASE 
            WHEN self.IsCorrect = 1 THEN 'Yes' 
            ELSE 'No' 
        END;
    END;

    MEMBER FUNCTION is_correct_answer RETURN NVARCHAR2 IS
    BEGIN
        IF self.IsCorrect = 1 THEN
            RETURN 'This answer is correct.';
        ELSE
            RETURN 'This answer is incorrect.';
        END IF;
    END;

END;


--3
-- Объектная таблица для тестов
CREATE TABLE Test_Obj_Table OF TestObj;

-- Объектная таблица для ответов
CREATE TABLE Answer_Obj_Table OF AnswerObj;


--DROP TABLE Test_Obj_Table;
--DROP TABLE Answer_Obj_Table;

INSERT INTO Test_Obj_Table
SELECT TestObj(test_id, test_name, creator, date_created, is_active) FROM Tests;

INSERT INTO Answer_Obj_Table
SELECT AnswerObj(answer_id, question_id, answer_text, is_correct) FROM Answers;

SELECT * from Test_Obj_Table;
SELECT * from Answer_Obj_Table;




CREATE VIEW TestObjView OF TestObj
WITH OBJECT IDENTIFIER (TestID) AS
SELECT * FROM Test_Obj_Table;

CREATE VIEW AnswerObjView OF AnswerObj
WITH OBJECT IDENTIFIER (AnswerID) AS
SELECT * FROM Answer_Obj_Table;




CREATE INDEX idx_testObj_name ON Test_Obj_Table (TestName);

CREATE BITMAP INDEX bmp_idx_test_active ON Test_Obj_Table(IsActive);

CREATE BITMAP INDEX bmp_idx_answer_correct ON Answer_Obj_Table(IsCorrect);




DECLARE
  test1 TestObj := TestObj('Test 1', 1, 1); 
  test2 TestObj := TestObj('Test 2', 2, 0);
  answer1 AnswerObj := AnswerObj(1, 1, 'Answer A', 1);
  answer2 AnswerObj := AnswerObj(2, 1, 'Answer B', 0);
BEGIN
  DBMS_OUTPUT.PUT_LINE('Test 1 full name: ' || test1.get_full_name());
  DBMS_OUTPUT.PUT_LINE('Test 2 full name: ' || test2.get_full_name());
  DBMS_OUTPUT.PUT_LINE('Test 1 active status: ' || test1.test_is_active());
  DBMS_OUTPUT.PUT_LINE('Test 2 active status: ' || test2.test_is_active());

  DBMS_OUTPUT.PUT_LINE('Answer 1 summary: ' || answer1.get_answer_summary());
  DBMS_OUTPUT.PUT_LINE('Answer 2 summary: ' || answer2.get_answer_summary());
  DBMS_OUTPUT.PUT_LINE('Answer 1 correctness: ' || answer1.is_correct_answer());
  DBMS_OUTPUT.PUT_LINE('Answer 2 correctness: ' || answer2.is_correct_answer());
END;