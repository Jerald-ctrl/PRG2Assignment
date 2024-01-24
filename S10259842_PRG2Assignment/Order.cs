using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        {  get { return iceCreamList; } set { iceCreamList = value; } }

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
                catch (Exception ex) //Acts like a finally block but doesn't execute 100% of the time.
                {
                    Console.Write("Please re-enter your option: ");
                    input = Console.ReadLine();
                }
            }
            return intInput;
        }
        public void ModifyIceCream(int index) //Modify a singular ice cream.
        /* if [1] is selected, have the user select which ice cream to modify then prompt the user 
         for the new information for the modifications they wish to make to the ice cream
          selected: option, scoops, flavours, toppings, dipped cone(if applicable), waffle flavour
             (if applicable) and update the ice cream object’s info accordingl */
        {

            int input = 0;
            string option = "";
            bool dipped = false;
            bool velvet = false;
            IceCream iceCream = null;


            Console.WriteLine(
                "Ice cream options: \r\n" +
                "[0] Cup \r\n" +
                "[1] Cone \r\n" +
                "[2] Waffle \r\n" +
                "Please enter ice cream option. "); //check for 1-3

            input = CheckIntInput(Console.ReadLine(), 2); //Check 

            // If Cone, ask for Chocolate dipped cone
            // If Waffle, ask for Red velvet, charcoal, or pandan waffle
            Console.Write("Please enter ice cream scoops (0-3): "); //Check for 0-3
            int scoops = CheckIntInput(Console.ReadLine(), 3);


            Dictionary<string, int> iceCreamFlavours = new Dictionary<string, int>
            {
                {"Vanilla", 0 },
                { "Chocolate", 0 },
                { "Strawberry", 0 },
                { "Durian", 0 },
                { "Ube", 0 },
                { "Sea Salt", 0 }
            };
            
        
            List<Flavour> FlavourList = new List<Flavour>();
            

            Console.WriteLine(
                "Flavours: \r\n" +
                "[0] Vanilla \r\n" +
                "[1] Chocolate \r\n" +
                "[2] Strawberry \r\n \r\n" +
                "Premium Flavours (+$2): \r\n" +
                "[3] Durian \r\n" +
                "[4] Ube \r\n" +
                "[5] Sea Salt \r\n");

            for (int i = 0; i < scoops; i++)
            {
                Console.Write($"Please input choice for scoop no.{i + 1}: ");
                int scoopFlavour = CheckIntInput(Console.ReadLine(), 5);
                if (scoopFlavour == 0)
                {
                    iceCreamFlavours["Vanilla"] += 1;
                }
                if (scoopFlavour == 1)
                {
                    iceCreamFlavours["Chocolate"] += 1;
                }
                if (scoopFlavour == 2)
                {
                    iceCreamFlavours["Strawberry"] += 1;
                }
                if (scoopFlavour == 3)
                {
                    iceCreamFlavours["Durian"] += 1;
                }
                if (scoopFlavour == 4)
                {
                    iceCreamFlavours["Ube"] += 1;
                }
                if (scoopFlavour == 5)
                {
                    iceCreamFlavours["Sea Salt"] += 1;
                }
            }
            int dictIndex = 0;
                foreach (KeyValuePair<string, int> kVp in iceCreamFlavours)
                {
                    dictIndex++;
                    string flavourName = kVp.Key;
                    if (dictIndex < 3)
                    {
                        FlavourList.Add(new Flavour(kVp.Key, false, kVp.Value));
                    }
                    else
                    {
                        FlavourList.Add(new Flavour(kVp.Key, true, kVp.Value));
                    }
                   
                }
            // IceCreamFlavourList.Add(new Flavour(flavourList[scoopFlavour],false,flavourList.));


            /*Dictionary<string, int> toppingFlavours = new Dictionary<string, int>
            {
                {"Sprinkles", 0 },
                { "Mochi", 0 },
                { "Sago", 0 },
                { "Oreos", 0 },
            }; */
            List<string> toppingFlavours = new List<string>() {"Sprinkles","Mochi","Sago","Oreos" };
            List<Topping> toppingList = new List<Topping>();

            Console.Write(
                  "Toppings:  \r\n" +

                  "[0] Sprinkles \r\n" +
                  "[1] Mochi \r\n " +
                  "[2] Sago \r\n " +
                  "[3] Oreos \r\n " +
                  "[4] End \r\n" +
                  "Please enter number of wanted ice cream toppings (up to 4): ");

            for (int i = 0; i < 4; i++)
            {
                int toppingOption = CheckIntInput(Console.ReadLine(), 4);
                if (toppingOption == 4)
                {
                    break;
                }
                toppingList.Add(new Topping(toppingFlavours[toppingOption]));
                

               
            } //For loop to add up to 4 toppings
            IceCream iceCream1 = null;
            if (input == 1)  //Check if he wants to order a Chocolate-cone
            {
                option = "Cone";
                Console.WriteLine(
                        "Do you wish to order a Chocolate-dipped cone? \r\n" +
                        "[0] Yes \r\n" +
                        "[1] No, I'd like a regular. \r\n" +
                        "Enter your option: ");
                int dippedChoice = CheckIntInput(Console.ReadLine(), 1);
                if (dippedChoice == 1)
                {
                    dipped = true;
                }

                iceCream1 = new Cone(option,scoops,FlavourList,toppingList,dipped); 
                /*
                while (true) //Check for YN
                {
                    Console.WriteLine(
                        "Do you wish to order a Chocolate-dipped cone? \r\n" +
                        "[1] Yes \r\n" +
                        "[2] No, I'd like a regular. \r\n" +
                        "Enter your option: ");
                    string dippedChoice = Console.ReadLine();

                    dippedCCheckIntInput()
                    if (dippedChoice == "Y")
                    {
                        dipped = true;
                        break;
                    }
                    if (dippedChoice == "N")
                    {
                        break;
                    }
                    
                    Console.WriteLine("Error: Invalid input. ");
                    
                } */

            }

            else if (input == 2) //Check for input waffle flavour
            {
                option = "Waffle";
                int waffleOption = 0;

                List<string> waffleFlavours = new List<string>() {"Regular", "Red Velvet", "Charcoal", "Pandan Waffle" };
                Console.WriteLine(
                   "Waffle Flavours: \r\n" +
                   "[0] Regular \r\n" +
                   "[1] Red Velvet \r\n" +
                   "[2] Charcoal \r\n " +
                   "[3] Pandan Waffle \r\n " +
                   "Enter the waffle flavour. ");

                waffleOption = CheckIntInput(Console.ReadLine(), 3);
                iceCream1 = new Waffle(option, scoops, FlavourList, toppingList, waffleFlavours[waffleOption]);

                /*
                while (true)
                {
                    try
                    {
                        waffleOption = Convert.ToInt16(Console.ReadLine());
                        if (option < 1 || option > 4)
                        {
                            Console.WriteLine("Error: Input must be between 1-4. ");
                        }
                        else
                        {
                            break;
                        }

                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                */

            }
            else
            {
                option = "Cup";
                iceCream1 = new Cup(option, scoops, FlavourList, toppingList);
            }

            IceCreamList[index] = iceCream1;

            /*
            int scoops = Convert.ToInt16(Console.ReadLine());
            for (int i = 0; i < scoops; i++)
            {

                Console.WriteLine(
                    "Flavours: \r\n" +
                    "[1] Vanilla \r\n" +
                    "[2] Chocolate \r\n" +
                    "[3] Strawberry \r\n \r\n" +
                    "Premium Flavours (+$2): \r\n" +
                    "[4] Durian \r\n" +
                    "[5] Ube \r\n" +
                    "[6] Sea Salt \r\n" +
                    "[0] None \r\n" +
                    $"Please enter number of all wanted flavours ({scoops} scoops):  "); //Check for "Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt" and no. of ints
            }

            Console.WriteLine(
                "Toppings:  \r\n" +
                "[1] Sprinkles \r\n" +
                "[2] Mochi \r\n " +
                "[3] Sago \r\n " +
                "[4] Oreos \r\n " +
                "[0] None \r\n" +
                "Please enter number of all wanted ice cream toppings: "); 

            Console.WriteLine("[1] for Cup, [2] for Cone, [3] for Waffle. Please enter ice cream option. ");
            */
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
            string? orders = "";
            foreach (IceCream i in iceCreamList)
            {
                orders += i.ToString() + "\n";
            }
            return $"{Id, -12} {TimeReceived, -12} {TimeFulfilled, -12} {orders}";
        }




    }
}
