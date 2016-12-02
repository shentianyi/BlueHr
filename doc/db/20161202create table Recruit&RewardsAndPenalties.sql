USE [BlueHr]
GO

/****** Object:  Table [dbo].[RewardsAndPenalties]    Script Date: 12/02/2016 15:33:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RewardsAndPenalties](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nr] [varchar](200) NOT NULL,
	[name] [varchar](50) NULL,
	[sex] [int] NULL,
	[companyId] [int] NULL,
	[departmentId] [int] NULL,
	[type] [int] NULL,
	[project] [varchar](50) NULL,
	[description] [varchar](200) NULL,
	[datetime] [datetime] NULL,
	[approver] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [BlueHr]
GO

/****** Object:  Table [dbo].[Recruit]    Script Date: 12/02/2016 15:33:41 ******/
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


