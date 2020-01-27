using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkillPool.Core.Animations.Base
{
    public abstract class AnimationBase: BindableObject
    {
        #region 定义可绑定属性

        public static readonly BindableProperty TargetProperty = BindableProperty.Create("Target", typeof(VisualElement), typeof(AnimationBase), null,
           propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).Target = (VisualElement)newValue);

        /// <summary>
        /// 这里定义了Target属性，xaml中才能使用
        /// </summary>
        public VisualElement Target
        {
            get { return (VisualElement)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }


        public static readonly BindableProperty DurationProperty = BindableProperty.Create("Duration", typeof(string), typeof(AnimationBase), "1000",
           propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).Duration = (string)newValue);
        /// <summary>
        /// 持续时间
        /// </summary>
        public string Duration
        {
            get { return (string)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly BindableProperty DelayProperty = BindableProperty.Create("Delay", typeof(int), typeof(AnimationBase), 0,
           propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).Delay = (int)newValue);
        /// <summary>
        /// 延迟
        /// </summary>
        public int Delay
        {
            get { return (int)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        public static readonly BindableProperty RepeatForeverProperty = BindableProperty.Create("RepeatForever", typeof(bool), typeof(AnimationBase), false,
            propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).RepeatForever = (bool)newValue);
        /// <summary>
        /// 永远重复
        /// </summary>
        public bool RepeatForever
        {
            get { return (bool)GetValue(RepeatForeverProperty); }
            set { SetValue(RepeatForeverProperty, value); }
        }
        #endregion


        #region 方法
        private bool _isRunning = false;
        /// <summary>
        /// 开始动画
        /// </summary>
        /// <returns></returns>
        protected abstract Task BeginAnimation();
        /// <summary>
        /// 重设动画
        /// </summary>
        /// <returns></returns>
        protected abstract Task ResetAnimation();
        public async Task Begin()
        {
            try
            {
                if (!_isRunning)
                {
                    _isRunning = true;

                    await InternalBegin()
                        .ContinueWith(t => t.Exception, TaskContinuationOptions.OnlyOnFaulted)
                        .ConfigureAwait(false);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in animation {ex}");
            }
        }

        private async Task InternalBegin()
        {
            if (Delay > 0)
            {
                await Task.Delay(Delay);
            }

            if (!RepeatForever)
            {
                await BeginAnimation();
            }
            else
            {
                do
                {
                    await BeginAnimation();
                    await ResetAnimation();
                } while (RepeatForever);
            }
        }

        public async Task Reset()
        {
            _isRunning = false;
            await ResetAnimation();
        }

        #endregion
    }
}
