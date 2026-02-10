CREATE Table ListLookup
(
Id int primary key identity(1,1),
ListGuid uniqueidentifier default(NewId()) not null,
ContentId uniqueidentifier,
ListFieldId uniqueidentifier
)
