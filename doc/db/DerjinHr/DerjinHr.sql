USE [master]
GO

/****** Object:  Database [DerjinHr]    Script Date: 12/12/2016 10:30:40 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'DerjinHr')
DROP DATABASE [DerjinHr]
GO

/****** Object:  Database [DerjinHr]    Script Date: 12/12/2016 10:05:07 ******/
CREATE DATABASE [DerjinHr]

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DerjinHr].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DerjinHr] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DerjinHr] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DerjinHr] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DerjinHr] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DerjinHr] SET ARITHABORT OFF
GO
ALTER DATABASE [DerjinHr] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [DerjinHr] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [DerjinHr] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DerjinHr] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DerjinHr] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DerjinHr] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [DerjinHr] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DerjinHr] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DerjinHr] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DerjinHr] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DerjinHr] SET  DISABLE_BROKER
GO
ALTER DATABASE [DerjinHr] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DerjinHr] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DerjinHr] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DerjinHr] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DerjinHr] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DerjinHr] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [DerjinHr] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [DerjinHr] SET  READ_WRITE
GO
ALTER DATABASE [DerjinHr] SET RECOVERY SIMPLE
GO
ALTER DATABASE [DerjinHr] SET  MULTI_USER
GO
ALTER DATABASE [DerjinHr] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DerjinHr] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'DerjinHr', N'ON'
GO
USE [DerjinHr]
GO
/****** Object:  Table [dbo].[StaffType]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StaffType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
 CONSTRAINT [PK_StaffType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExtraWorkType]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExtraWorkType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
	[systemCode] [int] NULL,
 CONSTRAINT [PK_ExtraWorkType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InsureType]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InsureType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
 CONSTRAINT [PK_InsureType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MessageRecord]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MessageRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NULL,
	[operatorId] [int] NULL,
	[messageType] [int] NULL,
	[createdAt] [datetime] NULL,
	[text] [text] NULL,
	[isRead] [bit] NULL,
	[isHandled] [bit] NULL,
	[uniqString] [varchar](255) NULL,
	[messageCategory] [int] NULL,
 CONSTRAINT [PK_MessageRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JobTitle]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JobTitle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
	[IsRevoked] [bit] NULL,
 CONSTRAINT [PK_JobTitle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AbsenceType]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AbsenceType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](50) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
	[systemCode] [int] NULL,
 CONSTRAINT [PK_AbsenceType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DegreeType]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DegreeType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
 CONSTRAINT [PK_DegreeType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Company]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
	[address] [text] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CertificateType]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CertificateType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
	[isSystem] [bit] NOT NULL,
	[isNecessary] [bit] NOT NULL,
	[systemCode] [int] NULL,
 CONSTRAINT [PK_CertificateType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemSetting]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemSetting](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[daysBeforeAlertStaffGoFull] [float] NULL,
	[goFullAlertMails] [text] NULL,
	[unCertifacteAlertMails] [text] NULL,
	[attendanceExceptionAlertMails] [text] NULL,
	[repeatAttendanceRecordTime] [float] NULL,
	[validAttendanceRecordTime] [float] NULL,
	[lateExceptionTime] [float] NULL,
	[earlyLeaveExceptionTime] [float] NULL,
	[systemHost] [varchar](50) NULL,
	[emaiSMTPHost] [varchar](50) NULL,
	[emailUser] [varchar](50) NULL,
	[emailPwd] [varchar](50) NULL,
	[emailAddress] [varchar](50) NULL,
	[defaultTrailMonth] [int] NOT NULL,
 CONSTRAINT [PK_SystemSetting] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WorkAndRest]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WorkAndRest](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateAt] [datetime] NULL,
	[dateType] [int] NULL,
	[remark] [varchar](255) NULL,
 CONSTRAINT [PK_WorkAndRest] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[pwd] [varchar](255) NOT NULL,
	[isLocked] [bit] NULL,
	[role] [int] NULL,
	[pwdSalt] [varchar](200) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaskRound]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskRound](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[taskStatus] [int] NOT NULL,
	[createdAt] [datetime] NOT NULL,
	[taskType] [int] NOT NULL,
	[finishAt] [datetime] NULL,
	[result] [text] NULL,
	[uuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TaskRound] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRole]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRole](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[remarks] [nvarchar](500) NULL,
 CONSTRAINT [PK_SysRole] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysAuthorization]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysAuthorization](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[controlName] [nvarchar](50) NULL,
	[actionName] [nvarchar](50) NULL,
	[parentId] [int] NULL,
	[funCode] [nvarchar](50) NULL,
	[isDelete] [int] NULL,
	[remarks] [nvarchar](200) NULL,
 CONSTRAINT [PK_SysAuthorization] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recruit]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Recruit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[requirement] [varchar](200) NULL,
	[companyId] [int] NULL,
	[departmentId] [int] NULL,
	[amount] [int] NULL,
	[createAt] [datetime] NULL,
	[requirementAt] [datetime] NULL,
	[requirementMan] [varchar](50) NULL,
	[status] [varchar](200) NULL,
	[auditRecord] [varchar](200) NULL,
	[auditView] [varchar](200) NULL,
 CONSTRAINT [PK_Recruit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuartzJob]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuartzJob](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cronSchedule] [text] NULL,
	[params] [text] NULL,
	[jobType] [int] NULL,
 CONSTRAINT [PK_QuartzJob] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonSchedule]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonSchedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[startTime] [datetime] NOT NULL,
	[endTime] [datetime] NOT NULL,
	[category] [nvarchar](50) NULL,
	[subject] [nvarchar](max) NULL,
	[significance] [nvarchar](50) NULL,
	[context] [text] NOT NULL,
	[createdAt] [datetime] NULL,
	[isDeleted] [bit] NULL,
 CONSTRAINT [PK_PersonSchedule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shift]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shift](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[startAt] [time](7) NOT NULL,
	[endAt] [time](7) NOT NULL,
	[shiftType] [int] NOT NULL,
	[remark] [varchar](255) NULL,
 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RewardsAndPenalty]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RewardsAndPenalty](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[type] [int] NULL,
	[project] [varchar](200) NULL,
	[description] [text] NULL,
	[createdAt] [datetime] NULL,
	[createdUserId] [int] NULL,
	[approvalUserId] [int] NULL,
	[approvalStatus] [varchar](50) NULL,
	[approvalRemark] [text] NULL,
	[approvalAt] [datetime] NULL,
 CONSTRAINT [PK_RewardsAndPenalty] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ResignType]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResignType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](50) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
 CONSTRAINT [PK_ResignType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ResignRecord]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResignRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[resignTypeId] [int] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[resignAt] [datetime] NOT NULL,
	[resignEffectiveAt] [datetime] NOT NULL,
	[resignReason] [text] NULL,
	[resignCheckUserId] [int] NULL,
	[resignChecker] [varchar](50) NULL,
	[approvalAt] [datetime] NULL,
	[approvalStatus] [varchar](50) NULL,
	[approvalRemark] [text] NULL,
	[createdAt] [datetime] NULL,
	[remark] [text] NULL,
	[userId] [int] NULL,
 CONSTRAINT [PK_ResignRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SysUserDataAuth]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUserDataAuth](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[cmpId] [int] NULL,
	[departId] [nvarchar](200) NULL,
	[remarks] [nvarchar](200) NULL,
 CONSTRAINT [PK_SysUserDataAuth] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRoleAuthorization]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRoleAuthorization](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roleId] [int] NULL,
	[authId] [int] NULL,
	[remarks] [nvarchar](200) NULL,
 CONSTRAINT [PK_SysRoleAuthorization] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[remark] [varchar](255) NULL,
	[companyId] [int] NULL,
	[parentId] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JobCertificate]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobCertificate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[jobTitleId] [int] NOT NULL,
	[certificateTypeId] [int] NOT NULL,
 CONSTRAINT [PK_JobCertificate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 12/12/2016 10:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Staff](
	[nr] [varchar](200) NOT NULL,
	[name] [varchar](50) NULL,
	[sex] [varchar](10) NULL,
	[birthday] [datetime] NULL,
	[ethnic] [varchar](50) NULL,
	[firstCompanyEmployAt] [datetime] NULL,
	[totalCompanySeniority] [float] NULL,
	[companyEmployAt] [datetime] NULL,
	[companySeniority] [float] NULL,
	[workStatus] [int] NOT NULL,
	[isOnTrial] [bit] NOT NULL,
	[trialOverAt] [datetime] NULL,
	[companyId] [int] NULL,
	[departmentId] [int] NULL,
	[jobTitleId] [int] NULL,
	[photo] [text] NULL,
	[staffTypeId] [int] NULL,
	[degreeTypeId] [int] NULL,
	[speciality] [varchar](200) NULL,
	[residenceAddress] [varchar](200) NULL,
	[address] [varchar](200) NULL,
	[id] [varchar](200) NOT NULL,
	[isIdChecked] [bit] NOT NULL,
	[phone] [varchar](50) NULL,
	[contactName] [varchar](50) NULL,
	[contactPhone] [varchar](50) NULL,
	[contactFamilyMemberType] [varchar](50) NULL,
	[domicile] [varchar](50) NULL,
	[residenceType] [int] NULL,
	[insureTypeId] [int] NULL,
	[isPayCPF] [bit] NOT NULL,
	[contractExpireAt] [date] NULL,
	[contractCount] [int] NULL,
	[totalSeniority] [float] NULL,
	[remark] [text] NULL,
	[workingYearsAt] [datetime] NULL,
	[contractExpireStr] [varchar](50) NULL,
	[resignAt] [datetime] NULL,
	[parentStaffNr] [varchar](200) NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[nr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[SysRoleAuthView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SysRoleAuthView]
AS
SELECT dbo.SysAuthorization.id AS SysAuthId, dbo.SysAuthorization.name AS SysAuthName, dbo.SysAuthorization.controlName, dbo.SysAuthorization.actionName, 
               dbo.SysAuthorization.parentId AS SysAuthParentId, dbo.SysAuthorization.funCode, dbo.SysAuthorization.isDelete, dbo.SysAuthorization.remarks AS SysAuthRemarks, dbo.SysRole.id, 
               dbo.SysRole.name AS SysRoleName, dbo.SysRole.remarks AS SysRoleRemarks, dbo.SysRoleAuthorization.roleId, dbo.SysRoleAuthorization.authId, 
               dbo.SysRoleAuthorization.remarks AS SysRoleAuthRemarks
FROM  dbo.SysRole INNER JOIN
               dbo.SysRoleAuthorization ON dbo.SysRole.id = dbo.SysRoleAuthorization.roleId RIGHT OUTER JOIN
               dbo.SysAuthorization ON dbo.SysRoleAuthorization.authId = dbo.SysAuthorization.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[27] 2[26] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "SysAuthorization"
            Begin Extent = 
               Top = 37
               Left = 24
               Bottom = 246
               Right = 266
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "SysRoleAuthorization"
            Begin Extent = 
               Top = 43
               Left = 424
               Bottom = 287
               Right = 842
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SysRole"
            Begin Extent = 
               Top = 7
               Left = 890
               Bottom = 132
               Right = 1052
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2220
         Table = 2424
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SysRoleAuthView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SysRoleAuthView'
GO
/****** Object:  Table [dbo].[ShiftSchedule]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShiftSchedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shiftId] [int] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[scheduleAt] [date] NOT NULL,
 CONSTRAINT [PK_ShiftSchedule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [NONCLUSTERED_KEY_ScheduleAtAsc] ON [dbo].[ShiftSchedule] 
(
	[scheduleAt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  View [dbo].[StaffViewSimple]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[StaffViewSimple]
AS
SELECT     dbo.Staff.nr, dbo.Staff.name, dbo.Staff.sex, dbo.Staff.birthday, dbo.Staff.firstCompanyEmployAt, dbo.Staff.ethnic, dbo.Staff.totalCompanySeniority, 
                      dbo.Staff.companyEmployAt, dbo.Staff.companySeniority, dbo.Staff.workStatus, dbo.Staff.isOnTrial, dbo.Staff.trialOverAt, dbo.Staff.companyId, dbo.Staff.departmentId, 
                      dbo.Staff.jobTitleId, dbo.Staff.photo, dbo.Staff.staffTypeId, dbo.Staff.degreeTypeId, dbo.Staff.speciality, dbo.Staff.residenceAddress, dbo.Staff.address, dbo.Staff.id, 
                      dbo.Staff.isIdChecked, dbo.Staff.phone, dbo.Staff.contactName, dbo.Staff.contactPhone, dbo.Staff.contactFamilyMemberType, dbo.Staff.domicile, 
                      dbo.Staff.residenceType, dbo.Staff.insureTypeId, dbo.Staff.isPayCPF, dbo.Staff.contractExpireAt, dbo.Staff.contractCount, dbo.Staff.totalSeniority, dbo.Staff.remark, 
                      dbo.Staff.workingYearsAt, dbo.Staff.contractExpireStr, dbo.Staff.resignAt, dbo.Company.name AS companyName, dbo.Department.name AS departmentName, 
                      dbo.JobTitle.name AS jobTitleName, dbo.StaffType.name AS staffTypeName, dbo.Department.parentId
FROM         dbo.Staff LEFT OUTER JOIN
                      dbo.Department ON dbo.Department.id = dbo.Staff.departmentId LEFT OUTER JOIN
                      dbo.Company ON dbo.Staff.companyId = dbo.Company.id LEFT OUTER JOIN
                      dbo.JobTitle ON dbo.Staff.jobTitleId = dbo.JobTitle.id LEFT OUTER JOIN
                      dbo.StaffType ON dbo.Staff.staffTypeId = dbo.StaffType.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[50] 4[22] 3[4] 2) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Department"
            Begin Extent = 
               Top = 146
               Left = 328
               Bottom = 330
               Right = 470
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Staff"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 208
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 29
         End
         Begin Table = "Company"
            Begin Extent = 
               Top = 6
               Left = 293
               Bottom = 125
               Right = 435
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "JobTitle"
            Begin Extent = 
               Top = 6
               Left = 653
               Bottom = 110
               Right = 795
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StaffType"
            Begin Extent = 
               Top = 114
               Left = 653
               Bottom = 218
               Right = 795
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StaffViewSimple'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StaffViewSimple'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StaffViewSimple'
GO
/****** Object:  View [dbo].[StaffView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[StaffView]
AS
SELECT     dbo.Staff.nr, dbo.Staff.name, dbo.Staff.sex, dbo.Staff.birthday, dbo.Staff.firstCompanyEmployAt, dbo.Staff.ethnic, dbo.Staff.totalCompanySeniority, 
                      dbo.Staff.companyEmployAt, dbo.Staff.companySeniority, dbo.Staff.workStatus, dbo.Staff.isOnTrial, dbo.Staff.trialOverAt, dbo.Staff.companyId, dbo.Staff.departmentId, 
                      dbo.Staff.jobTitleId, dbo.Staff.photo, dbo.Staff.staffTypeId, dbo.Staff.degreeTypeId, dbo.Staff.speciality, dbo.Staff.residenceAddress, dbo.Staff.address, dbo.Staff.id, 
                      dbo.Staff.isIdChecked, dbo.Staff.phone, dbo.Staff.contactName, dbo.Staff.contactPhone, dbo.Staff.contactFamilyMemberType, dbo.Staff.domicile, 
                      dbo.Staff.residenceType, dbo.Staff.insureTypeId, dbo.Staff.isPayCPF, dbo.Staff.contractExpireAt, dbo.Staff.contractCount, dbo.Staff.totalSeniority, dbo.Staff.remark, 
                      dbo.Staff.workingYearsAt, dbo.Staff.contractExpireStr, dbo.Staff.resignAt, dbo.Company.name AS companyName, dbo.Department.name AS departmentName, 
                      dbo.JobTitle.name AS jobTitleName, dbo.StaffType.name AS staffTypeName, ParentDepartment.name AS parenDepartName, dbo.Department.parentId, 
                      ParentDepartment.parentId AS parentDepartParentId, ParentParentDepartment.name AS parentParentDepartmentName, 
                      ParentParentDepartment.parentId AS parentParentDeparParentId
FROM         dbo.Staff LEFT OUTER JOIN
                      dbo.Department ON dbo.Staff.departmentId = dbo.Department.id LEFT OUTER JOIN
                      dbo.Department AS ParentDepartment ON ParentDepartment.id = dbo.Department.parentId LEFT OUTER JOIN
                      dbo.Department AS ParentParentDepartment ON ParentDepartment.parentId = ParentParentDepartment.id LEFT OUTER JOIN
                      dbo.Company ON dbo.Company.id = dbo.Staff.companyId LEFT OUTER JOIN
                      dbo.JobTitle ON dbo.Staff.jobTitleId = dbo.JobTitle.id LEFT OUTER JOIN
                      dbo.StaffType ON dbo.Staff.staffTypeId = dbo.StaffType.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[1] 3[18] 2) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Department"
            Begin Extent = 
               Top = 229
               Left = 200
               Bottom = 413
               Right = 342
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ParentDepartment"
            Begin Extent = 
               Top = 338
               Left = 431
               Bottom = 515
               Right = 656
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Staff"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 208
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 29
         End
         Begin Table = "Company"
            Begin Extent = 
               Top = 6
               Left = 293
               Bottom = 125
               Right = 435
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "JobTitle"
            Begin Extent = 
               Top = 6
               Left = 653
               Bottom = 110
               Right = 795
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StaffType"
            Begin Extent = 
               Top = 114
               Left = 653
               Bottom = 218
               Right = 795
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ParentParentDepartment"
            Begin Extent = 
               Top = 182
               Left = 437
               Bottom = 297
               Right = 648
            End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StaffView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1395
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StaffView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StaffView'
GO
/****** Object:  View [dbo].[MessageRecordView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MessageRecordView]
AS
SELECT     dbo.MessageRecord.id, dbo.MessageRecord.staffNr, dbo.MessageRecord.operatorId, dbo.MessageRecord.messageType, dbo.MessageRecord.createdAt, 
                      dbo.MessageRecord.text, dbo.MessageRecord.isRead, dbo.MessageRecord.isHandled, dbo.MessageRecord.messageCategory, dbo.MessageRecord.uniqString, 
                      dbo.[User].name AS operatorName, dbo.[User].email AS operatorEmail, dbo.Staff.name AS staffName
FROM         dbo.MessageRecord LEFT OUTER JOIN
                      dbo.[User] ON dbo.MessageRecord.operatorId = dbo.[User].id LEFT OUTER JOIN
                      dbo.Staff ON dbo.MessageRecord.staffNr = dbo.Staff.nr
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[43] 2[17] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "MessageRecord"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 305
               Right = 197
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 102
               Left = 235
               Bottom = 221
               Right = 377
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Staff"
            Begin Extent = 
               Top = 6
               Left = 415
               Bottom = 361
               Right = 632
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 4230
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'MessageRecordView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'MessageRecordView'
GO
/****** Object:  Table [dbo].[FullMemberRecord]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FullMemberRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[isPassCheck] [bit] NOT NULL,
	[checkScore] [float] NULL,
	[beFullAt] [datetime] NULL,
	[remark] [text] NULL,
	[checkAt] [datetime] NULL,
	[approvalUserId] [int] NULL,
	[approvalAt] [datetime] NULL,
	[approvalStatus] [varchar](50) NULL,
	[approvalRemark] [text] NULL,
	[createdAt] [datetime] NULL,
	[userId] [int] NULL,
 CONSTRAINT [PK_FullMemberRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FamilyMemeber]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FamilyMemeber](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[memberName] [varchar](50) NOT NULL,
	[familyMemberType] [varchar](50) NULL,
	[birthday] [datetime] NULL,
	[staffNr] [varchar](200) NULL,
 CONSTRAINT [PK_FamilyMemeber] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AbsenceRecrod]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AbsenceRecrod](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[absenceTypeId] [int] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[duration] [float] NOT NULL,
	[durationType] [int] NOT NULL,
	[remark] [varchar](255) NULL,
	[absenceDate] [datetime] NULL,
	[startHour] [time](7) NULL,
	[endHour] [time](7) NULL,
 CONSTRAINT [PK_AbsenceRecrod] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AttendanceRecordCal]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AttendanceRecordCal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[oriWorkingHour] [float] NOT NULL,
	[attendanceDate] [datetime] NOT NULL,
	[actWorkingHour] [float] NOT NULL,
	[remark] [text] NULL,
	[createdAt] [datetime] NOT NULL,
	[isManualCal] [bit] NOT NULL,
	[isException] [bit] NOT NULL,
	[exceptionCodes] [text] NULL,
	[isExceptionHandled] [bit] NULL,
	[oriExtraWorkingHour] [float] NULL,
	[actExtraWorkingHour] [float] NULL,
	[extraworkType] [int] NULL,
 CONSTRAINT [PK_AttendanceRecordCal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [NONCLUSTERED_KEY_StaffNrAsc_AttendanceDateDesc] ON [dbo].[AttendanceRecordCal] 
(
	[staffNr] ASC,
	[attendanceDate] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttendanceRecordDetail]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AttendanceRecordDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[recordAt] [datetime] NOT NULL,
	[createdAt] [datetime] NULL,
	[soureType] [varchar](200) NOT NULL,
	[isCalculated] [bit] NOT NULL,
	[device] [varchar](50) NULL,
 CONSTRAINT [PK_AttendanceRecordDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [NONCLUSTERED_KEY_StaffNrAsc_RecrodAtDesc] ON [dbo].[AttendanceRecordDetail] 
(
	[staffNr] ASC,
	[recordAt] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Certificate]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Certificate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[certificateTypeId] [int] NOT NULL,
	[certiLevel] [varchar](50) NULL,
	[effectiveFrom] [datetime] NULL,
	[effectiveEnd] [datetime] NULL,
	[institution] [varchar](200) NULL,
	[remark] [text] NULL,
 CONSTRAINT [PK_Certificate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BankCard]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BankCard](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nr] [varchar](50) NOT NULL,
	[bank] [varchar](50) NULL,
	[bankAddress] [varchar](50) NULL,
	[remark] [varchar](255) NULL,
	[isDefault] [bit] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
 CONSTRAINT [PK_BankCard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExtraWorkRecord]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExtraWorkRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[extraWorkTypeId] [int] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[duration] [float] NOT NULL,
	[durationType] [int] NOT NULL,
	[otReason] [varchar](255) NULL,
	[otTime] [datetime] NULL,
	[startHour] [time](7) NULL,
	[endHour] [time](7) NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_ExtraWorkRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExtraWorkRecordApproval]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtraWorkRecordApproval](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[extraWorkId] [int] NULL,
	[approvalTime] [datetime] NULL,
	[userId] [int] NULL,
	[approvalStatus] [nvarchar](50) NULL,
	[remarks] [nchar](10) NULL,
 CONSTRAINT [PK_ExtraWorkRecordApproval] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AttendanceRecordDetailView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AttendanceRecordDetailView]
AS
SELECT     dbo.AttendanceRecordDetail.id, dbo.AttendanceRecordDetail.staffNr, dbo.AttendanceRecordDetail.recordAt, dbo.AttendanceRecordDetail.createdAt, 
                      dbo.AttendanceRecordDetail.soureType, dbo.AttendanceRecordDetail.isCalculated, dbo.Staff.name, dbo.Staff.sex, dbo.Staff.birthday, dbo.Staff.ethnic, 
                      dbo.Staff.firstCompanyEmployAt, dbo.Staff.totalCompanySeniority, dbo.Staff.companyEmployAt, dbo.Staff.companySeniority, dbo.Staff.workStatus, dbo.Staff.isOnTrial, 
                      dbo.Staff.trialOverAt, dbo.Staff.companyId, dbo.Staff.departmentId, dbo.Staff.jobTitleId, dbo.Staff.photo, dbo.Staff.staffTypeId, dbo.Staff.degreeTypeId, 
                      dbo.Staff.speciality, dbo.Staff.residenceAddress, dbo.Staff.address, dbo.Staff.id AS staffId, dbo.Staff.isIdChecked, dbo.Staff.phone, dbo.Staff.contactName, 
                      dbo.Staff.contactPhone, dbo.Staff.contactFamilyMemberType, dbo.Staff.domicile, dbo.Staff.residenceType, dbo.Staff.insureTypeId, dbo.Staff.isPayCPF, 
                      dbo.Staff.contractExpireAt, dbo.Staff.contractCount, dbo.Staff.totalSeniority,dbo.Staff.resignAt, dbo.Staff.remark AS staffRemark, dbo.AttendanceRecordDetail.device, 
                      dbo.Department.name AS departmentName
FROM         dbo.AttendanceRecordDetail INNER JOIN
                      dbo.Staff ON dbo.AttendanceRecordDetail.staffNr = dbo.Staff.nr left JOIN
                      dbo.Department ON dbo.Staff.departmentId = dbo.Department.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[32] 4[29] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -576
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AttendanceRecordDetail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 205
               Right = 184
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Staff"
            Begin Extent = 
               Top = 5
               Left = 349
               Bottom = 215
               Right = 566
            End
            DisplayFlags = 280
            TopColumn = 25
         End
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 604
               Bottom = 142
               Right = 831
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1830
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AttendanceRecordDetailView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AttendanceRecordDetailView'
GO
/****** Object:  View [dbo].[AttendanceRecordCalView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AttendanceRecordCalView]
AS
SELECT     dbo.AttendanceRecordCal.id, dbo.AttendanceRecordCal.staffNr, dbo.AttendanceRecordCal.oriWorkingHour, dbo.AttendanceRecordCal.attendanceDate, 
                      dbo.AttendanceRecordCal.actWorkingHour, dbo.AttendanceRecordCal.remark, dbo.AttendanceRecordCal.createdAt, dbo.AttendanceRecordCal.isManualCal, 
                      dbo.AttendanceRecordCal.isException, dbo.AttendanceRecordCal.exceptionCodes,dbo.AttendanceRecordCal.actExtraWorkingHour,dbo.AttendanceRecordCal.oriExtraWorkingHour,dbo.AttendanceRecordCal.extraworkType, dbo.Staff.name, dbo.Staff.nr, dbo.Staff.sex, dbo.Staff.birthday, dbo.Staff.ethnic, 
                      dbo.Staff.firstCompanyEmployAt, dbo.Staff.totalCompanySeniority, dbo.Staff.companyEmployAt, dbo.Staff.companySeniority, dbo.Staff.workStatus, dbo.Staff.isOnTrial, 
                      dbo.Staff.trialOverAt, dbo.Staff.companyId, dbo.Staff.departmentId, dbo.Staff.jobTitleId, dbo.Staff.photo, dbo.Staff.staffTypeId, dbo.Staff.degreeTypeId, 
                      dbo.Staff.speciality, dbo.Staff.residenceAddress, dbo.Staff.address, dbo.Staff.id AS staffid, dbo.Staff.isIdChecked, dbo.Staff.phone, dbo.Staff.contactName, 
                      dbo.Staff.contactPhone, dbo.Staff.contactFamilyMemberType, dbo.Staff.domicile, dbo.Staff.residenceType, dbo.Staff.insureTypeId, dbo.Staff.isPayCPF, 
                      dbo.Staff.contractExpireAt, dbo.Staff.contractCount, dbo.Staff.totalSeniority, dbo.Department.name AS departmentName,dbo.Staff.resignAt, dbo.Staff.remark AS staffRemark, 
                      dbo.AttendanceRecordCal.isExceptionHandled
FROM         dbo.AttendanceRecordCal INNER JOIN
                      dbo.Staff ON dbo.AttendanceRecordCal.staffNr = dbo.Staff.nr left JOIN
                      dbo.Department ON dbo.Staff.departmentId = dbo.Department.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[31] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AttendanceRecordCal"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 234
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Staff"
            Begin Extent = 
               Top = 26
               Left = 677
               Bottom = 304
               Right = 894
            End
            DisplayFlags = 280
            TopColumn = 21
         End
         Begin Table = "Department"
            Begin Extent = 
               Top = 136
               Left = 260
               Bottom = 255
               Right = 402
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1905
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AttendanceRecordCalView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AttendanceRecordCalView'
GO
/****** Object:  View [dbo].[AttendanceRecordCalExceptionView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AttendanceRecordCalExceptionView]
AS
SELECT     attendanceDate, isExceptionHandled, COUNT(isExceptionHandled) AS isExceptionHandledCount
FROM         dbo.AttendanceRecordCal
GROUP BY attendanceDate, isExceptionHandled
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AttendanceRecordCal"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 301
               Right = 310
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AttendanceRecordCalExceptionView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AttendanceRecordCalExceptionView'
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attachment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[attachmentType] [int] NOT NULL,
	[path] [varchar](200) NULL,
	[attachmentAbleId] [int] NULL,
	[attachmentAbleType] [varchar](50) NULL,
	[certificateId] [int] NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[AbsenceRecordView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AbsenceRecordView]
AS
SELECT     dbo.AbsenceRecrod.id, dbo.AbsenceRecrod.absenceTypeId, dbo.AbsenceRecrod.staffNr, dbo.AbsenceRecrod.duration, dbo.AbsenceRecrod.absenceDate, 
                      dbo.AbsenceRecrod.startHour, dbo.AbsenceRecrod.endHour, dbo.AbsenceType.code, dbo.AbsenceType.name, dbo.AbsenceType.systemCode, dbo.Staff.nr, 
                      dbo.Staff.name AS staffName, dbo.Staff.workStatus, dbo.Staff.sex, dbo.Staff.birthday, dbo.Staff.ethnic, dbo.Staff.totalCompanySeniority, 
                      dbo.Staff.firstCompanyEmployAt, dbo.Staff.companyEmployAt, dbo.Staff.companySeniority, dbo.Staff.isOnTrial, dbo.Staff.trialOverAt, dbo.Staff.companyId, 
                      dbo.Staff.departmentId, dbo.Staff.jobTitleId, dbo.Staff.photo, dbo.Staff.staffTypeId, dbo.Staff.degreeTypeId, dbo.Staff.speciality, dbo.Staff.residenceAddress, 
                      dbo.Staff.address, dbo.Staff.id AS staffId, dbo.Staff.isIdChecked, dbo.Staff.phone, dbo.Staff.contactName, dbo.Staff.contactPhone, dbo.Staff.contactFamilyMemberType, 
                      dbo.Staff.domicile, dbo.Staff.residenceType, dbo.Staff.insureTypeId, dbo.Staff.isPayCPF, dbo.Staff.contractExpireAt, dbo.Staff.contractCount, dbo.Staff.totalSeniority, 
                      dbo.Staff.remark, dbo.Staff.workingYearsAt, dbo.Staff.contractExpireStr, dbo.Staff.resignAt
FROM         dbo.AbsenceRecrod INNER JOIN
                      dbo.AbsenceType ON dbo.AbsenceRecrod.absenceTypeId = dbo.AbsenceType.id INNER JOIN
                      dbo.Staff ON dbo.AbsenceRecrod.staffNr = dbo.Staff.nr
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[45] 4[30] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AbsenceRecrod"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 232
               Right = 201
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AbsenceType"
            Begin Extent = 
               Top = 20
               Left = 403
               Bottom = 198
               Right = 551
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Staff"
            Begin Extent = 
               Top = 6
               Left = 589
               Bottom = 240
               Right = 806
            End
            DisplayFlags = 280
            TopColumn = 27
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AbsenceRecordView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AbsenceRecordView'
GO
/****** Object:  Table [dbo].[AbsenceRecordApproval]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbsenceRecordApproval](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[absRecordId] [int] NULL,
	[approvalTime] [datetime] NULL,
	[userId] [int] NULL,
	[approvalStatus] [nvarchar](50) NULL,
	[remarks] [nvarchar](500) NULL,
 CONSTRAINT [PK_AbsenceRecordApproval] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ShiftScheduleView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ShiftScheduleView]
AS
SELECT     dbo.ShiftSchedule.id, dbo.ShiftSchedule.staffNr, dbo.ShiftSchedule.scheduleAt, dbo.ShiftSchedule.shiftId, dbo.Shift.code, dbo.Shift.name, dbo.Shift.startAt, 
                      dbo.Shift.endAt, dbo.Shift.shiftType, dbo.Shift.remark, CONVERT(DATETIME, dbo.ShiftSchedule.scheduleAt) + CONVERT(DATETIME, dbo.Shift.startAt) AS fullStartAt, 
                      CONVERT(DATETIME, dbo.ShiftSchedule.scheduleAt) + (CASE dbo.Shift.shiftType WHEN 100 THEN 0 WHEN 200 THEN 1 END) + CONVERT(DATETIME, dbo.Shift.endAt) 
                      AS fullEndAt
FROM         dbo.Shift INNER JOIN
                      dbo.ShiftSchedule ON dbo.Shift.id = dbo.ShiftSchedule.shiftId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[24] 4[37] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Shift"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 201
               Right = 192
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ShiftSchedule"
            Begin Extent = 
               Top = 6
               Left = 218
               Bottom = 254
               Right = 412
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2370
         Table = 3000
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ShiftScheduleView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ShiftScheduleView'
GO
/****** Object:  View [dbo].[ExtraWorkRecordView]    Script Date: 12/12/2016 10:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ExtraWorkRecordView]
AS
SELECT     dbo.ExtraWorkRecord.id, dbo.ExtraWorkRecord.extraWorkTypeId, dbo.ExtraWorkRecord.staffNr, dbo.ExtraWorkRecord.duration, dbo.ExtraWorkRecord.otReason, 
                      dbo.ExtraWorkRecord.otTime, dbo.ExtraWorkRecord.startHour, dbo.ExtraWorkRecord.endHour, dbo.ExtraWorkType.systemCode, dbo.ExtraWorkType.name, 
                      dbo.Staff.nr, dbo.Staff.name AS staffName, dbo.Staff.sex, dbo.Staff.birthday, dbo.Staff.ethnic, dbo.Staff.firstCompanyEmployAt, dbo.Staff.totalCompanySeniority, 
                      dbo.Staff.companyEmployAt, dbo.Staff.companySeniority, dbo.Staff.workStatus, dbo.Staff.isOnTrial, dbo.Staff.trialOverAt, dbo.Staff.companyId, dbo.Staff.departmentId, 
                      dbo.Staff.jobTitleId, dbo.Staff.photo, dbo.Staff.staffTypeId, dbo.Staff.degreeTypeId, dbo.Staff.speciality, dbo.Staff.residenceAddress, dbo.Staff.address, 
                      dbo.Staff.id AS staffId, dbo.Staff.isIdChecked, dbo.Staff.phone, dbo.Staff.contactName, dbo.Staff.contactPhone, dbo.Staff.contactFamilyMemberType, dbo.Staff.domicile, 
                      dbo.Staff.residenceType, dbo.Staff.insureTypeId, dbo.Staff.isPayCPF, dbo.Staff.contractExpireAt, dbo.Staff.contractCount, dbo.Staff.remark, dbo.Staff.totalSeniority, 
                      dbo.Staff.workingYearsAt, dbo.Staff.contractExpireStr, dbo.Staff.resignAt, dbo.ExtraWorkRecord.userId, dbo.ExtraWorkRecordApproval.extraWorkId, 
                      dbo.ExtraWorkRecordApproval.approvalTime, dbo.ExtraWorkRecordApproval.userId AS ApprovalUserId, dbo.ExtraWorkRecordApproval.approvalStatus, 
                      dbo.ExtraWorkRecordApproval.remarks
FROM         dbo.ExtraWorkRecord INNER JOIN
                      dbo.ExtraWorkType ON dbo.ExtraWorkRecord.extraWorkTypeId = dbo.ExtraWorkType.id INNER JOIN
                      dbo.Staff ON dbo.ExtraWorkRecord.staffNr = dbo.Staff.nr LEFT OUTER JOIN
                      dbo.ExtraWorkRecordApproval ON dbo.ExtraWorkRecord.id = dbo.ExtraWorkRecordApproval.extraWorkId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[22] 2[26] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ExtraWorkRecord"
            Begin Extent = 
               Top = 122
               Left = 30
               Bottom = 363
               Right = 205
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExtraWorkType"
            Begin Extent = 
               Top = 98
               Left = 424
               Bottom = 217
               Right = 572
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Staff"
            Begin Extent = 
               Top = 112
               Left = 852
               Bottom = 321
               Right = 1069
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExtraWorkRecordApproval"
            Begin Extent = 
               Top = 22
               Left = 234
               Bottom = 141
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 3045
         Table = 2475
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtraWorkRecordView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtraWorkRecordView'
GO
/****** Object:  Default [DF_MessageRecord_isRead]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[MessageRecord] ADD  CONSTRAINT [DF_MessageRecord_isRead]  DEFAULT ((0)) FOR [isRead]
GO
/****** Object:  Default [DF_MessageRecord_isHandled]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[MessageRecord] ADD  CONSTRAINT [DF_MessageRecord_isHandled]  DEFAULT ((0)) FOR [isHandled]
GO
/****** Object:  Default [DF_JobTitle_IsRevoked]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[JobTitle] ADD  CONSTRAINT [DF_JobTitle_IsRevoked]  DEFAULT ((0)) FOR [IsRevoked]
GO
/****** Object:  Default [DF_CertificateType_isSystem]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[CertificateType] ADD  CONSTRAINT [DF_CertificateType_isSystem]  DEFAULT ((0)) FOR [isSystem]
GO
/****** Object:  Default [DF_CertificateType_isNecessary]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[CertificateType] ADD  CONSTRAINT [DF_CertificateType_isNecessary]  DEFAULT ((0)) FOR [isNecessary]
GO
/****** Object:  Default [DF_User_isLocked]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_isLocked]  DEFAULT ((0)) FOR [isLocked]
GO
/****** Object:  Default [DF_Staff_isOnTrial]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_isOnTrial]  DEFAULT ((0)) FOR [isOnTrial]
GO
/****** Object:  Default [DF_Staff_isIdChecked]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_isIdChecked]  DEFAULT ((0)) FOR [isIdChecked]
GO
/****** Object:  Default [DF_Staff_isPayCFP]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_isPayCFP]  DEFAULT ((0)) FOR [isPayCPF]
GO
/****** Object:  Default [DF_FullMemberRecord_isPassCheck]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[FullMemberRecord] ADD  CONSTRAINT [DF_FullMemberRecord_isPassCheck]  DEFAULT ((0)) FOR [isPassCheck]
GO
/****** Object:  Default [DF_AttendanceRecordCal_isManualCal]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AttendanceRecordCal] ADD  CONSTRAINT [DF_AttendanceRecordCal_isManualCal]  DEFAULT ((0)) FOR [isManualCal]
GO
/****** Object:  Default [DF_AttendanceRecordCal_isException]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AttendanceRecordCal] ADD  CONSTRAINT [DF_AttendanceRecordCal_isException]  DEFAULT ((0)) FOR [isException]
GO
/****** Object:  Default [DF_AttendanceRecordCal_isExceptionHandled]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AttendanceRecordCal] ADD  CONSTRAINT [DF_AttendanceRecordCal_isExceptionHandled]  DEFAULT ((1)) FOR [isExceptionHandled]
GO
/****** Object:  Default [DF_AttendanceRecordDetail_isCalculated]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AttendanceRecordDetail] ADD  CONSTRAINT [DF_AttendanceRecordDetail_isCalculated]  DEFAULT ((0)) FOR [isCalculated]
GO
/****** Object:  Default [DF_BankCard_isDefault]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[BankCard] ADD  CONSTRAINT [DF_BankCard_isDefault]  DEFAULT ((0)) FOR [isDefault]
GO
/****** Object:  ForeignKey [FK_ResignRecord_ResignType]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[ResignRecord]  WITH CHECK ADD  CONSTRAINT [FK_ResignRecord_ResignType] FOREIGN KEY([resignTypeId])
REFERENCES [dbo].[ResignType] ([id])
GO
ALTER TABLE [dbo].[ResignRecord] CHECK CONSTRAINT [FK_ResignRecord_ResignType]
GO
/****** Object:  ForeignKey [FK_SysUserDataAuth_User]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[SysUserDataAuth]  WITH CHECK ADD  CONSTRAINT [FK_SysUserDataAuth_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[SysUserDataAuth] CHECK CONSTRAINT [FK_SysUserDataAuth_User]
GO
/****** Object:  ForeignKey [FK_SysRoleAuthorization_SysAuthorization]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[SysRoleAuthorization]  WITH CHECK ADD  CONSTRAINT [FK_SysRoleAuthorization_SysAuthorization] FOREIGN KEY([authId])
REFERENCES [dbo].[SysAuthorization] ([id])
GO
ALTER TABLE [dbo].[SysRoleAuthorization] CHECK CONSTRAINT [FK_SysRoleAuthorization_SysAuthorization]
GO
/****** Object:  ForeignKey [FK_SysRoleAuthorization_SysRole]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[SysRoleAuthorization]  WITH CHECK ADD  CONSTRAINT [FK_SysRoleAuthorization_SysRole] FOREIGN KEY([roleId])
REFERENCES [dbo].[SysRole] ([id])
GO
ALTER TABLE [dbo].[SysRoleAuthorization] CHECK CONSTRAINT [FK_SysRoleAuthorization_SysRole]
GO
/****** Object:  ForeignKey [FK_Department_Company]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Company]
GO
/****** Object:  ForeignKey [FK_Department_Department]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department] FOREIGN KEY([parentId])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department]
GO
/****** Object:  ForeignKey [FK_JobCertificate_CertificateType]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[JobCertificate]  WITH CHECK ADD  CONSTRAINT [FK_JobCertificate_CertificateType] FOREIGN KEY([certificateTypeId])
REFERENCES [dbo].[CertificateType] ([id])
GO
ALTER TABLE [dbo].[JobCertificate] CHECK CONSTRAINT [FK_JobCertificate_CertificateType]
GO
/****** Object:  ForeignKey [FK_JobCertificate_JobTitle]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[JobCertificate]  WITH CHECK ADD  CONSTRAINT [FK_JobCertificate_JobTitle] FOREIGN KEY([jobTitleId])
REFERENCES [dbo].[JobTitle] ([id])
GO
ALTER TABLE [dbo].[JobCertificate] CHECK CONSTRAINT [FK_JobCertificate_JobTitle]
GO
/****** Object:  ForeignKey [FK_Staff_Company]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Company]
GO
/****** Object:  ForeignKey [FK_Staff_DegreeType]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_DegreeType] FOREIGN KEY([degreeTypeId])
REFERENCES [dbo].[DegreeType] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_DegreeType]
GO
/****** Object:  ForeignKey [FK_Staff_Department]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Department] FOREIGN KEY([departmentId])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Department]
GO
/****** Object:  ForeignKey [FK_Staff_InsureType]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_InsureType] FOREIGN KEY([insureTypeId])
REFERENCES [dbo].[InsureType] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_InsureType]
GO
/****** Object:  ForeignKey [FK_Staff_JobTitle]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_JobTitle] FOREIGN KEY([jobTitleId])
REFERENCES [dbo].[JobTitle] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_JobTitle]
GO
/****** Object:  ForeignKey [FK_Staff_StaffType]    Script Date: 12/12/2016 10:05:08 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_StaffType] FOREIGN KEY([staffTypeId])
REFERENCES [dbo].[StaffType] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_StaffType]
GO
/****** Object:  ForeignKey [FK_ShiftSchedule_Shift]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[ShiftSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ShiftSchedule_Shift] FOREIGN KEY([shiftId])
REFERENCES [dbo].[Shift] ([id])
GO
ALTER TABLE [dbo].[ShiftSchedule] CHECK CONSTRAINT [FK_ShiftSchedule_Shift]
GO
/****** Object:  ForeignKey [FK_ShiftSchedule_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[ShiftSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ShiftSchedule_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[ShiftSchedule] CHECK CONSTRAINT [FK_ShiftSchedule_Staff]
GO
/****** Object:  ForeignKey [FK_FullMemberRecord_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[FullMemberRecord]  WITH CHECK ADD  CONSTRAINT [FK_FullMemberRecord_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[FullMemberRecord] CHECK CONSTRAINT [FK_FullMemberRecord_Staff]
GO
/****** Object:  ForeignKey [FK_FamilyMemeber_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[FamilyMemeber]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMemeber_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[FamilyMemeber] CHECK CONSTRAINT [FK_FamilyMemeber_Staff]
GO
/****** Object:  ForeignKey [FK_AbsenceRecrod_AbsenceType]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AbsenceRecrod]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceRecrod_AbsenceType] FOREIGN KEY([absenceTypeId])
REFERENCES [dbo].[AbsenceType] ([id])
GO
ALTER TABLE [dbo].[AbsenceRecrod] CHECK CONSTRAINT [FK_AbsenceRecrod_AbsenceType]
GO
/****** Object:  ForeignKey [FK_AbsenceRecrod_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AbsenceRecrod]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceRecrod_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[AbsenceRecrod] CHECK CONSTRAINT [FK_AbsenceRecrod_Staff]
GO
/****** Object:  ForeignKey [FK_AttendanceRecordCal_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AttendanceRecordCal]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceRecordCal_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[AttendanceRecordCal] CHECK CONSTRAINT [FK_AttendanceRecordCal_Staff]
GO
/****** Object:  ForeignKey [FK_AttendanceRecordDetail_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AttendanceRecordDetail]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceRecordDetail_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[AttendanceRecordDetail] CHECK CONSTRAINT [FK_AttendanceRecordDetail_Staff]
GO
/****** Object:  ForeignKey [FK_Certificate_CertificateType]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD  CONSTRAINT [FK_Certificate_CertificateType] FOREIGN KEY([certificateTypeId])
REFERENCES [dbo].[CertificateType] ([id])
GO
ALTER TABLE [dbo].[Certificate] CHECK CONSTRAINT [FK_Certificate_CertificateType]
GO
/****** Object:  ForeignKey [FK_Certificate_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD  CONSTRAINT [FK_Certificate_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[Certificate] CHECK CONSTRAINT [FK_Certificate_Staff]
GO
/****** Object:  ForeignKey [FK_BankCard_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[BankCard]  WITH CHECK ADD  CONSTRAINT [FK_BankCard_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[BankCard] CHECK CONSTRAINT [FK_BankCard_Staff]
GO
/****** Object:  ForeignKey [FK_ExtraWorkRecord_ExtraWorkType]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[ExtraWorkRecord]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecord_ExtraWorkType] FOREIGN KEY([extraWorkTypeId])
REFERENCES [dbo].[ExtraWorkType] ([id])
GO
ALTER TABLE [dbo].[ExtraWorkRecord] CHECK CONSTRAINT [FK_ExtraWorkRecord_ExtraWorkType]
GO
/****** Object:  ForeignKey [FK_ExtraWorkRecord_Staff]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[ExtraWorkRecord]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecord_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[ExtraWorkRecord] CHECK CONSTRAINT [FK_ExtraWorkRecord_Staff]
GO
/****** Object:  ForeignKey [FK_ExtraWorkRecordApproval_ExtraWorkRecord]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[ExtraWorkRecordApproval]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecordApproval_ExtraWorkRecord] FOREIGN KEY([extraWorkId])
REFERENCES [dbo].[ExtraWorkRecord] ([id])
GO
ALTER TABLE [dbo].[ExtraWorkRecordApproval] CHECK CONSTRAINT [FK_ExtraWorkRecordApproval_ExtraWorkRecord]
GO
/****** Object:  ForeignKey [FK_ExtraWorkRecordApproval_User]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[ExtraWorkRecordApproval]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecordApproval_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[ExtraWorkRecordApproval] CHECK CONSTRAINT [FK_ExtraWorkRecordApproval_User]
GO
/****** Object:  ForeignKey [FK_Attachment_Certificate]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Certificate] FOREIGN KEY([certificateId])
REFERENCES [dbo].[Certificate] ([id])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_Certificate]
GO
/****** Object:  ForeignKey [FK_AbsenceRecordApproval_AbsenceRecrod]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AbsenceRecordApproval]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceRecordApproval_AbsenceRecrod] FOREIGN KEY([absRecordId])
REFERENCES [dbo].[AbsenceRecrod] ([id])
GO
ALTER TABLE [dbo].[AbsenceRecordApproval] CHECK CONSTRAINT [FK_AbsenceRecordApproval_AbsenceRecrod]
GO
/****** Object:  ForeignKey [FK_AbsenceRecordApproval_User]    Script Date: 12/12/2016 10:05:09 ******/
ALTER TABLE [dbo].[AbsenceRecordApproval]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceRecordApproval_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[AbsenceRecordApproval] CHECK CONSTRAINT [FK_AbsenceRecordApproval_User]
GO


