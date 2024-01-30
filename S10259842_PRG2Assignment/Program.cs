//==========================================================
// Student Number : S10259305
// Student Name : Keagan Alexander Sng Yu
// Partner Name : Jerald Tee Li Yi
//==========================================================

using Microsoft.VisualBasic;
using System;
using System.Diagnostics.Metrics;
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

            InitialiseCustomers();


            List<string> cupList = new List<string>();
            List<string> coneList = new List<string>();
            List<string> waffleList = new List<string>();

            void InitialiseItems()
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

            InitialiseItems();

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

            InitialiseFlavours();

            /*
            //Code to Test order functions
            Order order1 = new Order(1,DateTime.Now);
            List<Flavour> flavours = new List<Flavour>();
            flavours.Add(new Flavour("Strawberry",false));

            List<Topping> toppings = new List<Topping>();
            toppings.Add(new Topping("Oreos")); 
            order1.AddIceCream(new Cup("Cup", 1,flavours,toppings));
            regularQueue.Enqueue(order1);
            Console.WriteLine("hi" + order1.CalculateTotal());

            order1.AddIceCream(new Cup("Cup", 1, flavours, toppings));
            //order1.ModifyIceCream(1);
            DateTime DoB = new DateTime(12 / 23 / 1232);
            Customer customer = new Customer("John", 103021, DoB);
            Console.WriteLine(customer.OrderHistory);
            */

            void DisplayMenu() //Displays the menu every iteration
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
            
            int CheckIntInput(string input,int minInt, int maxInt) //checks that input is INT and is 0 < x < maxInt
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

            //method to search for customer and return Customer object (used in features 4 and 5 and 6)
            Customer SearchCustomer(int id) 
            {
                Customer customer = null;
                foreach (KeyValuePair<int, Customer> c in customerDict)
                {
                    if (id == c.Value.MemberId)
                    {
                        customer = c.Value;
                        break;
                    }
                }

                return customer;
            }

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

            void DisplayOrder(Order order) // Function to display all information about an order, used for Q2, Q5
            {
                
                //$"{Id,-12} {TimeReceived,-12} {TimeFulfilled,-12} {orders}";
                Console.WriteLine($"{"Order Id",-12} {"TimeReceived",-35} {"TimeFulfilled",-35}");
                
                Console.WriteLine(order+"\r\n"+$"Cost = {order.CalculateTotal()}");
                
                /*
                Console.WriteLine("Order Id:" + order.Id);
                Console.WriteLine($"Time Received: {order.TimeReceived}");
                Console.WriteLine($"Cost: {order.CalculateTotal()}");

                if (order.TimeFulfilled == null)
                {
                    Console.WriteLine("Order is currently not fulfilled.");
                }
                else
                {
                    Console.WriteLine($"Time fulfilled: {order.TimeFulfilled}");
                }
                */
            }

            void RegisterCustomer() //basic feature 3 (Keagan)
            {
                Console.WriteLine();
                Console.WriteLine("---------------REGISTER NEW CUSTOMER---------------");

                string? newName = "";
                int newId = 0;
                DateTime newDob = DateTime.Today;

                while (true)
                {
                    Console.Write("Enter name: ");
                    newName = Console.ReadLine();

                    if (newName.Any(char.IsNumber) || newName.Any(char.IsSymbol))
                    {
                        Console.WriteLine($"Invalid name. Please try again. ");
                        Console.WriteLine();
                        continue;
                    }

                    break;
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Enter ID: ");
                        string? id = Console.ReadLine();

                        if (id.Length == 6 && id.All(char.IsDigit))
                        {
                            newId = Convert.ToInt32(id);
                            break;
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("ID has to be a 6-digit number. Please try again. ");
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
                            newDob = Convert.ToDateTime(date);
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
                Customer selectedCustomer = null;

                while (true) //try-catch block for error handling for ID user input
                {
                    try
                    {
                        Console.Write("Select customer (enter ID to select): ");
                        string? id = Console.ReadLine();

                        if (id.Length == 6)
                        {
                            selectedId = Convert.ToInt32(id);
                        }
                        else if (id.Length != 6)
                        {
                            throw new FormatException();
                        }

                        selectedCustomer = SearchCustomer(selectedId);
                        if (selectedCustomer == null)
                        {
                            throw new Exception();
                        }

                        Console.WriteLine($"Selected customer: {selectedCustomer.Name} (ID: {selectedCustomer.MemberId})");
                        Console.WriteLine();
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("ID has to be a 6-digit number. Please try again. ");
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Customer not found. Please try again");
                        Console.WriteLine();
                    }
                }

                Order newOrder = new Order();

                while (true) //loop to allow user to add ice creams to order until user decides to stop
                {
                    Console.WriteLine("What would you like today? ");
                    Console.WriteLine("[1] Cup");
                    Console.WriteLine("[2] Cone");
                    Console.WriteLine("[3] Waffle");

                    Console.WriteLine();

                    int newOption = 0;
                    while (true) //try-catch block for error handling for user input for option
                    {
                        try
                        {
                            Console.Write("Enter your option: ");

                            newOption = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();


                            if (newOption < 1 || newOption > 3)
                            {
                                Console.WriteLine("Invalid option. Please try again. ");
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

                    //downcasting the IceCream object to one of its subclasses depending on the user's selection

                    IceCream newIceCream = null;

                    if (newOption == 1)
                    {
                        newIceCream = new Cup();
                    }
                    else if (newOption == 2)
                    {
                        newIceCream = new Cone();
                    }
                    else if (newOption == 3)
                    {
                        newIceCream = new Waffle();
                    }
                    //no need to handle error of incorrect input as newOption has already been checked to be within range in the above try-catch block;
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
                                continue;
                            }
                            else if (selectedScoops < 1)
                            {
                                Console.WriteLine("Items must have a minimum of 1 scoop. Please try again. ");
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

                    newIceCream.Scoops = selectedScoops; //sets selected number of scoops to the ice cream order



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
                                int option = Convert.ToInt32(Console.ReadLine()) - 1;

                                Flavour selectedFlavour = new Flavour();
                                bool found = false;

                                foreach (string flavour in flavourList)
                                {
                                    if (flavourList[option].ToLower() == flavour.ToLower())
                                    {
                                        if (flavourList[option] == "Durian" || flavourList[option] == "Ube" || flavourList[option] == "Sea salt")
                                        {
                                            selectedFlavour = new Flavour(flavourList[option], true);
                                        }
                                        else
                                        {
                                            selectedFlavour = new Flavour(flavourList[option], false);
                                        }

                                        newIceCream.Flavours.Add(selectedFlavour);
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

                    newOrder.AddIceCream(newIceCream);
                    Console.WriteLine(newIceCream);


                    int continueOption = 0;

                    while (true) //prompt user if they want to continue
                    {
                        Console.WriteLine("Would you like to add another order: ");
                        Console.WriteLine("[1] Yes");
                        Console.WriteLine("[2] No");

                        try
                        {
                            continueOption = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (FormatException f)
                        {
                            Console.WriteLine($"{f.Message} Please enter 1 for Yes, 2 for No");
                        }
                        catch (ArgumentOutOfRangeException a)
                        {
                            Console.WriteLine($"{a.Message} Please enter 1 for Yes, 2 for No");
                        }
                    }

                    if (continueOption == 1)
                    {
                        continue;
                    }
                    else if (continueOption == 2)
                    {
                        break;
                    }
                }
            }

            //basic feature 5 (Jerald)
            void DisplayOrderDetails() 
            {
                ListAllCustomers(); // List information about all Customers: 

                //Code to select a Customer PLEASE ADD VALIDATION
                // ---
                Console.Write("Input a customer ID to search: ");
                int searchId = CheckIntInput(Console.ReadLine(),100000,999999);
                Customer customer = SearchCustomer(searchId);
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
                Console.Write("Select customer (enter ID to select): ");
                Customer customer = SearchCustomer(Convert.ToInt32(Console.ReadLine()));
                Order currentOrder = customer.CurrentOrder;
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
                        Console.WriteLine("Option is unavailable since there is no current order. Please try again and input option 2");
                        return;
                    }
                    Console.Write("Enter Ice Cream no. to modify (must be int): ");
                    int index = CheckIntInput(Console.ReadLine(),1,iceCreamListCount); //Check that the input is a valid index within the list
                    currentOrder.ModifyIceCream(index-1);

                }
                else if (option == 2) //Option to Create a new ice cream object and add it to the order
                {
                    //Keagan i need ur code to pop up pretty soon man.
                }

                else if (option == 3) //Option to Delete an ice cream object from the list
                {
                    if (iceCreamListCount < 1)
                    {
                        Console.WriteLine("Option is unavailable since there is no current order. Please try again and input option 2");
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
                Console.WriteLine(currentOrder); //List all the ice cream information in the order


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


            void ProcessOrderAndCheckout()
            {
                
            }
            
            
            Flavour CreateFlavour(string FlavourName)
            {
                if (FlavourName == "Durian" || FlavourName == "Ube" || FlavourName == "Sea Salt")
                {
                    return new Flavour(FlavourName,true);
                }
                return new Flavour(FlavourName, false);
            }



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
                          
                        for (int i = 8; i < 11; i++)
                        {
                            if (orderInfo[i] != "")
                            {
                                flavours.Add(CreateFlavour(orderInfo[i]));
                            }

                        }
                        for (int i = 11; i < 15; i++)
                        {
                            if (orderInfo[i] != "")
                            {
                                toppings.Add(new Topping(orderInfo[i]));
                            }

                        }

                        
                        if (orderInfo[4] == "Cup")
                        {
                            
                            iceCream = new Cup(orderInfo[4], Convert.ToInt16(orderInfo[5]),flavours,toppings);
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
                        Dictionary<int,Order> orderDict = new Dictionary<int, Order>();


                        int orderID = Convert.ToInt32(orderInfo[0]);
                        if (orderDict.ContainsKey(orderID) == false)
                        {
                            orderDict[orderID] = new Order(Convert.ToInt16(orderInfo[0]), Convert.ToDateTime(orderInfo[2]));
                            orderDict[orderID].TimeFulfilled = Convert.ToDateTime(orderInfo[3]);
                            Customer customer = SearchCustomer(Convert.ToInt32(orderInfo[1]));
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

            void DisplayChargedAmts()
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
                        

                        
                        string key = order.TimeFulfilled?.ToString("MMMM yyyy");
                        
                        if (MonthCharged.ContainsKey(key))
                        {
                            //Console.WriteLine(key+ order.CalculateTotal+"hi");
                            Console.WriteLine("Matched Total", key);
                            MonthCharged[key] += order.CalculateTotal();
                        }
                        else
                        {
                            Console.WriteLine("Not Matched Total", key);
                            MonthCharged.Add(key, order.CalculateTotal());
                        }

                    }
                    
                }
                double annualTotal = 0;
                foreach (KeyValuePair<string, double> MonthTotalPair in MonthCharged)
                {
                    Console.WriteLine($"{MonthTotalPair.Key}: {MonthTotalPair.Value}");
                    annualTotal += MonthTotalPair.Value;
                }
                Console.WriteLine($"Total: {annualTotal}");


            }
            // Testing
            string choice = "";


            // Main loop 
            while (true) 
            {

                DisplayMenu();
                choice = Console.ReadLine();
                if (choice == "1") //if else conditions (with validation) to ensure valid inputs are given.
                {
                    ListAllCustomers();
                }
                else if (choice == "2")
                {
                    ListAllCurrentOrders(regularQueue, goldenQueue);
                }
                else if (choice == "3")
                {
                    RegisterCustomer();
                }
                else if (choice == "4")
                {
                    CreateCustomerOrder();
                }
                else if (choice == "5")
                {
                    DisplayOrderDetails();
                }
                else if (choice == "6")
                {
                    ModifyOrderDetails();
                }
                else if (choice == "7")
                {
                    ProcessOrderAndCheckout();
                }
                else if (choice == "8")
                {
                    DisplayChargedAmts();
                }
                else if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option, must be an int from 0 to 8. Please try again.");
                }

                //using switch would require an additional user entry validation using either try-catch or if-else
                //to check that the value entered is within range. 
            }
        }
    }
}

        