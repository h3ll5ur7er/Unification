using System;
using System.Collections.Generic;

namespace Unification
{
    public static class Parser
    {
        static readonly Stack<string> FuncStack = new Stack<string>();
        static readonly Stack<List<IExpression>> ParamStack = new Stack<List<IExpression>>();
        
        /// <summary>
        /// Unify given expressions using tokens
        /// </summary>
        /// <param name="input1">first expression</param>
        /// <param name="input2">second expression</param>
        /// <param name="tokens">array of tokens to match</param>
        /// <returns>most general unifier</returns>
        public static IExpression Unify(string input1, string input2, params Token[] tokens)
        {
            var l1 = new Lexer(input1, tokens);
            var l2 = new Lexer(input2, tokens);

            return Unify(Eval(ref l1), Eval(ref l2));
        }
        /// <summary>
        /// Consumes Tokens from Lexer l until a expression is paresed completely
        /// </summary>
        /// <param name="l">Lexical analyzer if the expression to parse</param>
        /// <returns>parsed expression</returns>
        public static IExpression Eval(ref Lexer l)
        {
            if (!l.Next()) return null;
            IExpression expr;
            do
            {
                EvalToken(ref l, out expr);

            } while (expr == null && l.Next());
            return expr;
        }
        /// <summary>
        /// Consume next token
        /// </summary>
        /// <param name="l">the lexer</param>
        /// <param name="expr">output expression</param>
        static void EvalToken(ref Lexer l, out IExpression expr)
        {
            switch (l.Token)
            {
                case Token.QuotedString:
                case Token.Float:
                case Token.Int:
                case Token.False:
                case Token.True:
                    if (FuncStack.Count == 0)
                    {
                        expr = new ValueExpression(l.Value);
                        return;
                    }
                    ParamStack.Peek().Add(new ValueExpression(l.Value));
                    break;

                case Token.Variable:
                    if (FuncStack.Count == 0)
                    {
                        expr = new VariableExpression(l.Value);
                        return;
                    }
                    ParamStack.Peek().Add(new VariableExpression(l.Value));
                    break;

                case Token.Function:
                    FuncStack.Push(l.Value);
                    break;

                case Token.LeftBrace:
                    ParamStack.Push(new List<IExpression>());
                    break;

                case Token.RightBrace:
                    if(FuncStack.Count == 1)
                    {
                        var p =  ParamStack.Pop().ToArray();
                        expr = new FunctionExpression(FuncStack.Pop(), p);
                        return;
                    }
                    var param =  ParamStack.Pop().ToArray();
                    ParamStack.Peek().Add(new FunctionExpression(FuncStack.Pop(),param));
                    break;

                case Token.Comma:
                case Token.Dot:
                case Token.Space:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            expr = null;
        }
        /// <summary>
        /// Unify the given Expressions
        /// </summary>
        /// <param name="ex1">first expression</param>
        /// <param name="ex2">second expression</param>
        /// <returns>most general unifier</returns>
        public static IExpression Unify(IExpression ex1, IExpression ex2)
        {
            if (ex1.IsVariable)
            {
                if (ex2.IsVariable)
                {
                    if (ex1.Value == ex2.Value) return ex1;

                    throw new NotUnifiableException(ex1.Value, ex2.Value, "both are variables");
                }
                if (ex2.IsFunction)
                {
                    VariableRegistry.Instance[(VariableExpression)ex1] = ex2;
                    return ex2;
                }
                VariableRegistry.Instance[(VariableExpression)ex1] = ex2;
                return ex2;
            }
            if (ex1.IsFunction)
            {
                if (ex2.IsVariable)
                {
                    VariableRegistry.Instance[(VariableExpression)ex2] = ex1;
                    return ex2;
                }
                if (ex2.IsFunction)
                {
                    if (ex1.Value != ex2.Value) throw new NotUnifiableException(ex1.Value, ex2.Value, "argument missmatch");

                    var exp1 = (FunctionExpression) ex1;
                    var exp2 = (FunctionExpression) ex2;

                    if (exp1.Expressions.Length != exp2.Expressions.Length)
                        throw new NotUnifiableException(ex1.ToString(), ex2.ToString(), "argument length missmatch");

                    var exp = new IExpression[exp1.Expressions.Length];

                    for (int i = 0; i < exp1.Expressions.Length; i++)
                    {
                        exp[i] = Unify(exp1.Expressions[i], exp2.Expressions[i]);
                    }
                    return new FunctionExpression(exp1.Value, exp);
                }
                throw new NotUnifiableException(ex1.Value, ex2.Value, "no variable preseent");
            }
            if (ex2.IsVariable)
            {
                VariableRegistry.Instance[(VariableExpression)ex2] = ex1;
                return ex1;
            }
            if (ex2.IsFunction)
            {
                throw new NotUnifiableException(ex1.Value, ex2.Value, "no variable preseent");
            }
            if (ex1.Value == ex2.Value)
                return ex1;
            throw new NotUnifiableException(ex1.Value, ex2.Value, "no variable preseent");
        }
    }
}