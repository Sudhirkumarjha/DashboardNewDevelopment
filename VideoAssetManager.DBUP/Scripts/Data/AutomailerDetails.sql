SET NOCOUNT ON

INSERT AutomailerDetails (HostName, AutomailerId, AutomailerSubject, AutomailerBody, IsCCAdmin, IsBCCAdmin, CCList, BCCList, IsSendSMS, SMSBody, IsSendPushNotification, PushNotificationBody, IsBlocked,  CreatedOn, CreatedBy, ModifyOn, ModifyBy) VALUES ('rekhta', 1, N'Course Launch!', N'Dear <<<1>>>,<br /> <br /> A new course <<<2>>> is now available on Rekhta Learning. Course details are - <br /> <br /> Description: <<<3>>><br /> <br /><br />Course URL: <<<4>>> <br />.  If you have any questions or need further assistance, please contact the system administrator.', 0, 0, NULL, NULL, 0, N'Hi <<<1>>>, A new course <<<2>>> is now available on Rekhta Learning. Course Details: <<<3>>>', 0, N'Hi <<<1>>>, A new course <<<2>>> is now available on Rekhta Learning.', 0, CAST(0xA58E0250 AS SmallDateTime), 'd50208e4-40c1-43cc-8bff-84dff3c3b1a5', CAST(0xA7740173 AS SmallDateTime), 'd50208e4-40c1-43cc-8bff-84dff3c3b1a5')

SET NOCOUNT OFF
