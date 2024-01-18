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
    abstract class IceCream
    {
        private string? option;
        private int scoops;
        private List<Flavour> flavours;
        private List<Topping> toppings;

        public string? Option
        {
            get { return option; }
            set { option = value; }
        }

        public int Scoops
        {
            get { return scoops; }
            set { scoops = value; }
        }

        public List<Flavour> Flavours
        {
            get { return flavours; }
            set { flavours = value; }
        }

        public List<Topping> Toppings
        {
            get { return toppings; }
            set { toppings = value; }
        }

        public IceCream()
        {
            Option = "Option";
            Scoops = 0;
            Flavours = new List<Flavour>();
            Toppings = new List<Topping>();
        }

        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }

        public abstract double CalculatePrice();

        public override string ToString()
        {
            string? flavours = "";
            foreach (Flavour flavour in Flavours)
            {
                flavours += flavour.ToString() + "\n";
            }

            string? toppings = "";
            foreach (Topping topping in Toppings)
            {
                toppings += topping.ToString() + "\n";
            }

            return $"{Option, -12} {Scoops, -12} {flavours, -12} {toppings, -12}";
        }

    }
}
