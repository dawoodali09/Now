using Microsoft.EntityFrameworkCore;
using System;

namespace SQLDataAccess.Models {
	public partial class NowDBContext : DbContext {
		private String configConnectionString;
		public NowDBContext(string ConnectionString) {
			configConnectionString = ConnectionString;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			if (!optionsBuilder.IsConfigured) {
				if (this != null && !string.IsNullOrEmpty(this.configConnectionString))
					optionsBuilder.UseSqlServer(this.configConnectionString);
				else
					throw new Exception("We need to fix this !!") ;
			}
		}
	}
}
