USE [BlueHrV1]
GO

/****** Object:  Table [dbo].[PersonSchedule]    Script Date: 12/07/2016 00:57:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PersonSchedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[startTime] [datetime] NOT NULL,
	[endTime] [datetime] NOT NULL,
	[category] [nvarchar](50) NULL,
	[subject] [nvarchar](max) NULL,
	[significance] [nvarchar](50) NULL,
	[context] [text] NOT NULL,
	[createdAt] [datetime] NULL,
	[isDeleted] [bit] NULL,
 CONSTRAINT [PK_PersonSchedule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


