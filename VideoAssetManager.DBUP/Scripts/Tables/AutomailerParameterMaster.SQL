CREATE TABLE AutomailerParameterMaster (
	AutomailerId				Int NOT NULL,
	ParameterIndex				Int	NOT NULL, 
	ParameterName				varchar(50) NOT NULL ,
	ParameterDisplayname		nvarchar(100) NOT NULL ,
	IsSoftDelete				bit NOT NULL	
) ON [PRIMARY]