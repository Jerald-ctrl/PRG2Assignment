﻿//==========================================================
// Student Number : S10259305
// Student Name : Keagan Alexander Sng Yu
// Partner Name : Jerald Tee Li Yi
//==========================================================

using Microsoft.VisualBasic;
using System;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Security;
using System.Text.RegularExpressions;

namespace S10259842_PRG2Assignment
{
    class Program
    {



        static void Main(string[] args)
        {
            //setup code
            Queue<Order> regularQueue = new Queue<Order>();
            Queue<Order> goldenQueue = new Queue<Order>();

            Customer currentCustomer = null; //will be set to the current customer making the order; will be used to process advanced feature (a) 

            List<double> orderPrices = new List<double>(); //to store the prices of each ice cream in the order, to make calculations for total bill subtraction due to PunchCard or Birthday discount easier to process

            Dictionary<int, Customer> customerDict = new Dictionary<int, Customer>();

            void InitialiseCustomers()
            {
                using (StreamReader cReader = new StreamReader("customers.csv"))
                {
                    cReader.ReadLine();

                    string? line;
                    while ((line = cReader.ReadLine()) != null)
                    {
                        string[] cInfo = line.Split(",");
                        Customer customer = new Customer(cInfo[0], Convert.ToInt32(cInfo[1]), Convert.ToDateTime(cInfo[2]));

                        PointCard card = new PointCard(Convert.ToInt32(cInfo[4]), Convert.ToInt32(cInfo[5]));
                        card.Tier = cInfo[3];
                        customer.Rewards = card;

                        customerDict.Add(customer.MemberId, customer);
                    }
                    cReader.Close();
                }
            }

            InitialiseCustomers(); //initialise customers in customers.csv


            List<string> cupList = new List<string>();
            List<string> coneList = new List<string>();
            List<string> waffleList = new List<string>();

            void InitialiseItems() //Keagan
            {
                using (StreamReader iReader = new StreamReader("options.csv"))
                {
                    iReader.ReadLine();

                    string? line;
                    while ((line = iReader.ReadLine()) != null)
                    {
                        string[] info = line.Split(",");
                        if (info[0] == "Cup")
                        {
                            cupList.Add($"{info[0],-12} {info[1],-12} ${info[4],-12}");
                        }
                        else if (info[0] == "Cone")
                        {
                            coneList.Add($"{info[0],-12} {info[1],-12} {info[2],-12} ${info[4],-12}");
                        }
                        else if (info[0] == "Waffle")
                        {
                            waffleList.Add($"{info[0],-12} {info[1],-10} {info[3],-15} ${info[4],-12}");
                        }
                    }

                    iReader.Close();
                }
            }

            InitialiseItems(); //initialise options in options.csv, Keagan


            List<string> flavourList = new List<string>();

            void InitialiseFlavours()
            {
                using (StreamReader fReader = new StreamReader("flavours.csv"))
                {
                    fReader.ReadLine();

                    string? line;
                    while ((line = fReader.ReadLine()) != null)
                    {
                        string[] fInfo = line.Split(",");
                        string flavour = fInfo[0];
                        flavourList.Add(flavour);
                    }
                    fReader.Close();
                }
            }

            InitialiseFlavours(); //initialise flavours in flavours.csv, Keagan

           


            void DisplayMenu() //Displays the menu every iteration, written by Jerald
            {
                Console.Write("" +
                    "------------- MENU --------------\r\n" +
                    "[1] List All Customers\r\n" +
                    "[2] List all current orders (the working one)\r\n" +
                    "[3] Register a new customer\r\n" +
                    "[4] Create a customer’s order\r\n" +
                    "[5] Display order details of a customer\r\n" +
                    "[6] Modify order details\r\n" +
                    "[7] Process an order and checkout \r\n" +
                    "[8] Display monthly & total charged amounts for the year \r\n" +
                    "[0] Exit\r\n" +
                    "---------------------------------\r\n" +
                    "Enter your option: ");
            }
            

            int CheckIntInput(string input,int minInt, int maxInt) //checks that input is INT and is 0 < x < maxInt, Written by Jerald
            {
                int intInput = 0;
                while (true)
                {
                    try
                    {
                        intInput = Convert.ToInt32(input);
                        if (intInput < minInt || intInput > maxInt)
                        {
                            Console.WriteLine($"Error: Input must be between {minInt}-{maxInt}. ");
                            Console.WriteLine();

                            throw new Exception();
                        }
                        break;
                    }
                    catch (FormatException ex) //Catches non-int inputs
                    {
                        Console.WriteLine(ex.Message);

                        Console.Write("Please re-enter your option: ");
                        input = Console.ReadLine();
                        Console.WriteLine();
                    }
                    catch (Exception ex) //Acts like a finally block but doesn't execute 100% of the time.
                    {
                        Console.Write("Please re-enter your option: ");
                        input = Console.ReadLine();
                        Console.WriteLine();
                    }
                }
                return intInput;
            }

            
            // Keagan
            int ProcessYesNo() //method to process all yes/no choices, Written by Keagan
            {
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[2] No");
                Console.WriteLine();
                Console.Write("Enter option: ");

                string? choice = Console.ReadLine();
                int option = CheckIntInput(choice, 1, 2);

                Console.WriteLine();

                return option;
            }


           // Keagan
            Customer SearchCustomer(string id) //Written by Keagan
            {
                int idToSearch = 0;
                while (true)
                {
                    try
                    {
                        
                        if (id.Length == 6 && id.All(char.IsDigit) && id[0] != '0') 
                        {
                            idToSearch = Convert.ToInt32(id);
                        }
                        else
                        {
                            throw new FormatException();
                        }
                        Customer customer = null;
                        foreach (KeyValuePair<int, Customer> c in customerDict)
                        {
                            if (idToSearch == c.Value.MemberId)
                            {
                                customer = c.Value;
                                
                                return customer;

                            }
                        }

                        Console.WriteLine("Customer ID not found. Please reinput.");
                        Console.WriteLine();
                    }
                    catch (FormatException f)
                    {
                        Console.WriteLine("ID has to be a positive 6-digit number, and cannot begin with 0. Please try again. ");
                        Console.WriteLine();
                    }
                    
                    Console.Write("Select customer (enter ID to select): ");
                    id = Console.ReadLine();
                }
            }
                

               
            

            IceCream OrderIceCream() //ice cream order process, written by Keagan
            {
                IceCream iceCream = null;

                Console.WriteLine("[1] Cup");
                Console.WriteLine("[2] Cone");
                Console.WriteLine("[3] Waffle");

                Console.WriteLine();

                int newOption = 0;
                Console.Write("Enter your option: ");

                newOption = CheckIntInput(Console.ReadLine(), 1, 3);
                Console.WriteLine();

                //downcasting the IceCream object to one of its subclasses depending on the user's selection

                if (newOption == 1)
                {
                    iceCream = new Cup();
                }
                else if (newOption == 2)
                {
                    iceCream = new Cone();
                }
                else if (newOption == 3)
                {
                    iceCream = new Waffle();
                }
                //no need to handle error of incorrect input as newOption has already been checked to be within range using the CheckIntInput method;
                //hence newIceCream will not return null. 

                if (newOption == 1)
                {
                    Console.WriteLine($"{"Option",-12} {"Scoops",-12} {"Cost",-12}");
                    foreach (string cup in cupList)
                    {
                        Console.WriteLine(cup);
                    }
                }

                else if (newOption == 2)
                {
                    Console.WriteLine($"{"Option",-12} {"Scoops",-12} {"Dipped",-12} {"Cost",-12}");
                    foreach (string cone in coneList)
                    {
                        Console.WriteLine(cone);
                    }
                }

                else if (newOption == 3)
                {
                    Console.WriteLine($"{"Option",-12} {"Scoops",-10} {"Waffle Flavour",-15} {"Cost",-12}");
                    foreach (string waffle in waffleList)
                    {
                        Console.WriteLine(waffle);
                    }
                }


                Console.WriteLine();

                int selectedScoops = 0;
                while (true) //try-catch block for error handling for user input for number of scoops
                {
                    try
                    {
                        Console.Write("Enter number of scoops: ");
                        selectedScoops = Convert.ToInt32(Console.ReadLine());

                        if (selectedScoops > 3) //using if-else statements to check for ArgumentOutOfRange exception, because different error messages are required
                        {
                            Console.WriteLine("Items can have a maximum of 3 scoops. Please try again. ");
                            Console.WriteLine();
                            continue;
                        }
                        else if (selectedScoops < 1)
                        {
                            Console.WriteLine("Items must have a minimum of 1 scoop. Please try again. ");
                            Console.WriteLine();
                            continue;
                        }
                        break;
                    }
                    catch (FormatException f)
                    {
                        Console.WriteLine(f.Message + " Please enter a number from 1 to 3. ");
                        Console.WriteLine();
                    }

                }

                iceCream.Scoops = selectedScoops; //sets selected number of scoops to the ice cream order



                for (int i = 0; i < selectedScoops; i++) //for loop loops as many times as the amount of scoops desired
                {
                    while (true) //while loop ensures flavour entered is valid before proceeding to the user's next flavour entry; if not, it will loop until flavour entered is valid. 
                    {
                        Console.WriteLine();
                        Console.WriteLine("Flavours: ");

                        int count = 1;
                        foreach (string flavour in flavourList)
                        {
                            if (flavour == "Durian" || flavour == "Ube" || flavour == "Sea salt")
                            {
                                Console.WriteLine($"[{count}] {flavour} (Premium)");
                            }
                            else
                            {
                                Console.WriteLine($"[{count}] {flavour}");
                            }

                            count++;
                        }

                        Console.WriteLine();

                        try
                        {
                            Console.Write($"Enter flavour {i + 1}: ");
                            int flavourOption = CheckIntInput(Console.ReadLine(), 1, 6) - 1;

                            Flavour selectedFlavour = new Flavour();
                            bool found = false;

                            foreach (string flavour in flavourList)
                            {
                                if (flavourList[flavourOption].ToLower() == flavour.ToLower())
                                {
                                    if (flavourList[flavourOption] == "Durian" || flavourList[flavourOption] == "Ube" || flavourList[flavourOption] == "Sea salt")
                                    {
                                        selectedFlavour = new Flavour(flavourList[flavourOption], true);
                                    }
                                    else
                                    {
                                        selectedFlavour = new Flavour(flavourList[flavourOption], false);
                                    }

                                    iceCream.Flavours.Add(selectedFlavour);
                                    found = true;
                                    break;
                                }
                            }

                            if (found == false)
                            {
                                Console.WriteLine("Invalid flavour. Please try again. ");
                                Console.WriteLine();
                                continue;
                            }

                            break;

                        }
                        catch (FormatException f)
                        {
                            Console.WriteLine($"{f.Message} Please enter a number from 1 to {flavourList.Count}");
                            Console.WriteLine();
                        }
                    }
                }


                if (iceCream is Cone) //selecting ice cream cone type if the ice cream ordered is a cone
                {
                    Cone c = (Cone)iceCream; //downcast iceCream to Cone to access Dipped property

                    Console.WriteLine("Select your cone type: ");
                    Console.WriteLine("[1] Original");
                    Console.WriteLine("[2] Chocolate-dipped");
                    Console.WriteLine();
                    Console.Write("Enter your option: ");

                    int ConeOption = CheckIntInput(Console.ReadLine(), 1, 2);

                    if (ConeOption == 1)
                    {
                        c.Dipped = false;
                    }
                    else if (ConeOption == 2)
                    {
                        c.Dipped = true;
                    }
                }

                if (iceCream is Waffle) //selecting waffle flavour if the ice cream ordered is a waffle
                {
                    Waffle w = (Waffle)iceCream;

                    Console.WriteLine("Select your waffle flavour: ");
                    Console.WriteLine("[1] Original");
                    Console.WriteLine("[2] Red Velvet");
                    Console.WriteLine("[3] Charcoal");
                    Console.WriteLine("[4] Pandan");
                    Console.WriteLine();
                    Console.Write("Enter your option: ");

                    int waffleOption = CheckIntInput(Console.ReadLine(), 1, 4);

                    switch (waffleOption) //no additional validation required, since validation has already been carried out by using the CheckIntInput method above
                    {
                        case 1:
                            w.WaffleFlavour = "Original";
                            break;
                        case 2:
                            w.WaffleFlavour = "Red Velvet";
                            break;
                        case 3:
                            w.WaffleFlavour = "Charcoal";
                            break;
                        case 4:
                            w.WaffleFlavour = "Pandan";
                            break;
                    }
                }


                Console.WriteLine("Would you like toppings? ");
                int option = ProcessYesNo();

                if (option == 1)
                {
                    int count = 0;
                    while (count < 4) //only allow 4 iterations so that customer does not over-add toppings; max 4 toppings on 1 ice cream 
                    {
                        Console.WriteLine("Toppings");
                        Console.WriteLine("[1] Sprinkles");
                        Console.WriteLine("[2] Mochi");
                        Console.WriteLine("[3] Sago");
                        Console.WriteLine("[4] Oreos");
                        Console.WriteLine();
                        Console.Write("Enter your option: ");

                        int toppingChoice = CheckIntInput(Console.ReadLine(), 1, 4);

                        switch (toppingChoice)
                        {
                            case 1:
                                iceCream.Toppings.Add(new Topping("Sprinkles"));
                                break;
                            case 2:
                                iceCream.Toppings.Add(new Topping("Mochi"));
                                break;
                            case 3:
                                iceCream.Toppings.Add(new Topping("Sago"));
                                break;
                            case 4:
                                iceCream.Toppings.Add(new Topping("Oreos"));
                                break;
                        }

                        count++;

                        if (count < 4) //4 = final allowed iteration
                        {
                            Console.WriteLine("Would you like to add more toppings: "); //prompt user if they would like to add more toppings; loop will continue if yes, and break if no
                            int addMoreToppings = ProcessYesNo();

                            if (addMoreToppings == 1)
                            {
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have reached the maximum amount of toppings allowed (4). ");
                            Console.WriteLine();
                            break;
                        }
                    }
                }
                return iceCream;
            }

            void DisplayOrder(Order order) // Function to display all information about an order, used for Q2, Q5, Jerald
            {

               
                Console.WriteLine($"{"Order Id",-12} {"TimeReceived",-35} {"TimeFulfilled",-35}");

                Console.WriteLine(order);

               
            }

            // Keagan
            Customer SelectCustomer(string customerID)
            {
                Customer selectedCustomer = SearchCustomer(customerID);
                Console.WriteLine($"Selected customer: {selectedCustomer.Name} (ID: {selectedCustomer.MemberId})");
                Console.WriteLine();
                return selectedCustomer;
            }


            Dictionary<int, Order> orderDict = new Dictionary<int, Order>();
            //Read customers.csv and create corresponding IceCream objects and append to Order to customer
            void ProcessOrdersCSV()
            {
                Order newOrder = null;
                using (StreamReader sR = new StreamReader("orders.csv"))  //Id,MemberId,TimeReceived,TimeFulfilled,Option,Scoops,Dipped,WaffleFlavour,Flavour1,Flavour2,Flavour3,Topping1,Topping2,Topping3,Topping4
                {
                    sR.ReadLine();
                    string? line;
                    IceCream iceCream = null;
                    while ((line = sR.ReadLine()) != null)
                    {
                        string[] orderInfo = line.Split(",");
                        double cost = 4;

                        List<Flavour> flavours = new List<Flavour>();
                        List<Topping> toppings = new List<Topping>();
                        string? Flavour1 = orderInfo[8];

                        for (int i = 8; i < 11; i++) //Reads fields Flavour1-Flavour4
                        {
                            if (orderInfo[i] != "")
                            {
                                flavours.Add(CreateFlavour(orderInfo[i]));
                            }

                        }
                        for (int i = 11; i < 15; i++) //Reads fields Topping1-Topping4
                        {
                            if (orderInfo[i] != "")
                            {
                                toppings.Add(new Topping(orderInfo[i]));
                            }

                        }

                        // If blocks to create new IceCream object based on the option field
                        if (orderInfo[4] == "Cup")
                        {

                            iceCream = new Cup(orderInfo[4], Convert.ToInt16(orderInfo[5]), flavours, toppings);
                            //Console.WriteLine($"Cup is {iceCream.CalculatePrice()}");
                        }
                        else if (orderInfo[4] == "Cone")
                        {
                            bool dipped = Convert.ToBoolean(orderInfo[6]);
                            iceCream = new Cone(orderInfo[4], Convert.ToInt16(orderInfo[5]), flavours, toppings, dipped);
                            //Console.WriteLine($"Cone is {iceCream.CalculatePrice()}");
                        }
                        else
                        {
                            string waffleFlavour = orderInfo[7];
                            iceCream = new Waffle(orderInfo[4], Convert.ToInt16(orderInfo[5]), flavours, toppings, waffleFlavour);
                            //Console.WriteLine($"Waffle is {iceCream.CalculatePrice()}");

                        }
                        


                        int orderID = Convert.ToInt32(orderInfo[0]);
                        // Doesn't use MakeOrder as its supposed to be in OrderHistory
                        //
                        if (orderDict.ContainsKey(orderID) == false)
                        {
                            orderDict[orderID] = new Order(Convert.ToInt32(orderInfo[0]), Convert.ToDateTime(orderInfo[2]));
                            orderDict[orderID].TimeFulfilled = Convert.ToDateTime(orderInfo[3]);
                            Customer customer = SearchCustomer(orderInfo[1]); //Prevents SearchCustomer from having Console.WriteLine("Customer found ...")

                            customer.OrderHistory.Add(orderDict[orderID]);
                        }
                        orderDict[orderID].IceCreamList.Add(iceCream);



                        /*
                        newOrder = new Order(Convert.ToInt16(orderInfo[0]), Convert.ToDateTime(orderInfo[2]));
                        newOrder.IceCreamList.Add( iceCream );
                        newOrder.TimeFulfilled = Convert.ToDateTime(orderInfo[3]); */


                    }

                }


            }
            
            
            ProcessOrdersCSV();
            int GiveOrderID() //Function to generate a unique order id
            {
                while (true)
                {
                    
                    Random newRandom = new Random();
                    int orderID = newRandom.Next(1, 1000);
                    if (orderDict.ContainsKey(orderID) != true)
                    {
                        return orderID;
                    }
                    Console.WriteLine("Sus");
                }
                

                
                   
            }
            /*--------------------------------------------------------FEATURES----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------FEATURES----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------FEATURES----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------FEATURES----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------FEATURES----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------FEATURES----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------FEATURES----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------FEATURES----------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            
            
            void ListAllCustomers() //basic feature 1 (Keagan)
            {
                Console.WriteLine();
                Console.WriteLine($"{"Name",-12} {"Member ID",-14} {"Date of Birth",-17} {"Card Tier",-12} {"Membership Points",-20} {"Punch Card",-20} ");

                foreach (KeyValuePair<int, Customer> c in customerDict)
                {
                    Console.WriteLine(c.Value);
                }
                Console.WriteLine();
            }

            void ListAllCurrentOrders(Queue<Order> regularQueue, Queue<Order> goldenQueue) //basic feature 2 (Jerald)
            {
                if (goldenQueue.Count == 0)
                {
                    Console.WriteLine("There are no orders in the golden queue.");
                }
                else
                {
                    Console.WriteLine("Golden queue: \r\n" + "--------------");
                    foreach (Order order in goldenQueue)
                    {
                        DisplayOrder(order);

                    }

                }
                if (regularQueue.Count == 0)
                {
                    Console.WriteLine("There are no orders in the regular queue.");
                }
                else
                {
                    Console.WriteLine("Regular queue: \r\n" + "--------------");
                    foreach (Order order in regularQueue)
                    {
                        DisplayOrder(order);
                    }
                }

            }

            void RegisterCustomer() //basic feature 3 (Keagan)
            {
                Console.WriteLine();

                string? newName = "";
                int newId = 0;
                DateTime newDob = DateTime.Today;

                while (true)
                {
                    Console.Write("Enter name: ");
                    newName = Console.ReadLine().TrimEnd(' ');

                    if (newName.All(char.IsLetter) && newName != "")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid name. Please try again. ");
                        Console.WriteLine();
                        continue;
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Enter ID: ");
                        string? id = Console.ReadLine();

                        if (id.Length == 6 && id.All(char.IsDigit) && id[0] != '0')
                        {
                            bool idAlreadyTaken = false;
                            foreach (KeyValuePair<int, Customer> c in customerDict)
                            {
                                if (c.Key == Convert.ToInt32(id))
                                {
                                    idAlreadyTaken = true;
                                    break;
                                }
                            }
                            if (idAlreadyTaken == true)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                newId = Convert.ToInt32(id);
                                break;
                            }
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("ID has to be a 6-digit number, and cannot begin with 0. Please try again. ");
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("This ID number is already taken. Please enter a different ID number. ");
                        Console.WriteLine();
                    }
                }

                while (true) 
                {
                    Console.Write("Enter Date of Birth (DD/MM/YYYY): ");
                    string? date = Console.ReadLine();

                    Regex dateFormat = new Regex(@"^(\d\d)/(\d\d)/(\d\d\d\d)$");

                    Match dateMatch = dateFormat.Match(date);

                    try
                    {
                        if (dateMatch.Success)
                        {
                            if (Convert.ToDateTime(date) < DateTime.Today)
                            {
                                newDob = Convert.ToDateTime(date);
                            }
                            else
                            {
                                Console.WriteLine("Future date entered. Please enter a valid Date of Birth. ");
                                Console.WriteLine();
                                continue;
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }

                        break;
                    }
                    catch (FormatException f)
                    {
                        Console.WriteLine(f.Message);
                    }

                    catch (Exception)
                    {
                        Console.WriteLine($"Date of Birth was not in correct format DD/MM/YYYY. Please try again. ");
                        Console.WriteLine();
                    }
                }

                Customer newCustomer = new Customer(newName, newId, newDob);
                customerDict.Add(newId, newCustomer);

                PointCard newCard = new PointCard();
                newCustomer.Rewards = newCard;

                using (StreamWriter cWriter = new StreamWriter("customers.csv"))
                {
                    cWriter.WriteLine($"{newName},{newId},{newDob},{"Ordinary"},{0},{0}");
                    cWriter.Close();
                }

                Console.WriteLine();
                Console.WriteLine($"Customer Details: \nName: {newName} \nMember ID: {newId} \nDate of Birth: {newDob.ToString("dd/MM/yyyy")} \r\n");
                //Console.WriteLine();
                Console.WriteLine("New customer successfully registered!");
                Console.WriteLine();
               
            }

            void CreateCustomerOrder() //basic feature 4 (Keagan)
            {
                Console.WriteLine();
                ListAllCustomers();

                int selectedId = 0;
                Console.Write("Select customer (enter ID to select): ");
                Customer selectedCustomer = SelectCustomer(Console.ReadLine());
                
                /*
                Customer selectedCustomer = null;
                
                while (true) //try-catch block for error handling for ID user input
                {
                    Console.Write("Select customer (enter ID to select): ");

                    try
                    {
                        string? id = Console.ReadLine();

                        selectedCustomer = SearchCustomer(id);
                        if (selectedCustomer == null)
                        {
                            throw new Exception();
                        }

                        Console.WriteLine($"Selected customer: {selectedCustomer.Name} (ID: {selectedCustomer.MemberId})");
                        Console.WriteLine();
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Customer not found. Please try again");
                        Console.WriteLine();
                    }
                }
                */

                Order newOrder = selectedCustomer.MakeOrder(GiveOrderID());

                Console.WriteLine($"Hello, {selectedCustomer.Name}! What would you like today? ");
                while (true)
                {
                    IceCream newIceCream = OrderIceCream();
                    newOrder.AddIceCream(newIceCream);

                    int continueOption = 0;

                    Console.WriteLine("Would you like to add another ice cream to the order? "); //prompt user if they would like to continue

                    continueOption = ProcessYesNo();

                    if (continueOption == 1)
                    {
                        continue;
                    }
                    else if (continueOption == 2)
                    {
                        break;
                    }
                }

                if (selectedCustomer.Rewards.Tier == "Gold")
                {
                    goldenQueue.Enqueue(newOrder);
                }
                else
                {
                    regularQueue.Enqueue(newOrder);
                }

                newOrder.CustomerID = selectedCustomer.MemberId; //assigns the current customer's ID to the order
                
                Console.WriteLine("Order made successfully. Please proceed to checkout. ");
                Console.WriteLine();
            }

           
            //basic feature 5 (Jerald)
            void DisplayOrderDetails() 
            {
                ListAllCustomers(); // List information about all Customers: 

                //Code to select a Customer PLEASE ADD VALIDATION
                // ---
                Console.Write("Input a customer ID to search: ");
                Customer customer = SelectCustomer(Console.ReadLine());
                // ---

                //customer.MakeOrder();
                if (customer.CurrentOrder != null) 
                {
                    Console.WriteLine(
                    "Current Order: \r\n" +
                    "--------------");

                    DisplayOrder(customer.CurrentOrder);
                }
                else
                {
                    Console.WriteLine("Customer has no current order.");
                }
                if (customer.OrderHistory.Count > 0) 
                {
                    Console.WriteLine(
                    "Order history: \r\n" +
                    "--------------");
                    foreach (Order order in customer.OrderHistory)
                    {
                        DisplayOrder(order); //Changed private Order currentOrder = null; in Customer.Cs
                    }
                }
                else
                {
                    Console.WriteLine("Customer has no past orders.");
                }
                

                /* List the customers
                 prompt user to select a customer and retrieve the selected customer
                 retrieve all the order objects of the customer, past and current
                 for each order, display all the details of the order including datetime received, datetime 
                fulfilled (if applicable) and all ice cream details associated with the order (Modify DisplayOrder so it works) */
            }

            void ModifyOrderDetails() //basic feature 6 (Jerald)
            {
                ListAllCustomers();
                /*
                while (true) //try-catch block for error handling for ID user input
                {
                    Console.Write("Select customer (enter ID to select): ");

                    try
                    {
                        string? id = Console.ReadLine();

                        selectedCustomer = SearchCustomer(id);
                        if (selectedCustomer == null)
                        {
                            throw new Exception();
                        }

                        Console.WriteLine($"Selected customer: {selectedCustomer.Name} (ID: {selectedCustomer.MemberId})");
                        Console.WriteLine();
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Customer not found. Please try again");
                        Console.WriteLine();
                    }
                }
                */

                Console.Write("Select customer (enter ID to select): ");
                Customer selectedCustomer = SelectCustomer(Console.ReadLine());
                

                if (selectedCustomer.CurrentOrder == null)
                {
                    Console.WriteLine("Selected customer has no order, Please create an order first.");
                    return;
                }
                Order currentOrder = selectedCustomer.CurrentOrder;
                Console.WriteLine(currentOrder); //List all the ice cream information in the order

                int option = 0;
               
                    Console.Write(
                   "[1] Choose an existing ice cream object to modify \r\n" +
                   "[2] Add an entirely new ice cream object to the order (compulsory if there are no orders) \r\n" +
                   "[3] Choose an existing ice cream object to delete from the order \r\n" +
                   "[0] Exit \r\n" +
                   "Enter your option: ");
                    option = CheckIntInput(Console.ReadLine(), 0, 3);

                
                int iceCreamListCount = currentOrder.IceCreamList.Count();
                if (option == 0)
                {
                    return;
                }
                else if (option == 1)  // Select which ice cream to modify 
                {
                    if (iceCreamListCount < 1)
                    {
                        Console.WriteLine("Option is unavailable since there is no ice cream in current order. Please try again and input option 2");
                        return;
                    }
                    Console.Write("Enter Ice Cream no. to modify (must be int): ");
                    int index = CheckIntInput(Console.ReadLine(),1,iceCreamListCount); //Check that the input is a valid index within the list
                    currentOrder.ModifyIceCream(index-1);

                }
                else if (option == 2) //Option to Create a new ice cream object and add it to the order
                {
                    OrderIceCream();
                }

                else if (option == 3) //Option to Delete an ice cream object from the list
                {
                    if (iceCreamListCount < 1)
                    {
                        Console.WriteLine("Option is unavailable since there is no ice cream in current order. Please try again and input option 2");
                        return;
                    }
                    else if (iceCreamListCount < 2)
                    {
                        Console.WriteLine("You cannot have 0 ice creams in an order. Please try again and input option 2");
                    }
                    else 
                    {
                        Console.Write("Enter Ice Cream no. to delete (must be int): ");
                        int index = CheckIntInput(Console.ReadLine(), 1, iceCreamListCount); //Check that the input is a valid index within the list
                        currentOrder.DeleteIceCream(index-1);
                    }
                }
                DisplayOrder(currentOrder); //List all the ice cream information in the order


                /*
                 * 
                    list the customers
                     prompt user to select a customer and retrieve the selected customer’s current order
                     list all the ice cream objects contained in the order
                     prompt the user to either [1] choose an existing ice cream object to modify, [2] add an 
                    entirely new ice cream object to the order, or [3] choose an existing ice cream object to 
                    delete from the order
                    o if [1] is selected, have the user select which ice cream to modify then prompt the user 
                    for the new information for the modifications they wish to make to the ice cream
                    selected: option, scoops, flavours, toppings, dipped cone (if applicable), waffle flavour 
                    (if applicable) and update the ice cream object’s info accordingly
                    o if [2] is selected prompt the user for all the required info to create a new ice cream 
                    object and add it to the order
                    o if [3] is selected, have the user select which ice cream to delete then remove that ice 
                    cream object from the order. But if this is the only ice cream in the order, then simply 
                    display a message saying they cannot have zero ice creams in an order
                     display the new updated orde 
                 */
            }


            void ProcessOrderAndCheckout() //advanced feature a (Keagan)
            {
                Console.WriteLine();

                Order currentOrder = new Order(); //sets current order to a new Order instance until it is set again to an actual Customer's order in the code below

                if (regularQueue.Count != 0) //checks if regular queue has orders in it; if there are, then orders can be processed. 
                {
                    if (goldenQueue.Count != 0) //if there are any orders in the gold members' queue, then its first order will be processed
                    {
                        currentOrder = goldenQueue.Dequeue();
                    }
                    else //if there are no gold member orders, then the regular queue's first order will be processed
                    {
                        currentOrder = regularQueue.Dequeue();
                    }

                    Customer customer = SearchCustomer(currentOrder.CustomerID.ToString()); //returns the customer whose order is to be processed 
                    string customerTier = customer.Rewards.Tier;
                    int customerPoints = customer.Rewards.Points;

                    Console.WriteLine($"{customer.Name}'s order (Member ID: {customer.MemberId}): ");
                    Console.WriteLine();

                    int count = 1;
                    foreach (IceCream i in currentOrder.IceCreamList) //display all orders in the current order being processed
                    {
                        Console.WriteLine($"---------------Item {count}---------------");
                        Console.WriteLine(i);
                        Console.WriteLine();
                        Console.WriteLine();
                        count++;
                    }

                    Console.WriteLine("------------------------------------");
                    Console.WriteLine();

                    double totalBill = 0;
                    List<double> orderPrices = new List<double>(); //to store the prices of each ice cream in the order, to make calculations for total bill subtraction due to PunchCard or Birthday discount easier to process

                    foreach (IceCream i in currentOrder.IceCreamList)
                    {
                        double price = i.CalculatePrice();
                        orderPrices.Add(price);
                        totalBill += price;
                    }


                    Console.WriteLine($"Your membership status: {customerTier}"); //displaying customer's membership status
                    Console.WriteLine($"Your membership points: {customerPoints}"); //displaying customer's membership points
                    Console.WriteLine();

                    if (customer.IsBirthday())
                    {
                        double highestPrice = 0;
                        foreach (double price in orderPrices)
                        {
                            if (price > highestPrice)
                            {
                                highestPrice = price;
                            }
                        }

                        totalBill -= highestPrice;
                        Console.WriteLine("Happy Birthday! Your most expensive order today is free of charge!");
                        Console.WriteLine($"Total birthday discount: {highestPrice:C}");
                        Console.WriteLine();
                    }


                    if (customer.Rewards.PunchCard == 10)
                    {
                        Console.WriteLine("You have completed your Punch Card! Your first ice cream in your order is now free of charge. ");
                        Console.WriteLine($"Total PunchCard discount: {orderPrices[0]:C}");
                        Console.WriteLine();

                        totalBill -= orderPrices[0];
                        customer.Rewards.PunchCard = 0;
                    }


                    if ((customerTier == "Silver" || customerTier == "Gold") && customerPoints > 0 && totalBill != 0) //totalBill != 0 is to check if the punchCard was used to get a single ice cream free of charge, or
                                                                                                                      //an ice cream was free of charge due to the birthday discount, or both,
                                                                                                                      //which would mean that points would not have to be redeemed. 
                    {
                        Console.WriteLine("Redeem PointCard points? ");

                        int option = ProcessYesNo();

                        if (option == 1)
                        {
                            Console.Write("Enter points to use: ");
                            string? points = Console.ReadLine();

                            double pointsToUse = CheckIntInput(points, 1, customerPoints);

                            double pointDiscount = pointsToUse * 0.02;

                            Console.WriteLine();
                            Console.WriteLine($"Total discount: {pointDiscount:C}");

                            totalBill -= pointDiscount;
                        }
                    }

                    if (totalBill < 0) //if discount makes total bill < 0, set total bill to 0
                    {
                        totalBill = 0;
                    }

                    Console.WriteLine($"Final Bill: {totalBill:C}");
                    Console.WriteLine("Press any key to make payment. ");
                    Console.ReadLine();
                    customer.CurrentOrder.TimeFulfilled = DateTime.Now;
                    customer.OrderHistory.Add(currentOrder);
                    customer.CurrentOrder = null;


                    foreach (IceCream i in currentOrder.IceCreamList)
                    {
                        customer.Rewards.Punch();
                    }

                    string currentTier = customerTier;

                    double pointsEarned = Math.Floor(totalBill * 0.72);
                    customer.Rewards.AddPoints(Convert.ToInt32(pointsEarned));


                    //customerTier may have updated after AddPoints() is called. 
                    //customerTier is compared to the currentTier which is the tier before AddPoints() is called.
                    //if customerTier is different from currentTier, the tier will be updated. 

                    if (customerTier != currentTier)
                    {
                        Console.WriteLine($"Your PointCard status has been upgraded! You are now a {customerTier} member. ");
                    }

                    currentOrder.TimeFulfilled = DateTime.Now; //marking order as fulfilled
                    customer.OrderHistory.Add(currentOrder);

                    currentOrder.AmountCharged = totalBill;
                }
                else
                {
                    Console.WriteLine("Order queue empty. Please make an order first. ");
                }
            }



            Flavour CreateFlavour(string FlavourName)
            {
                if (FlavourName == "Durian" || FlavourName == "Ube" || FlavourName == "Sea Salt")
                {
                    return new Flavour(FlavourName,true);
                }
                return new Flavour(FlavourName, false);
            }



            

            

            void DisplayChargedAmts() //advanced feature b (Jerald)
            {
                Console.Write("Enter the year: ");
                int year = CheckIntInput(Console.ReadLine(),1000,DateTime.Today.Year);
                
                Dictionary<string, double> MonthCharged = new Dictionary<string, double>()
                {
                    { $"January {year}", 0 },
                    { $"February {year}", 0 },
                    { $"March {year}", 0 },
                    { $"April {year}", 0 },
                    { $"May {year}", 0 },
                    { $"June {year}", 0 },
                    { $"July {year}", 0 },
                    { $"August {year}", 0 },
                    { $"September {year}", 0 },
                    { $"October {year}", 0 },
                    { $"November {year}", 0 },
                    { $"December {year}", 0 }
                };

                /*
                using (StreamReader sR = new StreamReader("orders.csv"))  //Id,MemberId,TimeReceived,TimeFulfilled,Option,Scoops,Dipped,WaffleFlavour,Flavour1,Flavour2,Flavour3,Topping1,Topping2,Topping3,Topping4
                {
                    sR.ReadLine();
                    string? line;
                    while ((line = sR.ReadLine()) != null)
                    {
                        string[] orderInfo = line.Split(",");
                        double cost = 4;
                        if (orderInfo[4] == "Cone")
                        {
                            if (orderInfo[6] == "TRUE") //Check if the cone is dipped
                            {
                                cost += 2;
                            }
                        }

                        DateTime timeFulfilled = DateTime.Parse(orderInfo[3]);
                        int Month = timeFulfilled.Month;
                        int Year = timeFulfilled.Year;
                        string key = $"{Month} {Year}";

                    }
                */
                foreach (KeyValuePair<int,Customer> IdCustPair in customerDict)
                {
                    
                    Customer customer = IdCustPair.Value;
                    foreach (Order order in customer.OrderHistory) 
                    {
                        //Console.WriteLine($"{order}");
                        //Console.WriteLine($"CustomerID = {customer.MemberId}. OrderID = {order.Id}. Cost = {order.CalculateTotal()}.");
                        
                        if (order.TimeFulfilled?.Year == year)
                        {
                            string key = order.TimeFulfilled?.ToString("MMMM yyyy");

                            if (MonthCharged.ContainsKey(key))
                            {
                                //Console.WriteLine(key+ order.CalculateTotal+"hi");
                                //Console.WriteLine("Matched Total", key);
                                if (order.AmountCharged == 0)
                                {
                                    MonthCharged[key] += order.CalculateTotal();
                                }
                                else
                                {
                                    MonthCharged[key] += order.AmountCharged;
                                }
                                
                            }
                            
                        }
                        
                        

                    }
                    
                }
                double annualTotal = 0;
                foreach (KeyValuePair<string, double> MonthTotalPair in MonthCharged)
                {
                    Console.WriteLine($"{MonthTotalPair.Key+":",-20} ${MonthTotalPair.Value:N2}");
                    annualTotal += MonthTotalPair.Value;
                }
                Console.WriteLine($"Total: ${annualTotal:N2}");


            }

            // Main loop 
            while (true) 
            {
                DisplayMenu();
                int choice = CheckIntInput(Console.ReadLine(), 0, 8);

                switch (choice) //switch is used because it is a more efficient option when dealing with multiple possible integer inputs
                                //especially in cases of selecting from a list of options, as compared to if-else
                {
                    case 1:
                        ListAllCustomers();
                        break;
                    case 2:
                        ListAllCurrentOrders(regularQueue, goldenQueue);
                        break;
                    case 3:
                        RegisterCustomer();
                        break;
                    case 4:
                        CreateCustomerOrder();
                        break;
                    case 5:
                        DisplayOrderDetails();
                        break;
                    case 6:
                        ModifyOrderDetails();
                        break;
                    case 7:
                        ProcessOrderAndCheckout();
                        break;
                    case 8:
                        DisplayChargedAmts();
                        break;
                }

                if (choice == 0)
                {
                    break;
                }
            }
        }
    }
}

        