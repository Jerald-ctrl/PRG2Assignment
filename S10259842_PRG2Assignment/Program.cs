//==========================================================
// Student Number : S10259305
// Student Name : Keagan Alexander Sng Yu
// Partner Name : Jerald Tee Li Yi
//==========================================================

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
            List<Customer> customerList = new List<Customer>();

            /*
            //Code to Test order functions
            Order order1 = new Order(1,DateTime.Now);
            List<Flavour> flavours = new List<Flavour>();
            flavours.Add(new Flavour("Strawberry",false,1));

            List<Topping> toppings = new List<Topping>();
            toppings.Add(new Topping("Oreos")); 
            order1.AddIceCream(new Cup("Cup", 1,flavours,toppings));
            regularQueue.Enqueue(order1);

            order1.AddIceCream(new Cup("Cup", 1, flavours, toppings));
            order1.ModifyIceCream(1);
            Console.WriteLine(order1.IceCreamList[1]); */

            void DisplayMenu() //Displays the menu every iteration
            {
                Console.Write("" +
                    "------------- MENU --------------\r\n" +
                    "[1] List All Customers\r\n" +
                    "[2] List all current orders (the working one)\r\n " +
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
                using (StreamReader cReader = new StreamReader("customers.csv"))
                {
                    string[] headers = cReader.ReadLine().Split(",");
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write($"{headers[i],-12}");
                    }
                    Console.WriteLine();

                    string? line;
                    while ((line = cReader.ReadLine()) != null)
                    {
                        string[] cInfo = line.Split(",");
                        //Customer customer = new Customer(cInfo[0], Convert.ToInt32(cInfo[1]), Convert.ToDateTime(cInfo[2]));
                        //customerList.Add(customer);
                        //Console.WriteLine($"{cInfo[0],-12} {cInfo[1],-12} {cInfo[2],-12} {cInfo[3],-20} {cInfo[4],-20} {cInfo[5],-12}");
                    }
                    cReader.Close();
                }

                foreach (Customer c in customerList)
                {
                    Console.WriteLine(c);
                }
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

                /*while (true)
                {
                    try
                    {
                        Console.Write("Enter name: ");
                        string? newName = Console.ReadLine();

                        if (newName.Any(char.IsNumber) || newName.Any(char.IsSymbol))
                        {
                            Console.WriteLine("Invalid name. Please try again. ");
                            continue;
                        }
                            Console.Write("Enter ID: ");
                            int newID = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Date of Birth (DD/MM/YYYY): ");
                            string? date = Console.ReadLine();

                            //Regex dateFormat = new Regex("(?<date>[^@]+)/(?<month>[^@]+)/?<year>[^@]+");
                            Regex dateFormat = new Regex(@"^(\d{1})/(\d{1})/(\d{1})$");
                            Match dateMatch = dateFormat.Match(date);

                            DateTime newDOB = DateTime.Today;
                            if (dateMatch.Success)
                            {
                                newDOB = Convert.ToDateTime(date);
                            }
                            else
                            {
                                Console.WriteLine("Date of Birth was not in correct format DD/MM/YYYY. Please try again. ");
                                continue;
                            }

                            //can only be implemented after Customer class is fully complete
                            Customer newCustomer = new Customer(newName, newID, newDOB);
                            PointCard newCard = new PointCard();
                            newCustomer.Rewards = newCard;
                            using (StreamWriter cWriter = new StreamWriter("customers.csv"))
                            {
                                cWriter.WriteLine(newName, newID, newDOB, "Ordinary", 0, 0);
                                cWriter.Close();
                            }

                            break;
                        }

                    catch (FormatException f)
                    {
                        Console.WriteLine($"{f.Message}");
                    }
                }*/
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
        