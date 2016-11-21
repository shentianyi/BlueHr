use bluehr
go

alter table systemsetting add defaultTrailMonth int

go

update SystemSetting set defaultTrailMonth=2;