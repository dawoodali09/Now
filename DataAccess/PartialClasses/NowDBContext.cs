using Microsoft.EntityFrameworkCore;

namespace SQLDataAccess.Models {
	public partial class NowDBContext : DbContext {
		private string configConnectionString;
		public NowDBContext(string ConnectionString) {
			configConnectionString = ConnectionString;			


		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		//	if (!optionsBuilder.IsConfigured) {
		//		optionsBuilder.UseSqlServer(this.configConnectionString);
		//	}
		//}
	}
}
