using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_3963_9714
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize bus lines and stops here
            Console.WriteLine("Please choose one of the following options:");
            Console.WriteLine("1 to add");
            Console.WriteLine("2 to erase");
            Console.WriteLine("3 to search");
            Console.WriteLine("4 to print");
            Console.WriteLine("5 to exit");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 5)
            {
                switch (choice)
                {
                    case 1://add
                        Console.WriteLine("Please enter 1 to add a new bus line and 2 to add a new stop to an existing line");
                        int add = int.Parse(Console.ReadLine());
                        if (add==1)
                            //add new bus line
                            if(add==2)
                                //add stop to an existing bus line
                                if(add!=1&&add!=2)
                                    Console.WriteLine("Error! Cannot complete the action");
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Please choose another option");
            }
            Console.WriteLine("Thank you for using our program! We hope to see you again soon.");
        }
    }
}
