USE [BlueHrV2]
GO

/****** Object:  View [dbo].[ExtraWorkRecordView]    Script Date: 12/05/2016 19:19:23 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ExtraWorkRecordView]'))
DROP VIEW [dbo].[ExtraWorkRecordView]
GO

USE [BlueHrV2]
GO

/****** Object:  View [dbo].[ExtraWorkRecordView]    Script Date: 12/05/2016 19:19:24 ******/
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


