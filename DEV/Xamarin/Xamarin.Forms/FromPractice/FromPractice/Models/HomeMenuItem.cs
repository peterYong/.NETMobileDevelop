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
        UserInterface
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
