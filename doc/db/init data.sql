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
           ,[earlyLeaveExceptionTime])
     VALUES
           (5,	NULL,	NULL,	NULL,	20,	3,	30,	30)
GO


