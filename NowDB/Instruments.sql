CREATE TABLE [dbo].[Instruments]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	SHID Uniqueidentifier not null,
	InstrumentType nvarchar(100) not null,
	Symbol nvarchar(6) not null, 
	KidsRecommended bit null default 0,
	IssVolatile bit null default 0,
	[Name] nvarchar(100) not null,
	[Description] nvarchar(MAX) null,
	Categories nvarchar(MAX) null,
	RiskRating int null default 0,
	MarketPrice money not null default 0,
	UpdatedOn DateTime2 not null default GETDATE(),
	MarketID int not null FOREIGN KEY (MarketID) REFERENCES Markets(ID),
	PERatio decimal null,
	MarketCap bigint not null default 0,
	WebsiteUrl varchar(400) null, 
	GrossDividendYieldPercent real null,
	AnnualisedReturnPercent real null,
	CEO nvarchar(300) null,
	Employee int null default 0
)
