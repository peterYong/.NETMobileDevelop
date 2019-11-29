using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BaseControls
{
    public class ButtonEventTriggerAction : TriggerAction<Button>
    {
        protected override void Invoke(Button sender)
        {
            sender.Text = "触发事件";
        }
    }
}
