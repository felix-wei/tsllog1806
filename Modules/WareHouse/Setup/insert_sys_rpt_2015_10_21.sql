INSERT [dbo].[sys_rpt] ([Name], [Path], [RefType], [ProcName], [IsBatch], [BatchSQL], [IsCheck], [CheckSQL], [Value1], [Value2], [RepxName1], [RepxName2], [SubPath], [SubProcName]) 
VALUES ( N'wh_TallySheet', N'/Modules/Warehouse/repx/TallySheet.repx', N'TallySheet', N'proc_PrintWh_Po', 0, N'', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)


INSERT [dbo].[sys_rpt] ([Name], [Path], [RefType], [ProcName], [IsBatch], [BatchSQL], [IsCheck], [CheckSQL], [Value1], [Value2], [RepxName1], [RepxName2], [SubPath], [SubProcName]) 
VALUES ( N'wh_StockCount', N'/Modules/Warehouse/repx/StockCountSheet.repx', N'StockCount', N'proc_PrintWh_Po', 0, N'', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

INSERT [dbo].[sys_rpt] ([Name], [Path], [RefType], [ProcName], [IsBatch], [BatchSQL], [IsCheck], [CheckSQL], [Value1], [Value2], [RepxName1], [RepxName2], [SubPath], [SubProcName]) 
VALUES ( N'wh_StockMove', N'/Modules/Warehouse/repx/StockMove.repx', N'StockMove', N'proc_PrintWh_Po', 0, N'', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)