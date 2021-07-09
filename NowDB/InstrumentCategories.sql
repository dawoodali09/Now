CREATE TABLE [dbo].[InstrumentCategories]
(
	[Id] INT NOT NULL Identity(1,1) PRIMARY KEY,
	[InstrumentId] int not null foreign key ([InstrumentId]) REFERENCES Instruments([Id]),
	[CategoryId] int not null foreign key ([CategoryId]) REFERENCES Categories([Id])
)
