CREATE TABLE AutomailerMaster(
	AutomailerId Int IDENTITY(1,1) NOT NULL,
	AutomailerCode varchar(50) NOT NULL,
	AutomailerName nvarchar(250) NOT NULL,
	AutomailerDescription nvarchar(max) NOT NULL,	
	IsOutlookMeetingMessage nvarchar(max) NOT NULL	
) ON [PRIMARY]