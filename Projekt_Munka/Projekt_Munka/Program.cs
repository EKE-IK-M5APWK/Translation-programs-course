using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Munka
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string p = projectDirectory + @"orders.txt";
            Console.WriteLine(p);
            Fordito f = new Fordito(p);
            f.Inditas();
            Console.ReadLine();
        }
    }
}
