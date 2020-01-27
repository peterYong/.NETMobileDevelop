using SkillPool.Core.Animations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkillPool.Core.Animations
{
    /// <summary>
    /// 淡入动画
    /// </summary>
    public class FadeInAnimation : AnimationBase
    {
        public enum FadeDirection
        {
            Up,
            Down
        }

        public static readonly BindableProperty DirectionProperty =
        BindableProperty.Create("Direction", typeof(FadeDirection), typeof(FadeInAnimation), FadeDirection.Up,
        propertyChanged: (bindable, oldValue, newValue) =>
        ((FadeInAnimation)bindable).Direction = (FadeDirection)newValue);

        public FadeDirection Direction
        {
            get { return (FadeDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if(Target ==null)
            {
                throw new NullReferenceException("Null Target property.");
            }
            return Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Target.Animate("FadeIn", FadeIn(), 16, Convert.ToUInt32(Duration));
                });
            });
        }

        protected override Task ResetAnimation()
        {
            throw new NotImplementedException();
        }

        internal Animation FadeIn()
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => Target.Opacity = f, 0, 1, Xamarin.Forms.Easing.CubicOut);

            animation.WithConcurrent(
              (f) => Target.TranslationY = f,
              Target.TranslationY + ((Direction == FadeDirection.Up) ? 50 : -50), Target.TranslationY,
              Xamarin.Forms.Easing.CubicOut, 0, 1);

            return animation;
        }
    }
}
