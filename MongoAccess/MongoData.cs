using Common.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoAccess {
	public static class MongoData {
		public static void AddInstrumment(Instrument ins) {
			BsonDocument doc = ins.ToBsonDocument();
			MongoClient dbClient = new MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");

			var dbList = dbClient.ListDatabases().ToList();
			var database = dbClient.GetDatabase("MarketData");
			var collection = database.GetCollection<BsonDocument>("Instruments");

			collection.InsertOne(doc);

		}
	}
}
