CREATE TABLE AutomailerDetails (
	HostName					nvarchar(10) NOT NULL ,
	AutomailerId				Int NOT NULL,
	AutomailerSubject			nvarchar(max) NOT NULL ,
	AutomailerBody				nvarchar(max) NOT NULL ,
	IsCCAdmin					bit NOT NULL ,
	IsBCCAdmin					bit NOT NULL ,
	CCList						varchar(250) NULL ,
	BCCList						varchar(250) NULL ,
	IsSendSMS					bit NOT NULL,
	SMSBody						nvarchar(max) NOT NULL,
	IsSendPushNotification		bit NOT NULL,
	PushNotificationBody		nvarchar(max) NOT NULL,
	IsBlocked					bit NOT NULL ,	
	CreatedOn					DateTime NULL ,
	CreatedBy					uniqueidentifier NULL ,
	ModifyOn					DateTime NULL ,
	ModifyBy					uniqueidentifier NULL 
) ON [PRIMARY]




