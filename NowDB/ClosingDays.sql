CREATE TABLE [dbo].[ClosingDays]
(
	[Id] INT Identity(1,1) NOT NULL PRIMARY KEY,
	MarketId int not null Foreign Key ([MarketId]) References [Markets]([Id]),
	ClosingDate DateTime2 Not null default GETDATE(),
	Reason nvarchar(500) not null default 'Weekend'
)
