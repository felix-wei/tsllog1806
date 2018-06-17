alter table [dbo].[CTM_Job]
add IsTrucking nvarchar(20) null,
    IsWarehouse nvarchar(20) null,
	IsFreight nvarchar(20) null,
	IsLocal nvarchar(20) null,
	IsAdhoc nvarchar(20) null,
	IsOthers nvarchar(20) null



alter table [dbo].[Wh_DoDet3]
add ContainerStatus nvarchar(100) null,
    JobStart datetime null,
	JobEnd datetime null


alter table [dbo].[wh_attachment]
add ContainerNo nvarchar(100) null