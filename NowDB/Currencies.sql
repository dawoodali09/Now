CREATE TABLE [dbo].[Currencies]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1) ,
	[Name] nvarchar(100) not null,
	ISOCode nvarchar(3) not null,
	ISONumber int not null default(0),
	ValueInNZD Money not null default 0
)