-- init 数据

use DerjinHr
go

-- 建立默认的证照类别

delete from CertificateType
insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('身份证','员工的身份证，系统默认证件照',1,0,100);


insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('职业证书','员工的职业证书，系统默认证件照',1,0,200);


insert CertificateType(name,remark,isSystem,isNecessary,systemCode)
values('健康证','员工的健康证，系统默认证件照',1,0,300);


-- 建立系统设置

INSERT INTO [DerjinHr].[dbo].[SystemSetting]
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

-- 建立自动任务
-- 每天23:30分运行QuartzJob, 计算转正提醒
INSERT INTO [DerjinHr].[dbo].[QuartzJob]
           ([cronSchedule]
           ,[params]
           ,[jobType])
     VALUES
           ('0 30 23 * * ? *'
           ,null
           ,100)
GO


-- 建立用户
INSERT INTO DerjinHr.[dbo].[User]
           ([name]
           ,[email]
           ,[pwd]
           ,[isLocked]
           ,[role]
           ,[pwdSalt])
     VALUES
           ('admin'
           ,'admin@derjin.com'
           ,'5086e4b986ee1ebd21694ce7d32a5269'
           ,0
           ,100
           ,'ZVqcOaidXIYon8nzi+C1Gqf6kHlgrZFWjoAHBX5zo9/JyDfC')
