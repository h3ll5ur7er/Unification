namespace Unification
{
    public class ValueExpression : IExpression
    {
        public bool IsVariable { get { return false; } }
        public bool IsFunction { get { return false; } }
        public ValueExpression(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public override string ToString()
        {
            return Value;
        }
    }
}