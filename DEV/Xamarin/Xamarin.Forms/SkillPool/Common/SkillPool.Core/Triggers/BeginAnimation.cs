using SkillPool.Core.Animations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SkillPool.Core.Triggers
{
    public class BeginAnimation : TriggerAction<VisualElement>
    {
        public AnimationBase Animation { get; set; }
        protected async override void Invoke(VisualElement sender)
        {
            if(Animation!=null)
            {
                await Animation.Begin();
            }
        }
    }
}
