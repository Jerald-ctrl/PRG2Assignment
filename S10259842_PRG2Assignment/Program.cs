namespace S10259842_PRG2Assignment
{
    class Program
    {

        static void DisplayMenu() //Displays the menu every iteration
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

        static void ListAllCustomers()
        {

        }

        static void ListAllCurrentOrders()
        {

        }

        static void RegisterCustomer()
        {

        }

        static void CreateCustomerOrder()
        {

        }

        static void DisplayOrderDetails()
        {

        }

        static void ModifyOrderDetails()
        {

        }


        static void Main(string[] args)
        {
            Console.WriteLine("I have edited this!");
            // Testing
            string choice = "";
            while (true)
            {
                DisplayMenu();
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    ListAllCustomers();
                }
                if (choice == "2")
                {
                    ListAllCurrentOrders();
                }
                if (choice == "3")
                {
                    RegisterCustomer();
                }
                if (choice == "4")
                {
                    CreateCustomerOrder();
                }
                if (choice == "5")
                {
                    DisplayOrderDetails();
                }
                if (choice == "6")
                {
                    ModifyOrderDetails();
                }
                if (choice == "0")
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