use BlueHr
alter table dbo.Staff add parentStaffNr varchar(200);
alter table dbo.JobTitle add IsRevoked bit;
UPDATE JobTitle SET IsRevoked=0;