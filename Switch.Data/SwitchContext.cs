using Microsoft.AspNet.Identity.EntityFramework;
using Switch.Data.Migrations;
using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data
{
    
    public class SwitchContext : IdentityDbContext<IdentityUser, IdentityRole,
       string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public SwitchContext()
            : base("AuthContext")
        {
            UpdateDatabase();
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }
        public DbSet<Channels> channels { get; set; }
        public DbSet<Fees> Fees { get; set; }
        public DbSet<Routes> Routes { get; set; }
        public DbSet<Schemes> Schemes { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<TransLogs> TransLogs { get; set; }
        public DbSet<SourceNode> SourceNode { get; set; }
        public DbSet<SinkNode> SinkNode { get; set; }
       

       // DbConfiguration
           

        static SwitchContext Create()
        {

            return new SwitchContext();

        }

        public void UpdateDatabase()
        {
           // Database.CreateIfNotExists();
            //Remember to un-comment this line...
            Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<SwitchContext, Configuration>());
        }
    }
}
