CREATE PROC SP_GetAdminUsers
AS  
   SET NOCOUNT ON   

  SELECT UserDetail.Id
,UserDetail.Email 
FROM 
    UserDetail
INNER JOIN
         AspNetUserRoles ON UserDetail.Id = AspNetUserRoles.UserId
INNER JOIN
         AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id
WHERE 
        AspNetRoles.[Name]='Admin'
     
   SET NOCOUNT OFF
GO



