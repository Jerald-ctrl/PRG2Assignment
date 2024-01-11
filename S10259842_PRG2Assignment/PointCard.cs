using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259842_PRG2Assignment
{
    internal class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;


        public int Points
        { get { return points; } set { points = value; } }

        public int PunchCard
        { get { return punchCard; } set { punchCard = value; } }

        public string Tier
        { get { return tier; } set { tier = value; } }

        public PointCard()
        {

        }

        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
        }

        public void AddPoints(int pointAdd)
        {
            Points += pointAdd;
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

        }


        public override string ToString()
        {
            return $"Points: {Points}. PunchCard: {PunchCard}. Tier: {Tier}.";
        }


    }
}
