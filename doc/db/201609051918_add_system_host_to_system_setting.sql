USE [BlueHr]
GO

alter table SystemSetting add [systemHost] varchar(50)
go

alter table SystemSetting add [emaiSMTPHost] varchar(50)
go

alter table SystemSetting add [emailUser] varchar(50)
go

alter table SystemSetting add [emailPwd] varchar(50)
go


alter table SystemSetting add [emailAddress] varchar(50)
go
