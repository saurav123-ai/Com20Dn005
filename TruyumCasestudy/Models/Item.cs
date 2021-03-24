using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Truyumcasestudy.Models
{
    public class Item
    {
        //public menu menu { get; set; }
        //public int quantity { get; set; }


        //public Item(menu Menu, int Quantity)
        //{
        //    menu = Menu;
        //    quantity = Quantity;
        //}
        public int menu_id { get; set; }
        public string menu_name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }
}