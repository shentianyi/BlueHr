USE [BlueHr]
GO
/****** Object:  Table [dbo].[User]    Script Date: 09/12/2016 21:24:27 ******/
DROP TABLE [dbo].[User]
GO


USE [BlueHr]
GO

/****** Object:  Table [dbo].[User]    Script Date: 09/12/2016 14:30:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[pwd] [varchar](255) NOT NULL,
	[isLocked] [bit] NULL,
	[role] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_isLocked]  DEFAULT ((0)) FOR [isLocked]
GO


