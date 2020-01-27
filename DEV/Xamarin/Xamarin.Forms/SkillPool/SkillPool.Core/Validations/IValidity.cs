using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Core.Validations
{
    /// <summary>
    /// 检验接口
    /// </summary>
    public interface IValidity
    {
        bool IsValid { get; set; }
    }
}
