USE [BlueHr]
GO

/****** Object:  Table [dbo].[Recruit]    Script Date: 12/02/2016 17:13:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Recruit]') AND type in (N'U'))
DROP TABLE [dbo].[Recruit]
GO

USE [BlueHr]
GO

/****** Object:  Table [dbo].[Recruit]    Script Date: 12/02/2016 17:13:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Recruit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[requirement] [varchar](200) NULL,
	[companyId] [int] NULL,
	[departmentId] [int] NULL,
	[amount] [int] NULL,
	[createAt] [datetime] NULL,
	[requirementAt] [datetime] NULL,
	[requirementMan] [varchar](50) NULL,
	[status] [varchar](200) NULL,
	[auditRecord] [varchar](200) NULL,
	[auditView] [varchar](200) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [BlueHr]
GO

/****** Object:  Table [dbo].[RewardsAndPenalty]    Script Date: 12/02/2016 17:13:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RewardsAndPenalty]') AND type in (N'U'))
DROP TABLE [dbo].[RewardsAndPenalty]
GO

USE [BlueHr]
GO

/****** Object:  Table [dbo].[RewardsAndPenalty]    Script Date: 12/02/2016 17:13:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RewardsAndPenalty](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[staffName] [varchar](50) NULL,
	[staffSex] [int] NULL,
	[companyId] [int] NULL,
	[departmentId] [int] NULL,
	[type] [int] NULL,
	[project] [varchar](200) NULL,
	[description] [text] NULL,
	[createAt] [datetime] NULL,
	[userId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


