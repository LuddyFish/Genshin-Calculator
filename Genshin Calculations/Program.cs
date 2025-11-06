using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genshin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========     Welcome to the Genshin Calculator     ==========");

            menuPrompt:
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\t1) Check a pre-existing character stats?\n" +
                "\t2) Add/Modify a new character stats?\n" +
                "\t3) Calculate a team rotation?\n" +
                "\t4) Quit\n");
            Console.Write("Your response: ");
            string? read = Console.ReadLine();
            if (string.IsNullOrEmpty(read))
                goto menuPrompt;

            bool running = true;
            while (running)
            {
                if (int.TryParse(read[0].ToString(), out int result))
                {
                    switch (result)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    };
                }
                else
                {
                    Console.WriteLine("\nInvalid Input. Please enter a number between 1 to 4.\n");
                    goto menuPrompt;
                }
            }

            return;
        }
    }
}
