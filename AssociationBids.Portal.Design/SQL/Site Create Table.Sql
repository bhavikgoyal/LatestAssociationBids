
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PaymentModel]
(
   	 [PaymentModelID] [uniqueidentifier] NOT NULL,	
	 [CardNumber] [nvarchar](max) NULL
	,[StripeTokenID] [nvarchar](max) NULL
    ,[AddedOn] datetime
	,[LastModificationTime] datetime,
 CONSTRAINT [PK_PaymentModel] PRIMARY KEY CLUSTERED 
(
	[PaymentModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


