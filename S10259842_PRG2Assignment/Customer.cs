


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259842_PRG2Assignment
{
    class Customer
    {
        private string? name;
        private int memberId;
        private DateTime dob;
        private Order currentOrder;
        private List<Order> orderHistory = new List<Order>();
        private PointCard rewards;

        public string? Name
        { get { return name; } set { name = value; } }

        public int MemberId
        { get { return memberId; } set {  memberId = value; } }

        public DateTime Dob
        { get { return dob; } set {  dob = value; } }


        public Order CurrentOrder
        { get { return currentOrder; } set { currentOrder = value; } }


    }
}
