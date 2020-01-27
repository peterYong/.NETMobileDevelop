using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillPool.Core.Services.Base
{
    /// <summary>
    /// 数据源接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T item);

        Task<int> DeleteItemAsync(T item);
        //Task<bool> DeleteItemAsyncByID(string id);

        Task<int> UpdateItemAsync(T item);

        Task<List<T>> GetItemsAsync(bool forceRefresh = false);
        Task<T> GetItemAsyncById(int id);
    }
}
