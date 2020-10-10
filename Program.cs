using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using statmathPostgreSample.Database;
using System;
using System.Linq;

namespace statmathPostgreSample
{
    class Program
    {
        private static bool InDocker 
        { 
            get 
            {
                if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("InDocker")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } 
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("--------------Start--------------------");
                Console.WriteLine(InDocker.ToString());
                
                Model model = new Model();
                var blogs = model.blog.ToList();

                foreach (var item in blogs)
                {
                    Console.WriteLine(item.description);
                }

                Console.WriteLine("End");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("-------------------Exception-----------------------");
                Console.WriteLine(e.ToString());

            }
        }
    }
}
