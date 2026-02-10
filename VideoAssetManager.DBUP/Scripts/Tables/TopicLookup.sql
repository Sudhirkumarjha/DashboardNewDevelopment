CREATE TABLE TopicLookup(
	Id int primary key IDENTITY(1,1) NOT NULL,
	TopicGuid uniqueidentifier default(NewId()) not null,
	ContentId uniqueidentifier NULL,
	TopicId uniqueidentifier NULL
	
)