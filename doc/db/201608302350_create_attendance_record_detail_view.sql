USE [BlueHr]
GO

/****** Object:  View [dbo].[AttendanceRecordDetailView]    Script Date: 08/31/2016 00:09:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[AttendanceRecordDetailView]
AS
SELECT     dbo.AttendanceRecordDetail.id AS Expr1, dbo.AttendanceRecordDetail.staffNr, dbo.AttendanceRecordDetail.recordAt, dbo.AttendanceRecordDetail.createdAt, 
                      dbo.AttendanceRecordDetail.soureType, dbo.AttendanceRecordDetail.isCalculated, dbo.Staff.name, dbo.Staff.sex, dbo.Staff.birthday, dbo.Staff.ethnic, 
                      dbo.Staff.firstCompanyEmployAt, dbo.Staff.totalCompanySeniority, dbo.Staff.companyEmployAt, dbo.Staff.companySeniority, dbo.Staff.workStatus, dbo.Staff.isOnTrial, 
                      dbo.Staff.trialOverAt, dbo.Staff.companyId, dbo.Staff.departmentId, dbo.Staff.jobTitleId, dbo.Staff.photo, dbo.Staff.staffTypeId, dbo.Staff.degreeTypeId, 
                      dbo.Staff.speciality, dbo.Staff.residenceAddress, dbo.Staff.address, dbo.Staff.id, dbo.Staff.isIdChecked, dbo.Staff.phone, dbo.Staff.contactName, 
                      dbo.Staff.contactPhone, dbo.Staff.contactFamilyMemberType, dbo.Staff.domicile, dbo.Staff.residenceType, dbo.Staff.insureTypeId, dbo.Staff.isPayCPF, 
                      dbo.Staff.contractExpireAt, dbo.Staff.contractCount, dbo.Staff.totalSeniority, dbo.Staff.remark
FROM         dbo.AttendanceRecordDetail INNER JOIN
                      dbo.Staff ON dbo.AttendanceRecordDetail.staffNr = dbo.Staff.nr

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
            TopColumn = 15
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


