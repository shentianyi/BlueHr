USE [BlueHr]
GO

/****** Object:  Table [dbo].[MessageRecord]    Script Date: 09/06/2016 17:53:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MessageRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NULL,
	[operatorId] [int] NULL,
	[messageType] [int] NULL,
	[createdAt] [datetime] NULL,
	[text] [text] NULL,
	[isRead] [bit] NULL,
	[isHandled] [bit] NULL,
	[isUrl] [bit] NULL,
	[url] [text] NULL,
 CONSTRAINT [PK_MessageRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MessageRecord] ADD  CONSTRAINT [DF_MessageRecord_isRead]  DEFAULT ((0)) FOR [isRead]
GO

ALTER TABLE [dbo].[MessageRecord] ADD  CONSTRAINT [DF_MessageRecord_isHandled]  DEFAULT ((0)) FOR [isHandled]
GO

ALTER TABLE [dbo].[MessageRecord] ADD  CONSTRAINT [DF_MessageRecord_isUrl]  DEFAULT ((0)) FOR [isUrl]
GO


