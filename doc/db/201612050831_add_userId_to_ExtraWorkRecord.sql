USE [BlueHrV2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ExtraWorkRecord_ExtraWorkType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExtraWorkRecord]'))
ALTER TABLE [dbo].[ExtraWorkRecord] DROP CONSTRAINT [FK_ExtraWorkRecord_ExtraWorkType]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ExtraWorkRecord_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExtraWorkRecord]'))
ALTER TABLE [dbo].[ExtraWorkRecord] DROP CONSTRAINT [FK_ExtraWorkRecord_Staff]
GO

USE [BlueHrV2]
GO

/****** Object:  Table [dbo].[ExtraWorkRecord]    Script Date: 12/05/2016 08:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExtraWorkRecord]') AND type in (N'U'))
DROP TABLE [dbo].[ExtraWorkRecord]
GO

USE [BlueHrV2]
GO

/****** Object:  Table [dbo].[ExtraWorkRecord]    Script Date: 12/05/2016 08:30:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ExtraWorkRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[extraWorkTypeId] [int] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[duration] [float] NOT NULL,
	[durationType] [int] NOT NULL,
	[otReason] [varchar](255) NULL,
	[otTime] [datetime] NULL,
	[startHour] [time](7) NULL,
	[endHour] [time](7) NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_ExtraWorkRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ExtraWorkRecord]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecord_ExtraWorkType] FOREIGN KEY([extraWorkTypeId])
REFERENCES [dbo].[ExtraWorkType] ([id])
GO

ALTER TABLE [dbo].[ExtraWorkRecord] CHECK CONSTRAINT [FK_ExtraWorkRecord_ExtraWorkType]
GO

ALTER TABLE [dbo].[ExtraWorkRecord]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecord_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO

ALTER TABLE [dbo].[ExtraWorkRecord] CHECK CONSTRAINT [FK_ExtraWorkRecord_Staff]
GO


