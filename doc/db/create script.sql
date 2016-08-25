USE [master]
GO
/****** Object:  Database [BlueHr]    Script Date: 08/25/2016 20:13:39 ******/
CREATE DATABASE [BlueHr] ON  PRIMARY 
( NAME = N'BlueHr', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER2008R\MSSQL\DATA\BlueHr.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BlueHr_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER2008R\MSSQL\DATA\BlueHr_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BlueHr] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BlueHr].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BlueHr] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [BlueHr] SET ANSI_NULLS OFF
GO
ALTER DATABASE [BlueHr] SET ANSI_PADDING OFF
GO
ALTER DATABASE [BlueHr] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [BlueHr] SET ARITHABORT OFF
GO
ALTER DATABASE [BlueHr] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [BlueHr] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [BlueHr] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [BlueHr] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [BlueHr] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [BlueHr] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [BlueHr] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [BlueHr] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [BlueHr] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [BlueHr] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [BlueHr] SET  DISABLE_BROKER
GO
ALTER DATABASE [BlueHr] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [BlueHr] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [BlueHr] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [BlueHr] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [BlueHr] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [BlueHr] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [BlueHr] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [BlueHr] SET  READ_WRITE
GO
ALTER DATABASE [BlueHr] SET RECOVERY SIMPLE
GO
ALTER DATABASE [BlueHr] SET  MULTI_USER
GO
ALTER DATABASE [BlueHr] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [BlueHr] SET DB_CHAINING OFF
GO
USE [BlueHr]
GO
/****** Object:  Table [dbo].[Shift]    Script Date: 08/25/2016 20:13:39 ******/
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
/****** Object:  Table [dbo].[ResignType]    Script Date: 08/25/2016 20:13:39 ******/
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
/****** Object:  Table [dbo].[StaffType]    Script Date: 08/25/2016 20:13:39 ******/
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
/****** Object:  Table [dbo].[InsureType]    Script Date: 08/25/2016 20:13:39 ******/
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
/****** Object:  Table [dbo].[JobTitle]    Script Date: 08/25/2016 20:13:39 ******/
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
 CONSTRAINT [PK_JobTitle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DegreeType]    Script Date: 08/25/2016 20:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DegreeType](
	[id] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Company]    Script Date: 08/25/2016 20:13:39 ******/
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
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CertificateType]    Script Date: 08/25/2016 20:13:39 ******/
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
	[isSystem] [tinyint] NOT NULL,
	[isNecessary] [tinyint] NOT NULL,
 CONSTRAINT [PK_CertificateType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 08/25/2016 20:13:39 ******/
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
	[attachmentAbleType] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AbsenceType]    Script Date: 08/25/2016 20:13:39 ******/
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
 CONSTRAINT [PK_AbsenceType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExtraWorkType]    Script Date: 08/25/2016 20:13:39 ******/
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
 CONSTRAINT [PK_ExtraWorkType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Department]    Script Date: 08/25/2016 20:13:39 ******/
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
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JobCertificate]    Script Date: 08/25/2016 20:13:39 ******/
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
/****** Object:  Table [dbo].[Staff]    Script Date: 08/25/2016 20:13:39 ******/
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
	[firstCompanyEmployAt] [datetime] NULL,
	[totalCompanySeniority] [float] NULL,
	[companyEmployAt] [datetime] NULL,
	[companySeniority] [float] NULL,
	[workStatus] [int] NOT NULL,
	[isOnTrial] [tinyint] NOT NULL,
	[trialOverAt] [datetime] NULL,
	[companyId] [int] NOT NULL,
	[departmentId] [int] NOT NULL,
	[jobTitleId] [int] NULL,
	[photo] [image] NULL,
	[staffTypeId] [int] NULL,
	[degreeTypeId] [int] NULL,
	[speciality] [varchar](200) NULL,
	[residenceAddress] [varchar](200) NULL,
	[address] [varchar](200) NULL,
	[id] [varchar](200) NOT NULL,
	[phone] [varchar](50) NULL,
	[contactName] [varchar](50) NULL,
	[contactPhone] [varchar](50) NULL,
	[contactFamilyMemberType] [varchar](50) NULL,
	[domicile] [varchar](50) NULL,
	[residenceType] [int] NULL,
	[insureTypeId] [int] NULL,
	[isPayCPF] [tinyint] NOT NULL,
	[contractExpireAt] [date] NULL,
	[contractCount] [int] NULL,
	[totalSeniority] [float] NULL,
	[remark] [text] NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[nr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ResignRecord]    Script Date: 08/25/2016 20:13:39 ******/
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
	[resignChecker] [varchar](50) NULL,
	[remark] [text] NULL,
 CONSTRAINT [PK_ResignRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShiftSchedule]    Script Date: 08/25/2016 20:13:39 ******/
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
/****** Object:  Table [dbo].[FullMemberRecord]    Script Date: 08/25/2016 20:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FullMemberRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[isPassCheck] [tinyint] NOT NULL,
	[beFullAt] [datetime] NULL,
	[checkAt] [datetime] NULL,
	[beFullChecker] [varchar](50) NULL,
	[checkScore] [float] NULL,
	[remark] [text] NULL,
 CONSTRAINT [PK_FullMemberRecord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FamilyMemeber]    Script Date: 08/25/2016 20:13:39 ******/
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
/****** Object:  Table [dbo].[ExtraWorkRecord]    Script Date: 08/25/2016 20:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExtraWorkRecord](
	[id] [int] NOT NULL,
	[extraWorkTypeId] [int] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
	[duration] [float] NOT NULL,
	[durationType] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AbsenceRecrod]    Script Date: 08/25/2016 20:13:39 ******/
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
 CONSTRAINT [PK_AbsenceRecrod] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Certificate]    Script Date: 08/25/2016 20:13:39 ******/
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
/****** Object:  Table [dbo].[BankCard]    Script Date: 08/25/2016 20:13:39 ******/
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
	[isDefault] [tinyint] NOT NULL,
	[staffNr] [varchar](200) NOT NULL,
 CONSTRAINT [PK_BankCard] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_CertificateType_isSystem]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[CertificateType] ADD  CONSTRAINT [DF_CertificateType_isSystem]  DEFAULT ((0)) FOR [isSystem]
