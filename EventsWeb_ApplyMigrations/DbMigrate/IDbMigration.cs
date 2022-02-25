using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWeb_ApplyMigrations.DbMigrate
{
    public interface IDbMigration
    {
        public void ApplyDbMigration();
    }
}
