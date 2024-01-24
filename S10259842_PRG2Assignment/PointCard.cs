using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace S10259842_PRG2Assignment
{
    class PointCard
    {
        private int points;
        private int punchCard;
        private string tier = "Regular"; // Placed default value here to give customers a tier simpler.


        public int Points
        { 
            get { return points; } 
            set { points = value; }
        }

        public int PunchCard
        { 
            get { return punchCard; } 
            set { punchCard = value; }
        }

        public string Tier
        { 
            get { return tier; } 
            set { tier = value; }
        }

        public PointCard()
        {
            Points = 0;
            PunchCard = 0;
        }

        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
            AddPoints(0); //Give members a tier automatically
        }

        public void AddPoints(int pointAdd)
        {
            Points += pointAdd;

            if (Points > 100)
            {
                Tier = "Gold";
            }
            else if (Points > 50 && Tier != "Gold") 
            {
                Tier = "Silver";
            }

        }

        public void RedeemPoints(int pointDed)
        {
            if (Points >= pointDed)
            {
                Points -= pointDed;
            }

        }

        public void Punch()
        {
            PunchCard += 1;
            if (PunchCard == 11)
            {
                PunchCard = 0;
            }
        }


        public override string ToString()
        {
           
            return $"{Points,-12} {PunchCard,-12} {Tier,-12}";
        }


    }
}
