
Create Function [dbo].[fun_GetDate](@date datetime)
returns nvarchar(100)
as 
begin
declare @m nvarchar(100)
declare @month nvarchar(100)
declare @rValue nvarchar(100)
declare @newDate datetime
set @newDate=CONVERT(varchar(100), @date, 23)
set @m=month(@newDate)
 if(@newDate<'1900-01-01')
   begin
	  set @rValue=''
	end
else if(@m='1')
    begin
	  set @month='Jan'
	  set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
 else if(@m='2')
    begin
	  set @month='Feb'
	  set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
 else if(@m='3')
    begin
	  set @month='Mar'
	  set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
 else if(@m='4')
    begin
	  set @month='Apr'
	  set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
else if(@m='5')
    begin
	  set @month='May'
	  set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
else if(@m='6')
    begin
	  set @month='Jun'
	  set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
else if(@m='7')
    begin
		set @month='Jul'
		set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@date)) 
	end
else if(@m='8')
    begin
	   set @month='Aug'
	   set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
else if(@m='9')
    begin
       set @month='Sep'
	   set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
else if(@m='10')
    begin
	   set @month='Oct'
	   set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
else if(@m='11')
    begin
	   set @month='Nov'
	   set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end
 else if(@m='12')
    begin
      set @month='Dec'
	  set @rValue=CONVERT(varchar(100),day(@newDate))+'/'+ @month+'/'+CONVERT(varchar(100),YEAR(@newDate)) 
	end

 return @rValue
end