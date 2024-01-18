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
        private DateTime? timeFulfilled;
        private List<IceCream> iceCreamList = new List<IceCream>();


        public int Id
        { get { return id; } set { id = value; } }

        public DateTime TimeReceived
        { get { return timeReceived; } set {  timeReceived = value; } } 

        public DateTime? TimeFulfilled
        { get { return timeFulfilled; } set { timeFulfilled = value; } } 


    }
}
