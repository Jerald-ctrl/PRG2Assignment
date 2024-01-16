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
                    string[] headers = Console.ReadLine().Split(",");

                    string? line;
                    while ((line = cReader.ReadLine()) != null)
                    {
                        string[] cInfo = line.Split(",");

                    }
                }
            }
        }
    }
}