SET NOCOUNT ON
 
SET IDENTITY_INSERT [dbo].[QuickLinkMaster] ON
GO
 
PRINT 'Inserting values into [QuickLinkMaster]'
--------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(1,'Rekhta',0,'User Management','L','UserManagement',NULL,1,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(2,'Rekhta',1,'Manage User','L','ManageUser','User',2,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(3,'Rekhta',0,'Learning Management','L',NULL,NULL,3,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(4,'Rekhta',3,'Master','L','QuestionLevelIndex','Content',4,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(5,'Rekhta',3,'Content','L','Index','Content',5,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(6,'Rekhta',3,'Programs','L','ProgramIndex','Program',6,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(7,'Rekhta',3,'Published Course','L','Index','Course',7,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(8,'Rekhta',0,'Event Management','L',NULL,NULL,8,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(9,'Rekhta',8,'Masters','L','ManageTopics','EventMasters',9,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(10,'Rekhta',8,'Manage Events','L','Index','EventManagement',10,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(11,'Rekhta',8,'Administration','L','Index','Administrator',11,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(12,'Rekhta',0,'Certificate Management','L',NULL,NULL,12,1)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(13,'Rekhta',12,'Manage Certificate','L',NULL,NULL,13,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(14,'Rekhta',0,'Settings','L',NULL,NULL,14,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(15,'Rekhta',14,'Auto mailer','L',NULL,NULL,15,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(16,'Rekhta',0,'Business Intelligence','L','Reports',NULL,16,0)
INSERT INTO [QuickLinkMaster] ([QuickLinkId],[HostName],[QuickLinkParentId],[QuickLinkName],[Type],[Action],[Area],[OrderNo],[IsDeleted])VALUES(17,'Rekhta',16,'Business Intelligence','L','Reports',NULL,17,0)

PRINT 'Done'
 
 
SET IDENTITY_INSERT [dbo].[QuickLinkMaster] OFF
GO
SET NOCOUNT OFF
