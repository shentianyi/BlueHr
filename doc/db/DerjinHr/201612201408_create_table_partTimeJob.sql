USE [DerjinHr]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PartTimeJob_isDelete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PartTimeJob] DROP CONSTRAINT [DF_PartTimeJob_isDelete]
END

GO

USE [DerjinHr]
GO

/****** Object:  Table [dbo].[PartTimeJob]    Script Date: 12/20/2016 14:49:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PartTimeJob]') AND type in (N'U'))
DROP TABLE [dbo].[PartTimeJob]
GO

USE [DerjinHr]
GO

/****** Object:  Table [dbo].[PartTimeJob]    Script Date: 12/20/2016 14:49:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PartTimeJob](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](50) NOT NULL,
	[companyId] [int] NULL,
	[departmentId] [int] NULL,
	[jobTitleId] [int] NULL,
	[startTime] [datetime] NULL,
	[endTime] [datetime] NULL,
	[isDelete] [bit] NULL,
 CONSTRAINT [PK_PartTimeJob] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PartTimeJob] ADD  CONSTRAINT [DF_PartTimeJob_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO


