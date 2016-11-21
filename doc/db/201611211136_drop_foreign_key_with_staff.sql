USE [BlueHr]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResignRecord_Staff]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResignRecord]'))
ALTER TABLE [dbo].[ResignRecord] DROP CONSTRAINT [FK_ResignRecord_Staff]
GO


