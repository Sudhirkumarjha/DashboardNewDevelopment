CREATE Table ContentTypeLookup
(
Id int primary key identity(1,1),
ContentGuid uniqueidentifier default(NewId()) not null,
ContentId uniqueidentifier,
ContentTypeId uniqueidentifier
)