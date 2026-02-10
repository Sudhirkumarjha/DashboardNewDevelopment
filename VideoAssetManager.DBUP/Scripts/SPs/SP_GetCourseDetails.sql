-- To get all public course launches
--CourseLaunchMaster
CREATE PROC  SP_GetCourseDetails
(
@CourseLaunchGuid UniqueIdentifier =null
)  
AS  
   SET NOCOUNT ON   ----------MAKE SURE TO SET THIS ON - AS IT IS A USELESS OVERHEAD IN THIS CASE----------      
    
DECLARE @CourseLaunchTeasureTmptable TABLE      
(      
	 CourseLaunchId INT NOT NULL,
	 TeasureVideoURL NVARCHAR(MAX) NULL,
	 SampleVideoURL NVARCHAR(MAX) NULL
)   

INSERT INTO @CourseLaunchTeasureTmptable
(
    
	 CourseLaunchId ,
	 TeasureVideoURL ,
	 SampleVideoURL 
)
SELECT   
      CourseLaunchId,
	  VideoMaster.PublishedURL,
	  null      
  FROM 
		CourseLaunchMaster		
		INNER JOIN ContentMaster ON
		CourseLaunchMaster.TreasureId=ContentMaster.ContentId
		INNER JOIN VideoMaster ON
		VideoMaster.ContentId=ContentMaster.ContentId		
  WHERE 
	(@CourseLaunchGuid  IS NULL OR CourseLaunchMaster.CourseLaunchGuid=@CourseLaunchGuid)
	AND CourseLaunchMaster.IsSoftDelete=0



   SELECT  
	CourseLaunchMaster.CourseLaunchId
      ,CourseLaunchMaster.[HostName]
      ,CourseLaunchMaster.[CourseLaunchGuid]
      ,CourseLaunchMaster.[CourseLaunchName]
      ,[CourseCode]
      ,CourseLaunchMaster.[Description]
      ,CourseLaunchMaster.ProgramId
	  ,LearningId
	  ,NodeType
      ,[SearchKeyword]
      ,[CourseAvailability]
      ,[StartDate]
      ,[EndDate]
      ,[CertificateId]
      ,[TreasureId] 
      ,[Thumbnail]
      ,[Price]
      ,[Discount]
      ,[OtherInformation]
      ,[VisibilitySettings]
	  , VideoMaster.PublishedURL
	  ,TMP.TeasureVideoURL
	  , '' SampleURl
	 
  FROM 
		ProgramMaster 
		INNER JOIN LearningPlanMaster ON 
		ProgramMaster.ProgramId=LearningPlanMaster.ProgramId
		INNER JOIN CourseLaunchMaster ON		
		CourseLaunchMaster.ProgramId=LearningPlanMaster.ProgramId		
		AND CourseLaunchMaster.ProgramId=ProgramMaster.ProgramId		
		INNER JOIN ContentMaster ON
		LearningPlanMaster.ResourceId=ContentMaster.ContentId
		INNER JOIN VideoMaster  ON
		VideoMaster.ContentId=ContentMaster.ContentId		
		INNER JOIN @CourseLaunchTeasureTmptable TMP ON 
		TMP.CourseLaunchId=CourseLaunchMaster.CourseLaunchId
  WHERE 
	(@CourseLaunchGuid  IS NULL OR CourseLaunchMaster.CourseLaunchGuid=@CourseLaunchGuid)
	AND CourseLaunchMaster.IsSoftDelete=0	
     
   SET NOCOUNT OFF
