using System.Reflection;
using TextConfig;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var c = Configuration.Load();
        }
    }
}