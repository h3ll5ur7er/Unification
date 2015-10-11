using System;
using System.Collections.Generic;

namespace Unification
{
    public class Evaluator
    {
        Stack<string> funcStack = new Stack<string>();
        Stack<List<IExpression>> paramStack = new Stack<List<IExpression>>();

        VariableRegistry registry = new VariableRegistry();
        public IExpression Eval(ref Lexer l)
        {
            if (l.Next())
            {
                IExpression expr = null;
                do
                {
                    EvalToken(ref l, out expr);

                } while (expr == null && l.Next());
                return expr;
            }
            return null;
        }

        private void EvalToken(ref Lexer l, out IExpression expr)
        {
            switch (l.Token)
            {
                case Token.QUOTED_STRING:
                case Token.FLOAT:
                case Token.INT:
                case Token.FALSE:
                case Token.TRUE:
                    if (funcStack.Count == 0)
                    {
                        expr = new ValueExpression(l.Value);
                        return;
                    }
                    paramStack.Peek().Add(new ValueExpression(l.Value));
                    break;
                case Token.VARIABLE:
                    if (funcStack.Count == 0)
                    {
                        expr = new VariableExpression(l.Value, ref registry);
                        return;
                    }
                    paramStack.Peek().Add(new VariableExpression(l.Value, ref registry));
                    break;
                case Token.FUNCTION:
                    funcStack.Push(l.Value);
                    break;
                case Token.LEFT:
                    paramStack.Push(new List<IExpression>());
                    break;
                case Token.RIGHT:
                    if(funcStack.Count == 1)
                    {
                        var p =  paramStack.Pop().ToArray();
                        expr = new FunctionExpression(funcStack.Pop(), p);
                        return;
                    }
                    var param =  paramStack.Pop().ToArray();
                    paramStack.Peek().Add(new FunctionExpression(funcStack.Pop(),param));
                    
                    break;
                case Token.COMMA:
                case Token.DOT:
                case Token.SPACE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            expr = null;
        }


        public IExpression Unify(string input1, string input2, params Token[] tokens)
        {
            var l1 = new Lexer(input1, tokens);
            var l2 = new Lexer(input2, tokens);

            var e1 = Eval(ref l1);
            var e2 = Eval(ref l2);

            return Unify(e1, e2);
        }

        public IExpression Unify(IExpression ex1, IExpression ex2)
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
                    registry[ex1.Value] = ex2;
                    return ex2;
                }
                registry[ex1.Value] = ex2;
                return ex2;
            }
            if (ex1.IsFunction)
            {
                if (ex2.IsVariable)
                {
                    registry[ex2.Value] = ex1;
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
                registry[ex2.Value] = ex1;
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

        public class VariableRegistry
        {
            private VariableRegistry me;
            public VariableRegistry()
            {
                me = this;
            }
            readonly Dictionary<string,IExpression> registry = new Dictionary<string, IExpression>(); 
            public IExpression this[string key]
            {
                get { return registry.ContainsKey(key) ? registry[key] : new VariableExpression(key, ref me); }
                set
                {
                    if (registry.ContainsKey(key))
                    {
                        if(registry[key].Value==value.Value)
                            return;
                        throw new Exception("registry values do not match " + value + " + " +registry[key]);
                    }
                    else
                        registry.Add(key, value);
                }
            }

            public bool Contains(string key)
            {
                return registry.ContainsKey(key);
            }
        }

        private class VariableRegistryImpl : VariableRegistry
        {
        }
    }
}