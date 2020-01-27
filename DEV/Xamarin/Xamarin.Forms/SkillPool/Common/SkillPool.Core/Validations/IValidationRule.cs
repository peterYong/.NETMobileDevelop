using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Core.Validations
{
    /// <summary>
    /// 检验规则接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }
}
