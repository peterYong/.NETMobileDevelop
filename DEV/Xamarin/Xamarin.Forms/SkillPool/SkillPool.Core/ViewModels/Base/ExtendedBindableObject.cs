using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace SkillPool.Core.ViewModels.Base
{
    /// <summary>
    /// 当(模型/视图模型)基础属性值更改时，在视图模型或模型类中实现INotifyPropertyChanged接口可使类向视图中的任何数据绑定控件提供更改通知
    /// </summary>
    public abstract class ExtendedBindableObject : BindableObject
    {
        /// <summary>
        /// 可绑定的属性 更改时引发OnPropertyChanged（位于INotifyPropertyChanged）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">以表达式目录树的形式将强类型 lambda 表达式表示为数据结构</param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            if (property != null)
            {
                string name = GetMemberInfo(property).Name;
                OnPropertyChanged(name);
            }
        }

        private static MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }
    }
}
