using System;
using System.Collections.Generic;

namespace Unification
{
    public static class Parser
    {
        static readonly Stack<string> funcStack = new Stack<string>();
        static readonly Stack<List<IExpression>> paramStack = new Stack<List<IExpression>>();

        //static VariableRegistry registry = new VariableRegistry();

        static void EvalToken(ref Lexer l, out IExpression expr, ref VariableRegistry registry)
        {
            switch (l.Token)
            {
                case Token.QuotedString:
                case Token.Float:
                case Token.Int:
                case Token.False:
                case Token.True:
                    if (funcStack.Count == 0)
                    {
                        expr = new ValueExpression(l.Value);
                        return;
                    }
                    paramStack.Peek().Add(new ValueExpression(l.Value));
                    break;
                case Token.Variable:
                    if (funcStack.Count == 0)
                    {
                        expr = new VariableExpression(l.Value, ref registry);
                        return;
                    }
                    paramStack.Peek().Add(new VariableExpression(l.Value, ref registry));
                    break;
                case Token.Function:
                    funcStack.Push(l.Value);
                    break;
                case Token.LeftBrace:
                    paramStack.Push(new List<IExpression>());
                    break;
                case Token.RightBrace:
                    if(funcStack.Count == 1)
                    {
                        var p =  paramStack.Pop().ToArray();
                        expr = new FunctionExpression(funcStack.Pop(), p);
                        return;
                    }
                    var param =  paramStack.Pop().ToArray();
                    paramStack.Peek().Add(new FunctionExpression(funcStack.Pop(),param));
                    
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

        public static IExpression Eval(ref Lexer l, ref VariableRegistry registry)
        {
            if (!l.Next()) return null;
            IExpression expr;
            do
            {
                EvalToken(ref l, out expr, ref registry);

            } while (expr == null && l.Next());
            return expr;
        }
        
        public static IExpression Unify(string input1, string input2, ref VariableRegistry registry, params Token[] tokens)
        {
            var l1 = new Lexer(input1, tokens);
            var l2 = new Lexer(input2, tokens);

            return Unify(Eval(ref l1, ref registry), Eval(ref l2, ref registry), ref registry);
        }

        public static IExpression Unify(IExpression ex1, IExpression ex2, ref VariableRegistry registry)
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
                    registry[(VariableExpression)ex1] = ex2;
                    return ex2;
                }
                registry[(VariableExpression)ex1] = ex2;
                return ex2;
            }
            if (ex1.IsFunction)
            {
                if (ex2.IsVariable)
                {
                    registry[(VariableExpression)ex2] = ex1;
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
                        exp[i] = Unify(exp1.Expressions[i], exp2.Expressions[i], ref registry);
                    }
                    return new FunctionExpression(exp1.Value, exp);
                }
                throw new NotUnifiableException(ex1.Value, ex2.Value, "no variable preseent");
            }
            if (ex2.IsVariable)
            {
                registry[(VariableExpression)ex2] = ex1;
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