

INSERT [dbo].[Menu2] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminHr', N'AdminLeave', N'Leave', 1, N'/Modules/Hr/Job/LeaveRecord.aspx', 2)
GO
INSERT [dbo].[Menu2] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminHr', N'AdminLeaveCalenda', N'Leave Calendar', 1, N'/Modules/Hr/Job/LeaveCalendar.aspx', 3)
GO
INSERT [dbo].[Menu2] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminHr', N'AdminPayroll', N'Payroll', 1, NULL, 100)
GO
INSERT [dbo].[Menu2] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminHr', N'AdminHrReport', N'HR Report', 1, NULL, 101)
GO

INSERT [dbo].[Menu3] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminPayroll', NULL, N'Payroll Item', 1, N'/Modules/Hr/Master/PayItem.aspx', 1)
GO
INSERT [dbo].[Menu3] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminPayroll', NULL, N'Payroll Setup', 1, N'/Modules/Hr/Job/Quotation.aspx', 2)
GO
INSERT [dbo].[Menu3] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminPayroll', NULL, N'Payroll Process', 1, N'/Modules/Hr/Job/PayrollEdit.aspx', 3)
GO
INSERT [dbo].[Menu3] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminPayroll', NULL, N'Payroll Record', 1, N'/Modules/Hr/Job/PayrollRecord.aspx', 4)
GO
INSERT [dbo].[Menu3] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminHrReport', NULL, N'Empolyee Print', 1, N'/Modules/Hr/Report/PrintEmployee.aspx', 4)
GO
INSERT [dbo].[Menu3] ([MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (N'AdminHrReport', NULL, N'Payroll Print', 1, N'/Modules/Hr/Report/PrintPayroll.aspx', 4)
GO