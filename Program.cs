using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using statmathPostgreSample.Database;
using System;
using System.Linq;

namespace statmathPostgreSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            Model model = new Model();
            var blogs = model.blog.ToList();

            foreach (var item in blogs)
            {
                Console.WriteLine(item.description);
            }

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
