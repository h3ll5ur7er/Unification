namespace Unification
{
    /// <summary>
    /// Expression supertype
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// IsVariable will try to substitute using VariableRegistry
        /// </summary>
        bool IsVariable { get; }
        /// <summary>
        /// IsFunction will capture parameters in braces following the value
        /// </summary>
        bool IsFunction { get; }
        /// <summary>
        /// Value contains the name of functions and variables and the value of valuetypes
        /// </summary>
        string Value { get; }
    }
}