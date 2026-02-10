  
  
CREATE PROCEDURE SP_Getvideosource  
AS  
BEGIN  
  
Select VideoSourceGuid,SourceName from videosourcemaster where IsSoftDelete=0  
  
END