USE [BlueHr]
GO

/****** Object:  Table [dbo].[TaskRound]    Script Date: 09/01/2016 23:04:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TaskRound](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[taskStatus] [int] NOT NULL,
	[createdAt] [datetime] NOT NULL,
	[taskType] [int] NOT NULL,
	[finishAt] [datetime] NULL,
	[result] [text] NULL,
	[uuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TaskRound] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


