using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalResource
{
    [ContentProperty("Text")]
    public class DynamicResourceBindingExtension : IMarkupExtension
    {
        public BindingBase Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            //if (string.IsNullOrWhiteSpace(Text))
            //{
            //    return null;
            //}
            //ResourceDictionary dic = App.GetCurrentByType("Language");
            //if (dic != null && dic.ContainsKey(Text))
            //{
            //    return dic[Text];
            //}
            return null;
        }
    }
}
