using System;
using System.Collections.Generic;
using System.Text;

namespace FromPractice.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        DataBinding,
        ActivityIndicatorPage,
        DisplayPopUps,
        Image
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
