USE [BlueHr]
GO

/****** Object:  Table [dbo].[AttendanceRecordDetail]    Script Date: 08/26/2016 13:47:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AttendanceRecordDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[recordAt] [datetime] NOT NULL,
	[createdAt] [date] NOT NULL,
	[soureType] [varchar](200) NOT NULL,
	[isCalculated] [bit] NOT NULL,
 CONSTRAINT [PK_AttendanceRecordDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AttendanceRecordDetail]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceRecordDetail_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO

ALTER TABLE [dbo].[AttendanceRecordDetail] CHECK CONSTRAINT [FK_AttendanceRecordDetail_Staff]
GO

ALTER TABLE [dbo].[AttendanceRecordDetail] ADD  CONSTRAINT [DF_AttendanceRecordDetail_isCalculated]  DEFAULT ((0)) FOR [isCalculated]
GO

