using System;

namespace Unification
{
    class Program
    {
        static void Main(string[] args)
        {
            //var input1 = "f(X,f(3,2))";
            //var input2 = "f(g(Y,5),Y)";

            if (args.Length != 2)
            {
                Console.WriteLine("usage:   unification expr1 expr2\r\n" +
                                  "example: unification f(X,f(3,2)) f(g(Y,5),Y)\r\n" +
                                  "value:   \"quoted string\", 3.24E2(float), 42(int), true/false(bool)\r\n" +
                                  "vars:    EXAMPLE, ANOTHER, X, Y(All Uppercase)\r\n" +
                                  "funcs:   afunction(anther), f(\"value\", 123), g(3.141) (All lowercase, parameters contained within braces, separated by commas)");
                Environment.ExitCode = -1;
                return;
            }
            
            var registry = new VariableRegistry();
            var expr = Parser.Unify(args[0], args[1], ref registry);

            Console.WriteLine(expr.ToString());
            Environment.ExitCode = 0;
        }
    }
}
