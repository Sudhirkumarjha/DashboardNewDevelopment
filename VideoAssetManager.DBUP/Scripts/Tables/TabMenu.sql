CREATE TABLE TabMenu(
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](max) NULL,
	[ClassName] [nvarchar](max) NULL,
	[area] [nvarchar](max) NULL,
	[action] [nvarchar](max) NULL,
	[clickEvent] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[IsSoftDelete] [bit] NOT NULL,
	[TabdivId] [nvarchar](max) NULL,
	[SerialNo] [int] NOT NULL,
 CONSTRAINT [PK_TabMenu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)



