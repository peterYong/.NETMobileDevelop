using SkillPool.Core.Models.Enum;
using SkillPool.Core.Models.Skilled;
using SkillPool.Core.Services.Base;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillPool.Core.Services.Skilled
{
    /// <summary>
    /// 模拟数据服务
    /// </summary>
    public class SkilledSqliteMockService : ISkilledService
    {

        //一个到SQLite数据库的异步连接池
        readonly SQLiteAsyncConnection connection;
        readonly string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SkillPool.db3");

        public SkilledSqliteMockService()
        {
            if (connection == null)
            {
                connection = new SQLiteAsyncConnection(dbpath);
                connection.CreateTableAsync<SkilledItem>().Wait();
            }
        }

        public Task<int> AddSkilledItemAsync(SkilledItem skilledItem)
        {
            if(skilledItem!=null)
            {
                skilledItem.Name = skilledItem.Name.ToUpper();
            }
            return connection.InsertAsync(skilledItem);
        }

        public Task<List<SkilledItem>> GetAllSkilledItemsAsync()
        {
            Task.Delay(TimeSpan.FromMilliseconds(100));
            return connection.Table<SkilledItem>().ToListAsync();
        }

        public Task<SkilledItem> GetSkilledItemByIdAsync(int id)
        {
            return connection.Table<SkilledItem>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<SkilledItem>> GetSkilledItemByNameAsync(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return connection.Table<SkilledItem>().ToListAsync();
            }
            name = name.ToUpper();
            return connection.Table<SkilledItem>()
                           .Where(i => i.Name.Contains(name))
                           .ToListAsync();
        }

        public Task<int> RemoveSkilledItemAsync(SkilledItem skilledItem)
        {
            return connection.DeleteAsync(skilledItem);
        }

        public Task<int> UpdateSkilledItemAsync(SkilledItem skilledItem)
        {
            if (skilledItem != null)
            {
                skilledItem.Name = skilledItem.Name.ToUpper();
            }
            return connection.UpdateAsync(skilledItem);
        }
    }
}
