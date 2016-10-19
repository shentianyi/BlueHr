USE [BlueHr]
GO
/****** Object:  Table [dbo].[SysRole]    Script Date: 10/19/2016 14:06:34 ******/
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
/****** Object:  Table [dbo].[SysAuthorization]    Script Date: 10/19/2016 14:06:34 ******/
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
/****** Object:  Table [dbo].[SysUserDataAuth]    Script Date: 10/19/2016 14:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUserDataAuth](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[cmpId] [int] NULL,
	[departId] [int] NULL,
	[remarks] [nvarchar](200) NULL,
 CONSTRAINT [PK_SysUserDataAuth] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRoleAuthorization]    Script Date: 10/19/2016 14:06:34 ******/
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
/****** Object:  Table [dbo].[ExtraWorkRecordApproval]    Script Date: 10/19/2016 14:06:34 ******/
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
/****** Object:  Table [dbo].[AbsenceRecordApproval]    Script Date: 10/19/2016 14:06:34 ******/
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
/****** Object:  ForeignKey [FK_AbsenceRecordApproval_AbsenceRecrod]    Script Date: 10/19/2016 14:06:34 ******/
ALTER TABLE [dbo].[AbsenceRecordApproval]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceRecordApproval_AbsenceRecrod] FOREIGN KEY([absRecordId])
REFERENCES [dbo].[AbsenceRecrod] ([id])
GO
ALTER TABLE [dbo].[AbsenceRecordApproval] CHECK CONSTRAINT [FK_AbsenceRecordApproval_AbsenceRecrod]
GO
/****** Object:  ForeignKey [FK_AbsenceRecordApproval_User]    Script Date: 10/19/2016 14:06:34 ******/
ALTER TABLE [dbo].[AbsenceRecordApproval]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceRecordApproval_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[AbsenceRecordApproval] CHECK CONSTRAINT [FK_AbsenceRecordApproval_User]
GO
/****** Object:  ForeignKey [FK_ExtraWorkRecordApproval_ExtraWorkRecord]    Script Date: 10/19/2016 14:06:34 ******/
ALTER TABLE [dbo].[ExtraWorkRecordApproval]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecordApproval_ExtraWorkRecord] FOREIGN KEY([extraWorkId])
REFERENCES [dbo].[ExtraWorkRecord] ([id])
GO
ALTER TABLE [dbo].[ExtraWorkRecordApproval] CHECK CONSTRAINT [FK_ExtraWorkRecordApproval_ExtraWorkRecord]
GO
/****** Object:  ForeignKey [FK_ExtraWorkRecordApproval_User]    Script Date: 10/19/2016 14:06:34 ******/
ALTER TABLE [dbo].[ExtraWorkRecordApproval]  WITH CHECK ADD  CONSTRAINT [FK_ExtraWorkRecordApproval_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[ExtraWorkRecordApproval] CHECK CONSTRAINT [FK_ExtraWorkRecordApproval_User]
GO
/****** Object:  ForeignKey [FK_SysRoleAuthorization_SysAuthorization]    Script Date: 10/19/2016 14:06:34 ******/
ALTER TABLE [dbo].[SysRoleAuthorization]  WITH CHECK ADD  CONSTRAINT [FK_SysRoleAuthorization_SysAuthorization] FOREIGN KEY([authId])
REFERENCES [dbo].[SysAuthorization] ([id])
GO
ALTER TABLE [dbo].[SysRoleAuthorization] CHECK CONSTRAINT [FK_SysRoleAuthorization_SysAuthorization]
GO
/****** Object:  ForeignKey [FK_SysRoleAuthorization_SysRole]    Script Date: 10/19/2016 14:06:34 ******/
ALTER TABLE [dbo].[SysRoleAuthorization]  WITH CHECK ADD  CONSTRAINT [FK_SysRoleAuthorization_SysRole] FOREIGN KEY([roleId])
REFERENCES [dbo].[SysRole] ([id])
GO
ALTER TABLE [dbo].[SysRoleAuthorization] CHECK CONSTRAINT [FK_SysRoleAuthorization_SysRole]
GO
/****** Object:  ForeignKey [FK_SysUserDataAuth_User]    Script Date: 10/19/2016 14:06:34 ******/
ALTER TABLE [dbo].[SysUserDataAuth]  WITH CHECK ADD  CONSTRAINT [FK_SysUserDataAuth_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[SysUserDataAuth] CHECK CONSTRAINT [FK_SysUserDataAuth_User]
GO
