﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Core.Validations
{
    /// <summary>
    /// 不为空检验
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
