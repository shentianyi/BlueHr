use BlueHrV1
go

-- ����Ĭ�ϵ�֤�����

delete from CertificateType
insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('���֤','Ա�������֤��ϵͳĬ��֤����',1,0,100);


insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('ְҵ֤��','Ա����ְҵ֤�飬ϵͳĬ��֤����',1,0,200);


insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('����֤','Ա���Ľ���֤��ϵͳĬ��֤����',1,0,300);


-- ����ϵͳ����

INSERT INTO [BlueHrV1].[dbo].[SystemSetting]
           ([daysBeforeAlertStaffGoFull]
           ,[goFullAlertMails]
           ,[unCertifacteAlertMails]
           ,[attendanceExceptionAlertMails]
           ,[repeatAttendanceRecordTime]
           ,[validAttendanceRecordTime]
           ,[lateExceptionTime]
           ,[earlyLeaveExceptionTime]
           ,[systemHost]
           ,[emaiSMTPHost]
           ,[emailUser]
           ,[emailPwd]
           ,[emailAddress]
           ,[defaultTrailMonth])
     VALUES
           (5,	NULL,	NULL,	NULL, 2,	50,	30,	30, 'http://localhost/', NULL, NULL, NULL, NULL, 2)
GO

-- �����Զ�����
-- ÿ��23:30������QuartzJob, ����ת������
INSERT INTO [BlueHrV1].[dbo].[QuartzJob]
           ([cronSchedule]
           ,[params]
           ,[jobType])
     VALUES
           ('0 30 23 * * ? *'
           ,null
           ,100)
GO


-- �����û�

INSERT INTO [BlueHrV1].[dbo].[User]
           ([name]
           ,[email]
           ,[pwd]
           ,[isLocked]
           ,[role])
     VALUES
           ('admin'
           ,'admin@ci.com'
           ,'123456@'
           ,0
           ,100)
GO

use BlueHrV1
go
delete from AbsenceType;
insert into AbsenceType(code,name,systemCode) values('��','�Ű�',100);
insert into AbsenceType(code,name,systemCode) values('��','�¼�',200);
insert into AbsenceType(code,name,systemCode) values('��','����',300);
insert into AbsenceType(code,name,systemCode) values('��','����',400);
insert into AbsenceType(code,name,systemCode) values('ɥ','ɥ��',500);
insert into AbsenceType(code,name,systemCode) values('��','����',600);
insert into AbsenceType(code,name,systemCode) values('��','����',700);
insert into AbsenceType(code,name,systemCode) values('��','���',800);
--insert into AbsenceType(code,name,systemCode) values('��','�½�',900);
insert into AbsenceType(code,name,systemCode) values('��','����',1000);
--insert into AbsenceType(code,name,systemCode) values('��','��ְ',1100);


use BlueHrV1
go

delete from ExtraWorkType;
insert into ExtraWorkType(name,systemCode) values('��ʱ�Ӱ�',100);
insert into ExtraWorkType(name,systemCode) values('˫�ݼӰ�',200);
insert into ExtraWorkType(name,systemCode) values('�ڼ��ռӰ�',300);

