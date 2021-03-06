drop table Hr_Rate
CREATE TABLE [dbo].[Hr_Rate](
    [Id] [int] IDENTITY(1,1) NOT NULL,
	[PayItem] [nvarchar](200) NULL,
	[Rate] [decimal](10, 2) NULL,
	[Age][nvarchar](200) NULL,
	[RateType][nvarchar](200) NULL,
	[EmployeeRate] [decimal](10, 2) NULL,
	[EmployerRate] [decimal](10, 2) NULL,
	[Remark] [nvarchar](500) NULL,
	[FromDate][datetime] NULL,
	[ToDate] [datetime] NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_Rate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


alter table hr_person
add IsCPF [nvarchar](20) NULL