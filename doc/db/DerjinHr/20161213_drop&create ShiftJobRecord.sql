USE [DerjinHr]
GO

/****** Object:  Table [dbo].[ShiftJobRecord]    Script Date: 12/13/2016 17:32:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShiftJobRecord]') AND type in (N'U'))
DROP TABLE [dbo].[ShiftJobRecord]
GO

USE [DerjinHr]
GO

/****** Object:  Table [dbo].[ShiftJobRecord]    Script Date: 12/13/2016 17:32:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ShiftJobRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[beforeCompanyId] [int] NULL,
	[beforeDepartmentId] [int] NULL,
	[beforeJobId] [int] NULL,
	[aftereCompanyId] [int] NULL,
	[aftereDepartmentId] [int] NULL,
	[aftereJobId] [int] NULL,
	[userId] [int] NOT NULL,
	[createdAt] [datetime] NOT NULL,
	[remark] [text] NULL,
	[approvalUserId] [int] NULL,
	[approvalAt] [datetime] NULL,
	[approvalStatus] [varchar](50) NULL,
	[approvalRemark] [text] NULL,
 CONSTRAINT [PK_ShiftJobRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


