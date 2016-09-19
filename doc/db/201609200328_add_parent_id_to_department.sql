ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department] FOREIGN KEY([parentId])
REFERENCES [dbo].[Department] ([id])
GO

ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department]
GO
