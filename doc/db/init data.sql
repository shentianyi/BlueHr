use BlueHr
go

-- 建立默认的证照类别
insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('身份证','员工的身份证，系统默认证件照',1,0,100);


insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('职业证书','员工的职业证书，系统默认证件照',1,0,200);


insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('健康证','员工的健康证，系统默认证件照',1,0,300);


-- 建立系统设置

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


