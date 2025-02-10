WITH DateIntervals AS (
    SELECT FORMAT(r.date_taken, 'yyyy-MM') AS MonthYear
    FROM Results r
    GROUP BY FORMAT(r.date_taken, 'yyyy-MM')
    UNION
    SELECT FORMAT(r.date_taken, 'yyyy') AS Year
    FROM Results r
    GROUP BY FORMAT(r.date_taken, 'yyyy')
)
SELECT
    di.MonthYear AS Period,
    COALESCE(AVG(r.score), 0) AS AverageScore
FROM DateIntervals di
LEFT JOIN Results r ON (
    FORMAT(r.date_taken, 'yyyy-MM') = di.MonthYear OR
    FORMAT(r.date_taken, 'yyyy') = di.MonthYear
)
GROUP BY di.MonthYear
ORDER BY di.MonthYear;



--3

WITH StudentScores AS (
    SELECT
        r.user_id,
        r.test_id,
        AVG(r.score) AS AverageScore
    FROM
        Results r
    WHERE
        r.date_taken >= '2023-10-05' 
        AND r.date_taken <= '2024-12-05'
    GROUP BY
        r.user_id, r.test_id
),
TestAverages AS (
    SELECT
        r.test_id,
        AVG(r.score) AS TestAverage
    FROM
        Results r
    WHERE
        r.date_taken >= '2023-10-05' 
        AND r.date_taken <= '2024-12-05'
    GROUP BY
        r.test_id
),
MaxScores AS (
    SELECT
        r.test_id,
        MAX(r.score) AS MaxScore
    FROM
        Results r
    WHERE
        r.date_taken >= '2023-10-05' 
        AND r.date_taken <= '2024-12-05'
    GROUP BY
        r.test_id
)
SELECT
    ss.user_id,
    ss.test_id,
    ss.AverageScore AS StudentAverageScore,
    ta.TestAverage AS TestAverageScore,
    (ss.AverageScore / ta.TestAverage) * 100 AS PercentageOfTestAverage,
    ms.MaxScore AS MaxTestScore,
    (ss.AverageScore / ms.MaxScore) * 100 AS PercentageOfMaxScore
FROM
    StudentScores ss
JOIN
    TestAverages ta ON ss.test_id = ta.test_id
JOIN
    MaxScores ms ON ss.test_id = ms.test_id;


--4

WITH OrderedResults AS (
    SELECT
        r.result_id,
        r.user_id,
        r.test_id,
        r.score,
        r.date_taken,
        ROW_NUMBER() OVER (ORDER BY r.result_id) AS RowNum
    FROM
        Results r
)
SELECT
    result_id,
    user_id,
    test_id,
    score,
    date_taken
FROM
    OrderedResults
WHERE
    RowNum BETWEEN 1 AND 10; -- Первая страница, 10 строк


--5
INSERT INTO Tests (test_id, test_name, creator) VALUES (NEXT VALUE FOR TestSeq, 'Science Test', 1);

SELECT * FROM TESTS;

WITH DeduplicatedTests AS (
    SELECT
        test_id,
        test_name,
        creator,
        ROW_NUMBER() OVER (PARTITION BY test_name ORDER BY test_id) AS RowNum
    FROM
        Tests
)

DELETE FROM Tests
WHERE test_id IN (
    SELECT test_id
    FROM DeduplicatedTests
    WHERE RowNum > 1
);


--6

WITH UserTestResults AS (
    SELECT
        u.user_id AS UserID,
        u.username AS Username,
        t.test_name AS TestName,
        YEAR(r.date_taken) AS ResultYear,
        MONTH(r.date_taken) AS ResultMonth,
        SUM(r.score) AS TotalScore
    FROM Users u
    JOIN Results r ON u.user_id = r.user_id
    JOIN Tests t ON r.test_id = t.test_id
    WHERE r.date_taken >= DATEADD(MONTH, -5, GETDATE()) -- Последние 6 месяцев
    GROUP BY u.user_id, u.username, t.test_name, YEAR(r.date_taken), MONTH(r.date_taken)
)
SELECT
    UserID,
    Username,
    TestName,
    ResultYear AS Year,
    ResultMonth AS Month,
    ISNULL(TotalScore, 0) AS TotalScore
FROM UserTestResults
ORDER BY UserID, ResultYear, ResultMonth, TestName;

--7

select * from results;

WITH TestCounts AS (
	SELECT
		T.test_name,
		R.user_id,
		COUNT(*) AS TestCount,
		RANK() OVER (PARTITION BY T.test_id ORDER BY COUNT(*) DESC) AS Rank
	FROM
		Results R
	JOIN Tests T ON R.test_id = T.test_id
	GROUP BY
		T.test_id,
		T.test_name,
		R.user_id
)

SELECT
	TC.test_name,
	TC.user_id,
	TC.TestCount
FROM
	TestCounts TC
WHERE
	TC.Rank = 1
ORDER BY
	TC.test_name;

select * from tests;