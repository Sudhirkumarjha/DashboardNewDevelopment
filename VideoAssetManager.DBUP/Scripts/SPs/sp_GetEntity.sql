CREATE Procedure sp_GetEntity     
(    
@ContentId uniqueidentifier    
)    
AS      
BEGIN      
select e.Id,Name_En as Name from Rekhta_V5.[dbo].Entity e    
inner join EntityMappingLookup eml on e.Id=eml.EntityMappingId    
where IsActive=1  and eml.ContentId=@ContentId    
END