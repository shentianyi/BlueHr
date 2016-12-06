USE [BlueHrV2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResignRecord_ResignType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResignRecord]'))
ALTER TABLE [dbo].[ResignRecord] DROP CONSTRAINT [FK_ResignRecord_ResignType]
GO

USE [BlueHrV2]
GO

/****** Object:  Table [dbo].[ResignRecord]    Script Date: 12/06/2016 10:41:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResignRecord]') AND type in (N'U'))
DROP TABLE [dbo].[ResignRecord]
GO

USE [BlueHrV2]
GO

/****** Object:  Table [dbo].[ResignRecord]    Script Date: 12/06/2016 10:41:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ResignRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[resignTypeId] [int] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[resignAt] [datetime] NOT NULL,
	[resignEffectiveAt] [datetime] NOT NULL,
	[resignReason] [text] NULL,
	[resignCheckUserId] [int] NOT NULL,
	[resignChecker] [varchar](50) NOT NULL,
	[approvalAt] [datetime] NULL,
	[approvalStatus] [varchar](50) NULL,
	[approvalRemark] [text] NULL,
	[createdAt] [datetime] NULL,
	[remark] [text] NULL,
	[userId] [int] NULL,
 CONSTRAINT [PK_ResignRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ResignRecord]  WITH CHECK ADD  CONSTRAINT [FK_ResignRecord_ResignType] FOREIGN KEY([resignTypeId])
REFERENCES [dbo].[ResignType] ([id])
GO

ALTER TABLE [dbo].[ResignRecord] CHECK CONSTRAINT [FK_ResignRecord_ResignType]
GO


