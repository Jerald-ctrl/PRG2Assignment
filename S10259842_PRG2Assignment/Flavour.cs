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
    class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool Premium
        {
            get { return premium; }
            set { premium = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Flavour()
        {
            Type = null;
            Premium = false;
            Quantity = 0;
        }

        public Flavour(string t, bool p, int q)
        {
            Type = t;
            Premium = p;
            Quantity = q;
        }

        public override string ToString()
        {
            if (Premium == true)
            {
                return $"{Type,12} {"Yes",-12} {Quantity,-12}";
            }
            else if (Premium == false)
            {
                return $"{Type,12} {"No",-12} {Quantity,-12}";
            }
            else
            {
                return null;
            }
        }
    }
}
