    
CREATE Procedure sp_GeContentType      
(    
@ContentId uniqueidentifier    
)    
AS     
BEGIN      
select c.Id,Name_En as Name from Rekhta_V5.[dbo].ContentType c       
inner join ContentTypeLookup ctl on c.Id=ctl.ContentTypeId    
where ctl.ContentId=@ContentId    
    
END 