


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
        public List<Order> orderHistory = new List<Order>();
        public PointCard rewards = new PointCard();

        public string? Name
        { get { return name; } set { name = value; } }

        public int MemberId
        { get { return memberId; } set {  memberId = value; } }

        public DateTime Dob
        { get { return dob; } set {  dob = value; } }


        public Order CurrentOrder
        { get { return currentOrder; } set { currentOrder = value; } }

        public Customer()
        { }

        public Customer(string name, int memberId, DateTime dob)
        {
            Name = name;
            MemberId = memberId;
            Dob = dob;
            
            
        }

        public Order MakeOrder()
        {
            CurrentOrder = new Order();
            orderHistory.Add(CurrentOrder);
            return CurrentOrder;
        }

        public bool IsBirthday()
        {
            if (dob == DateTime.Today)
            {
                return true;

            }
            else 
            {
                return false;
            }
        }
    }
}
