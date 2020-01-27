using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SkillPool.Core.Behaviors.Base
{
    /// <summary>
    /// 为Xamarin.Forms行为提供一个基类，该行为需要将该行为的BindingContext设置为附加控件。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        /// <summary>
        /// 存储对附加控件的引用
        /// </summary>
        public T AssociatedObject { get; private set; }

        /// <summary>
        /// 用于设置行为的BindingContext,在行为附加到控件后立即触发。 
        /// 此方法接收对其附加的控件的引用，并可用于注册事件处理程序或执行支持行为功能所需的其他设置。 
        /// 例如，你可以订阅控件上的事件。 然后，行为功能将在事件的事件处理程序中实现。
        /// </summary>
        /// <param name="bindable">附加行为的可绑定对象</param>
        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;

            if (bindable?.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        /// <summary>
        /// 用于清除BindingContext,当行为从控件中移除时，将触发此方法。 
        /// 此方法接收对其附加的控件的引用，并用于执行任何所需的清理。 
        /// 例如，可以取消订阅控件上的事件，以防止内存泄漏。
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
        }

       
    }
}
