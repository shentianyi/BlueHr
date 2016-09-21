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
           ,[earlyLeaveExceptionTime],
            [systemHost])
     VALUES
           (5,	NULL,	NULL,	NULL,	20,	2,	30,	30, 'http://localhost/')
GO


-- 建立自动任务
-- 每天23:30分运行QuartzJob, 计算转正提醒
INSERT INTO [BlueHr].[dbo].[QuartzJob]
           ([cronSchedule]
           ,[params]
           ,[jobType])
     VALUES
           ('0 30 23 * * ? *'
           ,null
           ,100)
GO


-- 建立加班类型


INSERT INTO [BlueHr].[dbo].[ExtraWorkType]
           ([name])
     VALUES
           ('延时加班')
GO

INSERT INTO [BlueHr].[dbo].[ExtraWorkType]
           ([name])
     VALUES
           ('双休加班')
GO


INSERT INTO [BlueHr].[dbo].[ExtraWorkType]
           ([name])
     VALUES
           ('节假日加班')
GO


INSERT INTO [BlueHr].[dbo].[ExtraWorkType]
           ([name])
     VALUES
           ('其它加班')
GO


-- 建立用户

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

use BlueHr
go

insert into AbsenceType(code,name,systemCode) values('放','放班',100);
insert into AbsenceType(code,name,systemCode) values('事','事假',200);
insert into AbsenceType(code,name,systemCode) values('病','病假',300);
insert into AbsenceType(code,name,systemCode) values('产','产假',400);
insert into AbsenceType(code,name,systemCode) values('丧','丧假',500);
insert into AbsenceType(code,name,systemCode) values('轮','轮休',600);
insert into AbsenceType(code,name,systemCode) values('公','公休',700);
insert into AbsenceType(code,name,systemCode) values('年','年',800);
insert into AbsenceType(code,name,systemCode) values('新','新进',900);
insert into AbsenceType(code,name,systemCode) values('旷','旷工',1000);
insert into AbsenceType(code,name,systemCode) values('离','离职',1100);


use BlueHr
go

insert into ExtraWorkType(name,systemCode) values('延时加班',100);
insert into ExtraWorkType(name,systemCode) values('双休加班',200);
insert into ExtraWorkType(name,systemCode) values('节假日加班',300);
insert into ExtraWorkType(name,systemCode) values('其它加班',400);

