using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_3963_9714
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome3963();
            welcome9714();
            Console.ReadKey();
        }
        static partial void welcome9714();
        private static void welcome3963()
        {
            string name;
            Console.WriteLine("Enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console aplication", name);
        }
    }
}

