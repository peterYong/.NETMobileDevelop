using SkillPool.Core.Animations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkillPool.Core.Animations
{
    [ContentProperty("Animations")]
    public  class StoryBoard : AnimationBase
    {
        public List<AnimationBase> Animations
        {
            get;
        }
        public StoryBoard()
        {
            Animations = new List<AnimationBase>();
        }

        public StoryBoard(List<AnimationBase> animations)
        {
            Animations = animations;
        }



        protected override async Task BeginAnimation()
        {
            foreach (var animation in Animations)
            {
                if (animation.Target == null)
                    animation.Target = Target;

                await animation.Begin();
            }
        }

        protected override async Task ResetAnimation()
        {
            foreach (var animation in Animations)
            {
                if (animation.Target == null)
                    animation.Target = Target;

                await animation.Reset();
            }
        }

    }
}
