SET NOCOUNT ON
 
SET IDENTITY_INSERT [dbo].[TabMenu] ON
GO
 
 
PRINT 'Inserting values into [TabMenu]'
--------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(1,'Basic Information','firstTab','Program,Course','ProgramIndex,Create,LearningIndex','openTab(event, "BasicInformation")',1,0,'BasicInformation',1)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(2,'Add Learning','secondTab','Program,Course','ProgramIndex,Create,LearningIndex','openTab(event, "AddLearning")',1,0,'AddLearning',2)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(3,'Publish As Course','thirdTab','Program,Course','ProgramIndex,Create,LearningIndex','openTab(event, "publishCourse")',1,0,'publishCourse',3)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(4,'Basic Information','firstTab','Content,Quiz','Create,ManageQuestion','openTabContain(event, "BasicInformationContain")',1,0,'BasicInformation',1)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(5,'Content Information','secondTab','Content,Quiz','Create,ManageQuestion','openTabContain(event, "ContentInformation")',1,0,'ContentInformation',2)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(6,'Manage Question','thirdTab','Content,Quiz','Create,ManageQuestion','openTabContain(event, "ManageQuestion")',1,0,'ManageQuestion',3)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(7,'Manage Topics','firstTab','EventMasters','ManageTopics,Create,ManageInstructor,CreateInstructor','openTabEvent(event, "ManageTopics")',1,0,'ManageTopics',1)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(8,'Add Topic','secondTab','EventMasters','ManageTopics,Create,ManageInstructor,CreateInstructor','openTabEvent(event, "AddTopic")',1,0,'AddTopic',2)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(9,'Manage Instructors','thirdTab','EventMasters','ManageTopics,Create,ManageInstructor,CreateInstructor','openTabEvent(event, "ManageInstructors")',1,0,'ManageInstructors',3)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(10,'Add Instructor','fourthTab','EventMasters','ManageTopics,Create,ManageInstructor,CreateInstructor','openTabEvent(event, "AddInstructor")',1,0,'AddInstructor',4)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(11,'Question Information','firstTab','Quiz','AddQuestion,AddResponse,AddFeedback','openTabQuestion(event, ''QuestionInformation'')',1,0,'QuestionInformation',1)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(12,'Add Response','secondTab','Quiz','AddQuestion,AddResponse,AddFeedback','openTabQuestion(event, ''AddResponse'')',1,0,'AddResponse',2)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(13,'Add FeedBack','thirdTab','Quiz','AddQuestion,AddResponse,AddFeedback','openTabQuestion(event, ''AddFeedBack'')',1,0,'FeedBack',3)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(14,'Manage Question Level','firstTab','Content,Category','CreateQuestionLevel,QuestionLevelIndex,TagMasterIndex,CreateTagMaster,Index,CreateCat','openTabMaster(event, ''ManageQuestionLevel'')',1,0,'questionLevel',1)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(15,'Add Question Level','secondTab','Content,Category','CreateQuestionLevel,QuestionLevelIndex,TagMasterIndex,CreateTagMaster,Index,CreateCat','openTabMaster(event, ''AddQuestionLevel'')',1,0,'AddLevel',2)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(16,'Manage Tag Master','thirdTab','Content,Category','CreateQuestionLevel,QuestionLevelIndex,TagMasterIndex,CreateTagMaster,Index,CreateCat','openTabMaster(event, ''ManageTagMaster'')',1,0,'Tag',3)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(17,'Add Tag Master','fourthTab','Content,Category','CreateQuestionLevel,QuestionLevelIndex,TagMasterIndex,CreateTagMaster,Index,CreateCat','openTabMaster(event,''CreateTagMaster'')',1,0,'AddTag',4)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(18,'Category Master','fivetab','Content,Category','CreateQuestionLevel,QuestionLevelIndex,TagMasterIndex,CreateTagMaster,Index,CreateCat','openTabMaster(event,''CategoryMaster'')',1,0,'Category',5)
INSERT INTO [TabMenu] ([MenuId],[MenuName],[ClassName],[area],[action],[clickEvent],[Active],[IsSoftDelete],[TabdivId],[SerialNo])VALUES(24,'Add Category','sixthtab','Content,Category','CreateQuestionLevel,QuestionLevelIndex,TagMasterIndex,CreateTagMaster,Index,CreateCat','openTabMaster(event,''AddCategoryMaster'')',1,0,'AddCategory',6)

PRINT 'Done'
 
 
SET IDENTITY_INSERT [dbo].[TabMenu] OFF
GO
SET NOCOUNT OFF