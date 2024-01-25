//==========================================================
// Student Number : S10259305
// Student Name : Keagan Alexander Sng Yu
// Partner Name : Jerald Tee Li Yi
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259842_PRG2Assignment
{
    class Topping
    {
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Topping()
        {
            Type = null;
        }

        public Topping(string t)
        {
            Type = t;
        }

        public override string ToString()
        {
            return $"{Type, -12}";
        }
    }
}
