using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System;


namespace MongoAccess.DataMethods {
	public static class Methods {
		public static void AddInstrumment(Instrument ins, string connection) {
			BsonDocument doc = ins.ToBsonDocument();
			MongoClient dbClient = new MongoClient(connection);
			var dbList = dbClient.ListDatabases().ToList();
			var database = dbClient.GetDatabase("MarketData");
			var collection = database.GetCollection<BsonDocument>("Instruments");
			var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", ins.Id);
			collection.FindOneAndDelete(deleteFilter);
			collection.InsertOne(doc);
		}

		public static void AddPriceHistory(Common.Models.SharePriceHistory sph, string connection) {			
			MongoClient dbClient = new MongoClient(connection);
			var dbList = dbClient.ListDatabases().ToList();
			var database = dbClient.GetDatabase("MarketData");
			var collection = database.GetCollection<BsonDocument>("PriceHistory");
			var deleteFilter = Builders<BsonDocument>.Filter.Eq("Id", sph.Id);
			collection.FindOneAndDelete(deleteFilter);
			BsonDocument doc = sph.ToBsonDocument();
			collection.InsertOne(doc);
		}

		public static void AddUpdateCategories(string connection) {
			List<Models.Category> CategoryList = new List<Models.Category>();
			MongoClient dbClient = new MongoClient(connection);
			var database = dbClient.GetDatabase("MarketData");
			var collection = database.GetCollection<Instrument>("Instruments");
			var queryable = collection.AsQueryable();
			int ind = 1;
			foreach (var ins in queryable.ToList()) {
				foreach (string cat in ins.Categories) {
					if (!CategoryList.Where(s => s.Name == cat).Any()) {
						CategoryList.Add(new Models.Category() { Id = ind, Name = cat });
						ind++;
					}
				}
			}

			var collection2 = database.GetCollection<BsonDocument>("Categories");
			foreach (var cat in CategoryList) {
				var deleteFilter = Builders<BsonDocument>.Filter.Eq("Name", cat.Name);
				collection2.FindOneAndDelete(deleteFilter);

				BsonDocument doc = cat.ToBsonDocument();
				collection2.InsertOne(doc);
			}
		}



		public static List<Instrument> ListInstruments(string connection) {
			List<Instrument> result = new List<Instrument>();			
			MongoClient dbClient = new MongoClient(connection);			
			var database = dbClient.GetDatabase("MarketData");
			var collection = database.GetCollection<Instrument>("Instruments");
			var queryable = collection.AsQueryable();
			foreach (var ins in queryable.ToList()){
				result.Add(ins);
			}
			return result;
		}

		public static List<Combo> ListInstruments(string market, string connection)
		{
			List<Instrument> result = new List<Instrument>();			
			MongoClient dbClient = new MongoClient(connection);
			var database = dbClient.GetDatabase("MarketData");
			var collection = database.GetCollection<Instrument>("Instruments");
			var builder = Builders<Instrument>.Filter;
			var filter = builder.Eq("Exchange", market);
			result = collection.Find(filter).ToList();

			List<Combo> comboList = new List<Combo>();
			foreach (var ins in result)
            {
				var sph = ListStockPriceHistory(ins.Id, connection);
				Combo cmb = new Combo()
				{
					instruent = ins,
					sph = sph
				};
				comboList.Add(cmb);

			}
			return comboList;
			
		}

		public static Common.Models.SharePriceHistory ListStockPriceHistory(string stockId, string connection)
		{
            Common.Models.SharePriceHistory result = new SharePriceHistory();
            MongoClient dbClient = new MongoClient(connection);
			var database = dbClient.GetDatabase("MarketData");
			var collection = database.GetCollection<Common.Models.SharePriceHistory>("PriceHistory");
			var builder = Builders<Common.Models.SharePriceHistory>.Filter;
			var filter = builder.Eq("Id", stockId);
			result = collection.Find(filter).FirstOrDefault();
			var test = collection.Find(filter).ToList();
			
			return result;
		}

		private static IMongoDatabase GetDatabase(string connection)
		{
			MongoClient mongoClient = new MongoClient(connection);
			return mongoClient.GetDatabase("MarketData");
}

		public static IMongoCollection<BsonDocument> GetCollection(string collection, string connection)
		{
			return GetDatabase(connection).GetCollection<BsonDocument>(collection);
		}

		public static IMongoCollection<TDocument> GetCollection<TDocument>(string collection, string con)
		{
			return GetDatabase(con).GetCollection<TDocument>(collection);
		}
	}
}
