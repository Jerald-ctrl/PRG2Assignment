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
    class Cone:IceCream
    {
        private bool dipped;
        public bool Dipped
        {
            get { return dipped; }
            set { dipped = value; }
        }

        public Cone():base()
        {
            dipped = false;
        }

        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped):base(option, scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            double totalPrice = 0;
            double conePrice = 0;
            double flavourPrice = 0;
            double toppingsPrice = 0;

            if (dipped)
            {
                conePrice += 2;
            }

            //calculate price per scoop
            if (Scoops == 1)
            {
                totalPrice += 4;
            }
            else if (Scoops == 2)
            {
                totalPrice += 5.5;
            }
            else if (Scoops == 3)
            {
                totalPrice += 6.5;
            }

            //add extra cost for premium flavours
            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Premium == true)
                {
                    flavourPrice += 2;
                }
            }

            foreach (Topping topping in Toppings)
            {
                toppingsPrice += 1;
            }

            totalPrice = totalPrice + conePrice + flavourPrice + toppingsPrice;
            return totalPrice;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
