using SkillPool.Core.Behaviors.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkillPool.Core.Behaviors
{
    /// <summary>
    /// 可响应发生的事件 执行命令，当行为使用时 可执行由Command属性指定的ICommand
    /// 适用于将命令 附加到那些不支持命令的控件，将事件转为命令
    /// </summary>
    public class EventToCommandBehavior : BindableBehavior<View>
    {
        #region Property

        public static BindableProperty EventNameProperty =
            BindableProperty.CreateAttached(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty CommandProperty =
            BindableProperty.CreateAttached(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty CommandParameterProperty =
           BindableProperty.CreateAttached(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior), null,
               BindingMode.OneWay);

        public static BindableProperty EventArgsConverterProperty =
           BindableProperty.CreateAttached(nameof(EventArgsConverter), typeof(IValueConverter), typeof(EventToCommandBehavior), null,
               BindingMode.OneWay);

        public static BindableProperty EventArgsConverterParameterProperty =
            BindableProperty.CreateAttached(nameof(EventArgsConverterParameter), typeof(object), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public IValueConverter EventArgsConverter
        {
            get { return (IValueConverter)GetValue(EventArgsConverterProperty); }
            set { SetValue(EventArgsConverterProperty, value); }
        }

        public object EventArgsConverterParameter
        {
            get { return GetValue(EventArgsConverterParameterProperty); }
            set { SetValue(EventArgsConverterParameterProperty, value); }
        }

        #endregion

        private EventInfo _eventInfo;
        protected Delegate _handler;

        /// <summary>
        /// 为控件的EventName属性中定义的事件 注册事件处理程序。事件触发时，将调用OnFired方法（该方法执行命令）
        /// </summary>
        /// <param name="visualElement"></param>
        protected override void OnAttachedTo(View visualElement)
        {
            base.OnAttachedTo(visualElement);

            //检索表示在指定对象（控件）上定义的所有事件（单击、属性更改...）的集合
            var events = AssociatedObject.GetType().GetRuntimeEvents().ToArray();
            if (events.Any())
            {
                _eventInfo = events.FirstOrDefault(e => e.Name == EventName);
                if (_eventInfo == null)
                {
                    throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "EventToCommand: Can't find any event named '{0}' on attached type", EventName));
                }
                AddEventHandler(_eventInfo, AssociatedObject, OnFired);
            }
        }
        /// <summary>
        /// 给控件的特定事件（绑定的事件）注册处理程序
        /// </summary>
        /// <param name="eventInfo"></param>
        /// <param name="item"></param>
        /// <param name="action"></param>
        private void AddEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
        {
            var eventParameters = eventInfo.EventHandlerType
                .GetRuntimeMethods().First(m => m.Name == "Invoke")
                .GetParameters()
                .Select(p => Expression.Parameter(p.ParameterType))
                .ToArray();

            var actionInvoke = action.GetType()
                .GetRuntimeMethods().First(m => m.Name == "Invoke");

            _handler = Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                eventParameters
            )
            .Compile();

            eventInfo.AddEventHandler(item, _handler);
        }
        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (Command == null)
                return;

            var parameter = CommandParameter;

            if (eventArgs != null && eventArgs != EventArgs.Empty)
            {
                parameter = eventArgs;

                if (EventArgsConverter != null)
                {
                    parameter = EventArgsConverter.Convert(eventArgs, typeof(object), EventArgsConverterParameter, CultureInfo.CurrentUICulture);
                }
            }

            //执行命令，并带有参数parameter。
            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }

        /// <summary>
        /// 为EventName属性中定义的事件取消注册事件处理程序
        /// </summary>
        /// <param name="view"></param>
        protected override void OnDetachingFrom(View view)
        {
            if (_handler != null)
                _eventInfo.RemoveEventHandler(AssociatedObject, _handler);

            base.OnDetachingFrom(view);
        }
    }
}
