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
    class Waffle:IceCream
    {
        private string? waffleFlavour;
        public string? WaffleFlavour
        {
            get {  return waffleFlavour; } 
            set { waffleFlavour = value; }
        }

        public Waffle():base()
        {
            WaffleFlavour = null;
        }

        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) :base("Waffle", scoops, flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
        }

        public override double CalculatePrice()
        {
            double totalPrice = 3;

            //calculate price of waffle
            
            if (WaffleFlavour == "Charcoal" || WaffleFlavour == "Red Velvet" || WaffleFlavour == "Pandan")
            {
                totalPrice += 3;
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
                    totalPrice += 2;
                }
            }

            foreach (Topping topping in Toppings)
            {
                totalPrice += 1;
            }

            return totalPrice;
        }

        public override string ToString()
        {
            return base.ToString() + WaffleFlavour;
        }
    }
}