GO


use DerjinHr
go
delete from AbsenceType;
insert into AbsenceType(code,name,systemCode) values('放','放班',100);
insert into AbsenceType(code,name,systemCode) values('事','事假',200);
insert into AbsenceType(code,name,systemCode) values('病','病假',300);
insert into AbsenceType(code,name,systemCode) values('产','产假',400);
insert into AbsenceType(code,name,systemCode) values('丧','丧假',500);
insert into AbsenceType(code,name,systemCode) values('轮','轮休',600);
insert into AbsenceType(code,name,systemCode) values('公','公休',700);
insert into AbsenceType(code,name,systemCode) values('年','年假',800);
--insert into AbsenceType(code,name,systemCode) values('新','新进',900);
insert into AbsenceType(code,name,systemCode) values('旷','旷工',1000);
--insert into AbsenceType(code,name,systemCode) values('离','离职',1100);


use DerjinHr
go

delete from ExtraWorkType;
insert into ExtraWorkType(name,systemCode) values('延时加班',100);
insert into ExtraWorkType(name,systemCode) values('双休加班',200);
insert into ExtraWorkType(name,systemCode) values('节假日加班',300);


--学历
INSERT INTO [DerjinHr].[dbo].[DegreeType]([name] ,[remark]) VALUES ('本科' ,'')
INSERT INTO [DerjinHr].[dbo].[DegreeType]([name] ,[remark]) VALUES ('硕士' ,'')
INSERT INTO [DerjinHr].[dbo].[DegreeType]([name] ,[remark]) VALUES ('专科' ,'')


