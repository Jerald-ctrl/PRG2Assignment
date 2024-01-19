//==========================================================
// Student Number : S10259305
// Student Name : Keagan Alexander Sng Yu
// Partner Name : Jerald Tee Li Yi
//==========================================================

namespace S10259842_PRG2Assignment
{
    class Program
    {



        static void Main(string[] args)
        {
            void DisplayCustomers()
            {
                using (StreamReader cReader = new StreamReader("customers.csv"))
                {
                    string[] headers = cReader.ReadLine().Split(",");
                    Console.WriteLine($"{headers[0],-12} {headers[1],-12} {headers[2],-12} {headers[3],-20} {headers[4],-20} {headers[5],-12}");

                    string? line;
                    while ((line = cReader.ReadLine()) != null)
                    {
                        string[] cInfo = line.Split(",");
                        Console.WriteLine($"{cInfo[0],-12} {cInfo[1],-12} {cInfo[2],-12} {cInfo[3],-20} {cInfo[4],-20} {cInfo[5],-12}");
                    }
                }
            }


            void DisplayMenu() //Displays the menu every iteration
            {
                Console.Write("" +
                    "------------- MENU --------------\r\n" +
                    "[1] List All Customers\r\n" +
                    "[2] List all current orders\r\n" +
                    "[3] Register a new customer\r\n" +
                    "[4] Create a customer’s order\r\n" +
                    "[5] Display order details of a customer\r\n" +
                    "[6] Modify order details\r\n" +
                    "[0] Exit\r\n" +
                    "---------------------------------\r\n" +
                    "Enter your option: ");
            }

            void ListAllCustomers()
            {

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

            void ListAllCurrentOrders(Queue<Order> regularQueue, Queue<Order> goldenQueue)
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
                    foreach (Order order in goldenQueue)
                    {
                        DisplayOrder(order);
                    }
                }
                

            }

            void RegisterCustomer()
            {

            }

            void CreateCustomerOrder()
            {

            }

            void DisplayOrderDetails()
            {

            }

            void ModifyOrderDetails()
            {

            }

            //setup code
            Queue<Order> regularQueue = new Queue<Order>();
            Queue<Order> goldenQueue = new Queue<Order>();


            // Testing
            string choice = "";
            DisplayCustomers(); 


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
        