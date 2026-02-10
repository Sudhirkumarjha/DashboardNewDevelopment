
CREATE PROC  SP_GetUserDetails
(
@UserId UniqueIdentifier 
)  
AS  
   SET NOCOUNT ON   

   SELECT  AspNetUsers.[Id]
      ,[UserName]
      ,[NormalizedUserName]
      ,AspNetUsers.[Email]
      ,[NormalizedEmail]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[ConcurrencyStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEnd]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,[Discriminator]
      ,AspNetUsers.Name
	  , [HostName]   
      ,[DOB]
      ,[Occupation]
      ,[MobileNumber]
      ,[StreetAddress]
      ,[City]
      ,[State]
      ,[PostalCode]
      ,[CountryCode]
      ,[CreatedOn]
      ,[Active]
      ,[IsDeleted]
      ,[ModifiedOn],
	  UserId
  FROM 
		AspNetUsers
  INNER JOIN
		UserDetail ON  AspNetUsers.Id=UserDetail.Id
  WHERE 
		AspNetUsers.[Id]=@UserId
     
   SET NOCOUNT OFF
