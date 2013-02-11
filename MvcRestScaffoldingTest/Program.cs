using System;
using System.Reflection;

namespace MvcRestScaffolding
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            RunTests();
            Console.ReadKey();
        }

        static void RunTests()
        {
            string[] my_args = { Assembly.GetExecutingAssembly().Location, "/include=Single", "/nologo" };

            int returnCode = NUnit.ConsoleRunner.Runner.Main(my_args);

            if (returnCode != 0)
                Console.Beep();
        }
    }
}
