


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259842_PRG2Assignment
{
    class Customer
    {
        private string? name = null;
        private int memberId = 0;
        private DateTime dob = DateTime.Today;
        private Order currentOrder = new Order();
        private List<Order> orderHistory = new List<Order>();
        private PointCard rewards = new PointCard();

        public string? Name
        {
            get { return name; }
            set { name = value; }
        }

        public int MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        public List<Order> OrderHistory
        {
            get { return orderHistory; }
            set { orderHistory = value; }
        }

        public PointCard Rewards
        {
            get { return rewards; }
            set { rewards = value; }
        }


        public Order CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; }
        }

        public Customer()
        { }

        public Customer(string name, int memberId, DateTime dob)
        {

            Name = name;
            MemberId = memberId;
            Dob = dob;



        }

        public Order MakeOrder() //Relies on CurrentOrder to Make a new order

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

        public override string ToString()
        {
            return $"{Name,-12} {MemberId,-12} {Dob,-12} {currentOrder,-12} {CurrentOrder,-12} {OrderHistory,-12} {Rewards,-12}";
        }
    }
}
