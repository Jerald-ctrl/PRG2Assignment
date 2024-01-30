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
        private string? type;
        private bool premium;

        public string? Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool Premium
        {
            get { return premium; }
            set { premium = value; }
        }

        public Flavour()
        {
            Type = null;
            Premium = false;
        }

        public Flavour(string t, bool p)
        {
            Type = t;
            Premium = p;
        }

        public override string ToString()
        {
            if (Premium == true)
            {
                return $"{Type} {"(Premium)"}";
            }
            else if (Premium == false)
            {
                return $"{Type} {"(Standard)"}";
            }
            else
            {
                return null;
            }
        }
    }
}
