USE [BlueHr]
GO

alter table dbo.Staff add parentStaffNr varchar(200);
alter table dbo.JobTitle add IsRevoked bit;
update  JobTitle set IsRevoked=0;