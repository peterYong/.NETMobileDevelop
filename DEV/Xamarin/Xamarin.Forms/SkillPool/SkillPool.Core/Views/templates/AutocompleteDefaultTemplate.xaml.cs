﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillPool.Core.Views.templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutocompleteDefaultTemplate : ViewCell
    {
        public AutocompleteDefaultTemplate()
        {
            InitializeComponent();
        }
    }
}