using System;
using System.Collections.Generic;
using System.Text;

namespace DataBros
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get { return name; }   set { name = value; } }
        private string name = "Username";

        //public string Name { get; set; }

        public int Money { get; set; }
        public string Password { get; set; }




    }
}
