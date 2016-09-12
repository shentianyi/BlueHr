use bluehr
go

alter table MessageRecord add messageCategory int
go

alter table MessageRecord drop column url 
go 

alter table MessageRecord drop column isUrl 
go