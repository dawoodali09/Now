using Common.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoAccess {
	public static class MongoData {
		public static void AddInstrumment(Instrument ins, string connection) {
			BsonDocument doc = ins.ToBsonDocument();
			//"mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false"
			MongoClient dbClient = new MongoClient(connection);

			var dbList = dbClient.ListDatabases().ToList();
			var database = dbClient.GetDatabase("MarketData");
		
			var collection = database.GetCollection<BsonDocument>("Instruments");

			var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", ins.Id);
			collection.FindOneAndDelete(deleteFilter);

			collection.InsertOne(doc);

		}
	}
}
