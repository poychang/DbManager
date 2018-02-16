using System;
using System.Threading.Tasks;
using DbManager.Repository;

namespace DbManager.Interface
{
    /// <inheritdoc />
    /// <summary>資料庫管理者介面</summary>
    public interface IDbManager : IDisposable
    {
        /// <summary>檢查資料庫是否存在</summary>
        /// <returns>是否存在</returns>
        bool IsDatabasebExist();

        /// <summary>儲存所有異動</summary>
        void Save();

        /// <summary>取得某一個 Entity Repository。如果沒有取過會初始化一個，如果有就取得之前的</summary>
        /// <typeparam name="TEntity">此 DbContext 裡面的 Entity Type</typeparam>
        /// <returns>Entity Repository</returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        /// <summary>執行 SQL 語句</summary>
        /// <param name="sql">SQL 語句</param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql);

        /// <summary>非同步執行 SQL 語句</summary>
        /// <param name="sql">SQL 語句</param>
        /// <returns></returns>
        Task<int> ExecuteSqlCommandAsync(string sql);
    }
}
