using System;
using System.Collections.Generic;

namespace Unification
{
    public class VariableRegistry
    {
        private static VariableRegistry me;
        public static VariableRegistry Instance => me ?? (me = new VariableRegistry());

        private VariableRegistry()
        {
            me = this;
        }

        readonly Dictionary<string,IExpression> registry = new Dictionary<string, IExpression>(); 

        public IExpression this[VariableExpression key]
        {
            get { return registry.ContainsKey(key.Value) ? registry[key.Value] : key; }
            set
            {
                if (registry.ContainsKey(key.Value))
                {
                    if(registry[key.Value].Value==value.Value)
                        return;
                    throw new Exception("registry values missmatch " + value + " + " +registry[key.Value]);
                }
                registry.Add(key.Value, value);
            }
        }

        public bool Contains(string key) => registry.ContainsKey(key);
    }
}