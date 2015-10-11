using System;
using System.Collections.Generic;

namespace Unification
{
    public class VariableRegistry
    {
        //singleton
        private static VariableRegistry me;
        public static VariableRegistry Instance => me ?? (me = new VariableRegistry());
        private VariableRegistry()
        {
            me = this;
        }

        readonly Dictionary<string,IExpression> registry = new Dictionary<string, IExpression>(); 

        /// <summary>
        /// Indexer handling reading adn writing to the registry
        /// </summary>
        /// <param name="key">target variable</param>
        /// <returns>variable value</returns>
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

        /// <summary>
        /// Returns true if key has a value
        /// </summary>
        /// <param name="key">target key</param>
        /// <returns>key has value</returns>
        public bool Contains(string key) => registry.ContainsKey(key);
    }
}