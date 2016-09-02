USE [BlueHr]
GO

alter table AttendanceRecordCal add [isExceptionHandled] bit
go

/****** Object:  Default [DF_AttendanceRecordCal_isExceptionHandled]    Script Date: 09/02/2016 13:33:58 ******/
ALTER TABLE [dbo].[AttendanceRecordCal] ADD  CONSTRAINT [DF_AttendanceRecordCal_isExceptionHandled]  DEFAULT ((1)) FOR [isExceptionHandled]
GO
