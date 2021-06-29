CREATE TABLE [dbo].[PriceHistory]
(
	[Id] bigINT NOT NULL Identity(1,1) PRIMARY KEY,
	[InstrumentId] Int not null FOREIGN KEY ([InstrumentId]) REFERENCES [Instruments]([Id]),
	[RecordedOn] DateTime2(7) Not Null Default GETDATE(),
	[Price] Money Not Null Default 0
)




GO


CREATE INDEX [NonClusteredIndex-PriceHistory-InstrumentId] ON [dbo].[PriceHistory] ([InstrumentId])
