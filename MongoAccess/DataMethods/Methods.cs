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

		public static void AddPriceHistory(Instrument ins, decimal price, DateTime RecordedOn, string connection) {			
			MongoClient dbClient = new MongoClient(connection);
			var dbList = dbClient.ListDatabases().ToList();
			var database = dbClient.GetDatabase("MarketData");
			var collection = database.GetCollection<BsonDocument>("PriceHistory");
			//var deleteFilter = Builders<BsonDocument>.Filter.Eq("Id", ins.Id);
			//collection.FindOneAndDelete(deleteFilter);
			Models.PriceHistory ph = new Models.PriceHistory() { Id = ins.Id, Price = price, RecordedOn = RecordedOn  };
			BsonDocument doc = ph.ToBsonDocument();
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
			List<Models.Category> CategoryList = new List<Models.Category>();
			MongoClient dbClient = new MongoClient(connection);			
			var database = dbClient.GetDatabase("MarketData");

			var collection = database.GetCollection<Instrument>("Instruments");
			var queryable = collection.AsQueryable();
			foreach (var ins in queryable.ToList()){
				result.Add(ins);
			}

	
			return result;
		}
	}
}
