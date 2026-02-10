CREATE Procedure sp_GeListMaster      
(    
@ContentId uniqueidentifier    
)    
AS      
BEGIN      
select lm.Id,Name from Rekhta_V5.[dbo].ListMaster lm    
join ListLookup l on lm.Id=l.ListFieldId    
where  l.ContentId=@ContentId    
END    