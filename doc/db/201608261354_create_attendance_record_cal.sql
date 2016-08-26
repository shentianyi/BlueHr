USE [BlueHr]
GO

/****** Object:  Table [dbo].[AttendanceRecordCal]    Script Date: 08/26/2016 14:28:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AttendanceRecordCal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[oriWorkingHour] [float] NOT NULL,
	[attendanceDate] [datetime] NOT NULL,
	[actWorkingHour] [float] NOT NULL,
	[remark] [text] NULL,
	[createdAt] [datetime] NOT NULL,
 CONSTRAINT [PK_AttendanceRecordCal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AttendanceRecordCal]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceRecordCal_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO

ALTER TABLE [dbo].[AttendanceRecordCal] CHECK CONSTRAINT [FK_AttendanceRecordCal_Staff]
GO

