CREATE TABLE [dbo].[Catalog_PackagingInquiry](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductSku] [nvarchar](450) NULL,
	[ProductName] [nvarchar](450) NULL,
	[CompanyName] [nvarchar](450) NULL,
	[ContactName] [nvarchar](450) NULL,
	[ContactEmail] [nvarchar](450) NULL,
	[BoxDimensions] [nvarchar](max) NULL,
	[QuantityNeeded] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Catalog_PackagingInquiry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
