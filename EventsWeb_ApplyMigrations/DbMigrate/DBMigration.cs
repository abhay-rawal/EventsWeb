using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events_Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace EventsWeb_ApplyMigrations.DbMigrate
{
    public class DBMigration : IDbMigration
    {
        private readonly ApplicationDbContext _dbContext;
        public DBMigration(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        ///<summary>
        ///     Check if anyMigrations are pending , If pending Apply Migrations
        ///</summary>
        public void ApplyDbMigration()
        {
            var pendingMigration =  _dbContext.Database.GetPendingMigrations();
            if(pendingMigration.Any())
            {
                 _dbContext.Database.Migrate();
            }
        }
    }
}
