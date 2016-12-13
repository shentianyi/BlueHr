USE [DerjinHr]
GO

/****** Object:  Table [dbo].[ShiftJobRecord]    Script Date: 12/13/2016 16:34:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ShiftJobRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[beforCompanyId] [int] NULL,
	[beforDepartmentId] [int] NULL,
	[beforJobId] [int] NULL,
	[afterCompanyId] [int] NULL,
	[afterDepartmentId] [int] NULL,
	[afterJobId] [int] NULL,
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