GO
/****** Object:  Default [DF_CertificateType_isNecessary]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[CertificateType] ADD  CONSTRAINT [DF_CertificateType_isNecessary]  DEFAULT ((0)) FOR [isNecessary]
GO
/****** Object:  Default [DF_Staff_isOnTrial]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_isOnTrial]  DEFAULT ((0)) FOR [isOnTrial]
GO
/****** Object:  Default [DF_Staff_isPayCFP]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_isPayCFP]  DEFAULT ((0)) FOR [isPayCPF]
GO
/****** Object:  Default [DF_FullMemberRecord_isPassCheck]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[FullMemberRecord] ADD  CONSTRAINT [DF_FullMemberRecord_isPassCheck]  DEFAULT ((0)) FOR [isPassCheck]
GO
/****** Object:  Default [DF_BankCard_isDefault]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[BankCard] ADD  CONSTRAINT [DF_BankCard_isDefault]  DEFAULT ((0)) FOR [isDefault]
GO
/****** Object:  ForeignKey [FK_Department_Company]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Company]
GO
/****** Object:  ForeignKey [FK_JobCertificate_CertificateType]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[JobCertificate]  WITH CHECK ADD  CONSTRAINT [FK_JobCertificate_CertificateType] FOREIGN KEY([certificateTypeId])
REFERENCES [dbo].[CertificateType] ([id])
GO
ALTER TABLE [dbo].[JobCertificate] CHECK CONSTRAINT [FK_JobCertificate_CertificateType]
GO
/****** Object:  ForeignKey [FK_JobCertificate_JobTitle]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[JobCertificate]  WITH CHECK ADD  CONSTRAINT [FK_JobCertificate_JobTitle] FOREIGN KEY([jobTitleId])
REFERENCES [dbo].[JobTitle] ([id])
GO
ALTER TABLE [dbo].[JobCertificate] CHECK CONSTRAINT [FK_JobCertificate_JobTitle]
GO
/****** Object:  ForeignKey [FK_Staff_Company]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Company]
GO
/****** Object:  ForeignKey [FK_Staff_DegreeType]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_DegreeType] FOREIGN KEY([degreeTypeId])
REFERENCES [dbo].[DegreeType] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_DegreeType]
GO
/****** Object:  ForeignKey [FK_Staff_Department]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Department] FOREIGN KEY([departmentId])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Department]
GO
/****** Object:  ForeignKey [FK_Staff_InsureType]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_InsureType] FOREIGN KEY([insureTypeId])
REFERENCES [dbo].[InsureType] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_InsureType]
GO
/****** Object:  ForeignKey [FK_Staff_JobTitle]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_JobTitle] FOREIGN KEY([jobTitleId])
REFERENCES [dbo].[JobTitle] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_JobTitle]
GO
/****** Object:  ForeignKey [FK_Staff_StaffType]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_StaffType] FOREIGN KEY([staffTypeId])
REFERENCES [dbo].[StaffType] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_StaffType]
GO
/****** Object:  ForeignKey [FK_ResignRecord_ResignType]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[ResignRecord]  WITH CHECK ADD  CONSTRAINT [FK_ResignRecord_ResignType] FOREIGN KEY([resignTypeId])
REFERENCES [dbo].[ResignType] ([id])
GO
ALTER TABLE [dbo].[ResignRecord] CHECK CONSTRAINT [FK_ResignRecord_ResignType]
GO
/****** Object:  ForeignKey [FK_ResignRecord_Staff]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[ResignRecord]  WITH CHECK ADD  CONSTRAINT [FK_ResignRecord_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[ResignRecord] CHECK CONSTRAINT [FK_ResignRecord_Staff]
GO
/****** Object:  ForeignKey [FK_ShiftSchedule_Shift]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[ShiftSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ShiftSchedule_Shift] FOREIGN KEY([shiftId])
REFERENCES [dbo].[Shift] ([id])
GO
ALTER TABLE [dbo].[ShiftSchedule] CHECK CONSTRAINT [FK_ShiftSchedule_Shift]
GO
/****** Object:  ForeignKey [FK_ShiftSchedule_Staff]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[ShiftSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ShiftSchedule_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[ShiftSchedule] CHECK CONSTRAINT [FK_ShiftSchedule_Staff]
GO
/****** Object:  ForeignKey [FK_FullMemberRecord_Staff]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[FullMemberRecord]  WITH CHECK ADD  CONSTRAINT [FK_FullMemberRecord_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[FullMemberRecord] CHECK CONSTRAINT [FK_FullMemberRecord_Staff]
GO
/****** Object:  ForeignKey [FK_FamilyMemeber_Staff]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[FamilyMemeber]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMemeber_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[FamilyMemeber] CHECK CONSTRAINT [FK_FamilyMemeber_Staff]
GO
/****** Object:  ForeignKey [FK_ExtraWorkRecord_ExtraWorkType]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[ExtraWorkRecord]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecord_ExtraWorkType] FOREIGN KEY([extraWorkTypeId])
REFERENCES [dbo].[ExtraWorkType] ([id])
GO
ALTER TABLE [dbo].[ExtraWorkRecord] CHECK CONSTRAINT [FK_ExtraWorkRecord_ExtraWorkType]
GO
/****** Object:  ForeignKey [FK_ExtraWorkRecord_Staff]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[ExtraWorkRecord]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecord_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[ExtraWorkRecord] CHECK CONSTRAINT [FK_ExtraWorkRecord_Staff]
GO
/****** Object:  ForeignKey [FK_AbsenceRecrod_AbsenceType]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[AbsenceRecrod]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceRecrod_AbsenceType] FOREIGN KEY([absenceTypeId])
REFERENCES [dbo].[AbsenceType] ([id])
GO
ALTER TABLE [dbo].[AbsenceRecrod] CHECK CONSTRAINT [FK_AbsenceRecrod_AbsenceType]
GO
/****** Object:  ForeignKey [FK_AbsenceRecrod_Staff]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[AbsenceRecrod]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceRecrod_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[AbsenceRecrod] CHECK CONSTRAINT [FK_AbsenceRecrod_Staff]
GO
/****** Object:  ForeignKey [FK_Certificate_CertificateType]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD  CONSTRAINT [FK_Certificate_CertificateType] FOREIGN KEY([certificateTypeId])
REFERENCES [dbo].[CertificateType] ([id])
GO
ALTER TABLE [dbo].[Certificate] CHECK CONSTRAINT [FK_Certificate_CertificateType]
GO
/****** Object:  ForeignKey [FK_Certificate_Staff]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD  CONSTRAINT [FK_Certificate_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[Certificate] CHECK CONSTRAINT [FK_Certificate_Staff]
GO
/****** Object:  ForeignKey [FK_BankCard_Staff]    Script Date: 08/25/2016 20:13:39 ******/
ALTER TABLE [dbo].[BankCard]  WITH CHECK ADD  CONSTRAINT [FK_BankCard_Staff] FOREIGN KEY([staffNr])
REFERENCES [dbo].[Staff] ([nr])
GO
ALTER TABLE [dbo].[BankCard] CHECK CONSTRAINT [FK_BankCard_Staff]
GO
