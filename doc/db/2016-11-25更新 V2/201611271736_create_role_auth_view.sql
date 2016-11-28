USE [BlueHr]
GO

/****** Object:  View [dbo].[SysRoleAuthView]    Script Date: 11/28/2016 10:03:03 ******/
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


