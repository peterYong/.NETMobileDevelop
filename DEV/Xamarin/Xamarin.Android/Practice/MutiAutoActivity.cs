using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
/*
      *   MultiAutoCompleteTextView
      * 1、初始化控件
      * 2、创建一个适配器
      * 3、初始化数据源--数据源去匹配文本框中输入的类容
      * 4、将adapter与当前AutoCompleteTextView绑定
      * 5、设置分隔符
      * */

namespace Practice
{
    [Activity(Label = "MutiAutoActivity")]
    public class MutiAutoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MultiAutoCompleteTextView);

            //3、初始化数据源--数据源去匹配文本框中输入的类容
            String[] res = { "beijing1", "beijing2", "beijing3", "shanghai1", "shanghai2", "shanghai3" };
            // 2、创建一个适配器
            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Resource.Layout.HelloAutoComplete, res);
            MultiAutoCompleteTextView MautoTxt = (MultiAutoCompleteTextView)FindViewById(Resource.Id.MAutoText);

            // 4、将adapter与当前AutoCompleteTextView绑定
            MautoTxt.Adapter = adapter;

            //设置输入多少字符时自动匹配
            MautoTxt.Threshold = 2;
            //5、设置分隔符，设置以逗号分割符为结束符号
            MautoTxt.SetTokenizer(new MultiAutoCompleteTextView.CommaTokenizer());
        }
    }
}