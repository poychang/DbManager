using DbManager.Option;
using DbManager.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DbManager
{
    /// <summary>SQLite 資料庫管理者</summary>
    public class SqliteDbManager : DbManager
    {
        /// <inheritdoc />
        public SqliteDbManager(IOptions<DbManagerOptions> optionsAccessor) : base(optionsAccessor) { }

        /// <inheritdoc />
        ~SqliteDbManager()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        protected override void UseDbContext()
        {
            var contextOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite(OptionsAccessor.Value.ConnectionString)
                .Options;
            Context = new MyDbContext(contextOptions);
        }
    }
}
