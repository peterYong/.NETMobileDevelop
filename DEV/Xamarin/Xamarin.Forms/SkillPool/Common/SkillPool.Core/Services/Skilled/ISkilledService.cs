using SkillPool.Core.Models.Skilled;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillPool.Core.Services.Skilled
{
    /// <summary>
    /// 数据接口
    /// </summary>
    public interface ISkilledService
    {
        /// <summary>
        /// 查，获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<SkilledItem>> GetAllSkilledItemsAsync();
        /// <summary>
        /// 查，通过id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SkilledItem> GetSkilledItemByIdAsync(int id);
        /// <summary>
        /// 查，通过name获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<SkilledItem>> GetSkilledItemByNameAsync(string name);

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="skilledItem"></param>
        /// <returns></returns>
        Task<int> AddSkilledItemAsync(SkilledItem skilledItem);

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="skilledItem"></param>
        /// <returns></returns>
        Task<int> UpdateSkilledItemAsync(SkilledItem skilledItem);

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="skilledItem"></param>
        /// <returns></returns>
        Task<int> RemoveSkilledItemAsync(SkilledItem skilledItem);

    }
}
