/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 * 
  FROM [DerjinHr].[dbo].[AttendanceRecordDetail] 
  order by recordAt desc, staffNr;
  
  SELECT TOP 1000 *
  FROM [DerjinHr].[dbo].[AttendanceRecordDetail]
  order by staffNr, recordAt desc;
  
  select COUNT(*) from AttendanceRecordDetail;