
Create FUNCTION dbo.fun_GetBirthday(@Interval varchar(10),@startDate DATETIME,@endDate DATETIME)
RETURNS VARCHAR
AS 
BEGIN
 DECLARE @RETURN VARCHAR(10)
 DECLARE @startDatetime DATETIME
 DECLARE @endDatetime DATETIME
 SET @startDatetime = @startDate
 SET @endDatetime = @endDate

 IF @Interval NOT IN('Y','YEAR','M','MONTH','D','DAY') OR DATEDIFF(D,@startDatetime,@endDatetime)<0
	RETURN -1

 --返回相差年份	
 IF @interval IN ('Y','YEAR')
	SET @RETURN = DATEDIFF(D,@startDatetime,@endDatetime)/365
 	 
 --返回相差年份后相差的月份	
 IF @Interval IN ('M','MONTH')
 BEGIN
	SET @startDatetime = DATEADD(YEAR,DATEDIFF(D,@startDatetime,@endDatetime)/365,@startDatetime)
	IF DAY(@startDatetime) <= DAY(@endDatetime)
		SET @RETURN = DATEDIFF(M,@startDatetime,@endDatetime)
	ELSE
		SET @RETURN = DATEDIFF(M,@startDatetime,DATEADD(M,-1,@endDatetime))
 END

 --返回相差月份后相差的天数
 IF @Interval IN ('D','DAY')
 BEGIN
	SET @startDatetime = DATEADD(YEAR,DATEDIFF(D,@startDatetime,@endDatetime)/365,@startDatetime)
	IF DAY(@startDatetime) <= DAY(@endDatetime)
		SET @startDatetime = DATEADD(M,DATEDIFF(M,@startDatetime,@endDatetime),@startDatetime)
	ELSE
		SET @startDatetime = DATEADD(M,DATEDIFF(M,@startDatetime,DATEADD(M,-1,@endDatetime)),@startDatetime)
	SET @RETURN = DATEDIFF(D,@startDatetime,@endDatetime)
 END

return @RETURN
END