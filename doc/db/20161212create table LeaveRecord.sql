USE [BlueHr]
GO

/****** Object:  Table [dbo].[LeaveRecord]    Script Date: 12/12/2016 10:19:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LeaveRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[leaveStart] [datetime] NULL,
	[leaveEnd] [datetime] NULL,
	[remark] [text] NULL,
	[createdAt] [datetime] NULL,
	[userId] [int] NULL,
	[approvalUserId] [int] NULL,
	[approvalAt] [datetime] NULL,
	[approvalStatus] [varchar](50) NULL,
	[approvalRemark] [text] NULL,
	[isDelete] [bit] NULL,
 CONSTRAINT [PK_LeaveRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


