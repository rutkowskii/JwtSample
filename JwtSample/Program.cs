using System;
using System.Security.Claims;
using System.Security.Principal;
using Nancy.Extensions;
using Nancy.Hosting.Self;

namespace Repetitions
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:19123")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:19123");

                new DatabaseConnectionInitializer().Start();
                Console.ReadLine();
            }
        }
    }
}
