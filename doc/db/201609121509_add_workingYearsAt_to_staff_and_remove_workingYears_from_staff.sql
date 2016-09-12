USE [BlueHr]
GO

ALTER TABLE [dbo].[Staff] DROP COLUMN [workingYears]

ALTER TABLE [dbo].[Staff] ADD  [workingYearsAt] datetime

go