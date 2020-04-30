using System;
using System.Collections.Generic;
using System.Text;

namespace PriceChecker.Models
{
    public enum MenuItemType
    {
        Search,
        Subscribed,
        SearchResultPage,
        Settings


    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
