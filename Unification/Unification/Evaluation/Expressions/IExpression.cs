namespace Unification
{
    public interface IExpression
    {
        bool IsVariable { get; }
        bool IsFunction { get; }
        string Value { get; }
    }
}