-- Тип для теста
CREATE OR REPLACE TYPE Test_obj AS OBJECT (
    test_id NUMBER,
    test_name NVARCHAR2(255),
    creator_id NUMBER,
    date_created DATE,
    is_active NUMBER(1),

    -- Конструктор
    CONSTRUCTOR FUNCTION Test_obj(test_id NUMBER, test_name NVARCHAR2, creator_id NUMBER, date_created DATE, is_active NUMBER) RETURN SELF AS RESULT,
    -- Дополнительный конструктор (с минимальным набором данных)
    CONSTRUCTOR FUNCTION Test_obj(test_id NUMBER, test_name NVARCHAR2) RETURN SELF AS RESULT,

    -- Метод сравнения (для сортировки тестов по ID)
    ORDER MEMBER FUNCTION compare_by_id(other_test Test_obj) RETURN NUMBER,

    -- Метод экземпляра (функция): возвращает строку "Название теста и статус"
    MEMBER FUNCTION get_test_status RETURN NVARCHAR2,

    -- Метод экземпляра (процедура): обновляет статус активности
    MEMBER PROCEDURE update_status(new_status NUMBER)
);
/
CREATE OR REPLACE TYPE BODY Test_obj AS
    -- Основной конструктор
    CONSTRUCTOR FUNCTION Test_obj(test_id NUMBER, test_name NVARCHAR2, creator_id NUMBER, date_created DATE, is_active NUMBER) RETURN SELF AS RESULT IS
    BEGIN
        SELF.test_id := test_id;
        SELF.test_name := test_name;
        SELF.creator_id := creator_id;
        SELF.date_created := date_created;
        SELF.is_active := is_active;
        RETURN;
    END;

    -- Дополнительный конструктор
    CONSTRUCTOR FUNCTION Test_obj(test_id NUMBER, test_name NVARCHAR2) RETURN SELF AS RESULT IS
    BEGIN
        SELF.test_id := test_id;
        SELF.test_name := test_name;
        SELF.creator_id := NULL;
        SELF.date_created := SYSDATE;
        SELF.is_active := 1;
        RETURN;
    END;

    -- Метод сравнения
    ORDER MEMBER FUNCTION compare_by_id(other_test Test_obj) RETURN NUMBER IS
    BEGIN
        IF SELF.test_id > other_test.test_id THEN
            RETURN 1;
        ELSIF SELF.test_id < other_test.test_id THEN
            RETURN -1;
        ELSE
            RETURN 0;
        END IF;
    END;

    -- Метод экземпляра (функция)
    MEMBER FUNCTION get_test_status RETURN NVARCHAR2 IS
    BEGIN
        RETURN 'Test: ' || SELF.test_name || ' (Active: ' || SELF.is_active || ')';
    END;

    -- Метод экземпляра (процедура)
    MEMBER PROCEDURE update_status(new_status NUMBER) IS
    BEGIN
        SELF.is_active := new_status;
    END;
END;
/

-- Тип для вопроса
CREATE OR REPLACE TYPE Question_obj AS OBJECT (
    question_id NUMBER,
    test_id NUMBER,
    question_text NVARCHAR2(255),
    question_type NVARCHAR2(50),

    -- Конструктор
    CONSTRUCTOR FUNCTION Question_obj(question_id NUMBER, test_id NUMBER, question_text NVARCHAR2, question_type NVARCHAR2) RETURN SELF AS RESULT,
    -- Дополнительный конструктор
    CONSTRUCTOR FUNCTION Question_obj(question_id NUMBER, test_id NUMBER, question_text NVARCHAR2) RETURN SELF AS RESULT,

    -- Метод сравнения (по длине текста вопроса)
    ORDER MEMBER FUNCTION compare_by_length(other_question Question_obj) RETURN NUMBER,

    -- Метод экземпляра (функция): возвращает категорию вопроса
    MEMBER FUNCTION get_question_category RETURN NVARCHAR2,

    -- Метод экземпляра (процедура): обновляет тип вопроса
    MEMBER PROCEDURE update_question_type(new_type NVARCHAR2)
);
/
CREATE OR REPLACE TYPE BODY Question_obj AS
    -- Основной конструктор
    CONSTRUCTOR FUNCTION Question_obj(question_id NUMBER, test_id NUMBER, question_text NVARCHAR2, question_type NVARCHAR2) RETURN SELF AS RESULT IS
    BEGIN
        SELF.question_id := question_id;
        SELF.test_id := test_id;
        SELF.question_text := question_text;
        SELF.question_type := question_type;
        RETURN;
    END;

    -- Дополнительный конструктор
    CONSTRUCTOR FUNCTION Question_obj(question_id NUMBER, test_id NUMBER, question_text NVARCHAR2) RETURN SELF AS RESULT IS
    BEGIN
        SELF.question_id := question_id;
        SELF.test_id := test_id;
        SELF.question_text := question_text;
        SELF.question_type := 'Undefined';
        RETURN;
    END;

    -- Метод сравнения
    ORDER MEMBER FUNCTION compare_by_length(other_question Question_obj) RETURN NUMBER IS
    BEGIN
        IF LENGTH(SELF.question_text) > LENGTH(other_question.question_text) THEN
            RETURN 1;
        ELSIF LENGTH(SELF.question_text) < LENGTH(other_question.question_text) THEN
            RETURN -1;
        ELSE
            RETURN 0;
        END IF;
    END;

    -- Метод экземпляра (функция)
    MEMBER FUNCTION get_question_category RETURN NVARCHAR2 IS
    BEGIN
        RETURN 'Category: ' || SELF.question_type;
    END;

    -- Метод экземпляра (процедура)
    MEMBER PROCEDURE update_question_type(new_type NVARCHAR2) IS
    BEGIN
        SELF.question_type := new_type;
    END;
END;
/

-- Таблица для тестов
CREATE TABLE Test_Table OF Test_obj;

-- Таблица для вопросов
CREATE TABLE Question_Table OF Question_obj;


-- Копируем данные из таблицы TESTS
INSERT INTO Test_Table
SELECT Test_obj(test_id, test_name, creator, date_created, is_active)
FROM Tests;

-- Копируем данные из таблицы QUESTIONS
INSERT INTO Question_Table
SELECT Question_obj(question_id, test_id, question_text, question_type)
FROM Questions;


DECLARE
    test1 Test_obj;
    test2 Test_obj;
    question1 Question_obj;
BEGIN
    -- Создание объектов с использованием конструкторов
    test1 := Test_obj(1, 'Math Test', 1, SYSDATE, 1);
    test2 := Test_obj(2, 'Physics Test');

    question1 := Question_obj(1, 1, 'What is 2+2?', 'Single Choice');

    -- Вызов метода экземпляра (функция)
    DBMS_OUTPUT.PUT_LINE(test1.get_test_status());
    DBMS_OUTPUT.PUT_LINE(question1.get_question_category());

    -- Вызов метода экземпляра (процедура)
    test1.update_status(0);
    question1.update_question_type('Multiple Choice');

    -- После обновления
    DBMS_OUTPUT.PUT_LINE(test1.get_test_status());
    DBMS_OUTPUT.PUT_LINE(question1.get_question_category());

    -- Сравнение объектов
    IF test1 > test2 THEN
        DBMS_OUTPUT.PUT_LINE('Test1 has a greater ID than Test2');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Test1 has a smaller or equal ID compared to Test2');
    END IF;
END;
/