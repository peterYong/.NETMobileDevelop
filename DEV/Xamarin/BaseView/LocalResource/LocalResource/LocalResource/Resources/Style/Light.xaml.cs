﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalResource.Resources.Style
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Light : ResourceDictionary
    {
        public Light()
        {
            InitializeComponent();
        }
    }
}