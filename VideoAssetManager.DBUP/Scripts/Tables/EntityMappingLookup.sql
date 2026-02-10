CREATE Table EntityMappingLookup
(
Id int primary key identity(1,1),
EntityGuid uniqueidentifier default(NewId()) not null,
ContentId uniqueidentifier,
EntityMappingId uniqueidentifier
)
