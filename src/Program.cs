using Newtonsoft.Json;
using Scorecard.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Scorecard
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new SelectionList()
            {
                { 1, "Red" },
                { 2, "Blue" },
                { 3, "Green" },
            };

            var cat = new Category()
            {
                Name = "General",
                Description = "General stuff",
                Weight = 1,
                ScoreExpression = "(A + B) * D / C",
                Fields = new ArrayList()
                {
                    new YesNo()
                    {
                        Name = "A",
                        Prompt = "Yes/no!",
                        Value = true,
                    },
                    new Numeric()
                    {
                        Name = "B",
                        Prompt = "Enter a number",
                        Value = 42,
                        Weight = 0.5,
                    },
                    new Selection()
                    {
                        Name = "C",
                        Prompt = "Select from a list",
                        SelectionList = options,
                        Value = new KeyValuePair<int, string>(2, "Blue"),
                        Weight = 2,
                    },
                    new Text()
                    {
                        Name = "D",
                        Prompt = "Enter some text!",
                        Value = "Hello, World",
                    },
                }
            };

            Console.WriteLine(cat);

            foreach (var field in cat.Fields)
            {
                Console.WriteLine($"  {field}");
            }

            Console.WriteLine($"Formula: {cat.ScoreExpression}");

            Console.WriteLine("\n" + JsonConvert.SerializeObject(cat));

            Console.WriteLine($"\n\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
