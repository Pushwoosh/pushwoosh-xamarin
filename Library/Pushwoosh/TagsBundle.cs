using System;
using System.Collections;
using System.Collections.Generic;

namespace Pushwoosh
{
    public class TagsBundle
    {
        public class IncrementalInteger {
            public int Value { get; set; }
        }
        Dictionary<string, object> innerData = new Dictionary<string, object>();

        public Dictionary<string, object> ToDictionary { get { return new Dictionary<string, object>(innerData); } }

        public void PutInteger(string key, int value)
        {
            innerData[key] = value;
        }
        public void PutString(string key, string value)
        {
            innerData[key] = value;
        }
        public void PutIncrementInteger(string key, int value)
        {
            innerData[key] = new IncrementalInteger() { Value = value };
        }
        public void PutList(string key, IList<string> value)
        {
            innerData[key] = value;
        }
        public void PutAll(Dictionary<string, object> values)
        {
            foreach (var val in values) 
            {
                if (val.Value is int? ||
                    val.Value is string ||
                    val.Value is IncrementalInteger ||
                    val.Value is IList<string>)
                    innerData[val.Key] = val.Value;
            }
        }
		public int GetInt(string key)
        {
            return (innerData[key] as Nullable<int>).Value;
        }
		public string GetString(string key)
		{
            return innerData[key] as string;
		}
        public IList<string> GetList(string key)
        {
            return innerData[key] as IList<string>;
        }
    }
}
