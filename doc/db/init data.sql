use BlueHr
go

-- ����Ĭ�ϵ�֤�����
insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('���֤','Ա�������֤��ϵͳĬ��֤����',1,0,100);


insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('ְҵ֤��','Ա����ְҵ֤�飬ϵͳĬ��֤����',1,0,200);


insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('����֤','Ա���Ľ���֤��ϵͳĬ��֤����',1,0,300);


-- ����ϵͳ����

INSERT INTO [BlueHr].[dbo].[SystemSetting]
           ([daysBeforeAlertStaffGoFull]
           ,[goFullAlertMails]
           ,[unCertifacteAlertMails]
           ,[attendanceExceptionAlertMails]
           ,[repeatAttendanceRecordTime]
           ,[validAttendanceRecordTime]
           ,[lateExceptionTime]
           ,[earlyLeaveExceptionTime],
            [systemHost])
     VALUES
           (5,	NULL,	NULL,	NULL,	20,	2,	30,	30, 'http://localhost/')
GO


-- �����Զ�����
-- ÿ��23:30������QuartzJob, ����ת������
INSERT INTO [BlueHr].[dbo].[QuartzJob]
           ([cronSchedule]
           ,[params]
           ,[jobType])
     VALUES
           ('0 30 23 * * ? *'
           ,null
           ,100)
GO


-- �����Ӱ�����


INSERT INTO [BlueHr].[dbo].[ExtraWorkType]
           ([name])
     VALUES
           ('��ʱ�Ӱ�')
GO

INSERT INTO [BlueHr].[dbo].[ExtraWorkType]
           ([name])
     VALUES
           ('˫�ݼӰ�')
GO


INSERT INTO [BlueHr].[dbo].[ExtraWorkType]
           ([name])
     VALUES
           ('�ڼ��ռӰ�')
GO


INSERT INTO [BlueHr].[dbo].[ExtraWorkType]
           ([name])
     VALUES
           ('�����Ӱ�')
GO


-- �����û�

INSERT INTO [BlueHr].[dbo].[User]
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



