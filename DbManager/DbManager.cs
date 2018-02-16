using DbManager.Repository;
using DbManager.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Threading.Tasks;
using DbManager.Interface;
using DbManager.Option;

namespace DbManager
{
    public class DbManager : IDbManager
    {
        /// <summary>資料庫 Context</summary>
        protected DbContext Context;

        /// <summary>Repository 池</summary>
        protected Hashtable Repositories;

        /// <summary>是否已清除</summary>
        protected bool Disposed;

        /// <summary></summary>
        protected IOptions<DbManagerOptions> OptionsAccessor;

        /// <summary>建構式</summary>
        /// <param name="optionsAccessor">選項存取器</param>
        public DbManager(IOptions<DbManagerOptions> optionsAccessor)
        {
            OptionsAccessor = optionsAccessor;
            InitDbContext();
        }

        /// <summary>解構式</summary>
        ~DbManager()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        /// <summary>清除此類別資源</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public void Save()
        {
            Context.SaveChanges();
        }

        /// <inheritdoc/>
        public bool IsDatabasebExist()
        {
            return Context.Database.EnsureCreated();
        }

        /// <inheritdoc/>
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (Repositories == null)
            {
                Repositories = new Hashtable();
            }

            // 檢查是否已經初始化過其類別為 TEntity 的 Entity Repository
            var type = typeof(TEntity).Name;
            if (Repositories.ContainsKey(type)) return (IRepository<TEntity>)Repositories[type];

            // 將初始化的 Entity Repository 實體存放進 Repository 池
            var repositoryType = typeof(EFGenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType
                    .MakeGenericType(typeof(TEntity)), Context);
            Repositories.Add(type, repositoryInstance);

            return (IRepository<TEntity>)Repositories[type];
        }

        /// <inheritdoc />
        public int ExecuteSqlCommand(string sql)
        {
            return Context.Database.ExecuteSqlCommand(sql);
        }

        /// <inheritdoc />
        public async Task<int> ExecuteSqlCommandAsync(string sql)
        {
            return await Context.Database.ExecuteSqlCommandAsync(sql);
        }

        /// <summary>使用 InMemory 資料庫</summary>
        protected virtual void UseDbContext()
        {
            var contextOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "MyDatabase")
                .Options;
            Context = new MyDbContext(contextOptions);
        }

        /// <summary>清除此類別資源</summary>
        /// <param name="disposing">是否在清理中</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            Disposed = true;
        }

        /// <summary>初始化 DbContext</summary>
        private void InitDbContext()
        {
            UseDbContext();
            Console.WriteLine("Initialed Database");
        }
    }
}
