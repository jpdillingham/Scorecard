using System;
using System.Collections;
using System.Collections.Generic;

namespace Scorecard.Model
{
    public class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public string ScoreExpression { get; set; }
        public double Score => GetScore();
        public double WeightedScore => Score * Weight;
        public List<Field> Fields { get; set; }

        public override string ToString()
        {
            return $"Category: {Name}: {Score} (Weight: {Weight}; Weighted Value: {WeightedScore})";
        }

        private double GetScore()
        {
            var expression = string.Copy(ScoreExpression);

            // todo: validate the formula
            foreach (var field in Fields)
            {
                if (field is Selection s)
                {
                    Console.WriteLine($"{field.Name}: " + s.SelectionList.Id);
                }

                expression = expression.Replace(field.Name, field.WeightedValue.ToString());
            }

            return expression.Evaluate(); // extension method
        }
    }
}
