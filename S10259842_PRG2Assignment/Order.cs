//==========================================================
// Student Number : S10259842
// Student Name : Jerald Tee Li Yi 
// Partner Name : Keagan Alexander Sng Yu
//==========================================================
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace S10259842_PRG2Assignment
{
    class Order
    {
        private int customerID;
        private int id;
        private DateTime timeReceived;
        private DateTime? timeFulfilled = null;
        private List<IceCream> iceCreamList = new List<IceCream>();
        
        private double amountCharged;


        public int CustomerID
        { get { return customerID; } set { customerID = value; } }

        public double AmountCharged
        { get { return amountCharged; } set { amountCharged = value; } }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime TimeReceived
        {
            get { return timeReceived; }
            set { timeReceived = value; }
        }

        public DateTime? TimeFulfilled
        {
            get { return timeFulfilled; }
            set { timeFulfilled = value; }
        }

        public List<IceCream> IceCreamList
        { get { return iceCreamList; } set { iceCreamList = value; } }

        

        public Order()
        {

        }

        public Order(int id, DateTime time)
        {
            Id = id;
            timeReceived = time;
        }

        public int CheckIntInput(string input, int maxInt) //checks that input is INT and is 0 < x < maxInt
        {
            int intInput = 0;
            while (true)
            {
                try
                {
                    intInput = Convert.ToInt16(input);
                    if (intInput < 0 || intInput > maxInt)
                    {
                        Console.WriteLine($"Error: Input must be between 0-{maxInt}. ");

                        throw new Exception();
                    }
                    break;
                }
                catch (FormatException ex) //Catches non-int inputs
                {
                    Console.WriteLine(ex.Message);

                    Console.Write("Please re-enter your option: ");
                    input = Console.ReadLine();
                }
                catch (Exception) //Acts like a finally block but doesn't execute 100% of the time.
                {
                    Console.Write("Please re-enter your option: ");
                    input = Console.ReadLine();
                }
            }
            return intInput;
        }


        public (Dictionary<String, Double>,List<String>) ReadCSV(string filename) // Function to read CSV files for orders.csv and toppings.csv
        {
            Dictionary<String, Double> info = new Dictionary<String, Double>();
            List<String> keys = new List<String>();
            using (StreamReader sR = new StreamReader(filename))
            {
                sR.ReadLine(); //Gets rid of header
                while (true)
                {
                    string line = sR.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                   
                    List<String> infoLine = line.Split(",").ToList();

                    //Console.ReadLine();
                    info[infoLine[0]] = Convert.ToDouble(infoLine[1]);
                    keys.Add(infoLine[0]);
                }
                return (info,keys);
            }
        }


        
        public void ModifyIceCream(int index) //Modify a singular ice cream. 
      
        {
            
            int input = 0;
            string option = "";
            bool dipped = false;
            Console.Write(
                "Ice cream options: \r\n" +
                "[0] Cup \r\n" +
                "[1] Cone \r\n" +
                "[2] Waffle \r\n" +
                "Please enter ice cream option: "); //check for 1-3

            input = CheckIntInput(Console.ReadLine(), 2); //Check 

            // If Cone, ask for Chocolate dipped cone
            // If Waffle, ask for Red velvet, charcoal, or pandan waffle
            Console.Write("Please enter ice cream scoops (0-3): "); //Check for 0-3
            int scoops = CheckIntInput(Console.ReadLine(), 3);






            //Code to print flavours
            //----

                (Dictionary<String, Double> flavoursData, List<string> FlavourNames) = ReadCSV("flavours.csv");
                Console.Write("Flavours: \r\n");
                
                int DataIndex = 0; 
                foreach (KeyValuePair<String, Double> flavoursKvP in flavoursData) 
                {
                    
                    Console.WriteLine($"[{DataIndex}] {flavoursKvP.Key} (+{flavoursKvP.Value})");
                   
                    DataIndex++;
                }
                
            
            List<Flavour> FlavourList = new List<Flavour>();
            //
            //----
            
            // 
            for (int i = 0; i < scoops; i++) //Add flavours to FlavourList
            {
                Console.Write($"Please input choice for scoop no.{i + 1}: ");
                int flavourIndex = CheckIntInput(Console.ReadLine(), 5);
             
                if (flavoursData[FlavourNames[flavourIndex]] > 0)  //Checks for premium flavour using the price in flavoursData
                {
                    FlavourList.Add(new Flavour(FlavourNames[flavourIndex], true));
                    
                }
                else
                {
                    FlavourList.Add(new Flavour(FlavourNames[flavourIndex], false));
                }
            }

            (Dictionary<String, Double> toppingsData, List<string> toppingNames) = ReadCSV("toppings.csv");
            List<Topping> toppingList = new List<Topping>();
            int toppingLines = 0;

            DataIndex = 0;
            foreach (KeyValuePair<String, Double> toppingKvP in toppingsData)
            {

                Console.WriteLine($"[{DataIndex}] {toppingKvP.Key} (+{toppingKvP.Value})");

                DataIndex++;
            }


           
            /*
            Console.Write("Toppings: (+$1 each)  \r\n");
            for (int i = 0; i < toppingLines; i++)
            {
                Console.Write($"[{i}] {toppingNames[i]} \r\n");
            }
            Console.Write($"[{toppingLines}] End \r\n");
            */
            for (int i = 0; i < 4; i++)  //Code to enter toppings
            {


                Console.Write($"Please enter your choice of Toppings (up to 4 toppings): ");
                int toppingOption = CheckIntInput(Console.ReadLine(), 4);
                if (toppingOption == toppingLines)
                {
                    break;
                }
                toppingList.Add(new Topping(toppingNames[toppingOption]));
            }
            // ------


            IceCream iceCream1 = null;
            if (input == 1)  //if block to make a cup based on input,
            {
                option = "Cone";
                Console.WriteLine(
                        "Do you wish to order a Chocolate-dipped cone? \r\n" +
                        "[0] Yes \r\n" +
                        "[1] No, I'd like a regular. \r\n" +
                        "Enter your option: ");
                int dippedChoice = CheckIntInput(Console.ReadLine(), 1);
                if (dippedChoice == 1) //Check if he wants to order a Chocolate-cone
                {
                    dipped = true;
                }

                iceCream1 = new Cone(option,scoops,FlavourList,toppingList,dipped); 
               

            }

            else if (input == 2) ////if block to make a waffle based on input
            {
                option = "Waffle";
                int waffleOption = 0;

                List<string> waffleFlavours = new List<string>() {"Regular", "Red Velvet", "Charcoal", "Pandan Waffle" };
                Console.Write(
                   "Waffle Flavours: \r\n" +
                   "[0] Regular \r\n" +
                   "[1] Red Velvet \r\n" +
                   "[2] Charcoal \r\n" +
                   "[3] Pandan Waffle \r\n" +
                   "Enter the waffle flavour: ");

                waffleOption = CheckIntInput(Console.ReadLine(), 3);
                iceCream1 = new Waffle(option, scoops, FlavourList, toppingList, waffleFlavours[waffleOption]);

               

            }
            else
            {
                option = "Cup";
                iceCream1 = new Cup(option, scoops, FlavourList, toppingList);
            } //else block to make a cup based on input

            IceCreamList[index] = iceCream1;

        }

        public void AddIceCream(IceCream iceCream)
        {
            iceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int index) //should you use index - 1 instead (?)
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
            string? orders = "";
            int index = 0;
            foreach (IceCream i in iceCreamList)
            {
                index++;
                orders += $"Ice Cream {index}\r\n" + i.ToString() + "\r\n\r\n";
            }
            if (TimeFulfilled == null)
            {
                return $"{Id,-12} {TimeReceived,-35} {"Order not fulfilled",-35} \r\n{orders}";
            }
            return $"{Id, -12} {TimeReceived, -35} {TimeFulfilled, -35}  \r\n{orders}";
        }




    }
}
