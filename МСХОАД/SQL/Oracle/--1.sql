--1
-- Создание объектного типа "Сотрудник"
CREATE OR REPLACE TYPE EmployeeObj AS OBJECT (
    EmployeeID NUMBER,
    Name NVARCHAR2(90),
    BirthDate TIMESTAMP,
    Address NVARCHAR2(150),
    CONSTRUCTOR FUNCTION EmployeeObj(self IN OUT NOCOPY EmployeeObj, Name NVARCHAR2, BirthDate TIMESTAMP) RETURN SELF AS RESULT,
    MEMBER FUNCTION get_full_name RETURN NVARCHAR2,
    MAP MEMBER FUNCTION compare_employee RETURN NUMBER
);

-- Создание объектного типа "Командировка"
CREATE OR REPLACE TYPE BusinessTripObj AS OBJECT (
    BusinessTripID NUMBER,
    Bonus NUMBER,
    EmployeeID NUMBER,
    StartDate TIMESTAMP,
    EndDate TIMESTAMP,
    CONSTRUCTOR FUNCTION BusinessTripObj(
        self IN OUT NOCOPY BusinessTripObj, 
        Bonus NUMBER, 
        StartDate TIMESTAMP, 
        EndDate TIMESTAMP
    ) RETURN SELF AS RESULT,
    -- Метод для расчёта продолжительности
    MEMBER FUNCTION get_trip_duration RETURN NUMBER
);

--2
-- Тело типа EmployeeObj
CREATE OR REPLACE TYPE BODY EmployeeObj AS
    -- Дополнительный конструктор
    CONSTRUCTOR FUNCTION EmployeeObj(self IN OUT NOCOPY EmployeeObj, Name NVARCHAR2, BirthDate TIMESTAMP) RETURN SELF AS RESULT IS
    BEGIN
        self.EmployeeID := NULL; -- ID будет автогенерироваться при вставке
        self.Name := Name;
        self.BirthDate := BirthDate;
        self.Address := NULL;
        RETURN;
    END;

    -- Функция для получения полного имени
    MEMBER FUNCTION get_full_name RETURN NVARCHAR2 IS
    BEGIN
        RETURN self.Name;
    END;

    -- MAP-функция для сравнения по ID
    MAP MEMBER FUNCTION compare_employee RETURN NUMBER IS
    BEGIN
        RETURN self.EmployeeID;
    END;
END;

-- Тело типа BusinessTripObj
CREATE OR REPLACE TYPE BODY BusinessTripObj AS
    -- Конструктор
    CONSTRUCTOR FUNCTION BusinessTripObj(
        self IN OUT NOCOPY BusinessTripObj, 
        Bonus NUMBER, 
        StartDate TIMESTAMP, 
        EndDate TIMESTAMP
    ) RETURN SELF AS RESULT IS
    BEGIN
        -- Инициализация атрибутов
        self.Bonus := Bonus;
        self.StartDate := StartDate;
        self.EndDate := EndDate;
        -- Возврат SELF без выражения
        RETURN;
    END;

    -- Функция для расчета продолжительности командировки
    MEMBER FUNCTION get_trip_duration RETURN NUMBER IS
    BEGIN
        RETURN TRUNC(CAST(self.EndDate AS DATE) - CAST(self.StartDate AS DATE));
    END;
END;
--3
-- Объектная таблица для сотрудников
CREATE TABLE Employee_Obj_Table OF EmployeeObj;

-- Объектная таблица для командировок
CREATE TABLE BusinessTrip_Obj_Table OF BusinessTripObj;
--4
-- Перенос данных из Employee в объектную таблицу
INSERT INTO Employee_Obj_Table
SELECT EmployeeObj(EmployeeID, Name, BirthDate, Address) FROM Employee;

-- Перенос данных из BusinessTrip в объектную таблицу
INSERT INTO BusinessTrip_Obj_Table
SELECT BusinessTripObj(BusinessTripID, Bonus, EmployeeID, StartDate, EndDate) FROM BusinessTrip;
--5
-- Индекс по имени сотрудника
CREATE INDEX idx_employee_namee ON Employee_Obj_Table (Name);

-- Индекс по методу get_trip_duration для командировок
CREATE TABLE BusinessTrip_Relational AS
SELECT 
    BusinessTripID,
    Bonus,
    EmployeeID,
    StartDate,
    EndDate,
    TRUNC(CAST(EndDate AS DATE) - CAST(StartDate AS DATE)) AS Trip_Duration
FROM BusinessTrip_Obj_Table;

CREATE INDEX idx_trip_duration ON BusinessTrip_Relational (Trip_Duration);

--6
-- Объектное представление для сотрудников
CREATE VIEW Employee_View OF EmployeeObj
WITH OBJECT IDENTIFIER (EmployeeID) AS
SELECT * FROM Employee;

-- Объектное представление для командировок
CREATE VIEW BusinessTrip_View OF BusinessTripObj
WITH OBJECT IDENTIFIER (BusinessTripID) AS
SELECT * FROM BusinessTrip;
--7
-- Вывод данных из объектных таблиц
SELECT * FROM Employee_Obj_Table;
SELECT * FROM BusinessTrip_Obj_Table;
SELECT * FROM BusinessTrip_Relational;

-- Использование методов объектов
SELECT ser.get_full_name() AS FullName FROM Employee_Obj_Table ser;
SELECT trip.get_trip_duration() AS TripDuration FROM BusinessTrip_Obj_Table trip;