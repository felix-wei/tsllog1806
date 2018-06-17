alter table XAApPayment
 add MastRefNo nvarchar(100) null,
     JobRefNo nvarchar(100) null

	 alter table XAApPaymentDet
 add MastRefNo nvarchar(100) null,
     JobRefNo nvarchar(100) null