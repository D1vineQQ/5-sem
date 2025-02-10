SELECT 
    user_id,
    date_taken,
    score,
    Updated_Score,
    Total_Score
FROM (
    SELECT 
        r.user_id,
        r.date_taken,
        r.score,
        r.score * 1.1 AS Updated_Score  -- Предполагаем, что успеваемость может увеличиться на 10%
    FROM 
        Results r
    WHERE 
        r.date_taken >= TRUNC(SYSDATE, 'YEAR') - INTERVAL '2' YEAR 
        AND r.date_taken < TRUNC(SYSDATE, 'YEAR') + INTERVAL '1' YEAR
)
MODEL
    PARTITION BY (user_id)
    DIMENSION BY (ROW_NUMBER() OVER (PARTITION BY user_id ORDER BY date_taken) AS rn)
    MEASURES (
        date_taken,
        score,
        Updated_Score,
        0 AS Total_Score
    )
    RULES (
        Total_Score[ANY] = Updated_Score[CV()] + NVL(Total_Score[CV() - 1], 0)
    );


INSERT INTO Results (result_id, user_id, test_id, score, date_taken) VALUES (ResultSeq.NEXTVAL, 1, 1, 100, DATE '2024-01-01');


--2

SELECT *
FROM Results
MATCH_RECOGNIZE (
    PARTITION BY test_id
    ORDER BY date_taken
    MEASURES
        A.user_id AS user_id,
        A.score AS start_score,
        B.user_id AS next_user_id,
        B.score AS end_score,
        C.user_id AS end_user_id,
        C.score AS final_score
    PATTERN (A B C)
    DEFINE
        B AS B.score > A.score,  -- Рост
        C AS C.score < B.score    -- Падение
);

commit;