
/****** Object:  Table [dbo].[Hr_Task]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Task]
GO
/****** Object:  Table [dbo].[Hr_Recruitment]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Recruitment]
GO
/****** Object:  Table [dbo].[Hr_Rate]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Rate]
GO
/****** Object:  Table [dbo].[Hr_Quote]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Quote]
GO
/****** Object:  Table [dbo].[Hr_Process]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Process]
GO
/****** Object:  Table [dbo].[Hr_PersonTrans]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonTrans]
GO
/****** Object:  Table [dbo].[Hr_PersonNews]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonNews]
GO
/****** Object:  Table [dbo].[Hr_PersonLog]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonLog]
GO
/****** Object:  Table [dbo].[Hr_PersonDet6]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonDet6]
GO
/****** Object:  Table [dbo].[Hr_PersonDet5]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonDet5]
GO
/****** Object:  Table [dbo].[Hr_PersonDet4]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonDet4]
GO
/****** Object:  Table [dbo].[Hr_PersonDet3]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonDet3]
GO
/****** Object:  Table [dbo].[Hr_PersonDet2]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonDet2]
GO
/****** Object:  Table [dbo].[Hr_PersonDet1]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonDet1]
GO
/****** Object:  Table [dbo].[Hr_PersonComment]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PersonComment]
GO
/****** Object:  Table [dbo].[Hr_Person]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Person]
GO
/****** Object:  Table [dbo].[Hr_PayrollDet]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PayrollDet]
GO
/****** Object:  Table [dbo].[Hr_Payroll]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Payroll]
GO
/****** Object:  Table [dbo].[Hr_PayItem]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_PayItem]
GO
/****** Object:  Table [dbo].[Hr_MastData]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_MastData]
GO
/****** Object:  Table [dbo].[Hr_LeaveTmp]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_LeaveTmp]
GO
/****** Object:  Table [dbo].[Hr_Leave]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Leave]
GO
/****** Object:  Table [dbo].[Hr_Interview]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Interview]
GO
/****** Object:  Table [dbo].[Hr_File]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_File]
GO
/****** Object:  Table [dbo].[Hr_cpf]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_cpf]
GO
/****** Object:  Table [dbo].[Hr_Contract]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Contract]
GO
/****** Object:  Table [dbo].[Hr_Attachment]    Script Date: 2015/9/28 9:14:12 ******/
DROP TABLE [dbo].[Hr_Attachment]
GO
/****** Object:  Table [dbo].[Hr_Attachment]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[FileType] [nvarchar](10) NULL,
	[FileName] [nvarchar](50) NULL,
	[FilePath] [nvarchar](50) NULL,
	[FileNote] [nvarchar](50) NULL,
	[FileDate] [datetime] NULL,
	[FileSize] [nvarchar](20) NULL,
	[FileStatus] [nvarchar](50) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Contract]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Contract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[No] [nvarchar](20) NULL,
	[Date] [date] NULL,
	[Person] [int] NULL,
	[Pic] [int] NULL,
	[Remark] [nvarchar](500) NULL,
	[Remark1] [nvarchar](500) NULL,
	[Remark2] [nvarchar](500) NULL,
	[Remark3] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
	[StatusCode] [nvarchar](20) NULL,
 CONSTRAINT [PK_Hr_Contract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_cpf]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_cpf](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[CPF1] [decimal](18, 2) NULL,
	[CPF2] [decimal](18, 2) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_cpf] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_File]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_File](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](100) NULL,
	[Type] [nvarchar](50) NULL,
	[Size] [nvarchar](50) NULL,
	[Path] [nvarchar](100) NULL,
	[ByWho] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[DocType] [nvarchar](50) NULL,
	[DocNo] [nvarchar](50) NULL,
	[CategoryCode] [nvarchar](50) NULL,
	[FolderCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Hr_File] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Interview]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Interview](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[Pic] [int] NULL,
	[Department] [nvarchar](100) NULL,
	[Remark] [nvarchar](500) NULL,
	[StatusCode] [nvarchar](20) NULL,
	[Remark1] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_Interview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Leave]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Leave](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[Date1] [date] NULL,
	[Time1] [nvarchar](20) NULL,
	[Date2] [date] NULL,
	[Time2] [nvarchar](20) NULL,
	[Days] [nvarchar](20) NULL,
	[Remark] [nvarchar](500) NULL,
	[ApplyDateTime] [datetime] NULL,
	[ApproveBy] [int] NULL,
	[ApproveStatus] [nvarchar](10) NULL,
	[ApproveDate] [datetime] NULL,
	[ApproveRemark] [nvarchar](500) NULL,
	[ApproveTime] [nvarchar](10) NULL,
	[LeaveType] [nvarchar](500) NULL,
 CONSTRAINT [PK_Hr_leave] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_LeaveTmp]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_LeaveTmp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[Year] [nvarchar](50) NULL,
	[LeaveType] [nvarchar](50) NULL,
	[Days] [int] NULL,
	[Remark] [nvarchar](500) NULL,
 CONSTRAINT [PK_Hr_LeaveTmp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_MastData]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_MastData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[Type] [nvarchar](20) NULL,
 CONSTRAINT [PK_Hr_MastData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PayItem]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PayItem](
	[Code] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Hr_PayItem] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Payroll]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Payroll](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[Person] [int] NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[Amt] [decimal](10, 2) NULL,
	[Remark] [nvarchar](500) NULL,
	[Term] [nvarchar](50) NULL,
	[Pic] [nvarchar](100) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
	[StatusCode] [nvarchar](20) NULL,
 CONSTRAINT [PK_Hr_Payroll] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PayrollDet]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PayrollDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayrollId] [int] NULL,
	[ChgCode] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[Amt] [decimal](10, 2) NULL,
	[Pic] [nvarchar](100) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
	[Before] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Hr_PayrollDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Person]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[BirthDay] [datetime] NULL,
	[IcNo] [nvarchar](50) NULL,
	[Gender] [nvarchar](10) NULL,
	[Country] [nvarchar](50) NULL,
	[Race] [nvarchar](100) NULL,
	[Religion] [nvarchar](100) NULL,
	[Married] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Telephone] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Remark] [nvarchar](500) NULL,
	[Remark1] [nvarchar](500) NULL,
	[Remark2] [nvarchar](500) NULL,
	[Remark3] [nvarchar](500) NULL,
	[Remark4] [nvarchar](500) NULL,
	[Status] [nvarchar](50) NULL,
	[Department] [nvarchar](50) NULL,
	[HrRole] [nvarchar](100) NULL,
	[RecruitId] [nvarchar](50) NULL,
	[InterviewId] [nvarchar](50) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
	[StatusCode] [nvarchar](10) NULL,
	[Remark5] [nvarchar](500) NULL,
	[HrGroup] [nvarchar](50) NULL,
	[PassType] [nvarchar](20) NULL,
	[PassNo] [nvarchar](20) NULL,
	[Dialect] [nvarchar](50) NULL,
	[LanguageWrittenRemark] [nvarchar](500) NULL,
	[LanguageSpokenRemark] [nvarchar](500) NULL,
	[NationalServiceStatus] [nvarchar](20) NULL,
	[NationalServiceRemark] [nvarchar](500) NULL,
	[EmploySource] [nvarchar](20) NULL,
	[EmploySourceRemark] [nvarchar](500) NULL,
	[DrivingType] [nvarchar](20) NULL,
	[DrivingRemark] [nvarchar](500) NULL,
	[HealthStatus] [nvarchar](20) NULL,
	[HealthRemark] [nvarchar](500) NULL,
	[OverTimeStatus] [nvarchar](10) NULL,
	[OverTimeRemark] [nvarchar](500) NULL,
	[RelativeWorkingStatus] [nvarchar](10) NULL,
	[RelativeWorkingRemark] [nvarchar](500) NULL,
	[TerminateStatus] [nvarchar](10) NULL,
	[TerminateRemark] [nvarchar](500) NULL,
	[CheckReferenceStatus] [nvarchar](10) NULL,
	[CheckReferenceRemark] [nvarchar](500) NULL,
	[EmploymentType] [nvarchar](20) NULL,
	[SalaryPaymode] [nvarchar](20) NULL,
	[HoursPerDay] [int] NULL,
	[HoursPerWeek] [int] NULL,
	[HoursPerMonth] [int] NULL,
	[DaysPerWeek] [int] NULL,
	[DaysPerMonth] [int] NULL,
	[ProbationFromDate] [date] NULL,
	[ProbationToDate] [date] NULL,
	[StartSalary] [decimal](10, 2) NULL,
	[PRInd] [nvarchar](10) NULL,
	[PRYear] [int] NULL,
	[Wage] [decimal](18, 2) NULL,
	[Allowance] [decimal](18, 2) NULL,
	[Overtime] [decimal](18, 2) NULL,
	[Amount1] [decimal](18, 2) NULL,
	[Amount2] [decimal](18, 2) NULL,
	[Amount3] [decimal](18, 2) NULL,
	[Amount4] [decimal](18, 2) NULL,
	[Amount5] [decimal](18, 2) NULL,
	[Date1] [datetime] NULL,
	[Date2] [datetime] NULL,
	[Date3] [datetime] NULL,
	[Date4] [datetime] NULL,
	[Date5] [datetime] NULL,
	[Date6] [datetime] NULL,
	[Date7] [datetime] NULL,
	[Date8] [datetime] NULL,
	[Status1] [nvarchar](20) NULL,
	[Status2] [nvarchar](20) NULL,
	[Status3] [nvarchar](20) NULL,
	[Status4] [nvarchar](20) NULL,
	[Status5] [nvarchar](20) NULL,
	[Status6] [nvarchar](20) NULL,
	[Status7] [nvarchar](20) NULL,
	[Status8] [nvarchar](20) NULL,
	[IsCPF] [nvarchar](20) NULL,
 CONSTRAINT [PK_Hr_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonComment]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonComment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[Date] [date] NULL,
	[Status] [nvarchar](20) NULL,
	[Manager] [nvarchar](100) NULL,
	[Rating] [nvarchar](100) NULL,
	[Remark] [nvarchar](500) NULL,
	[Remark1] [nvarchar](500) NULL,
	[Remark2] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_PersonComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonDet1]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonDet1](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[BeginDate] [date] NULL,
	[ResignDate] [date] NULL,
	[Salary] [decimal](18, 2) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
	[StartSalary] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Hr_PersonDet1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonDet2]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonDet2](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[ContactName] [nvarchar](100) NULL,
	[Gender] [nvarchar](10) NULL,
	[Dob] [datetime] NULL,
	[Phone] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Relationship] [nvarchar](50) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_PersonDet2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonDet3]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonDet3](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[BankCode] [nvarchar](50) NULL,
	[BankName] [nvarchar](100) NULL,
	[BranchCode] [nvarchar](50) NULL,
	[SwiftCode] [nvarchar](50) NULL,
	[AccNo] [nvarchar](50) NULL,
	[IsPayroll] [bit] NULL,
	[Remark] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_PersonDet3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonDet4]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonDet4](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[SchoolName] [nvarchar](100) NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[HighestLevel] [nvarchar](100) NULL,
	[Status] [nvarchar](20) NULL,
	[SchoolYear] [int] NULL,
	[Remark] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_PersonDet4] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonDet5]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonDet5](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[EmployerName] [nvarchar](100) NULL,
	[PositionHold] [nvarchar](100) NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[Salary] [decimal](10, 2) NULL,
	[Allowance] [nvarchar](100) NULL,
	[LeaveStatus] [nvarchar](20) NULL,
	[ReasonForLeaving] [nvarchar](200) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_PersonDet5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonDet6]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonDet6](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Relationship] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[Occupation] [nvarchar](50) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_PersonDet6] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonLog]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[LogDate] [datetime] NULL,
	[LogTime] [nvarchar](20) NULL,
	[Status] [nvarchar](20) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_PersonLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonNews]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Note] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[UpdateDateTime] [datetime] NULL,
	[ExpireDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_PersonNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_PersonTrans]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_PersonTrans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](20) NULL,
	[Person] [int] NULL,
	[Date1] [date] NULL,
	[Time1] [nvarchar](20) NULL,
	[Date2] [date] NULL,
	[Time2] [nvarchar](20) NULL,
	[Hrs] [nvarchar](100) NULL,
	[Amt] [decimal](18, 2) NULL,
	[Pic] [int] NULL,
	[Remark] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
	[StatusCode] [nvarchar](20) NULL,
 CONSTRAINT [PK_Hr_PersonTrans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Process]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Process](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocType] [nvarchar](50) NULL,
	[DocNo] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[ByWho] [nvarchar](50) NULL,
	[DateTime] [datetime] NULL,
	[Remark] [nvarchar](500) NULL,
 CONSTRAINT [PK_Hr_Process] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Quote]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Quote](
	[Person] [int] NULL,
	[PayItem] [nvarchar](50) NULL,
	[Amt] [decimal](10, 2) NULL,
	[Remark] [nvarchar](500) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_Quote] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Rate]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Rate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayItem] [nvarchar](200) NULL,
	[Rate] [decimal](10, 2) NULL,
	[EmployeeRate] [decimal](10, 2) NULL,
	[EmployerRate] [decimal](10, 2) NULL,
	[Remark] [nvarchar](500) NULL,
	[FromDate] [datetime] NULL,
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
/****** Object:  Table [dbo].[Hr_Recruitment]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Recruitment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[No] [nvarchar](20) NULL,
	[Date] [date] NULL,
	[Department] [nvarchar](100) NULL,
	[StatusCode] [nvarchar](20) NULL,
	[Pic] [int] NULL,
	[Remark1] [nvarchar](500) NULL,
	[Remark2] [nvarchar](500) NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](20) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_Recruitment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hr_Task]    Script Date: 2015/9/28 9:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hr_Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person] [int] NULL,
	[Date] [date] NULL,
	[Time] [nvarchar](50) NULL,
	[RefNo] [nvarchar](20) NULL,
	[Remark] [nvarchar](500) NULL,
	[Status] [nvarchar](50) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[UpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hr_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
