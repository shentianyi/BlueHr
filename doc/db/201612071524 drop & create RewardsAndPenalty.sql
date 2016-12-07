USE [BlueHr]
GO

/****** Object:  Table [dbo].[RewardsAndPenalty]    Script Date: 12/07/2016 15:24:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RewardsAndPenalty]') AND type in (N'U'))
DROP TABLE [dbo].[RewardsAndPenalty]
GO

USE [BlueHr]
GO

/****** Object:  Table [dbo].[RewardsAndPenalty]    Script Date: 12/07/2016 15:24:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RewardsAndPenalty](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[type] [int] NULL,
	[project] [varchar](200) NULL,
	[description] [text] NULL,
	[createdAt] [datetime] NULL,
	[createdUserId] [int] NULL,
	[approvalUserId] [int] NULL,
	[approvalStatus] [varchar](50) NULL,
	[approvalRemark] [text] NULL,
	[approvalAt] [datetime] NULL,
 CONSTRAINT [PK_RewardsAndPenalty] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


