//==========================================================
// Student Number : S10259305
// Student Name : Keagan Alexander Sng Yu
// Partner Name : Jerald Tee Li Yi
//==========================================================

using Microsoft.VisualBasic;
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
            

            //Code to Test order functions
            /*Order order1 = new Order(1,DateTime.Now);
            List<Flavour> flavours = new List<Flavour>();
            flavours.Add(new Flavour("Strawberry",false,1));

            List<Topping> toppings = new List<Topping>();
            toppings.Add(new Topping("Oreos")); 
            order1.AddIceCream(new Cup("Cup", 1,flavours,toppings));
            regularQueue.Enqueue(order1);

            order1.AddIceCream(new Cup("Cup", 1, flavours, toppings));
            order1.ModifyIceCream(1);
            Console.WriteLine(order1.IceCreamList[1]); 
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
                    "[0] Exit\r\n" +
                    "---------------------------------\r\n" +
                    "Enter your option: ");
            }

            void ListAllCustomers() //basic feature 1 (Keagan)
            {
                Console.WriteLine();
                Console.WriteLine($"{"Name", -12} {"Member ID", -14} {"Date of Birth", -17} {"Card Tier",-12} {"Membership Points", -20} {"Punch Card", -20} ");

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
                Console.WriteLine("Order Id:" + order.Id);
                Console.WriteLine($"Time Received: {order.TimeReceived}");
                
                if (order.TimeFulfilled == null)
                {
                    Console.WriteLine("Order is currently not fulfilled.");
                }
                else
                {
                    Console.WriteLine($"Time fulfilled: {order.TimeFulfilled}");
                }

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

                        if (id.Length == 6)
                        {
                            newId = Convert.ToInt32(id);
                            break;
                        }
                        else if (id.Length != 6)
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
                    try
                    {
                        Console.Write("Enter Date of Birth (DD/MM/YYYY): ");
                        string? date = Console.ReadLine();

                        Regex dateFormat = new Regex(@"^(\d\d)/(\d\d)/(\d\d\d\d)$");
                        
                        Match dateMatch = dateFormat.Match(date);

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
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Date of Birth was not in correct format DD/MM/YYYY. Please try again. ");
                        Console.WriteLine();
                    }
                }

                Customer newCustomer = new Customer(newName, newId, newDob);
                PointCard newCard = new PointCard();
                newCustomer.Rewards = newCard;

                using (StreamWriter cWriter = new StreamWriter("customers.csv"))
                {
                    cWriter.WriteLine($"{newName},{newId},{newDob},{"Ordinary"},{0},{0}");
                    cWriter.Close();
                }

                Console.WriteLine();
                Console.WriteLine("New customer successfully registered!");
                Console.WriteLine();
            }
        
            void CreateCustomerOrder() //basic feature 4 (Keagan)
            {

            }

            void DisplayOrderDetails() //basic feature 5 (Jerald)
            {
                // List information about all Customers: ListAllCustomers(); 
                Console.WriteLine("Select a customer");
                /* List the customers
                 prompt user to select a customer and retrieve the selected customer
                 retrieve all the order objects of the customer, past and current
                 for each order, display all the details of the order including datetime received, datetime 
                fulfilled (if applicable) and all ice cream details associated with the order (Modify DisplayOrder so it works) */
            }

            void ModifyOrderDetails() //basic feature 6 (Jerald)
            {

            }

            


            // Testing
            string choice = "";
             


            while (true) // Main loop 
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
                else if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }

            }
        }
    }
}
        