USE [BlueHr]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FullMemberRecord_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[FullMemberRecord]'))
ALTER TABLE [dbo].[FullMemberRecord] DROP CONSTRAINT [FK_FullMemberRecord_Staff]
GO

USE [BlueHr]
GO

/****** Object:  Table [dbo].[FullMemberRecord]    Script Date: 12/07/2016 16:58:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FullMemberRecord]') AND type in (N'U'))
DROP TABLE [dbo].[FullMemberRecord]
GO

USE [BlueHr]
GO

/****** Object:  Table [dbo].[FullMemberRecord]    Script Date: 12/07/2016 16:58:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FullMemberRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[checkScore] [float] NULL,
	[remark] [text] NULL,
	[approvalUserId] [int] NULL,
	[approvalAt] [datetime] NULL,
	[approvalStatus] [varchar](50) NULL,
	[approvalRemark] [text] NULL,
	[createdAt] [datetime] NULL,
	[createdUserId] [int] NULL,
	[isPassCheck] [bit] NOT NULL,
 CONSTRAINT [PK_FullMemberRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[FullMemberRecord]  WITH CHECK ADD  CONSTRAINT [FK_FullMemberRecord_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO

ALTER TABLE [dbo].[FullMemberRecord] CHECK CONSTRAINT [FK_FullMemberRecord_Staff]
GO


