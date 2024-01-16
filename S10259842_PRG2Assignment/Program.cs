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

                    string ? line;
                    while ((line = cReader.ReadLine()) != null)
                    {
                        string[] cInfo = line.Split(",");
                        Console.WriteLine($"{cInfo[0], -12} {cInfo[1],-12} {cInfo[2],-12} {cInfo[3],-20} {cInfo[4],-20} {cInfo[5],-12}");
                    }
                }
            }

            DisplayCustomers();
        }
    }
}