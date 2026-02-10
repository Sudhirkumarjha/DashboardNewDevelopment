CREATE Procedure sp_GeTopic      
(    
@ContentId uniqueidentifier    
)    
AS      
BEGIN      
select lm.Id,Name_En as Name from Rekhta_V5.[dbo].ContentTag lm    
join TopicLookup tl on lm.Id=tl.TopicId    
where  tl.ContentId=@ContentId    
END