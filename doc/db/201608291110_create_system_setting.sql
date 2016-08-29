USE [BlueHr]
GO

/****** Object:  Table [dbo].[SystemSetting]    Script Date: 08/29/2016 11:06:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemSetting](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[daysBeforeAlertStaffGoFull] [float] NULL,
	[goFullAlertMails] [text] NULL,
	[unCertifacteAlertMails] [text] NULL,
	[attendanceExceptionAlertMails] [text] NULL,
	[repeatAttendanceRecordTime] [float] NULL,
	[validAttendanceRecordTime] [float] NULL,
	[lateExceptionTime] [float] NULL,
	[earlyLeaveExceptionTime] [float] NULL,
 CONSTRAINT [PK_SystemSetting] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

