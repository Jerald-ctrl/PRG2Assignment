using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259842_PRG2Assignment
{
    class Order
    {
        private int id;
        private DateTime timeReceived;
        private DateTime? timeFulfilled = null; 
        private List<IceCream> iceCreamList = new List<IceCream>();


        public int Id
        { get { return id; } set { id = value; } }

        public DateTime TimeReceived
        { get { return timeReceived; } set { timeReceived = value; } }

        public DateTime? TimeFulfilled
        { get { return timeFulfilled; } set { timeFulfilled = value; } }


        public Order()
            {
            }

        public Order(int id, DateTime time)
        {
            Id = id;
            timeReceived = time;
        }

        public void  ModifyIceCream(int id)
        {

        }

        public void AddIceCream(IceCream iceCream)
        {
            iceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int index) 
        {
            iceCreamList.RemoveAt(index);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream iceCream in iceCreamList)
            {
                total += iceCream.CalculatePrice();
            }
            return total;
        }

        public override string ToString()
        {
            return "";
        }




    }
}
