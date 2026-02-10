
CREATE TABLE [dbo].[Kids_VocabCategories](
	[Id] uniqueidentifier NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[BackgroundImage] [nvarchar](max) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime2](7) NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyOn] [datetime2](7) NULL,
	[Active] [bit] NOT NULL,
	[IsSoftDelete] [bit] NOT NULL,
	
 CONSTRAINT [Kids_VocabCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE PROCEDURE [dbo].[Kids_App_Sp_GetVocabCategories]
AS
BEGIN

SELECT 
		ID, 
		NAME,
		DESCRIPTION,
		BACKGROUNDIMAGE  
FROM 
		KIDS_VOCABCATEGORIES
WHERE 
		ISSOFTDELETE=0 AND ACTIVE=1 
ORDER BY
		DISPLAYORDER DESC

END




CREATE TABLE [dbo].[Kids_VocabSubCategories](
	[Id] uniqueidentifier NOT NULL,
	[CategoryId] uniqueidentifier NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[BackgroundImage] [nvarchar](max) NULL,
	[Sound_En] [nvarchar](max) NULL,
	[Sound_Gj] [nvarchar](max) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime2](7) NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyOn] [datetime2](7) NULL,
	[Active] [bit] NOT NULL,
	[IsSoftDelete] [bit] NOT NULL,
	
 CONSTRAINT [Kids_VocabSubCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


alter PROCEDURE [DBO].[KIDS_APP_SP_GETVOCABSUBCATEGORIES]
@CATEGORYID UNIQUEIDENTIFIER
AS
BEGIN

SELECT 
		ID, 
		CATEGORYID,
		NAME,
		DESCRIPTION,
		BACKGROUNDIMAGE,
		[SOUND_EN],
		[SOUND_GJ]
FROM 
		KIDS_VOCABSUBCATEGORIES
WHERE 
		CATEGORYID=@CATEGORYID
		AND ISSOFTDELETE=0 AND ACTIVE=1 
ORDER BY
		DISPLAYORDER DESC

END