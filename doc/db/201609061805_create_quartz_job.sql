USE [BlueHr]
GO

/****** Object:  Table [dbo].[QuartzJob]    Script Date: 09/06/2016 18:07:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QuartzJob](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cronSchedule] [text] NULL,
	[params] [text] NULL,
	[jobType] [int] NULL,
 CONSTRAINT [PK_QuartzJob] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


