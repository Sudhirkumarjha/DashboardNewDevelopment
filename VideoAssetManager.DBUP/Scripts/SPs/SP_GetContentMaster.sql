create PROCEDURE SP_GetContentMaster    
AS    
BEGIN    
Select c.ContentId,c.ContentGUID,isnull(c.ContentName,'') ContentName,
isnull(c.Title_HI,'') Title_HI,isnull(c.Title_UR,'') Title_UR,
isnull(c.ContentName,'')+','+isnull(c.Title_HI,'')+','+isnull(c.Title_UR,'') as Titles ,  
isnull(c.Description,'') Description,isnull(c.Description_HI,'') as Description_HI,isnull(c.Description_UR,'') Description_UR,
isnull(c.HostName,'') HostName,isnull(c.VideoFormat,0) VideoFormat,
case when isnull(c.Recordeddate,'1900-01-01 00:00:00.0000000')='0001-01-01 00:00:00.0000000' then '1900-01-01 00:00:00.0000000' else   c.Recordeddate end  Recordeddate,
isnull(vs.SourceName,'') SourceName,isnull(c.VideoStatus,0) VideoStatus,
case when isnull(c.CreatedOn,'1900-01-01 00:00:00.0000000')='0001-01-01 00:00:00.0000000' then '1900-01-01 00:00:00.0000000' else   c.CreatedOn end CreatedOn,  
case when isnull(c.ModifyOn,'1900-01-01 00:00:00.0000000')='0001-01-01 00:00:00.0000000' then '1900-01-01 00:00:00.0000000' else   c.ModifyOn end ModifyOn ,isnull(c.Active,0) Active,isnull(vm.VideoThumbnail,'') VideoThumbnail,isnull(vm.FileName,'') as VideoFileNamne  
from ContentMaster c    
left join VideoSourceMaster vs on vs.VideoSourceGuid=c.SourceId   
left join VideoMaster vm on vm.ContentId=c.ContentId  
  
where c.IsSoftDelete=0    
order by c.ModifyOn desc
END  
