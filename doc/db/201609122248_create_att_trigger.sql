
use BlueHr
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER   AttRecordInsertTrigger
   ON   AttendanceRecordDetail -- 需要改动
   AFTER INSERT
AS 
 set nocount on
 set xact_abort on;
 
    insert into [bluehr_db_server].BlueHr.dbo.AttendanceRecordDetail(staffNr,recordAt,createdAt,soureType,isCalculated,device)
   select   staffNr,recordAt,createdAt,'DbTrigger',0,[device] from inserted

GO
