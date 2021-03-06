
INSERT [dbo].[Menu1] ([MasterId], [Name], [IsActive], [SortIndex], [RoleName]) VALUES (N'AdminHr', N'Human Resource', 1, 2, N'Hr')
GO



INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3350, N'AdminHr', N'', N'DashBoard', 1, N'/PagesHr/DashBoard.aspx', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3351, N'AdminHr', N'HrMastData', N'HR Setup', 1, NULL, 99)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3352, N'AdminHr', N'', N'Recruitment', 1, N'/PagesHr/Job/Recruitment.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3353, N'AdminHr', N'', N'InterView', 1, N'/PagesHr/Job/Interview.aspx', 3)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3354, N'AdminHr', N'', N'Contracts', 1, N'/PagesHr/Job/Contract.aspx', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3355, N'AdminHr', N'', N'Overtime', 1, N'/PagesHr/Job/Transhipment.aspx?type=OT', 5)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3356, N'AdminHr', N'', N'Expense', 1, N'/PagesHr/Job/Transhipment.aspx?type=Expense', 6)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3357, N'AdminHr', N'', N'Appraisal', 1, N'/PagesHr/Job/Appraisal.aspx', 7)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3358, N'AdminHr', N'HrPayroll', N'Payroll', 1, N'', 8)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3359, N'AdminHr', N'HrReport', N'Report', 1, N'', 9)
GO

INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3426, N'AdminHr', N'AdminEmployee', N'Employee Data', 1, N'/Modules/Hr/Master/Person.aspx?type=Employee', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3427, N'AdminHr', N'AdminCrewDaily', N'Crew Payment', 0, N'/PagesHr/Job/Crews.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3428, N'AdminHr', N'AdminHrVoucher', N'Hr Voucher Print', 0, N'/PagesHr/Job/UnHrPayment.aspx', 3)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3429, N'AdminHr', N'AdminHrVoucherList', N'Hr Voucher', 0, N'/pagesAccount/ApPayment.aspx?type=HR', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8568, N'AdminHr', N'', N'DashBoard', 1, N'/PagesHr/DashBoard.aspx', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8569, N'AdminHr', N'HrMastData', N'Mast Data', 1, N'', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8570, N'AdminHr', N'', N'Recruitment', 1, N'/PagesHr/Job/Recruitment.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8571, N'AdminHr', N'', N'InterView', 1, N'/PagesHr/Job/Interview.aspx', 3)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8572, N'AdminHr', N'', N'Contracts', 1, N'/PagesHr/Job/Contract.aspx', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8573, N'AdminHr', N'', N'Overtime', 1, N'/PagesHr/Job/Transhipment.aspx?type=OT', 5)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8574, N'AdminHr', N'', N'Expense', 1, N'/PagesHr/Job/Transhipment.aspx?type=Expense', 6)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8575, N'AdminHr', N'', N'Appraisal', 1, N'/PagesHr/Job/Appraisal.aspx', 7)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8576, N'AdminHr', N'HrPayroll', N'Payroll', 1, N'', 8)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8577, N'AdminHr', N'HrReport', N'Report', 1, N'', 9)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8578, N'Admin16', N'AdminWmsBilling', N'Billing', 0, NULL, 8)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8579, N'AdminWmsOrder', N'AdminWmsOrders', N'Orders', 0, NULL, 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8580, N'AdminWmsOrder', N'AdminWmsParty', N'Customer List', 1, N'/PagesMaster/Party.aspx?type=C', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8581, N'Admin7', N'AdminWhQuote', N'STD Rate', 1, N'/PagesQuote/wh/Rate_Std.aspx', 8)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8582, N'Admin5', NULL, N'Doctor', 0, N'/PagesMaster/PersonInfo.aspx?type=D', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8583, N'AdminWmsOrder', N'AdminOrderSales', N'Sales Order - SO', 1, N'/Warehouse/Job/SoList.aspx', 3)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8584, N'AdminWmsOrder', N'AdminOrderPrice', N'Sales Quotation - RFQ', 1, N'/Warehouse/MastData/SellingPrice.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8585, N'AdminWmsOrder', N'AdminOrderDelivery', N'Delivery Order - DO', 1, N'/Warehouse/Job/DoOutList.aspx', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8586, N'AdminWmsOrder', N'AdminOrderBill', N'Sales Invoice', 1, N'/PagesAccount/ArInvoice.aspx', 5)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8587, N'AdminWmsOrder', N'AdminOrderPay', N'Receive Payment', 1, N'/PagesAccount/ArReceipt.aspx', 6)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8588, N'AdminPurchase', N'AdminPurchaseParty', N'Supplier', 1, N'/PagesMaster/Party.aspx?type=V', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8589, N'AdminPurchase', N'AdminPurchasePrice', N'Buying Price', 1, N'/Warehouse/MastData/PurchasePrice.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8590, N'AdminPurchase', N'AdminPurchaseOrder', N'Purchase Order - PO', 1, N'/Warehouse/Job/PoList.aspx', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8591, N'AdminPurchase', N'AdminPurchaseReceive', N'Goods Receive - GRN', 1, N'/Warehouse/Job/DoInList.aspx', 5)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8592, N'AdminPurchase', N'AdminPurchaseBill', N'Make Payment', 1, N'/PagesAccount/ApPayment.aspx', 7)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8593, N'AdminPurchase', N'AdminPuchaseReport', N'Report', 1, NULL, 8)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8594, N'AdminWmsOrder', N'AdminSalesReport', N'Report', 1, NULL, 7)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8595, N'AdminPurchase', N'AdminPuchaseRequest', N'Purchase Request - PR', 1, N'/Warehouse/Job/PoRequest.aspx', 3)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8596, N'AdminPurchase', N'AdminPuchaseInvoice', N'Purchase Invoice', 1, N'/PagesAccount/ApPayable.aspx', 6)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8597, N'Admin5', NULL, N'Patient', 0, N'/PagesMaster/PersonInfo.aspx?type=P', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8598, N'Admin5', NULL, N'Operations Theater', 0, N'/PagesMaster/PersonInfo.aspx?type=O', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8599, N'Admin5', NULL, N'Hospital', 0, N'/PagesMaster/Party.aspx?type=C&group=HOSPITAL', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8601, N'Admin5', NULL, N'Supplier', 1, N'/PagesMaster/Party.aspx?type=V', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8602, N'Admin5', NULL, N'Transporter', 1, N'/PagesMaster/Party.aspx?type=V&group=TRANSPORT', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8603, N'AdminOrder', N'ScheduleOrder', N'Schedule', 1, N'/WareHouse/Job/Schedule.aspx', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8604, N'AdminOrder', N'Replenishment', N'Replenishment', 1, N'/WareHouse/Job/Replenishment.aspx', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8605, N'AdminOrder', N'TransferHistory', N'Transfer History', 1, N'/WareHouse/Job/TransferHistory.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8606, N'AdminOpsDirect', N'OpsDirectJob', N'Job Search', 1, N'/Z/job_list.aspx', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8607, N'AdminOpsDirect', N'OpsDirectBill', N'Bill Search', 1, N'/Z/bill_list.aspx', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8608, N'AdminOpsDirect', N'OpsDirectRate', N'Rate Search', 1, N'/Z/rate_list.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8609, N'AdminOpsDirect', N'OpsDirectPay', N'Pay Search', 1, N'/Z/pay_list.aspx', 3)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8610, N'AdminOpsDirect', N'OpsDirectJnl', N'Jnl Search', 1, N'/Z/jnl_list.aspx', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8611, N'Admin6', NULL, N'Page Log', 1, N'/PagesLog/Log_LoadPages.aspx', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8612, N'AdminBlanketOrder', N'BlanketSO', N'Blanket Sales Order', 1, N'/WareHouse/Job/BlanketSOList.aspx', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8613, N'AdminBlanketOrder', N'BlanketPO', N'Blanket Purchase Order', 1, N'/WareHouse/Job/BlanketSOList.aspx', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8614, N'Admin6', NULL, N'Page Authority', 1, N'/PagesMaster/control/HelperAuthority.aspx', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8615, N'AdminJobManager', N'NewJob', N'New Job', 0, N'/WareHouse/Job/JobEdit.aspx?no=0', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8619, N'AdminWhs', N'StoringOrder', N'Storing Order', 0, N'/WareHouse/Job/DoInList.aspx', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8620, N'AdminWhs', N'ReleaseOrder', N'Release Order', 0, N'/WareHouse/Job/DoOutList.aspx', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8621, N'AdminWhs', N'WarehouseSchedule', N'Warehouse Schedule', 0, N'/WareHouse/MastData/WareHouse.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8622, N'AdminWhs', N'StockBalance', N'Stock Balance', 0, N'/ReportWarehouse/Report/StockBalance.aspx', 3)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8623, N'AdminWhs', N'StockMovement', N'Stock Movement', 0, N'/ReportWarehouse/Report/StockMove.aspx', 4)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8624, N'AdminWhs', N'JobStatus', N'Job By Status', 0, NULL, 5)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8625, N'AdminWhs', N'CustomerAccess', N'Customer Access', 0, NULL, 6)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8626, N'Admin5', NULL, N'ChatGroup', 0, N'/Mobile/ChatGroup.aspx', 40)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8627, N'Admin6', NULL, N'Mobile Control', 1, N'Mobile/MobileControl.aspx', 5)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8628, N'AdminChat', NULL, N'Chat', 1, N'/Mobile/Chat/ChatList.aspx', 0)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8629, N'AdminChat', NULL, N'MyGroup', 1, N'/Mobile/Chat/ChatGroupList.aspx', 1)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8630, N'AdminChat', N'AdminChatMasterData', N'MasterData', 1, NULL, 10)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8631, N'AdminWhs', N'AdminWhsStk', N'CM Stocks', 1, NULL, 12)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8632, N'AdminHr', N'AdminLeave', N'Leave', 1, N'/Modules/Hr/Job/LeaveRecord.aspx', 2)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8633, N'AdminHr', N'AdminLeaveCalenda', N'Leave Calendar', 1, N'/Modules/Hr/Job/LeaveCalendar.aspx', 3)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8634, N'AdminHr', N'AdminPayroll', N'Payroll', 1, NULL, 100)
GO
INSERT [dbo].[Menu2] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (8635, N'AdminHr', N'AdminHrReport', N'HR Report', 1, NULL, 101)
GO
SET IDENTITY_INSERT [dbo].[Menu2] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu3] ON 

GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3550, N'Admin16Purchase', NULL, N'PO', 1, N'/Warehouse/PurchaseOrders/PurchaseOrderList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3551, N'Admin16Purchase', NULL, N'PO Receipt', 1, N'/Warehouse/PurchaseOrders/PurchaseOrderReceiptList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3552, N'Admin16Purchase', NULL, N'SO', 1, N'/Warehouse/SalesOrders/SalesOrderList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3553, N'Admin16Purchase', NULL, N'SO Receipt', 1, N'/Warehouse/SalesOrders/SalesOrderReleaseList.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3554, N'Admin16Mast', NULL, N'Warehouse', 1, N'/Warehouse/MastData/Warehouse.aspx', 9)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3555, N'Admin16Mast', NULL, N'Product Class', 1, N'/Warehouse/MastData/ProductClass.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3556, N'Admin16Mast', NULL, N'Product', 1, N'/Warehouse/MastData/Product.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3557, N'Admin16Mast', NULL, N'Location', 1, N'/Warehouse/MastData/Location.aspx', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3558, N'Admin3Import', NULL, N'Sea Import Shipment', 1, N'/PagesFreight/import/importrefList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3559, N'Admin3Import', NULL, N'Sea Import Job', 1, N'/PagesFreight/import/importList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3560, N'Admin3Export', NULL, N'Sea Export Shipment', 1, N'/PagesFreight/export/exportrefList.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3561, N'Admin3Export', NULL, N'Sea Export Job', 1, N'/PagesFreight/export/exportList.aspx', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3562, N'Admin1Import', NULL, N'Volume By Agent', 1, N'/ReportFreightSea/Report/import/ImportVolumeByAgtView.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3563, N'Admin1Import', NULL, N'Volume By Date', 1, N'/ReportFreightSea/Report/import/ImportVolumeByDateView.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3564, N'Admin1Import', NULL, N'Volume By Port', 1, N'/ReportFreightSea/Report/import/ImportVolumeByPortView.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3565, N'Admin1Import', NULL, N'Sales Report', 1, N'/ReportFreightSea/Report/import/ImportSalesProfitView.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3566, N'Admin1Import', NULL, N'UnBilling Job', 1, N'/ReportFreightSea/Report/import/ImportJobPrint.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3567, N'SeaRpt', NULL, N'Volume By Agent', 1, N'/ReportFreightSea/Report/import/ImportVolumeByAgtView.aspx', 30)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3568, N'SeaRpt', NULL, N'Volume By Date', 1, N'/ReportFreightSea/Report/import/ImportVolumeByDateView.aspx', 31)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3569, N'SeaRpt', NULL, N'Volume By Port', 1, N'/ReportFreightSea/Report/import/ImportVolumeByPortView.aspx', 32)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3570, N'SeaRpt', NULL, N'Sales Report', 1, N'/ReportFreightSea/Report/Import/ImportSalesProfitView.aspx', 33)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3571, N'SeaRpt', NULL, N'UnBilling Job', 1, N'/ReportFreightSea/Report/Import/ImportJobPrint.aspx', 34)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3572, N'Admin1Import', NULL, N'Teus', 1, N'/ReportFreightSea/Analysis/import/TeusReport.aspx', 99)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3573, N'Admin1Import', NULL, N'CBM', 1, N'/ReportFreightSea/Analysis/import/WtM3Report.aspx', 100)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3574, N'SeaRpt', NULL, N'Teus', 1, N'/ReportFreightSea/Analysis/Import/TeusReport.aspx', 99)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3575, N'SeaRpt', NULL, N'CBM', 1, N'/ReportFreightSea/Analysis/Import/WtM3Report.aspx', 100)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3576, N'Admin4AR', NULL, N'AR - Invoice', 1, N'/PagesAccount/ArInvoice.aspx?t=IV', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3577, N'Admin4AR', NULL, N'AR - Credit Note', 1, N'/PagesAccount/ArCn.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3578, N'Admin4AR', NULL, N'AR - Receipt', 1, N'/PagesAccount/ArReceipt.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3580, N'Admin4Ap', NULL, N'AP - Invoice', 1, N'/PagesAccount/ApPayable.aspx?t=PL', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3581, N'Admin4Ap', NULL, N'AP - Cash Voucher', 0, N'/PagesAccount/ApVoucher.aspx', 13)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3582, N'Admin4Ap', NULL, N'AP - Payment', 1, N'/PagesAccount/ApPayment.aspx', 14)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3583, N'Admin4Ap', NULL, N'AP - Debit Note', 1, N'/PagesAccount/ApPayable.aspx?t=SD', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3584, N'Admin4GL', NULL, N'GL - GL Entry', 0, N'/PagesAccount/GlEntry.aspx', 20)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3585, N'Admin4GL', NULL, N'GL - Journal Entry', 1, N'/PagesAccount/GlEntry_Ge.aspx', 21)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3586, N'Admin4Control', NULL, N'AR - Posting', 1, N'/PagesAccount/Control/ArBatchPosting.aspx', 30)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3587, N'Admin4Control', NULL, N'AP - Posting', 1, N'/PagesAccount/Control/ApBatchPosting.aspx', 31)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3588, N'Admin4Control', NULL, N'GL - Account Close', 1, N'/PagesAccount/Control/AccountClose.aspx', 32)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3589, N'Admin4Control', NULL, N'GL -Check Error', 1, N'/PagesAccount/Control/checkerror.aspx', 32)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3590, N'Admin40AR', NULL, N'AR - Customer Transactions', 1, N'/ReportAccount/RptAR/CustomerTransView.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3591, N'Admin40AR', NULL, N'AR - Statement', 1, N'/ReportAccount/RptAR/StatementView.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3592, N'Admin40AR', NULL, N'AR - Aging Summary', 1, N'/ReportAccount/RptAR/AgingSummaryView.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3593, N'Admin40AR', NULL, N'AR - Aging Details', 1, N'/ReportAccount/RptAR/AgingDetailView.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3595, N'Admin40AP', NULL, N'AP - Vendor Trans', 1, N'/ReportAccount/RptAP/VendorTransView.aspx', 20)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3596, N'Admin40AP', NULL, N'AP - Statement', 1, N'/ReportAccount/RptAP/StatementView.aspx', 21)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3597, N'Admin40AP', NULL, N'AP - Aging Summary', 1, N'/ReportAccount/RptAP/AgingSummaryView.aspx', 22)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3598, N'Admin40AP', NULL, N'AP - Aging Details', 1, N'/ReportAccount/RptAP/AgingDetailView.aspx', 23)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3600, N'Admin40GL', NULL, N'GL - GST Report', 0, N'/mis/acc/mis_acc_gl_gst.aspx', 40)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3601, N'Admin40GL', NULL, N'GL - Trial Balance', 0, N'/mis/acc/mis_acc_gl_tb.aspx', 41)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3602, N'Admin40GL', NULL, N'GL - Profit & Loss', 0, N'/mis/acc/mis_acc_gl_pl.aspx', 42)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3603, N'Admin40GL', NULL, N'GL - Balance Sheet', 0, N'/mis/acc/mis_acc_gl_bs.aspx', 43)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3604, N'Admin40GL', NULL, N'GL - Listing', 0, N'/ReportAccount/RptGl/AuditTrialView.aspx', 44)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3606, N'Admin40Bank', NULL, N'Bank - Receipt', 0, N'/ReportAccount/Other/BankReceipt.aspx', 60)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3607, N'Admin40Bank', NULL, N'Bank - Receipt Report', 0, N'/ReportAccount/Other/BankReceiptRptView.aspx', 61)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3608, N'Admin40Bank', NULL, N'Bank - Recon', 1, N'/mis/acc/mis_acc_gl_bank_recon.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3609, N'Admin40Bank', NULL, N'Bank - Payment', 0, N'/ReportAccount/Other/BankPayment.aspx', 65)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3610, N'Admin40Bank', NULL, N'Bank - Payment Report', 0, N'/ReportAccount/Other/BankPaymentRptView.aspx', 66)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3611, N'Admin40Other', NULL, N'Misc - AR Document Listing', 1, N'/ReportAccount/Other/ArDocListingView.aspx', 70)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3612, N'Admin40Other', NULL, N'Misc - AP Document Listing', 1, N'/ReportAccount/Other/ApDocListingView.aspx', 71)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3613, N'Admin40Other', NULL, N'Misc - Accrued Income', 1, N'/ReportAccount/Other/AccruedIncomeRptView.aspx', 72)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3614, N'Admin40Other', NULL, N'Misc - Deferred Income', 1, N'/ReportAccount/Other/DeferredIncomeRptView.aspx', 73)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3615, N'AdminSI', NULL, N'Direct Job', 1, N'/PagesFreight/import/importrefList.aspx?refType=SIF', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3616, N'AdminSI', NULL, N'Contract Job', 1, N'/PagesFreight/import/importrefList.aspx?refType=SIL', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3617, N'AdminSI', NULL, N'Local Job', 1, N'/PagesFreight/import/importrefList.aspx?refType=SIC', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3618, N'AdminSE', NULL, N'Export FCL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SEF', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3619, N'AdminSE', NULL, N'Export LCL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SEL', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3620, N'AdminSE', NULL, N'Export CONSOL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SEC', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3621, N'AdminSC', NULL, N'Cross FCL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SCF', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3622, N'AdminSC', NULL, N'Cross LCL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SCL', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3623, N'AdminSC', NULL, N'Cross CONSOL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SCC', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3624, N'AirOperation', NULL, N'Import', 1, N'/PagesAir/import/Air_ImportRefList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3625, N'AirOperation', NULL, N'Export', 1, N'/PagesAir/export/Air_exportRefList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3626, N'AirOperation', NULL, N'Cross Trade', 1, N'/PagesAir/CrossTrade/Air_CrossTradeRefList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3627, N'AirRpt', NULL, N'UnBilling Job', 1, N'/ReportAir/Report/UnbillingJob.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3628, N'Admin1Import', NULL, N'Unmatched Ref', 1, N'/ReportFreightSea/Report/import/UnMathcedRef.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3629, N'SeaRpt', NULL, N'Unmatched Ref', 1, N'/ReportFreightSea/Report/Import/UnMathcedRef.aspx', 35)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3630, N'AirRpt', NULL, N'Unmatched Ref', 1, N'/ReportAir/Report/UnMathcedRef.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3631, N'ContainerMasterData', NULL, N'Container List', 1, N'/PagesContainer/MasterData/ContainerMaster.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3632, N'ContainerMasterData', NULL, N'Container Category', 1, N'/PagesContainer/MasterData/Category.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3633, N'ContainerMasterData', NULL, N'Container Type', 1, N'/PagesContainer/MasterData/ContainerType.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3634, N'ContainerMasterData', NULL, N'Movement Type', 1, N'/PagesContainer/MasterData/MovementType.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3635, N'ContainerMasterData', NULL, N'Release Type', 1, N'/PagesContainer/MasterData/ReleaseType.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3636, N'ContainerMasterData', NULL, N'Return Type', 1, N'/PagesContainer/MasterData/ReturnType.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3637, N'ContainerMasterData', NULL, N'Container Status', 1, N'/PagesContainer/MasterData/TankState.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3638, N'ContainerMasterData', NULL, N'Depot/Yard', 1, N'/PagesContainer/MasterData/DepotCode.aspx', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3639, N'ContainerYard', NULL, N'Release Order', 1, N'/PagesContainer/Job/ReleaseOrderList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3640, N'ContainerYard', NULL, N'Storing Order', 1, N'/PagesContainer/Job/StoringOrderList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3641, N'ContainerYard', NULL, N'Depot In', 1, N'/PagesContainer/Job/DepotInList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3642, N'ContainerYard', NULL, N'Depot Out', 1, N'/PagesContainer/Job/DepotOutList.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3643, N'SeaRpt', NULL, N'Bill', 1, N'/ReportFreightSea/Report/Account/BillListView.aspx?refType=SI', 200)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3644, N'Admin40GL', NULL, N'GL - Aging Summary', 1, N'/ReportAccount/RptGl/AgingSummaryView.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3645, N'Admin40GL', NULL, N'GL - Aging Detail', 0, N'/ReportAccount/RptGl/AgingDetailView.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3646, N'Admin4AR', NULL, N'AR - Debit Note', 1, N'PagesAccount/ArInvoice.aspx?t=DN', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3647, N'AirRpt', NULL, N'Volume By Agent', 1, N'/ReportAir/Report/VolumeByAgtView.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3648, N'AirRpt', NULL, N'Volume By Date', 1, N'/ReportAir/Report/VolumeByDateView.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3649, N'AirRpt', NULL, N'Volume By Port', 1, N'/ReportAir/Report/VolumeByPortView.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3650, N'AirRpt', NULL, N'Bill', 1, N'/ReportAir/Report/Account/BillListView.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3651, N'SeaExRpt', NULL, N'Volume By Agent', 1, N'/ReportFreightSea/Report/Export/exportVolumeByAgtView.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3652, N'SeaExRpt', NULL, N'Volume By Date', 1, N'/ReportFreightSea/Report/Export/ExportVolumeByDateView.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3653, N'SeaExRpt', NULL, N'Volume By Port', 1, N'/ReportFreightSea/Report/Export/ExportVolumeByPortView.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3654, N'SeaExRpt', NULL, N'Sales Report', 1, N'/ReportFreightSea/Report/export/exportSalesProfitView.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3655, N'SeaExRpt', NULL, N'UnBilling Job', 1, N'/ReportFreightSea/Report/Export/ExportJobPrint.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3656, N'SeaExRpt', NULL, N'Unmatched Ref', 1, N'/ReportFreightSea/Report/Export/UnMathcedRef.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3657, N'SeaExRpt', NULL, N'Batch Print OBL', 1, N'/PagesFreight/Export/PrintObl.aspx', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3658, N'SeaExRpt', NULL, N'Teus', 1, N'/ReportFreightSea/Analysis/Export/TeusReport.aspx', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3659, N'SeaExRpt', NULL, N'CBM', 1, N'/ReportFreightSea/Analysis/Export/WtM3Report.aspx', 9)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3660, N'SeaExRpt', NULL, N'Bill', 1, N'/ReportFreightSea/Report/Account/BillListView.aspx?refType=SE', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3661, N'AdminSeaStd', N'', N'IMP FCL Rate', 1, N'/PagesQuote/Sea/FclRate_Std.aspx?typ=SI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3662, N'AdminSeaStd', N'', N'IMP LCL Rate', 1, N'/PagesQuote/Sea/LclRate_Std.aspx?typ=SI', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3663, N'AdminSeaStd', N'', N'EXP FCL Rate', 1, N'/PagesQuote/Sea/FclRate_Std.aspx?typ=SE', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3664, N'AdminSeaStd', N'', N'EXP LCL Rate', 1, N'/PagesQuote/Sea/LclRate_Std.aspx?typ=SE', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3665, N'AdminSeaQuote', N'', N'IMP FCL Quote', 1, N'/PagesQuote/Sea/FclQuote.aspx?typ=SI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3666, N'AdminSeaQuote', N'', N'IMP LCL Quote', 1, N'/PagesQuote/Sea/LclQuote.aspx?typ=SI', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3667, N'AdminSeaQuote', N'', N'EXP FCL Quote', 1, N'/PagesQuote/Sea/FclQuote.aspx?typ=SE', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3668, N'AdminSeaQuote', N'', N'EXP LCL Quote', 1, N'/PagesQuote/Sea/LclQuote.aspx?typ=SE', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3669, N'AdminAirStd', N'', N'IMP Rate', 1, N'/PagesQuote/Air/Rate_Std.aspx?typ=AI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3670, N'AdminAirStd', N'', N'EXP Rate', 1, N'/PagesQuote/Air/Rate_Std.aspx?typ=AE', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3671, N'AdminAirQuote', N'', N'IMP Quote', 1, N'/PagesQuote/Air/Quote.aspx?typ=AI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3672, N'AdminAirQuote', N'', N'EXP Quotee', 1, N'/PagesQuote/Air/Quote.aspx?typ=AE', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3673, N'AdminLhStd', N'', N'STD Rate', 1, N'/PagesQuote/Lh/Rate_Std.aspx?typ=TPT', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3674, N'AdminLhQuote', N'', N'LH Quote', 1, N'/PagesQuote/Lh/Quote.aspx?typ=TPT', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3675, N'AdminLhJob', N'', N'Haulier', 1, N'/PagesTpt/Job/TptJobList.aspx?typ=Haulier', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3676, N'AdminLhJob', N'', N'Trucking', 1, N'/PagesTpt/Job/TptJobList.aspx?typ=Trucking', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3677, N'AdminLhJob', N'', N'Permit', 1, N'/PagesTpt/Job/TptJobList.aspx?typ=Permit', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3678, N'AdminLhJob', N'', N'Warehouse', 1, N'/PagesTpt/Job/TptJobList.aspx?typ=Warehouse', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3679, N'AdminLhRpt', N'', N'Billing', 1, N'/ReportTpt/Report/BillingList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3680, N'AdminEdiSea', N'', N'Import By File', 1, N'/EDI/Sea/ImportRefEDI.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3681, N'AdminEdiSea', N'', N'Import By DB', 1, N'/EDI/Sea/ImportRefList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3682, N'AdminEdiSea', N'', N'Export By File', 1, N'/EDI/Sea/ExportRefEDI.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3683, N'AdminEdiSea', N'', N'Export By DB', 1, N'/EDI/Sea/ExportRefList.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3684, N'AdminEdiAir', N'', N'Import By File', 1, N'/EDI/Air/AirImportRefEDI.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3685, N'AdminEdiAir', N'', N'Import By DB', 1, N'/EDI/Air/AirImportRefList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3686, N'AdminEdiAir', N'', N'Export By File', 1, N'/EDI/Air/AirExportRefEDI.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3687, N'AdminEdiAir', N'', N'Export By DB', 1, N'/EDI/Air/AirExportRefList.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3688, N'AdminEdiAcc', N'', N'Ap By File', 1, N'/Edi/Acc/Ap_Edi.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3689, N'AdminEdiAcc', N'', N'Ap By DB', 1, N'/Edi/Acc/ArInvoiceList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3690, N'AdminApQuote', N'', N'Quote Title', 1, N'/PagesQuote/Ap/ApQuoteTitle.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3691, N'AdminApQuote', N'', N'FCL Rate', 1, N'/PagesQuote/Ap/ExpFclRate_Std.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3692, N'AdminApQuote', N'', N'LCL Rate', 1, N'/PagesQuote/Ap/ExpLclRate_Std.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3693, N'AdminApQuote', N'', N'FCL Quote', 1, N'/PagesQuote/Ap/ExpFclApQuote.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3694, N'AdminApQuote', N'', N'LCL Quote', 1, N'/PagesQuote/Ap/ExpLclApQuote.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3695, N'Admin16Purchase', NULL, N'Transfer', 1, N'/Warehouse/Transfer.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3696, N'Admin16Purchase', NULL, N'Inventory', 1, N'/Warehouse/Inventory.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3697, N'Admin16Job', NULL, N'Stock In', 0, N'/Warehouse/Job/DoInList.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3698, N'Admin16Job', NULL, N'Stock Out', 0, N'/Warehouse/Job/DoOutList.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3699, N'Admin16Report', NULL, N'Stock Balance', 1, N'/ReportWarehouse/Report/StockBalance.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3700, N'CTMJob', N'', N'Job List', 1, N'/PagesContTrucking/Job/JobList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3703, N'CTMMasterData', N'', N'Driver List', 1, N'/PagesContTrucking/MasterData/Driver.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3704, N'CTMMasterData', N'', N'Container', 1, N'/PagesContainer/MasterData/ContainerMaster.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3705, N'CTMMasterData', N'', N'Container Type', 1, N'/PagesContainer/MasterData/ContainerType.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3706, N'CTMJob', N'', N'Dispatch Sheet', 1, N'/PagesContTrucking/Job/DispatchPlanner.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3707, N'CTMJob', N'', N'Assign Driver', 1, N'/PagesContTrucking/Job/AssignDriver.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3713, N'CTMDailySchedule', NULL, N'Driver Schedule', 1, N'/PagesContTrucking/Daily/DriverDaily.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3714, N'CTMVehicle', NULL, N'Vehicle List', 1, N'/PagesContTrucking/Vehicle/Vehicle.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3719, N'CTMReport', NULL, N'Trips - Local', 1, N'/PagesContTrucking/Report/TripReportLocal.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3720, N'CTMJobReport', NULL, N'Unbilled - Local', 1, N'/PagesContTrucking/Report/UnbillingJobListLocal.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3721, N'CTMMasterData', NULL, N'TripCode', 1, N'/PagesContTrucking/MasterData/TripCode.aspx', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3722, N'CTMDailySchedule', NULL, N'Unscheduled Containers', 1, N'/PagesContTrucking/Daily/UnscheduledContainers.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3723, N'CTMGPSMonitor', NULL, N'Drivers Local', 1, N'/PagesContTrucking/GPSMonitor/DriverList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3724, N'CTMReport', NULL, N'Analysis - Local', 1, N'/PagesContTrucking/Report/Analysis/TripAnalysisLocal.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3726, N'AdminJOL', N'', N'Job List', 1, N'/Pagestpt/Local/Job/TptJobList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3727, N'AdminJOL', N'', N'Assign Driver', 1, N'/Pagestpt/Local/Job/AssignDriverEdit.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3729, N'AdminDS', N'', N'Unscheduled Jobs', 1, N'/Pagestpt/Local/Job/UnScheduleJob.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3730, N'AdminDS', N'', N'Import Jobs', 1, N'/Pagestpt/Local/Job/DispatchPlanner.aspx?typ=IMP', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3731, N'AdminDS', N'', N'Export Jobs', 1, N'/Pagestpt/Local/Job/DispatchPlanner.aspx?typ=EXP', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3732, N'AdminDS', N'', N'Transhipment Jobs', 1, N'/Pagestpt/Local/Job/DispatchPlanner.aspx?typ=TSP', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3733, N'AdminDS', N'', N'Local Jobs', 1, N'/Pagestpt/Local/Job/DispatchPlanner.aspx?typ=LOC', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3734, N'CTMGPSMonitor', NULL, N'Drivers Haulier', 1, N'/PagesContTrucking/GPSMonitor/DriverList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3735, N'CTMGPSMonitor', NULL, N'Driver Messages', 1, N'/PagesContTrucking/GPSMonitor/DriverMessages.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3737, N'CTMJobReport', NULL, N' Unbilled - Haulier', 1, N'/PagesContTrucking/Report/UnbillingJobList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3738, N'CTMReport', NULL, N'Trips - Haulier', 1, N'/PagesContTrucking/Report/TripReport.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3739, N'CTMReport', NULL, N'Analysis - Haulier', 1, N'/PagesContTrucking/Report/Analysis/TripAnalysis.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3740, N'CTMJobByStatus', NULL, N'New Booking', 1, N'/PagesContTrucking/Job/JobList.aspx?ContStatus=New', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3741, N'CTMJobByStatus', NULL, N'Scheduled', 1, N'/PagesContTrucking/Job/JobList.aspx?ContStatus=Scheduled', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3742, N'CTMJobByStatus', NULL, N'In Transit', 1, N'/PagesContTrucking/Job/JobList.aspx?ContStatus=InTransit', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3743, N'CTMJobByStatus', NULL, N'Completed', 1, N'/PagesContTrucking/Job/JobList.aspx?ContStatus=Completed', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3744, N'CTMJobByStatus', NULL, N'Canceled', 1, N'/PagesContTrucking/Job/JobList.aspx?ContStatus=Canceled', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3745, N'AdminTptLocViewer', N'', N'Shipper Viewer', 1, N'/Pagestpt/Local/Viewer/ShipperView.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3746, N'AdminTptLoc3', N'', N'New Booking', 1, N'/Pagestpt/Local/Job/TptJobListByStatus.aspx?typ=', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3748, N'AdminTptLoc3', N'', N'Confirmed', 1, N'/Pagestpt/Local/Job/TptJobListByStatus.aspx?typ=Confirmed', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3752, N'CTMDriverView', NULL, N'Driver View', 1, N'/PagesContTrucking/DriverView/DriverView.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3753, N'Admin16Mast', N'', N'TptMode', 1, N'/Warehouse/MastData/MastType.aspx?type=TptMode', 13)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3754, N'Admin16Mast', N'', N'IncoTerms', 0, N'/Warehouse/MastData/MastType.aspx?type=IncoTerms', 14)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3755, N'Admin16Mast', N'', N'InStatus', 1, N'/Warehouse/MastData/MastType.aspx?type=InStatus', 15)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3756, N'Admin16Mast', N'', N'Type', 1, N'/Warehouse/MastData/MastType.aspx?type=Priority', 16)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3757, N'Admin16Mast', N'', N'EquipmentNo', 1, N'/Warehouse/MastData/MastType.aspx?type=EquipmentNo', 17)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3758, N'Admin16Mast', N'', N'Personnel', 1, N'/Warehouse/MastData/MastType.aspx?type=Personnel', 18)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3759, N'Admin16Mast', N'', N'OutStatus', 1, N'/Warehouse/MastData/MastType.aspx?type=OutStatus', 19)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3760, N'AdminTptLoc3', N'', N'Delivered', 1, N'/Pagestpt/Local/Job/TptJobListByStatus.aspx?typ=Delivered', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3761, N'AdminTptLocViewer', N'', N'Driver View', 1, N'/Pagestpt/Local/Viewer/DriverView.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3762, N'SeaChartRPT', NULL, N'Chart by Agt', 1, N'/ReportFreightSea/Chart/SeaChartRPT_ByAgt.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3763, N'SeaChartRPT', NULL, N'Chart by Custom', 1, N'/ReportFreightSea/Chart/SeaChartRPT_ByCustom.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3764, N'Admin16Job', N'', N'PO', 0, N'/Warehouse/Job/PoList.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3765, N'Admin16Job', N'', N'SO', 0, N'/Warehouse/Job/SoList.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3766, N'HrMastData', N'', N'Department', 1, N'/Modules/Hr/Master/Department.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3767, N'HrMastData', N'', N'Role', 1, N'/Modules/Hr/Master/Role.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3768, N'HrMastData', N'', N'Employee', 1, N'/Modules/Hr/Master/Person.aspx?type=Employee', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3769, N'HrMastData', N'', N'Candidate', 1, N'/Modules/Hr/Master/Person.aspx?type=Candidate', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3770, N'HrMastData', N'', N'Resignation', 1, N'/Modules/Hr/Master/Person.aspx?type=Resignation', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3771, N'HrPayroll', N'', N'Item', 1, N'/PagesHr/MasterData/PayItem.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3772, N'HrPayroll', N'', N'Quotation', 1, N'/PagesHr/Job/Quotation.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3773, N'HrPayroll', N'', N'Payroll', 1, N'/PagesHr/Job/PayrollEdit.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3774, N'HrReport', N'', N'Daily Report', 1, N'/ReportJob/PrintCrew.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3775, N'HrReport', N'', N'OT Report', 1, N'', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3776, N'HrReport', N'', N'Expense Report', 1, N'', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3777, N'HrReport', N'', N'Payroll Report', 1, N'', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3778, N'SeaChartRPT', NULL, N'Billing Chart', 1, N'/ReportFreightSea/Chart/BillingChartRPT.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3779, N'Admin16Job', N'', N'PO Request', 0, N'/Warehouse/Job/PoRequest.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3780, N'Admin16Report', N'', N'Stock Move', 1, N'/ReportWarehouse/Report/Stockmove.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3781, N'Admin16Report', N'', N'Fast Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=Out&by=0', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3782, N'Admin16Report', N'', N'Slow Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=Out&by=1', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3783, N'Admin16Report', N'', N'PO Fast Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=in&by=0', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3784, N'Admin16Report', N'', N'PO Slow Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=in&by=1', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3785, N'AdminWmsBilling', NULL, N'Contracts', 1, N'/Warehouse/ContractList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3786, N'AdminWmsBilling', NULL, N'Billing Schedule', 1, N'/Warehouse/Schedule.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3787, N'AdminWmsOrders', NULL, N'Sales Orders', 1, N'Warehouse/job/Solist.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3788, N'AdminWmsOrders', NULL, N'Purchases', 1, N'Warehouse/job/Polist.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3789, N'AdminWmsOrders', NULL, N'Pending Purchase', 1, N'Warehouse/job/PoRequest.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3790, N'Admin16Report', NULL, N'Incoming Stock', 1, N'/ReportWarehouse/Report/incomingstock.aspx', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3791, N'AdminWmsOrders', NULL, N'Online Order', 1, N'Warehouse/job/Polist.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3792, N'Admin16Report', NULL, N'Unbilling Job', 1, N'/ReportWarehouse/Report/Unbilling.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3794, N'AdminWhQuote', N'', N'IMP FCL Rate', 1, N'/PagesQuote/wh/FclRate_Std.aspx?typ=WHI', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3798, N'Admin16Report', N'', N'Summary Report', 1, N'/ReportWarehouse/Report/SummaryReportView.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3799, N'Admin16Report', N'', N'Detail Report', 1, N'/ReportWarehouse/Report/DoDetView.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3800, N'Admin16Report', N'', N'FCL Report', 1, N'/ReportWarehouse/Report/FCLReportView.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3802, N'AdminWhQuote', N'', N'STD Rate', 1, N'/PagesQuote/wh/Rate_Std.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3803, N'Admin16Mast', NULL, N'LotNo', 1, N'/Warehouse/MastData/LotNo.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3804, N'Admin16Mast', NULL, N'PurchasePrice', 1, N'/Warehouse/MastData/PurchasePrice.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3805, N'Admin16Mast', NULL, N'SellingPrice', 1, N'/Warehouse/MastData/SellingPrice.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3806, N'Admin16Report', NULL, N'Reorder Report', 1, N'/ReportWarehouse/Report/MinOrderQty.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3807, N'Admin16Report', NULL, N'Expiry Report', 1, N'/ReportWarehouse/Report/ExpiryStockReport.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3808, N'Admin16Report', NULL, N'Unbilled Handling Report', 1, N'/ReportWarehouse/Report/HandlingReport.aspx', 9)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3809, N'Admin16Report', NULL, N'Unbilled Storage Report', 1, N'/ReportWarehouse/Report/StorageReport.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3810, N'CTMJob', NULL, N'Haulier Service', 1, N'/PagesContTrucking/Job/ContTruckingManagement.aspx', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3811, N'CTMJob', NULL, N'Dispatch Planner', 1, N'/PagesContTrucking/Job/DispatchPlanner2.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3812, N'CTMJob', NULL, N'Dispatch Planner3', 0, N'/PagesContTrucking/Job/DispatchPlanner3_web.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3813, N'CTMJob', NULL, N'Dispatch Timesheet', 1, N'/PagesContTrucking/Job/DispatchPlanner4.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3814, N'CTMMasterData', NULL, N'Planner Stage', 1, N'/PagesContTrucking/MasterData/DispatchPlanner_Stage_level0.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3815, N'CTMJob', NULL, N'Monthly Summary', 1, N'/PagesContTrucking/Job/DispatchSummary.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3816, N'AdminJOL', NULL, N'Monthly Summary', 1, N'/Pagestpt/Local/Job/DispatchSummary.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3817, N'CTMJob', NULL, N'Dispatch Planner2', 1, N'/PagesContTrucking/Job/DispatchPlanner2.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3818, N'CTMJob', NULL, N'Dispatch Planner3', 1, N'/PagesContTrucking/Job/DispatchPlanner3.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3819, N'CTMJob', NULL, N'Dispatch Planner4', 1, N'/PagesContTrucking/Job/DispatchPlanner4.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3828, N'JobStatus', NULL, N'Customer Inquiry', 1, N'/WareHouse/Job/JobList.aspx?type=Customer Inquiry', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3829, N'JobStatus', NULL, N'Site Survey', 1, N'/WareHouse/Job/JobList.aspx?type=Site Survey', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3831, N'JobStatus', NULL, N'Costing', 1, N'/WareHouse/Job/JobList.aspx?type=Costing', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3832, N'JobStatus', NULL, N'Quotation', 1, N'/WareHouse/Job/JobList.aspx?type=Quotation', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3833, N'JobStatus', NULL, N'Job Confirmation', 1, N'/WareHouse/Job/JobList.aspx?type=Job Confirmation', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3834, N'JobStatus', NULL, N'Job Completion', 1, N'/WareHouse/Job/JobList.aspx?type=Job Completion', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3835, N'JobStatus', NULL, N'Billing', 1, N'/WareHouse/Job/JobList.aspx?type=Billing', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3836, N'JobStatus', NULL, N'Job Close', 1, N'/WareHouse/Job/JobList.aspx?type=Job Close', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3837, N'CustomerAccess', NULL, N'Stock Balance', 1, N'/ReportWarehouse/Report/StockBalance.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3838, N'CustomerAccess', NULL, N'Stock Movement', 1, N'/ReportWarehouse/Report/Stockmove.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3839, N'CustomerAccess', NULL, N'Stock Inquiry', 1, NULL, 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3840, N'CustomerAccess', NULL, N'Booking Request', 1, NULL, 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3841, N'JobByStatus2', NULL, N'Pending Jobs', 1, N'/WareHouse/Job/JobList.aspx?status=Pending', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3842, N'JobByStatus2', NULL, N'Working Jobs', 1, N'/WareHouse/Job/JobList.aspx?status=Working', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3843, N'JobByStatus2', NULL, N'Unsuccess Jobs', 1, N'/WareHouse/Job/JobList.aspx?status=Unsuccess', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3844, N'JobByStatus2', NULL, N'Completed Jobs', 0, N'/WareHouse/Job/JobList.aspx?status=Complete', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3845, N'Admin4AP', NULL, N'AP - Credit Note', 1, N'/PagesAccount/ApPayable.aspx?t=SC', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3846, N'Admin41Post', NULL, N'Batch Posting', 1, N'/PagesAccount/Control/BatchPost.aspx', 21)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3847, N'Admin41Post', NULL, N'GL Unpost', 1, N'/PagesAccount/GlEntry.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3848, N'Admin41Post', NULL, N'GL Account Close', 1, N'/PagesAccount/Control/AccountClose.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3849, N'AdminDay', NULL, N'MCST Report', 1, N'/ReportJob/PrintMcst.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3850, N'JobReport', NULL, N'Unbilled Jobs', 1, N'/Mis/Job/Mis_Job_Unbill.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3851, N'TempJob', NULL, N'Template for Local Move', 1, N'/WareHouse/Job/LocalMoveTemp.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3853, N'TempJob', NULL, N'Template for Office Move', 1, N'/WareHouse/Job/OfficeMoveTemp.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3854, N'TempJob', NULL, N'Template  for Outbound', 1, N'/WareHouse/Job/OutboundTemp.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3855, N'TempJob', NULL, N'Template for Air', 0, N'/WareHouse/Job/AirTemp.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3856, N'TempJob', NULL, N'Template for Inbound', 1, N'/WareHouse/Job/InboundTemp.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3857, N'TempJob', NULL, N'Template for Storage', 1, N'/WareHouse/Job/StorageTemp.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3858, N'HrPayroll', NULL, N'Crews', 1, N'/PagesHr/Job/Crews.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3859, N'HrPayroll', NULL, N'Un Hr Payment', 1, N'/PagesHr/Job/UnHrPayment.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3860, N'Admin40Bank', NULL, N'Bank - Entry', 1, N'/ReportAccount/Other/BankTransGl.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3861, N'Admin40Bank', NULL, N'Bank - Report', 0, N'/ReportAccount/Other/BankTransRptview.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3862, N'Admin40GL', NULL, N'GL - Inquiry', 0, N'/mis/acc/mis_acc_gl_iq.aspx', 46)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3863, N'Admin40GL', NULL, N'GL - Ledger', 0, N'/mis/acc/mis_acc_gl_gl.aspx', 47)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3864, N'AdminDay', NULL, N'Transaction List', 1, N'/mis/acc/mis_acc_gl_iq.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3865, N'AdminDay', NULL, N'Collection List', 0, N'/mis/acc/mis_acc_gl_re.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3866, N'AdminDay', NULL, N'New Business Party', 0, N'/mis/acc/day-party.aspx', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3867, N'AdminDay', NULL, N'Ledger List', 0, N'/mis/acc/day-gl.aspx', 13)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3868, N'AdminDay', NULL, N'Sales Summary', 1, N'/mis/acc/mis_acc_gl_ar.aspx', 14)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3869, N'AdminDay', NULL, N'Purchase Summary', 0, N'/mis/acc/day-purchase.aspx', 15)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3870, N'Admin40Tax', NULL, N'GST Inquiry', 1, N'/mis/acc/mis_acc_gl_gst.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3871, N'Admin40Tax', NULL, N'GST F5 Report', 1, N'/mis/acc/mis_acc_gl_gst_f5.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3872, N'Admin40GL', NULL, N'GL - Cashflows', 0, N'/mis/acc/mis_acc_gl_cf.aspx', 44)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3873, N'AdminWhsMat', N'MatIn', N'CM Material From SPJ', 0, N'/Warehouse/Job/MatIn.aspx', 99)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3874, N'AdminWhsMat', N'matBal', N'CM Material Balance', 1, N'/Warehouse/cm/mb_clm.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3875, N'AdminWhsMat', N'MatOut', N'CM Material Out', 0, N'/Warehouse/Job/MatOut.aspx', 99)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3876, N'AdminWhsMat', N'MatIn3', N'3PL Material In', 0, N'/Warehouse/Job/MatIn3.aspx', 21)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3877, N'AdminWhsMat', N'MatOut3', N'3PL Material Out', 0, N'/Warehouse/Job/MatOut3.aspx', 22)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3878, N'AdminWhsMat', N'MatBal3', N'3PL Material Balance', 0, N'/Warehouse/Job/MatBalance3.aspx', 23)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3879, N'Admin40AR', NULL, N'AR - Aging Detail 2', 1, N'/mis/acc/mis_acc_ar_age.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3880, N'AdminWhsMat', NULL, N'3PL Material Movement', 0, N'/Warehouse/Job/MaterialMovement.aspx', 24)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3881, N'AdminWhsMat', NULL, N'CM Material In/Return', 1, N'/Warehouse/cm/mo_clm.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3882, N'Admin41Post', NULL, N'Batch Unpost', 1, N'/PagesAccount/Control/BatchUnPost.aspx', 22)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3883, N'Admin40Tax', NULL, N'GST Return Report', 0, N'/reportaccount/GstRptReturnView.aspx', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3884, N'JobReport', NULL, N'Accrual Report', 1, N'/Mis/Job/Mis_job_accrual.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3885, N'JobReport', NULL, N'Reversal Report', 1, N'/Mis/Job/Mis_Job_Reversal.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3886, N'JobReport', NULL, N'Gl Report', 1, N'/Mis/Job/Mis_Job_Gl.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3887, N'Admin40Bank', NULL, N'Bank - Recon - NS', 1, N'/mis/acc/mis_acc_gl_bank_recon_netsuite.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3888, N'Admin40AR', NULL, N'AR - Aging Detail 3', 1, N'/mis/acc/mis_acc_ar_age_ext.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3890, N'MatSPJ', NULL, N'Material In', 1, N'/Warehouse/Job/mi_spj.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3891, N'MatSPJ', NULL, N'Material Out', 1, N'/Warehouse/Job/mo_spj.aspx', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3892, N'MatSPJ', NULL, N'Material Balance', 1, N'/Warehouse/Job/mb_spj.aspx', 13)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3893, N'MatSPJ', NULL, N'Material Movement', 1, N'/Warehouse/Job/mm_spj.aspx', 14)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3894, N'MatSPJ', NULL, N'Material Rates', 1, N'/Warehouse/Job/mr_spj.aspx', 20)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3895, N'WhsSpjMat', NULL, N'SPJ Material In', 1, N'/Warehouse/Job/mi_spj_whs.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3896, N'WhsSpjMat', NULL, N'SPJ Material Out', 1, N'/Warehouse/Job/mo_spj_whs.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3897, N'WhsSpjMat', NULL, N'SPJ Material Balance', 1, N'/Warehouse/Job/mb_spj_whs.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3898, N'WhsSpjMat', NULL, N'SPJ Material Movement', 1, N'/Warehouse/Job/mm_spj_whs.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3899, N'Admin40AP', NULL, N'AP - Aging Details 2', 1, N'/mis/acc/mis_acc_ap_age_ext.aspx', 23)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3900, N'AdminPayrollReport_1', NULL, N'UnPayrollEmployee', 1, N'/PagesHr/Payroll/UnPayrollEmployee.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3901, N'AdminPayrollReport_1', NULL, N'Report Payroll', 1, N'/PagesHr/Payroll/ReportPayroll.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3914, N'Admin16Purchase', NULL, N'PO', 1, N'/Warehouse/PurchaseOrders/PurchaseOrderList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3915, N'Admin16Purchase', NULL, N'PO Receipt', 1, N'/Warehouse/PurchaseOrders/PurchaseOrderReceiptList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3916, N'Admin16Purchase', NULL, N'SO', 1, N'/Warehouse/SalesOrders/SalesOrderList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3917, N'Admin16Purchase', NULL, N'SO Receipt', 1, N'/Warehouse/SalesOrders/SalesOrderReleaseList.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3918, N'Admin16Mast', NULL, N'Warehouse', 1, N'/Warehouse/MastData/Warehouse.aspx', 9)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3919, N'Admin16Mast', NULL, N'Product Class', 1, N'/Warehouse/MastData/ProductClass.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3920, N'Admin16Mast', NULL, N'Product', 1, N'/Warehouse/MastData/Product.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3921, N'Admin16Mast', NULL, N'Location', 1, N'/Warehouse/MastData/Location.aspx', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3922, N'Admin3Import', NULL, N'Sea Import Shipment', 1, N'/PagesFreight/import/importrefList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3923, N'Admin3Import', NULL, N'Sea Import Job', 1, N'/PagesFreight/import/importList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3924, N'Admin3Export', NULL, N'Sea Export Shipment', 1, N'/PagesFreight/export/exportrefList.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3925, N'Admin3Export', NULL, N'Sea Export Job', 1, N'/PagesFreight/export/exportList.aspx', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3926, N'Admin1Import', NULL, N'Volume By Agent', 1, N'/ReportFreightSea/Report/import/ImportVolumeByAgtView.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3927, N'Admin1Import', NULL, N'Volume By Date', 1, N'/ReportFreightSea/Report/import/ImportVolumeByDateView.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3928, N'Admin1Import', NULL, N'Volume By Port', 1, N'/ReportFreightSea/Report/import/ImportVolumeByPortView.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3929, N'Admin1Import', NULL, N'Sales Report', 1, N'/ReportFreightSea/Report/import/ImportSalesProfitView.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3930, N'Admin1Import', NULL, N'UnBilling Job', 1, N'/ReportFreightSea/Report/import/ImportJobPrint.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3931, N'SeaRpt', NULL, N'Volume By Agent', 1, N'/ReportFreightSea/Report/import/ImportVolumeByAgtView.aspx', 30)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3932, N'SeaRpt', NULL, N'Volume By Date', 1, N'/ReportFreightSea/Report/import/ImportVolumeByDateView.aspx', 31)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3933, N'SeaRpt', NULL, N'Volume By Port', 1, N'/ReportFreightSea/Report/import/ImportVolumeByPortView.aspx', 32)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3934, N'SeaRpt', NULL, N'Sales Report', 1, N'/ReportFreightSea/Report/Import/ImportSalesProfitView.aspx', 33)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3935, N'SeaRpt', NULL, N'UnBilling Job', 1, N'/ReportFreightSea/Report/Import/ImportJobPrint.aspx', 34)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3936, N'Admin1Import', NULL, N'Teus', 1, N'/ReportFreightSea/Analysis/import/TeusReport.aspx', 99)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3937, N'Admin1Import', NULL, N'CBM', 1, N'/ReportFreightSea/Analysis/import/WtM3Report.aspx', 100)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3938, N'SeaRpt', NULL, N'Teus', 1, N'/ReportFreightSea/Analysis/Import/TeusReport.aspx', 99)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3939, N'SeaRpt', NULL, N'CBM', 1, N'/ReportFreightSea/Analysis/Import/WtM3Report.aspx', 100)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3940, N'Admin4AR', NULL, N'AR - Invoice', 1, N'/PagesAccount/ArInvoice.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3941, N'Admin4AR', NULL, N'AR - Credit Note', 1, N'/PagesAccount/ArCn.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3942, N'Admin4AR', NULL, N'AR - Receipt', 1, N'/PagesAccount/ArReceipt.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3943, N'Admin4AR', NULL, N'AR - Refund', 1, N'/PagesAccount/ArReceipt_Cn.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3944, N'Admin4Ap', NULL, N'AP - Invoice', 1, N'/PagesAccount/ApPayable.aspx', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3945, N'Admin4Ap', NULL, N'AP - Voucher', 1, N'/PagesAccount/ApVoucher.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3946, N'Admin4Ap', NULL, N'AP - Payment', 1, N'/PagesAccount/ApPayment.aspx', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3947, N'Admin4Ap', NULL, N'AP - Refund', 1, N'/PagesAccount/ApPayment_SR.aspx', 13)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3948, N'Admin4GL', NULL, N'GL - GL Entry', 1, N'/PagesAccount/GlEntry.aspx', 20)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3949, N'Admin4GL', NULL, N'GL - Journal Entry', 1, N'/PagesAccount/GlEntry_Ge.aspx', 21)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3950, N'Admin4Control', NULL, N'AR - Posting', 1, N'/PagesAccount/Control/ArBatchPosting.aspx', 30)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3951, N'Admin4Control', NULL, N'AP - Posting', 1, N'/PagesAccount/Control/ApBatchPosting.aspx', 31)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3952, N'Admin4Control', NULL, N'GL - Account Close', 1, N'/PagesAccount/Control/AccountClose.aspx', 32)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3953, N'Admin4Control', NULL, N'GL -Check Error', 1, N'/PagesAccount/Control/checkerror.aspx', 32)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3957, N'Admin40AR', NULL, N'AR - Aging Details', 1, N'/ReportAccount/RptAR/AgingDetailView.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3958, N'Admin40AR', NULL, N'AR - GST Output Tax', 1, N'/ReportAccount/RptAR/GstOutputTaxView.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3963, N'Admin40AP', NULL, N'AP - GST Input Tax', 1, N'/ReportAccount/RptAP/GstInputTaxView.aspx', 24)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3966, N'Admin40GL', NULL, N'GL - Profit & Loss', 1, N'/ReportAccount/RptGl/PlStatementView.aspx', 42)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3967, N'Admin40GL', NULL, N'GL - Balance Sheet', 1, N'/ReportAccount/RptGl/BalanceSheetView.aspx', 43)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3968, N'Admin40GL', NULL, N'GL - Audit Trail', 1, N'/ReportAccount/RptGl/AuditTrialView.aspx', 44)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3969, N'Admin40GL', NULL, N'GL - Journal Listing', 1, N'/ReportAccount/RptGl/JournalListingView.aspx', 45)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3970, N'Admin40Bank', NULL, N'Bank - Receipt', 1, N'/ReportAccount/Other/BankReceipt.aspx', 60)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3971, N'Admin40Bank', NULL, N'Bank - Receipt Report', 1, N'/ReportAccount/Other/BankReceiptRptView.aspx', 61)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3972, N'Admin40Bank', NULL, N'Bank - Recon Report', 1, N'/ReportAccount/Other/BankReconRptView.aspx', 62)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3973, N'Admin40Bank', NULL, N'Bank - Payment', 1, N'/ReportAccount/Other/BankPayment.aspx', 65)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3974, N'Admin40Bank', NULL, N'Bank - Payment Report', 1, N'/ReportAccount/Other/BankPaymentRptView.aspx', 66)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3979, N'AdminSI', NULL, N'Import FCL', 1, N'/PagesFreight/import/importrefList.aspx?refType=SIF', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3980, N'AdminSI', NULL, N'Import LCL', 1, N'/PagesFreight/import/importrefList.aspx?refType=SIL', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3981, N'AdminSI', NULL, N'Import CONSOL', 1, N'/PagesFreight/import/importrefList.aspx?refType=SIC', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3985, N'AdminSC', NULL, N'Cross FCL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SCF', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3986, N'AdminSC', NULL, N'Cross LCL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SCL', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3987, N'AdminSC', NULL, N'Cross CONSOL', 1, N'/PagesFreight/export/exportrefList.aspx?refType=SCC', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3988, N'AirOperation', NULL, N'Import', 1, N'/PagesAir/import/Air_ImportRefList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3989, N'AirOperation', NULL, N'Export', 1, N'/PagesAir/export/Air_exportRefList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3990, N'AirOperation', NULL, N'Cross Trade', 1, N'/PagesAir/CrossTrade/Air_CrossTradeRefList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3991, N'AirRpt', NULL, N'UnBilling Job', 1, N'/ReportAir/Report/UnbillingJob.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3992, N'Admin1Import', NULL, N'Unmatched Ref', 1, N'/ReportFreightSea/Report/import/UnMathcedRef.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3993, N'SeaRpt', NULL, N'Unmatched Ref', 1, N'/ReportFreightSea/Report/Import/UnMathcedRef.aspx', 35)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3994, N'AirRpt', NULL, N'Unmatched Ref', 1, N'/ReportAir/Report/UnMathcedRef.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3995, N'ContainerMasterData', NULL, N'Container List', 1, N'/PagesContainer/MasterData/ContainerMaster.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3996, N'ContainerMasterData', NULL, N'Container Category', 1, N'/PagesContainer/MasterData/Category.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3997, N'ContainerMasterData', NULL, N'Container Type', 1, N'/PagesContainer/MasterData/ContainerType.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3998, N'ContainerMasterData', NULL, N'Movement Type', 1, N'/PagesContainer/MasterData/MovementType.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (3999, N'ContainerMasterData', NULL, N'Release Type', 1, N'/PagesContainer/MasterData/ReleaseType.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4000, N'ContainerMasterData', NULL, N'Return Type', 1, N'/PagesContainer/MasterData/ReturnType.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4001, N'ContainerMasterData', NULL, N'Container Status', 1, N'/PagesContainer/MasterData/TankState.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4002, N'ContainerMasterData', NULL, N'Depot/Yard', 1, N'/PagesContainer/MasterData/DepotCode.aspx', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4003, N'ContainerYard', NULL, N'Release Order', 1, N'/PagesContainer/Job/ReleaseOrderList.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4004, N'ContainerYard', NULL, N'Storing Order', 1, N'/PagesContainer/Job/StoringOrderList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4005, N'ContainerYard', NULL, N'Depot In', 1, N'/PagesContainer/Job/DepotInList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4006, N'ContainerYard', NULL, N'Depot Out', 1, N'/PagesContainer/Job/DepotOutList.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4007, N'SeaRpt', NULL, N'Bill', 1, N'/ReportFreightSea/Report/Account/BillListView.aspx?refType=SI', 200)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4010, N'Admin4AR', NULL, N'AR - Cash', 1, N'/PagesAccount/ArCashInvoice.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4011, N'AirRpt', NULL, N'Volume By Agent', 1, N'/ReportAir/Report/VolumeByAgtView.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4012, N'AirRpt', NULL, N'Volume By Date', 1, N'/ReportAir/Report/VolumeByDateView.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4013, N'AirRpt', NULL, N'Volume By Port', 1, N'/ReportAir/Report/VolumeByPortView.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4014, N'AirRpt', NULL, N'Bill', 1, N'/ReportAir/Report/Account/BillListView.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4015, N'SeaExRpt', NULL, N'Volume By Agent', 1, N'/ReportFreightSea/Report/Export/exportVolumeByAgtView.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4016, N'SeaExRpt', NULL, N'Volume By Date', 1, N'/ReportFreightSea/Report/Export/ExportVolumeByDateView.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4017, N'SeaExRpt', NULL, N'Volume By Port', 1, N'/ReportFreightSea/Report/Export/ExportVolumeByPortView.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4018, N'SeaExRpt', NULL, N'Sales Report', 1, N'/ReportFreightSea/Report/export/exportSalesProfitView.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4019, N'SeaExRpt', NULL, N'UnBilling Job', 1, N'/ReportFreightSea/Report/Export/ExportJobPrint.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4020, N'SeaExRpt', NULL, N'Unmatched Ref', 1, N'/ReportFreightSea/Report/Export/UnMathcedRef.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4021, N'SeaExRpt', NULL, N'Batch Print OBL', 1, N'/PagesFreight/Export/PrintObl.aspx', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4022, N'SeaExRpt', NULL, N'Teus', 1, N'/ReportFreightSea/Analysis/Export/TeusReport.aspx', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4023, N'SeaExRpt', NULL, N'CBM', 1, N'/ReportFreightSea/Analysis/Export/WtM3Report.aspx', 9)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4024, N'SeaExRpt', NULL, N'Bill', 1, N'/ReportFreightSea/Report/Account/BillListView.aspx?refType=SE', 10)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4025, N'AdminSeaStd', N'', N'IMP FCL Rate', 1, N'/PagesQuote/Sea/FclRate_Std.aspx?typ=SI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4026, N'AdminSeaStd', N'', N'IMP LCL Rate', 1, N'/PagesQuote/Sea/LclRate_Std.aspx?typ=SI', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4027, N'AdminSeaStd', N'', N'EXP FCL Rate', 1, N'/PagesQuote/Sea/FclRate_Std.aspx?typ=SE', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4028, N'AdminSeaStd', N'', N'EXP LCL Rate', 1, N'/PagesQuote/Sea/LclRate_Std.aspx?typ=SE', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4029, N'AdminSeaQuote', N'', N'IMP FCL Quote', 1, N'/PagesQuote/Sea/FclQuote.aspx?typ=SI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4030, N'AdminSeaQuote', N'', N'IMP LCL Quote', 1, N'/PagesQuote/Sea/LclQuote.aspx?typ=SI', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4031, N'AdminSeaQuote', N'', N'EXP FCL Quote', 1, N'/PagesQuote/Sea/FclQuote.aspx?typ=SE', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4032, N'AdminSeaQuote', N'', N'EXP LCL Quote', 1, N'/PagesQuote/Sea/LclQuote.aspx?typ=SE', 12)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4033, N'AdminAirStd', N'', N'IMP Rate', 1, N'/PagesQuote/Air/Rate_Std.aspx?typ=AI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4034, N'AdminAirStd', N'', N'EXP Rate', 1, N'/PagesQuote/Air/Rate_Std.aspx?typ=AE', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4035, N'AdminAirQuote', N'', N'IMP Quote', 1, N'/PagesQuote/Air/Quote.aspx?typ=AI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4036, N'AdminAirQuote', N'', N'EXP Quotee', 1, N'/PagesQuote/Air/Quote.aspx?typ=AE', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4037, N'AdminLhStd', N'', N'STD Rate', 1, N'/PagesQuote/Lh/Rate_Std.aspx?typ=TPT', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4038, N'AdminLhQuote', N'', N'LH Quote', 1, N'/PagesQuote/Lh/Quote.aspx?typ=TPT', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4039, N'AdminLhJob', N'', N'Haulier', 1, N'/PagesTpt/Job/TptJobList.aspx?typ=Haulier', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4040, N'AdminLhJob', N'', N'Trucking', 1, N'/PagesTpt/Job/TptJobList.aspx?typ=Trucking', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4041, N'AdminLhJob', N'', N'Permit', 1, N'/PagesTpt/Job/TptJobList.aspx?typ=Permit', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4042, N'AdminLhJob', N'', N'Warehouse', 1, N'/PagesTpt/Job/TptJobList.aspx?typ=Warehouse', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4043, N'AdminLhRpt', N'', N'Billing', 1, N'/ReportTpt/Report/BillingList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4044, N'AdminEdiSea', N'', N'Import By File', 1, N'/EDI/Sea/ImportRefEDI.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4045, N'AdminEdiSea', N'', N'Import By DB', 1, N'/EDI/Sea/ImportRefList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4046, N'AdminEdiSea', N'', N'Export By File', 1, N'/EDI/Sea/ExportRefEDI.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4047, N'AdminEdiSea', N'', N'Export By DB', 1, N'/EDI/Sea/ExportRefList.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4048, N'AdminEdiAir', N'', N'Import By File', 1, N'/EDI/Air/AirImportRefEDI.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4049, N'AdminEdiAir', N'', N'Import By DB', 1, N'/EDI/Air/AirImportRefList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4050, N'AdminEdiAir', N'', N'Export By File', 1, N'/EDI/Air/AirExportRefEDI.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4051, N'AdminEdiAir', N'', N'Export By DB', 1, N'/EDI/Air/AirExportRefList.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4052, N'AdminEdiAcc', N'', N'Ap By File', 1, N'/Edi/Acc/Ap_Edi.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4053, N'AdminEdiAcc', N'', N'Ap By DB', 1, N'/Edi/Acc/ArInvoiceList.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4054, N'AdminApQuote', N'', N'Quote Title', 1, N'/PagesQuote/Ap/ApQuoteTitle.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4055, N'AdminApQuote', N'', N'FCL Rate', 1, N'/PagesQuote/Ap/ExpFclRate_Std.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4056, N'AdminApQuote', N'', N'LCL Rate', 1, N'/PagesQuote/Ap/ExpLclRate_Std.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4057, N'AdminApQuote', N'', N'FCL Quote', 1, N'/PagesQuote/Ap/ExpFclApQuote.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4058, N'AdminApQuote', N'', N'LCL Quote', 1, N'/PagesQuote/Ap/ExpLclApQuote.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4059, N'Admin16Purchase', NULL, N'Transfer', 1, N'/Warehouse/Transfer.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4060, N'Admin16Purchase', NULL, N'Inventory', 1, N'/Warehouse/Inventory.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4061, N'Admin16Job', NULL, N'Goods Receipt', 1, N'/Warehouse/Job/DoInList.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4062, N'Admin16Job', NULL, N'Delivery Order', 1, N'/Warehouse/Job/DoOutList.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4065, N'CTMMasterData', N'', N'Location', 1, N'/PagesContTrucking/MasterData/Location.aspx', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4066, N'CTMVehicle', N'', N'Chessis', 1, N'/PagesContTrucking/MasterData/Chessis.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4072, N'CTMDailySchedule', NULL, N'KD Export', 1, N'/PagesContTrucking/Daily/KDExport.aspx?JobType=KD-EXP', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4073, N'CTMDailySchedule', NULL, N'KD Import', 1, N'/PagesContTrucking/Daily/KDExport.aspx?JobType=KD-IMP', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4074, N'CTMDailySchedule', NULL, N'FCL Export', 1, N'/PagesContTrucking/Daily/KDExport.aspx?JobType=FCL-EXP', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4075, N'CTMDailySchedule', NULL, N'FCL Import', 1, N'/PagesContTrucking/Daily/KDExport.aspx?JobType=FCL-IMP', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4076, N'CTMDailySchedule', NULL, N'Local Job', 1, N'/PagesContTrucking/Daily/KDExport.aspx?JobType=LOCAL', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4077, N'CTMDailySchedule', NULL, N'Driver On/Off', 1, N'/PagesContTrucking/Daily/DriverDaily.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4079, N'CTMVehicle', NULL, N'Fuel Log', 1, N'/PagesContTrucking/Vehicle/Fuel.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4080, N'CTMVehicle', NULL, N'Servicing Log', 1, N'/PagesContTrucking/Vehicle/Activity.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4081, N'CTMVehicle', NULL, N'Vehicle Payment', 1, N'/PagesContTrucking/Vehicle/Rent.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4082, N'CTMDailySchedule', NULL, N'Driver Cash', 1, N'/PagesContTrucking/Vehicle/DriverCash.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4086, N'CTMDailySchedule', NULL, N'Unscheduled Containers', 1, N'/PagesContTrucking/Daily/UnscheduledContainers.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4089, N'CTMJobReport', NULL, N'Job Analysis - Local', 1, N'/PagesContTrucking/Report/Analysis/JobAnalysisLocal.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4092, N'AdminJOL', N'', N'Dispatch Planner', 1, N'/Pagestpt/Local/Job/DispatchPlanner.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4100, N'CTMJobReport', NULL, N'Job Analysis - Haulier', 1, N'/PagesContTrucking/Report/Analysis/JobAnalysis.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4111, N'AdminTptLoc3', N'', N'Assigned', 1, N'/Pagestpt/Local/Job/TptJobListByStatus.aspx?typ=Assigned', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4113, N'AdminTptLoc3', N'', N'Picked', 1, N'/Pagestpt/Local/Job/TptJobListByStatus.aspx?typ=Picked', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4114, N'AdminTptLoc3', N'', N'Completed', 1, N'/Pagestpt/Local/Job/TptJobListByStatus.aspx?typ=Completed', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4115, N'AdminTptLoc3', N'', N'Canceled', 1, N'/Pagestpt/Local/Job/TptJobListByStatus.aspx?typ=Canceled', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4117, N'Admin16Mast', N'', N'TptMode', 1, N'/Warehouse/MastData/MastType.aspx?type=TptMode', 13)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4118, N'Admin16Mast', N'', N'IncoTerms', 0, N'/Warehouse/MastData/MastType.aspx?type=IncoTerms', 14)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4119, N'Admin16Mast', N'', N'InStatus', 1, N'/Warehouse/MastData/MastType.aspx?type=InStatus', 15)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4120, N'Admin16Mast', N'', N'Type', 1, N'/Warehouse/MastData/MastType.aspx?type=Priority', 16)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4121, N'Admin16Mast', N'', N'EquipmentNo', 1, N'/Warehouse/MastData/MastType.aspx?type=EquipmentNo', 17)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4122, N'Admin16Mast', N'', N'Personnel', 1, N'/Warehouse/MastData/MastType.aspx?type=Personnel', 18)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4123, N'Admin16Mast', N'', N'OutStatus', 1, N'/Warehouse/MastData/MastType.aspx?type=OutStatus', 19)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4126, N'SeaChartRPT', NULL, N'Chart by Agt', 1, N'/ReportFreightSea/Chart/SeaChartRPT_ByAgt.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4127, N'SeaChartRPT', NULL, N'Chart by Custom', 1, N'/ReportFreightSea/Chart/SeaChartRPT_ByCustom.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4128, N'Admin16Job', N'', N'PO', 0, N'/Warehouse/Job/PoList.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4129, N'Admin16Job', N'', N'SO', 0, N'/Warehouse/Job/SoList.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4135, N'HrPayroll', N'', N'Item', 1, N'/PagesHr/MasterData/PayItem.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4136, N'HrPayroll', N'', N'Quotation', 1, N'/PagesHr/Job/Quotation.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4137, N'HrPayroll', N'', N'Payroll', 1, N'/PagesHr/Job/PayrollEdit.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4138, N'HrReport', N'', N'Working Report', 1, N'', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4139, N'HrReport', N'', N'OT Report', 1, N'', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4140, N'HrReport', N'', N'Expense Report', 1, N'', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4141, N'HrReport', N'', N'Payroll Report', 1, N'', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4142, N'SeaChartRPT', NULL, N'Billing Chart', 1, N'/ReportFreightSea/Chart/BillingChartRPT.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4143, N'Admin16Job', N'', N'PO Request', 0, N'/Warehouse/Job/PoRequest.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4144, N'Admin16Report', N'', N'Stock Move', 1, N'/ReportWarehouse/Report/Stockmove.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4145, N'Admin16Report', N'', N'Fast Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=Out&by=0', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4146, N'Admin16Report', N'', N'Slow Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=Out&by=1', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4147, N'Admin16Report', N'', N'PO Fast Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=in&by=0', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4148, N'Admin16Report', N'', N'PO Slow Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=in&by=1', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4149, N'AdminWmsBilling', NULL, N'Contracts', 1, N'/Warehouse/ContractList.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4150, N'AdminWmsBilling', NULL, N'Billing Schedule', 1, N'/Warehouse/Schedule.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4151, N'AdminWmsOrders', NULL, N'Sales Orders', 1, N'Warehouse/job/Solist.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4152, N'AdminWmsOrders', NULL, N'Purchases', 1, N'Warehouse/job/Polist.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4153, N'AdminWmsOrders', NULL, N'Pending Purchase', 1, N'Warehouse/job/PoRequest.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4154, N'Admin16Report', NULL, N'Incoming Stock', 1, N'/ReportWarehouse/Report/incomingstock.aspx', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4155, N'AdminWmsOrders', NULL, N'Online Order', 1, N'Warehouse/job/Polist.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4156, N'Admin16Report', NULL, N'Unbilling Job', 0, N'/ReportWarehouse/Report/Unbilling.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4158, N'AdminWhQuote', N'', N'IMP LCL Rate', 1, N'/PagesQuote/wh/LclRate_Std.aspx?typ=WHI', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4159, N'AdminWhQuote', N'', N'EXP FCL Rate', 1, N'/PagesQuote/wh/FclRate_Std.aspx?typ=WHE', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4160, N'AdminWhQuote', N'', N'EXP LCL Rate', 1, N'/PagesQuote/wh/LclRate_Std.aspx?typ=WHE', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4164, N'Admin16Job', NULL, N'Stock Transfer', 1, N'/Warehouse/Transfer.aspx', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4165, N'AdminSalesReport', NULL, N'Stock Balance', 1, N'/ReportWarehouse/Report/StockBalance.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4166, N'AdminSalesReport', NULL, N'Stock Movement', 1, N'/ReportWarehouse/Report/StockMove.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4167, N'AdminSalesReport', NULL, N'Fast Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=Out&by=0', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4168, N'AdminSalesReport', NULL, N'Slow Move', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=Out&by=1', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4169, N'AdminPuchaseReport', NULL, N'Stock Balance', 1, N'/ReportWarehouse/Report/StockBalance.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4170, N'AdminPuchaseReport', NULL, N'Stock Movement', 1, N'/ReportWarehouse/Report/StockMove.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4171, N'AdminPuchaseReport', NULL, N'Fast Move PO', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=in&by=0', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4172, N'AdminPuchaseReport', NULL, N'Slow Move PO', 1, N'/ReportWarehouse/Report/MoveRpt.aspx?typ=in&by=1', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4173, N'AdminPuchaseReport', NULL, N'Incoming Stock', 1, N'/ReportWarehouse/Report/incomingstock.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4174, N'AdminSalesReport', NULL, N'Reorder Report', 1, N'/ReportWarehouse/Report/MinOrderQty.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4175, N'AdminPuchaseReport', NULL, N'MinOrder Qty', 1, N'/ReportWarehouse/Report/MinOrderQty.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4176, N'Admin16Report', NULL, N'MinOrder Qty', 1, N'/ReportWarehouse/Report/MinOrderQty.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4177, N'Admin16Report', NULL, N'Expiry Report', 1, N'/ReportWarehouse/Report/ExpiryStockReport.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4178, N'AdminSalesReport', NULL, N'Expiry Report', 1, N'/ReportWarehouse/Report/ExpiryStockReport.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4179, N'CTMJob', NULL, N'View By Location', 1, N'/PagesContTrucking/Job/DispatchPlanner2.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4180, N'CTMJob', NULL, N'View By Timeslot', 1, N'/PagesContTrucking/Job/DispatchPlanner3.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4181, N'CTMJob', NULL, N'View Summary', 1, N'/PagesContTrucking/Job/DispatchPlanner4.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4190, N'JobStatus', NULL, N'Customer Inquiry', 1, N'/WareHouse/Job/JobList.aspx?type=Customer Inquiry', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4191, N'JobStatus', NULL, N'Site Survey', 1, N'/WareHouse/Job/JobList.aspx?type=Site Survey', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4192, N'JobStatus', NULL, N'Costing', 1, N'/WareHouse/Job/JobList.aspx?type=Costing', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4193, N'JobStatus', NULL, N'Quotation', 1, N'/WareHouse/Job/JobList.aspx?type=Quotation', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4194, N'JobStatus', NULL, N'Job Confirmation', 1, N'/WareHouse/Job/JobList.aspx?type=Job Confirmation', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4195, N'JobStatus', NULL, N'Job Completion', 1, N'/WareHouse/Job/JobList.aspx?type=Job Completion', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4196, N'JobStatus', NULL, N'Billing', 1, N'/WareHouse/Job/JobList.aspx?type=Billing', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4197, N'JobStatus', NULL, N'Job Close', 1, N'/WareHouse/Job/JobList.aspx?type=Job Close', 7)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4198, N'CustomerAccess', NULL, N'Stock Balance', 1, N'	/ReportWarehouse/Report/StockBalance.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4199, N'CustomerAccess', NULL, N'Stock Movement', 1, N'/ReportWarehouse/Report/Stockmove.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4200, N'CustomerAccess', NULL, N'Stock Inquiry', 1, NULL, 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4201, N'CustomerAccess', NULL, N'Booking Request', 1, NULL, 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4202, N'AdminChatMasterData', NULL, N'ChatGroup', 1, N'/Mobile/Chat/MasterData/ChatGroup.aspx', 0)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4203, N'CTMJob', NULL, N'Container Inquiry', 1, N'/PagesContTrucking/Job/ContainerInquery.aspx', 8)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4204, N'CTMMasterData', NULL, N'Carpark', 1, N'/PagesContTrucking/MasterData/Carpark.aspx', 9)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4205, N'Admin16Report', NULL, N'Unbilled Handling Charges', 1, N'/ReportWarehouse/Report/HandlingReport.aspx', 21)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4206, N'Admin16Report', NULL, N'Unbilled Storage Charges', 1, N'/ReportWarehouse/Report/StorageReport.aspx', 22)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4207, N'AdminWhsMat', NULL, N'CM Material Requisition', 1, N'/Warehouse/cm/mr_clm.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4208, N'AdminWhsMat', NULL, N'CM Material Consumption', 1, N'/Warehouse/cm/ma_clm.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4209, N'AdminWhsMat', NULL, N'CM Material Acquisition', 1, N'/Warehouse/cm/mc_clm.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4210, N'AdminWhsMat', NULL, N'CM Material Setup', 1, N'/Warehouse/MastData/MaterialClm.aspx', 6)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4211, N'AdminWhsStk', NULL, N'CM Stock Balance', 1, N'/Warehouse/Job/JobScheduleWhs.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4212, N'AdminWhsStk', NULL, N'CM Stock Analysis', 1, N'/Warehouse/cm/sa_clm.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (4215, N'CTMMasterData', NULL, N'Packing Lot', 1, N'/PagesContTrucking/MasterData/PackingLot.aspx', 9)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5213, N'CTMMasterData', N'', N'Zone', 1, N'/PagesContTrucking/MasterData/ParkingZone.aspx', 11)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5214, N'AdminPayroll', NULL, N'Payroll Item', 1, N'/Modules/Hr/Master/PayItem.aspx', 1)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5215, N'AdminPayroll', NULL, N'Payroll Setup', 1, N'/Modules/Hr/Job/Quotation.aspx', 2)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5216, N'AdminPayroll', NULL, N'Payroll Record', 1, N'/Modules/Hr/Job/PayrollRecord.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5217, N'AdminPayroll', NULL, N'Payroll Process', 1, N'/Modules/Hr/Job/PayrollEdit.aspx', 3)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5218, N'AdminHrReport', NULL, N'Empolyee Print', 1, N'/Modules/Hr/Report/PrintEmployee.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5219, N'AdminHrReport', NULL, N'Payroll Print', 1, N'/Modules/Hr/Report/PrintPayroll.aspx', 4)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5220, N'HrMastData', NULL, N'Group', 1, N'/Modules/Hr/Master/HrGroup.aspx', 5)
GO
INSERT [dbo].[Menu3] ([SequenceId], [MasterId], [SubId], [Name], [IsActive], [Link], [SortIndex]) VALUES (5221, N'HrMastData', NULL, N'Percentage', 1, N'/Modules/Hr/Master/HrRate.aspx', 6)
GO
SET IDENTITY_INSERT [dbo].[Menu3] OFF
GO
