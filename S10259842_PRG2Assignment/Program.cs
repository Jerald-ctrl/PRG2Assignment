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

            void ListAllCurrentOrders()
            {

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




            // Testing
            string choice = "";
            while (true)
            {
                DisplayCustomers();
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
        