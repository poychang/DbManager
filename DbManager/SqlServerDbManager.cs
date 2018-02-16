using DbManager.Option;
using DbManager.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DbManager
{
    /// <summary>SQLite 資料庫管理者</summary>
    public class SqlServerDbManager : DbManager
    {
        /// <inheritdoc />
        public SqlServerDbManager(IOptions<DbManagerOptions> optionsAccessor) : base(optionsAccessor) { }

        /// <inheritdoc />
        ~SqlServerDbManager()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        protected override void UseDbContext()
        {
            var contextOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer(OptionsAccessor.Value.ConnectionString)
                .Options;
            Context = new MyDbContext(contextOptions);
        }
    }
}
