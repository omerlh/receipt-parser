using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace ReceiptParser
{
    public class Product
    {
        public string Name
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public string Price
        {
            get;
            set;
        }
    }
    
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Receipt file name is missing");
                return 1;
            }
            var lines = File.ReadAllLines(args[0]);
            var regex = new Regex(@"([א-ת].*)");

            var products = lines.Select(line =>
            {
                var parts = line.Split(" ");

                if (parts.Length < 4 || parts[0].Length == 0 || parts[0].Any(c => !Char.IsDigit(c)))
                {
                    return null;
                }

                var id = parts[0];

                var name = string.Join(" ", parts.Skip(1).TakeWhile(s => !s.Any(char.IsDigit)));

                var price = parts.Last();

                return new Product
                {
                    Name = name,
                    Id = id,
                    Price = price
                };

            }).Where(p => p != null).ToList();

            foreach(var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }

            return 0;
        }
    }
}