-- 权限

USE DerjinHr
GO
 
 
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'角色列表', N'SysRole', N'Index', NULL, N'权限管理', 0, N'[Index 页面]角色管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'角色创建', N'SysRole', N'Create', NULL, N'权限管理', 0, N'[Create 请求]角色管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'角色编辑', N'SysRole', N'Edit', NULL, N'权限管理', 0, N'[Edit 请求]角色管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'角色删除', N'SysRole', N'Delete', NULL, N'权限管理', 0, N'[Delete 请求]角色管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'权限列表', N'SystemAuthorization', N'Index', NULL, N'权限管理', 0, N'[Index 页面]权限管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'权限创建', N'SystemAuthorization', N'Create', NULL, N'权限管理', 0, N'[Create 请求]权限管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'权限编辑', N'SystemAuthorization', N'Edit', NULL, N'权限管理', 0, N'[Edit 请求]权限管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'权限删除', N'SystemAuthorization', N'Delete', NULL, N'权限管理', 0, N'[Delete 请求]权限管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'角色权限列表', N'SysRoleAuthorization', N'Index', NULL, N'权限管理', 0, N'[Index 页面]角色权限')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'角色权限创建', N'SysRoleAuthorization', N'Create', NULL, N'权限管理', 0, N'[Create 请求]角色权限')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'角色权限编辑', N'SysRoleAuthorization', N'Edit', NULL, N'权限管理', 0, N'[Edit 请求]角色权限')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'角色权限删除', N'SysRoleAuthorization', N'Delete', NULL, N'权限管理', 0, N'[Delete 请求]角色权限')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'用户列表', N'User', N'Index', NULL, N'用户管理', 0, N'[Index 页面]用户管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'用户创建', N'User', N'Create', NULL, N'用户管理', 0, N'[Create 请求]用户管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'用户编辑', N'User', N'Edit', NULL, N'用户管理', 0, N'[Edit 请求]用户管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'用户删除', N'User', N'Delete', NULL, N'用户管理', 0, N'[Delete 请求]用户管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'职位列表', N'JobTitle', N'Index', NULL, N'基础信息', 0, N'[Index 页面]职位管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'职位添加', N'JobTitle', N'Create', NULL, N'基础信息', 0, N'[Create 请求]职位管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'职位编辑', N'JobTitle', N'Edit', NULL, N'基础信息', 0, N'[Edit 请求]职位管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'职位删除', N'JobTitle', N'Delete', NULL, N'基础信息', 0, N'[Delete 请求]职位管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'人员类型列表', N'StaffType', N'Index', NULL, N'基础信息', 0, N'[Index 页面]人员类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'人员类型添加', N'StaffType', N'Create', NULL, N'基础信息', 0, N'[Create 请求]人员类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'人员类型编辑', N'StaffType', N'Edit', NULL, N'基础信息', 0, N'[Edit 请求]人员类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'人员类型删除', N'StaffType', N'Delete', NULL, N'基础信息', 0, N'[Delete 请求]人员类型管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'证照类型列表', N'CertificateType', N'Index', NULL, N'基础信息', 0, N'[Index 页面]证照类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'证照类型添加', N'CertificateType', N'Create', NULL, N'基础信息', 0, N'[Create 请求]证照类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'证照类型编辑', N'CertificateType', N'Edit', NULL, N'基础信息', 0, N'[Edit 请求]证照类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'证照类型删除', N'CertificateType', N'Delete', NULL, N'基础信息', 0, N'[Delete 请求]证照类型管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'学历列表', N'DegreeType', N'Index', NULL, N'基础信息', 0, N'[Index 页面]学历管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'学历添加', N'DegreeType', N'Create', NULL, N'基础信息', 0, N'[Create 请求]学历管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'学历编辑', N'DegreeType', N'Edit', NULL, N'基础信息', 0, N'[Edit 请求]学历管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'学历删除', N'DegreeType', N'Delete', NULL, N'基础信息', 0, N'[Delete 请求]学历管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'保险列表', N'InSureType', N'Index', NULL, N'基础信息', 0, N'[Index 页面]保险管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'保险添加', N'InSureType', N'Create', NULL, N'基础信息', 0, N'[Create 请求]保险管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'保险编辑', N'InSureType', N'Edit', NULL, N'基础信息', 0, N'[Edit 请求]保险管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'保险删除', N'InSureType', N'Delete', NULL, N'基础信息', 0, N'[Delete 请求]保险管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'离职类型列表', N'ResignType', N'Index', NULL, N'基础信息', 0, N'[Index 页面]离职类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'离职类型添加', N'ResignType', N'Create', NULL, N'基础信息', 0, N'[Create 请求]离职类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'离职类型编辑', N'ResignType', N'Edit', NULL, N'基础信息', 0, N'[Edit 请求]离职类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'离职类型删除', N'ResignType', N'Delete', NULL, N'基础信息', 0, N'[Delete 请求]离职类型管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤类型列表', N'AbsenceType', N'Index', NULL, N'基础信息', 0, N'[Index 页面]缺勤类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤类型添加', N'AbsenceType', N'Create', NULL, N'基础信息', 0, N'[Create 请求]缺勤类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤类型编辑', N'AbsenceType', N'Edit', NULL, N'基础信息', 0, N'[Edit 请求]缺勤类型管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤类型删除', N'AbsenceType', N'Delete', NULL, N'基础信息', 0, N'[Delete 请求]缺勤类型管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'加班类型列表', N'ExtraWorkType', N'Index', NULL, N'基础信息', 0, N'[Index 页面]加班类型管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'作息表列表', N'WorkAndRests', N'Index', NULL, N'基础信息', 0, N'[Index 页面]作息表管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'作息表添加', N'WorkAndRests', N'Create', NULL, N'基础信息', 0, N'[Create 请求]作息表管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'作息表编辑', N'WorkAndRests', N'Edit', NULL, N'基础信息', 0, N'[Edit 请求]作息表管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'作息表删除', N'WorkAndRests', N'Delete', NULL, N'基础信息', 0, N'[Delete 请求]作息表管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'作息表导入', N'WorkAndRests', N'Import', NULL, N'基础信息', 0, N'[Import 请求]作息表管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'公司列表', N'Company', N'Index', NULL, N'公司管理', 0, N'[Index 页面]公司管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'公司添加', N'Company', N'Create', NULL, N'公司管理', 0, N'[Create 请求]公司管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'公司编辑', N'Company', N'Edit', NULL, N'公司管理', 0, N'[Edit 请求]公司管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'公司删除', N'Company', N'Delete', NULL, N'公司管理', 0, N'[Delete 请求]公司管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'部门列表', N'Department', N'Index', NULL, N'部门管理', 0, N'[Index 页面]部门管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'部门添加', N'Department', N'Create', NULL, N'部门管理', 0, N'[Create 请求]部门管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'部门编辑', N'Department', N'Edit', NULL, N'部门管理', 0, N'[Edit 请求]部门管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'部门删除', N'Department', N'Delete', NULL, N'部门管理', 0, N'[Delete 请求]部门管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'部门导入', N'Department', N'Import', NULL, N'部门管理', 0, N'[Import 请求]部门管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工列表', N'Staff', N'Index', NULL, N'员工管理', 0, N'[Index 页面]员工管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工添加', N'Staff', N'Create', NULL, N'员工管理', 0, N'[Create 请求]员工管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工编辑', N'Staff', N'Edit', NULL, N'员工管理', 0, N'[Edit 请求]员工管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工删除', N'Staff', N'Delete', NULL, N'员工管理', 0, N'[Delete 请求]员工管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工查看', N'Staff', N'Show', NULL, N'员工管理', 0, N'[Show 请求]员工管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工导入', N'Staff', N'Import', NULL, N'员工管理', 0, N'[Import 请求]员工管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工转正', N'Staff', N'ToFullMemeber', NULL, N'员工管理', 0, N'[ToFullMemeber 请求]员工管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工离职', N'Staff', N'Resign', NULL, N'员工管理', 0, N'[Resign 请求]员工管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'员工调岗', N'Staff', N'changeJob', NULL, N'员工管理', 0, N'[changeJob 请求]员工管理')




INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'证照列表', N'Certificate', N'Index', NULL, N'员工证照管理', 0, N'[Index 页面]证照管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'证照添加', N'Certificate', N'Create', NULL, N'员工证照管理', 0, N'[Create 请求]证照管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'证照编辑', N'Certificate', N'Edit', NULL, N'员工证照管理', 0, N'[Edit 请求]证照管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'证照删除', N'Certificate', N'Delete', NULL, N'员工证照管理', 0, N'[Delete 请求]证照管理')


INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'班次列表', N'Shift', N'Index', NULL, N'排班管理', 0, N'[Index 页面]班次管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'班次添加', N'Shift', N'Create', NULL, N'排班管理', 0, N'[Create 请求]班次管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'班次编辑', N'Shift', N'Edit', NULL, N'排班管理', 0, N'[Edit 请求]班次管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'班次删除', N'Shift', N'Delete', NULL, N'排班管理', 0, N'[Delete 请求]班次管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'排班列表', N'ShiftSchedule', N'Index', NULL, N'排班管理', 0, N'[Index 页面]排班管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'排班添加', N'ShiftSchedule', N'Create', NULL, N'排班管理', 0, N'[Create 请求]排班管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'排班编辑', N'ShiftSchedule', N'Edit', NULL, N'排班管理', 0, N'[Edit 请求]排班管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'排班删除', N'ShiftSchedule', N'Delete', NULL, N'排班管理', 0, N'[Delete 请求]排班管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'排班导入', N'ShiftSchedule', N'Import', NULL, N'排班管理', 0, N'[Import 请求]排班管理')


INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'详细记录列表', N'AttendanceRecordDetail', N'Index', NULL, N'考勤管理', 0, N'[Index 页面]详细记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'详细记录添加', N'AttendanceRecordDetail', N'Create', NULL, N'考勤管理', 0, N'[Create 请求]详细记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'详细记录编辑', N'AttendanceRecordDetail', N'Edit', NULL, N'考勤管理', 0, N'[Edit 请求]详细记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'详细记录删除', N'AttendanceRecordDetail', N'Delete', NULL, N'考勤管理', 0, N'[Delete 请求]详细记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'执行考勤计算', N'AttendanceRecordDetail', N'Calculate', NULL, N'考勤管理', 0, N'[Calculate 请求]详细记录管理')



INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'统计记录列表', N'AttendanceRecordCal', N'Index', NULL, N'考勤管理', 0, N'[Index 页面]统计记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'统计记录添加', N'AttendanceRecordCal', N'Create', NULL, N'考勤管理', 0, N'[Create 请求]统计记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'统计记录调整工时', N'AttendanceRecordCal', N'Edit', NULL, N'考勤管理', 0, N'[Edit 请求]统计记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'统计记录详情', N'AttendanceRecordCal', N'Show', NULL, N'考勤管理', 0, N'[Show 请求]统计记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'统计记录删除', N'AttendanceRecordCal', N'Delete', NULL, N'考勤管理', 0, N'[Delete 请求]统计记录管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'统计记录导出报表', N'AttendanceRecordCal', N'ExportReport', NULL, N'考勤管理', 0, N'[ExportReport 请求]统计记录管理')


INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'异常统计列表', N'AttendanceRecordCal', N'ExceptionList', NULL, N'考勤管理', 0, N'[Index 页面]异常统计管理')
 
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤列表', N'AbsenceRecrod', N'Index', NULL, N'缺勤管理', 0, N'[Index 页面]缺勤管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤添加', N'AbsenceRecrod', N'Create', NULL, N'缺勤管理', 0, N'[Create 请求]缺勤管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤编辑', N'AbsenceRecrod', N'Edit', NULL, N'缺勤管理', 0, N'[Edit 请求]缺勤管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤删除', N'AbsenceRecrod', N'Delete', NULL, N'缺勤管理', 0, N'[Delete 请求]缺勤管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'缺勤审批', N'AbsenceRecrod', N'ApprovalAbsenceRecord', NULL, N'缺勤管理', 0, N'[ApprovalAbsenceRecord 请求]缺勤管理')


INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'加班列表', N'ExtraWorkRecord', N'Index', NULL, N'加班管理', 0, N'[Index 页面]加班管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'加班添加', N'ExtraWorkRecord', N'Create', NULL, N'加班管理', 0, N'[Create 请求]加班管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'加班编辑', N'ExtraWorkRecord', N'Edit', NULL, N'加班管理', 0, N'[Edit 请求]加班管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'加班删除', N'ExtraWorkRecord', N'Delete', NULL, N'加班管理', 0, N'[Delete 请求]加班管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'加班审批', N'ExtraWorkRecord', N'ApprovalExtraWorkRecord', NULL, N'加班管理', 0, N'[ApprovalExtraWorkRecord 请求]加班管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'系统任务列表', N'TaskRound', N'Index', NULL, N'系统任务管理', 0, N'[Index 页面]系统任务管理') 

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'考勤计算设置列表', N'QuartzJob', N'Index', NULL, N'系统设置', 0, N'[Index 页面]考勤计算设置管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'考勤计算设置添加', N'QuartzJob', N'Create', NULL, N'系统设置', 0, N'[Create 请求]考勤计算设置管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'考勤计算设置编辑', N'QuartzJob', N'Edit', NULL, N'系统设置', 0, N'[Edit 请求]考勤计算设置管理')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'考勤计算设置删除', N'QuartzJob', N'Delete', NULL, N'系统设置', 0, N'[Delete 请求]考勤计算设置管理')

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'系统信息设置列表', N'SystemSetting', N'Index', NULL, N'系统设置', 0, N'[Index 页面]系统信息设置管理') 

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'查看消息', N'MessageRecord', N'Index', NULL, N'系统设置', 0, N'[Index 页面]查看消息')  

INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'我的申请', N'Personal', N'Application', NULL, N'我的申请', 0, N'[我的申请]个人事项')
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'我的已办', N'Personal', N'Application', NULL, N'我的已办', 0, N'[我的已办]个人事项')  
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'我的审批', N'Personal', N'Application', NULL, N'我的审批', 0, N'[我的审批]个人事项')  
INSERT [dbo].[SysAuthorization] ( [name], [controlName], [actionName], [parentId], [funCode], [isDelete], [remarks]) VALUES ( N'日程安排', N'Personal', N'Application', NULL, N'日程安排', 0, N'[日程安排]个人事项')  
  

--作息表

use DerjinHr

INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-1','300','元旦节')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-2','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-3','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-4','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-5','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-6','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-7','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-9','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-10','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-11','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-13','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-16','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-17','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-20','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-23','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-24','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-27','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-30','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-1-31','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-2','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-3','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-4','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-5','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-6','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-7','200','双休日/周末/公休日 除夕')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-8','300','法定假日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-9','300','法定假日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-10','300','法定假日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-11','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-12','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-13','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-16','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-17','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-20','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-21','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-23','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-24','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-27','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-28','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-2-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-2','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-3','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-4','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-5','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-6','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-7','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-9','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-10','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-11','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-12','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-13','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-16','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-17','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-19','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-20','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-23','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-24','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-26','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-27','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-30','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-3-31','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-2','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-3','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-4','300','法定假日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-5','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-6','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-7','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-9','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-10','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-11','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-13','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-16','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-17','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-20','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-23','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-24','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-27','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-4-30','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-1','300','劳动节')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-2','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-3','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-4','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-5','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-6','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-7','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-8','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-9','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-10','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-11','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-13','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-14','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-15','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-16','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-17','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-20','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-21','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-22','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-23','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-24','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-27','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-28','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-29','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-30','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-5-31','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-2','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-3','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-4','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-5','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-6','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-7','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-9','300','端午节')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-10','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-11','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-13','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-16','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-17','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-18','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-19','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-20','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-23','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-24','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-25','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-26','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-27','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-6-30','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-2','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-3','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-4','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-5','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-6','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-7','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-9','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-10','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-11','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-13','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-16','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-17','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-20','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-23','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-24','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-27','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-30','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-7-31','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-2','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-3','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-4','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-5','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-6','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-7','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-9','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-10','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-11','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-13','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-14','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-16','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-17','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-20','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-21','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-23','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-24','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-27','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-28','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-30','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-8-31','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-2','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-3','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-4','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-5','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-6','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-7','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-9','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-10','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-11','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-13','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-15','300','中秋节')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-16','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-17','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-20','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-23','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-24','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-25','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-27','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-9-30','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-1','300','法定假日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-2','300','法定假日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-3','300','法定假日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-4','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-5','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-6','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-7','300','调休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-9','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-10','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-11','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-13','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-15','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-16','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-17','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-20','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-22','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-23','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-24','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-27','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-29','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-30','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-10-31','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-2','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-3','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-4','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-5','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-6','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-7','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-9','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-10','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-11','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-12','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-13','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-16','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-17','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-18','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-19','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-20','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-23','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-24','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-25','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-26','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-27','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-11-30','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-1','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-2','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-3','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-4','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-5','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-6','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-7','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-8','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-9','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-10','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-11','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-12','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-13','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-14','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-15','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-16','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-17','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-18','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-19','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-20','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-21','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-22','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-23','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-24','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-25','200','双休日/周末/公休日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-26','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-27','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-28','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-29','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-30','100','工作日')
INSERT INTO [DerjinHr].[dbo].[WorkAndRest]([dateAt],[dateType],[remark]) VALUES('2016-12-31','200','双休日/周末/公休日')


-- 添加公司

INSERT INTO [DerjinHr].[dbo].[Company] ([name] ,[remark] ,[address]) VALUES('上海德晋' ,NULL ,NULL)
INSERT INTO [DerjinHr].[dbo].[Company] ([name] ,[remark] ,[address]) VALUES('上海德奎' ,NULL ,NULL)
INSERT INTO [DerjinHr].[dbo].[Company] ([name] ,[remark] ,[address]) VALUES('上海园晋' ,NULL ,NULL)
INSERT INTO [DerjinHr].[dbo].[Company] ([name] ,[remark] ,[address]) VALUES('上海德昕' ,NULL ,NULL)

--添加部门

INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('财务部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('采购部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('人资行政部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('研发中心','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('业务中心','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('营运中心','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('质量管理部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('总经办','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('生产部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('业务部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('研发部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('供应部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('品保部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('总部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('模具部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('品质部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('顾问室','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('开发部','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('秘书室','','1',NULL)
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('技术','','1','4')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('计划部','','1','6')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('设计','','1','4')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('生产部','','1','6')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('工程部','','1','6')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('专案','','1','4')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('钢模课','','1','6')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('品质部','','1','6')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('供应部','','1','6')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('成型','','1','4')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('组件部','','1','6')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('财务课','','1','1')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('财务部','','1','1')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('采购课','','1','2')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('报关','','1','2')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('打样','','1','29')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('试模','','1','29')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('钳工组','','1','26')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('数控加工组','','1','26')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('钢模课','','1','26')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('设备课','','1','24')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('工程部','','1','24')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('生物管课','','1','12')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('报关','','1','12')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('储运课','','1','12')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('供应部','','1','12')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('顾问室','','1','17')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('储运课','','1','21')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('生物管课','','1','21')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('计划部','','1','21')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('物控','','1','21')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('二次加工','','1','20')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('成型','','1','20')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('开发部','','1','18')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('专案一课','','1','18')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('秘书室','','1','19')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('模具部','','1','15')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('QA','','1','13')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('QC','','1','13')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('QA','','1','16')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('品管课','','1','16')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('标准中心','','1','16')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('品保课','','1','16')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('行政课','','1','3')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('工程课','','1','3')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('人资课','','1','3')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('资讯课','','1','3')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('管理课','','1','3')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('报价评估','','1','22')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('产品设计','','1','22')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('创新专案','','1','22')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('组装课','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('成型课','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('自动化课','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('生产部','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('模具部','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('组件部','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('组装兼自动化','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('印刷课','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('泵组装','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('成品组装课','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('模具课','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('二次加工','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('备料课','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('园晋课','','1','9')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('研发部','','1','11')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('研发中心','','1','4')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('模具课','','1','4')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('业务部','','1','10')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('销售','','1','10')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('南区国内线','','1','10')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('客户服务','','1','10')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('销售部','','1','5')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('客户服务部','','1','5')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('营运中心','','1','6')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('标准中心','','1','7')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('品管课','','1','7')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('品保课','','1','7')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('新品开发','','1','25')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('专案','','1','25')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('专案一课','','1','25')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('专案二课','','1','25')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('总经理室','','1','14')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('总经办','','1','8')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('组件','','1','30')
INSERT INTO [DerjinHr].[dbo].[Department] ([name] ,[remark] ,[companyId] ,[parentId]) VALUES ('1','','3',NULL)


--职位

INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('QE',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('报关员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('采购员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('仓管员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('叉车工',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('成型生管',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('出纳',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('厨工',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('厨师',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('大堂接待',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('副经理',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('副课长',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('副理',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('副总经理',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('副组长',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('工程师',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('管理员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('会计',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('技术员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('经理',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('课长',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('量测',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('秘书助理',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('钳工',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('清洁工',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('设计员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('司机',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('文员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('物料员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('线长',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('销售代表',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('巡检员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('帐务员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('助理',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('助理工程师',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('专员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('组长',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('作业员',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('客服',NULL,'0')
INSERT INTO [DerjinHr].[dbo].[JobTitle]([name] ,[remark] ,[IsRevoked]) VALUES ('补贴人员',NULL,'0')


--建立班次

INSERT INTO [DerjinHr].[dbo].[Shift] ([code] ,[name] ,[startAt] ,[endAt] ,[shiftType] ,[remark]) VALUES ('生产白班','生产白班','08:00:00','16:00:00','100',NULL)
INSERT INTO [DerjinHr].[dbo].[Shift] ([code] ,[name] ,[startAt] ,[endAt] ,[shiftType] ,[remark]) VALUES ('生产夜班','生产夜班','20:00:00','04:00:00','200',NULL)
INSERT INTO [DerjinHr].[dbo].[Shift] ([code] ,[name] ,[startAt] ,[endAt] ,[shiftType] ,[remark]) VALUES ('园晋白班','园晋白班','08:00:00','16:30:00','100',NULL)
INSERT INTO [DerjinHr].[dbo].[Shift] ([code] ,[name] ,[startAt] ,[endAt] ,[shiftType] ,[remark]) VALUES ('园晋夜班','园晋夜班','20:00:00','04:30:00','200',NULL)
INSERT INTO [DerjinHr].[dbo].[Shift] ([code] ,[name] ,[startAt] ,[endAt] ,[shiftType] ,[remark]) VALUES ('办公室','办公室','08:00:00','17:00:00','100',NULL)
INSERT INTO [DerjinHr].[dbo].[Shift] ([code] ,[name] ,[startAt] ,[endAt] ,[shiftType] ,[remark]) VALUES ('德晋白班','德晋白班','09:00:00','22:00:00','100','白班')
