using SkillPool.DataAccess.Base;
using SkillPool.Model.Skilled;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SkillPool.Core.Services.Skilled
{
    /// <summary>
    /// 技能数据接口，通过sqlite存储
    /// </summary>
    public class SkilledSqliteService : IDataStore<SkilledItem>
    {
        //一个到SQLite数据库的异步连接池
        readonly SQLiteAsyncConnection connection;
        string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SkillPool.db3");

        public SkilledSqliteService()
        {
            if(connection==null)
            {
                connection = new SQLiteAsyncConnection(dbpath);
                connection.CreateTableAsync<SkilledItem>().Wait();
            }
        }

        #region CRUD

        public Task<int> AddItemAsync(SkilledItem item)
        {
            return connection.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(SkilledItem item)
        {
            return connection.DeleteAsync(item);
        }

        public Task<int> UpdateItemAsync(SkilledItem item)
        {
            //if (item.Id != 0)
            //{
            return connection.UpdateAsync(item);
            //}
            //else
            //{

            //}
        }

        public Task<SkilledItem> GetItemAsyncById(int id)
        {
            return connection.Table<SkilledItem>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<SkilledItem>> GetItemsAsync(bool forceRefresh = false)
        {
            Task.Delay(TimeSpan.FromMilliseconds(100));
            return connection.Table<SkilledItem>().ToListAsync();
        }

        #endregion
    }
}
