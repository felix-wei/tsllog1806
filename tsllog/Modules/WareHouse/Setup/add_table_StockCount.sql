
/****** Object:  Table [dbo].[StockCount]    Script Date: 2015/10/22 9:37:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockCount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartyId] [nvarchar](100) NULL,
	[PartyName] [nvarchar](200) NULL,
	[PartyAdd] [nvarchar](500) NULL,
	[DoDate] [datetime] NULL,
	[StockDate] [datetime] NULL,
	[WareHouseId] [nvarchar](50) NULL,
	[Remark] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[UpdateDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StockCountDet]    Script Date: 2015/10/22 9:37:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockCountDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Product] [nvarchar](100) NULL,
	[LotNo] [nvarchar](100) NULL,
	[Description] [nvarchar](200) NULL,
	[Remark] [nvarchar](200) NULL,
	[PalletNo] [nvarchar](200) NULL,
	[Qty1] [decimal](10, 4) NULL,
	[Qty2] [decimal](10, 4) NULL,
	[Qty3] [decimal](10, 4) NULL,
	[NewQty] [decimal](10, 4) NULL,
	[Price] [decimal](10, 4) NULL,
	[NewPrice] [decimal](10, 4) NULL,
	[Packing] [nvarchar](200) NULL,
	[Location] [nvarchar](200) NULL,
	[Uom] [nvarchar](50) NULL,
	[Att1] [nvarchar](100) NULL,
	[Att2] [nvarchar](100) NULL,
	[Att3] [nvarchar](100) NULL,
	[Att4] [nvarchar](100) NULL,
	[Att5] [nvarchar](100) NULL,
	[Att6] [nvarchar](100) NULL,
	[ExpiryDate] [datetime] NULL,
	[RefNo] [nvarchar](200) NULL,
	[GrossWeight] [decimal](10, 4) NULL,
	[NettWeght] [decimal](10, 4) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
