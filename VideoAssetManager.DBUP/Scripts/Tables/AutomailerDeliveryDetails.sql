CREATE TABLE AutomailerDeliveryDetails (
	AutomailerId 				Int NOT NULL,
	HostName					nvarchar(10) NOT NULL ,
	IsDeliveryFaled				bit NOT NULL ,
	DeliveryFailureReason	    nvarchar(max) NULL,
	DeliveryDate				Datetime NOT NULL ,
	AutomailerSubject			nvarchar(max) NOT NULL ,
	AutomailerBody				nvarchar(max) NOT NULL ,
	FromAddress					nvarchar(max) NOT NULL ,
	ToList						nvarchar(max) NOT NULL ,
	CCList						varchar(250) NULL ,
	BCCList						varchar(250) NULL ,
	SMTPServer					varchar(250) NULL ,	
	IsSoftDelete				bit NOT NULL 	
) ON [PRIMARY]
