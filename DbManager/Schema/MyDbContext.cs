using Microsoft.EntityFrameworkCore;

namespace DbManager.Schema
{
    public class MyDbContext : DbContext
    {
        /// <summary>建構式</summary>
        /// <param name="options">設定 MyDbContext 的選項</param>
        public MyDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<MyTableSet> MyTable { get; set; }
    }
}
