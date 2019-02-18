using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Scorecard.Model
{
    public enum FieldType
    {
        YesNo = 0,
        Selection = 1,
        Numeric = 2,
        Text = 3,
    }

    public abstract class Field
    {
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FieldType Type => Enum.Parse<FieldType>(GetType().Name);
        public string Prompt { get; set; }
        public object Value { get; set; }

        public double Weight { get; set; } = 1;
        public abstract double WeightedValue { get; }

        [JsonIgnore]
        public Type ValueType => Value.GetType();

        public override string ToString()
        {
            return $"[{Type}] {Prompt}: {Value} (Weight: {Weight}; Weighted Value: {WeightedValue})";
        }
    }

    public class YesNo : Field
    {
        public new bool Value { get; set; }
        public override double WeightedValue => Convert.ToInt32(Value) * Weight;
    }

    public class Numeric : Field
    {
        public new double Value { get; set; }
        public override double WeightedValue => Value * Weight;
    }

    public class Text : Field
    {
        public new string Value { get; set; }
        public override double WeightedValue => 1;
    }

    public class Selection : Field
    {
        public new KeyValuePair<int, string> Value { get; set; }
        public override double WeightedValue => Value.Key * Weight;
        public SelectionList SelectionList { get; set; }
    }

    public class SelectionList : IDictionary<int, string>
    {
        public string this[int key] { get => ((IDictionary<int, string>)Selections)[key]; set => ((IDictionary<int, string>)Selections)[key] = value; }

        public Guid Id { get; set; }
        public Dictionary<int, string> Selections { get; set; } = new Dictionary<int, string>();

        public ICollection<int> Keys => ((IDictionary<int, string>)Selections).Keys;

        public ICollection<string> Values => ((IDictionary<int, string>)Selections).Values;

        public int Count => ((IDictionary<int, string>)Selections).Count;

        public bool IsReadOnly => ((IDictionary<int, string>)Selections).IsReadOnly;

        public void Add(int key, string value)
        {
            ((IDictionary<int, string>)Selections).Add(key, value);
        }

        public void Add(KeyValuePair<int, string> item)
        {
            ((IDictionary<int, string>)Selections).Add(item);
        }

        public void Clear()
        {
            ((IDictionary<int, string>)Selections).Clear();
        }

        public bool Contains(KeyValuePair<int, string> item)
        {
            return ((IDictionary<int, string>)Selections).Contains(item);
        }

        public bool ContainsKey(int key)
        {
            return ((IDictionary<int, string>)Selections).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<int, string>[] array, int arrayIndex)
        {
            ((IDictionary<int, string>)Selections).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            return ((IDictionary<int, string>)Selections).GetEnumerator();
        }

        public bool Remove(int key)
        {
            return ((IDictionary<int, string>)Selections).Remove(key);
        }

        public bool Remove(KeyValuePair<int, string> item)
        {
            return ((IDictionary<int, string>)Selections).Remove(item);
        }

        public bool TryGetValue(int key, out string value)
        {
            return ((IDictionary<int, string>)Selections).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<int, string>)Selections).GetEnumerator();
        }
    }
}
