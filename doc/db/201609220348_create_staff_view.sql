USE [BlueHr]
GO

/****** Object:  View [dbo].[StaffView]    Script Date: 09/22/2016 04:43:53 ******/
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


