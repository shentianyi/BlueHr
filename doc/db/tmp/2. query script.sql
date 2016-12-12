select * from Staff

select * from FullMemberRecord

select * from ExtraWorkRecordApproval

select * from Shift

select * from ShiftSchedule where staffNr = '202274 ' and scheduleAt = '2016-8-10'

select * from Shift

select * from ShiftSchedule where scheduleAt = '2016-8-9'

select * from ShiftScheduleView where scheduleAt = '2016-8-9'

select * from Company

select nr, companyId, companyName from StaffView

select companyName, departmentName from StaffView group by companyName, departmentName

select * from ShiftScheduleView where scheduleAt='2016-8-3' order by scheduleAt

select Count(*) from ShiftScheduleView

select * from ExtraWorkRecord

select * from ExtraWorkRecordApproval

select * from FullMemberRecord

select * from [User]

select * from ResignRecord

select * from Staff