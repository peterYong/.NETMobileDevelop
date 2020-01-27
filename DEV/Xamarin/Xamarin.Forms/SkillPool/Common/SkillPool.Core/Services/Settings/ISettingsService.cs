using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillPool.Core.Services.Settings
{
    /// <summary>
    /// 设置页面服务
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// 使用模拟数据
        /// </summary>
        bool UseMocks { get; set; }
        /// <summary>
        /// 访问Token
        /// </summary>
        string AuthAccessToken { get; set; }

        bool GetValueOrDefault(string key, bool defaultValue);
        string GetValueOrDefault(string key, string defaultValue);

        Task AddOrUpdateValue(string key, bool value);
        Task AddOrUpdateValue(string key, string value);
    }
}
