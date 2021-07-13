CREATE TABLE [dbo].[Rules]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	UUID UniqueIdentifier NOT NULL DEFAULT NEWID(),
	[Name] nvarchar(100) NOT NULL ,
	[Type] nvarchar(100) NOT NULL,	
	[Description]  nvarchar(800) NULL,
	SupportPoint Int NOT NULL DEFAULT 0
)
