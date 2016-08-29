USE [BlueHr]
GO

alter table AttendanceRecordCal add [isException] bit;

alter table AttendanceRecordCal add [exceptionCodes] text;

go

