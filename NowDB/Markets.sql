CREATE TABLE [dbo].[Markets]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(100) not null,
	[Code] nvarchar(100) not null,
	IsOpen bit not null default (0),
	Currency nvarchar(5) not null, 
    [Country] NVARCHAR(100) NULL	
)